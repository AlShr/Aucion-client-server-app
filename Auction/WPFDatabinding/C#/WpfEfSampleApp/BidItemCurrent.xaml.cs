using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfEfSampleApp.RealAuctionServiceRef;
using System.Threading;
using System.Windows.Threading;

namespace WpfEfSampleApp
{
    /// <summary>
    /// Логика взаимодействия для BidItemCurrent.xaml
    /// </summary>
    public partial class BidItemCurrent : Page
    {
        NavigationService ns;
        public IAsyncResult arAdd;
        List<RealAuctionServiceRef.PostsUItem> postsUItems;
        CollectionViewSource userPostLookup;
        CollectionViewSource userBidsLookup;
        RealAuctionServiceRef.AuctionItem auctionItem;
        RealAuctionServiceRef.UserItem currentUItem;
        RealAuctionServiceRef.Bid currentBidItem;
        RealAuctionServiceRef.DealsItem dealItem;
        public static AuctionItemView mpageref = null;
        DateTime d1, d2;
        private DispatcherTimer timer;
        private DispatcherTimer timerDeal,timerInfo;
        private string currentTime;
        public int idAItem;
        bool bay=false;
        bool baynow = false;
        static bool bider = false;
        public BidItemCurrent()
        {
            InitializeComponent();
            userPostLookup = (CollectionViewSource)this.Resources["UserPostLookup"];
            userBidsLookup = (CollectionViewSource)this.Resources["UserBidsLookup"];
            dealbt.IsEnabled = false;
            //Загрузка листа постов

            CallBackHandler.bpage = this; 
            arAdd = CallBackHandler.client.BeginGetPostsUserItem(GetPostsUItemCallBack, CallBackHandler.client);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timerDeal = new DispatcherTimer();
            timerDeal.Interval = TimeSpan.FromSeconds(30);
            timerDeal.Tick += timer_Deal;
            timerInfo = new DispatcherTimer();
            timerInfo.Interval = TimeSpan.FromSeconds(30);
            timerInfo.Tick += timerInfo_Tick;
            ListPosts.SelectedIndex = ListPosts.Items.Count - 1;

        }

        void timerInfo_Tick(object sender, EventArgs e)
        {
            TimeTablo.Content = "Продано";
            buyingbt.IsEnabled = false;
            computebt.IsEnabled = false;
            dealbt.IsEnabled = false;
        }
        public void GetPostsUItemCallBack(IAsyncResult ar)
        {
            List<RealAuctionServiceRef.PostsUItem> postsUItems = ((AuctionServiceClient)ar.AsyncState).EndGetPostsUserItem(ar).ToList();
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    userPostLookup.Source = postsUItems;
                    ListPosts.SelectedIndex = ListPosts.Items.Count - 1;
                });
        }
        void timer_Tick(object sender, EventArgs e)
        {
            d2 = DateTime.Now;
            long time = d2.Ticks - d1.Ticks;
            DateTime time2 = new DateTime(time);
            long seconds = time2.Second;
            long minits = time2.Minute;
            long hours = time2.Hour;
            currentTime=hours.ToString()+":"+minits.ToString()+":"+seconds.ToString();
            TimeTablo.Content = currentTime;            
        }
        //обработчик таймера покупки(60 сек)
        void timer_Deal(object sender, EventArgs e)
        {
            //покупка лота        
            arAdd = CallBackHandler.client.BeginExecuteDealItem(dealItem, currentBidItem, auctionItem, currentUItem, ExecuteDealItemCallBack, CallBackHandler.client);
        }
        //callback продажа
        void ExecuteDealItemCallBack(IAsyncResult ar)
        {
            bay = ((AuctionServiceClient)ar.AsyncState).EndExecuteDealItem(ar);
            if (bay == true)
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                    (ThreadStart)delegate()
                    {
                        timer.Stop();
                        TimeTablo.Content = "Продано";
                        buyingbt.IsEnabled = false;
                        computebt.IsEnabled = false;
                        dealbt.IsEnabled = false;
                    });
            }
        }
        
        public List<RealAuctionServiceRef.PostsUItem> PostsUItems
        {
            get { return this.postsUItems; }
            set { this.postsUItems = value; }
        }
        private void BidItemCurrent_Loaded(object sender, RoutedEventArgs e)
        {
            ns = this.NavigationService;
            ns.Navigating += new NavigatingCancelEventHandler(ns_Navigating);
            ns.Navigated += new NavigatedEventHandler(ns_Navigated);
        }
        void ns_Navigating(object sender,NavigatingCancelEventArgs e)
        {
            
        }
        void ns_Navigated(object sender, NavigationEventArgs e)
        {
            ns.Navigating -= ns_Navigating;
            ns.Navigated -= ns_Navigated;
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
           //TODO Recived CurrentUser CurrentActionItem
            List<object> lobj = (e.ExtraData as List<object>);
            currentUItem = (lobj[0] as RealAuctionServiceRef.UserItem);
            auctionItem = (lobj[1] as RealAuctionServiceRef.AuctionItem);
            //получение ID лота
            idAItem=auctionItem.AuctionItemID;
            //представление текущего пользователя
            ViewUserItem.DataContext = currentUItem;
            //представление текущего лота
            ViewAuctionItem.DataContext= auctionItem;

            arAdd = CallBackHandler.client.BeginGetCurrentBid(idAItem, GetCurrentBidCallBack, CallBackHandler.client);
            //TODO CurentUserPosts
            var currentPostUItem=new RealAuctionServiceRef.PostsUItem 
		    {
		        PostTime=DateTime.Now,
                Post="Написать сообщение",
                Said=currentUItem
            };
            DealData.DataContext = auctionItem;
	        PostsUWriter.DataContext=currentPostUItem;
            currentBidItem = new RealAuctionServiceRef.Bid
            {
                Amount = 0,
                CurrentDate=DateTime.Now
            };
	        BindingBid.DataContext=currentBidItem;
            BidAction.DataContext = currentBidItem;
            //Загрузка листа ставок          
          //  arAdd = client.BeginGetBidsItem(idAItem, GetBidsItemCallBack, client); 
            arAdd = CallBackHandler.client.BeginGetBidsItem(idAItem, GetBidsItemCallBack, CallBackHandler.client);
            var parrentWindow = this.Parent as NavigationWindow;
            if (parrentWindow != null)
                parrentWindow.WindowStyle = WindowStyle.None;
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
        }
        public  void GetCurrentBidCallBack(IAsyncResult ar)
        {
            currentBidItem = ((AuctionServiceClient)ar.AsyncState).EndGetCurrentBid(ar);
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    BidAction.DataContext = currentBidItem;
                    BindingBid.DataContext = currentBidItem;
                    lastBid.DataContext = currentBidItem;
                });
        }
        public void GetBidsItemCallBack(IAsyncResult ar)
        {
            List<RealAuctionServiceRef.Bid> bidsUItems = ((AuctionServiceClient)ar.AsyncState).EndGetBidsItem(ar).ToList();
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    userBidsLookup.Source = bidsUItems;
                });
        }
	    private void SendPostUItem(object sender, RoutedEventArgs e)
        {
            RealAuctionServiceRef.PostsUItem currentPost = (RealAuctionServiceRef.PostsUItem)PostsUWriter.DataContext;
         
            arAdd = CallBackHandler.client.BeginInsertPostUserItem(currentPost, InsertPostUserItemCallBack, CallBackHandler.client);

	    
        }
	    void InsertPostUserItemCallBack(IAsyncResult ar)
	    {
	         bool flag=((AuctionServiceClient)ar.AsyncState).EndInsertPostUserItem(ar);
	         if(flag==true)
	         {
		
                 arAdd = CallBackHandler.client.BeginGetPostsUserItem(GetPostsUItemCallBack, CallBackHandler.client);
	         }	
	     
	    }
/*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        void ComputeBidAuctionItem(object sender, RoutedEventArgs e)
        {
           int startPice=((RealAuctionServiceRef.AuctionItem)(ViewAuctionItem.DataContext)).StartPrice;
           int lastBid =((RealAuctionServiceRef.Bid)(BidAction.DataContext)).Amount;
           int currentBid;
           if (lastBid == 0)
           {
               currentBid = (int)(startPice * 1.05);
               currentBidData.DataContext =
                   new RealAuctionServiceRef.Bid
                   {
                       Amount = currentBid,
                       CurrentDate=DateTime.Now
                   };
           }
           else
           {              
               currentBid = (int)(lastBid * 1.05);
               currentBidData.DataContext =
                   new RealAuctionServiceRef.Bid
                   {
                       Amount = currentBid,
                       CurrentDate=DateTime.Now
                   };
           }
           dealbt.IsEnabled = true;
        }
        
        void SendBidAuctionItem(object sender,RoutedEventArgs e)
	    {          
           currentBidItem = ((RealAuctionServiceRef.Bid)(currentBidData.DataContext));
           arAdd = CallBackHandler.client.BeginInsertBidsItem(currentBidItem, auctionItem, currentUItem, SendBidAuctionItemCallBack, CallBackHandler.client);
	    }
	    void  SendBidAuctionItemCallBack(IAsyncResult ar)
	    {           
	         bool flag=((AuctionServiceClient)ar.AsyncState).EndInsertBidsItem(ar);
	         if(flag==true)
	         {
              //  RefreshBidAuctionItem();

                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                        (ThreadStart)delegate()
                        {
                            BindingBid.DataContext = currentBidItem;
                            BidAction.DataContext = currentBidItem;
                            lastBid.DataContext = currentBidItem;
                            bider = true;                        
                            if (baynow != true)
                            {         
                                timerDeal.Start();
                               
                            }
                            dealItem = new DealsItem()       
                            {     
                                CurrentTime=DateTime.Now
                            };
                        });
                if (baynow == true)
                {
                    Thread.Sleep(5000);
                    ExecDealBayNow();
                }
	         }	
	    }
        public void GetBidTime()
        {
            timer.Stop();
            timerInfo.Stop();
            d1 = DateTime.Now;
            timer.Start();
            if (bider == false)
            {
                timerDeal.Stop();
                timerInfo.Start();
            }         
            lastBid.DataContext = currentBidItem;
            bider = false;
        }
        void BuyNowBidAuctionItem(object sender, RoutedEventArgs e)
        {
            int bayNowPice = ((RealAuctionServiceRef.AuctionItem)(ViewAuctionItem.DataContext)).BuyNowPrice;
            currentBidItem = new RealAuctionServiceRef.Bid
            {
                Amount = bayNowPice,
                CurrentDate = DateTime.Now
            };
            baynow = true;
          
            arAdd = CallBackHandler.client.BeginInsertBidsItem(currentBidItem, auctionItem, currentUItem, SendBidAuctionItemCallBack, CallBackHandler.client);

        }
        void ExecDealBayNow()
        {
            //покупка лота
        
            arAdd = CallBackHandler.client.BeginExecuteDealItem(dealItem, currentBidItem, auctionItem, currentUItem, ExecuteDealItemCallBack, CallBackHandler.client);
        }
        void RefreshBidAuctionItem()
        {
      
            arAdd = CallBackHandler.client.BeginGetCurrentBid(idAItem, GetCurrentBidCallBack, CallBackHandler.client);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuctionItemView aiv = new AuctionItemView();
            NavigationService.LoadCompleted += aiv.NavigationService_LoadCompleted;
            NavigationService.Navigate(aiv, currentUItem);

        }

        private void LeavedAuction(object sender, RoutedEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
               CallBackHandler.client.LeavedAuctionNavigation(currentUItem);
               parentWindow.Close();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
                parentWindow.DragMove();
            }
        }    
    }
}
