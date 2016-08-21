using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using WpfEfSampleApp.RealAuctionServiceRef;
using System.Threading;
using Microsoft.TeamFoundation.MVVM;
using System.ServiceModel;


namespace WpfEfSampleApp
{
    /// <summary>
    /// Логика взаимодействия для AuctionItemView.xaml
    /// </summary>
    public partial class AuctionItemView : Page
    {
        CollectionViewSource listingDataView;        
        public IAsyncResult arAdd;
        NavigationService ns;
        RealAuctionServiceRef.UserItem currentUser;
        List<RealAuctionServiceRef.AuctionItem> auctionItems;
        RealAuctionServiceRef.AuctionItem auctionItem;      
        public List<RealAuctionServiceRef.AuctionItem> AuctionItems
        {
            get { return this.auctionItems; }
            set { this.auctionItems = value; }
        }
        public AuctionItemView()
        {
            InitializeComponent();
            listingDataView = (CollectionViewSource)this.Resources["listingDataView"];
            CallBackHandler.mpage = this;
            BidItemCurrent.mpageref = this;
            arAdd = CallBackHandler.client.BeginGetAuctionItem(GetAuctionItemCallBack, CallBackHandler.client);
            
        }
        public void GetAuctionItemCallBack(IAsyncResult ar)
        {
            List<RealAuctionServiceRef.AuctionItem> auctionItems = ((AuctionServiceClient)ar.AsyncState).EndGetAuctionItem(ar).ToList();
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    listingDataView.Source = auctionItems;
                });
        }
        private void AuctionItemView_Loaded(object sender, RoutedEventArgs e)
        {
            ns = this.NavigationService;
            ns.Navigating += new NavigatingCancelEventHandler(ns_Navigating);
            ns.Navigated += new NavigatedEventHandler(ns_Navigated);
            var parrentWindow = this.Parent as NavigationWindow;
            if (parrentWindow != null)
                parrentWindow.WindowStyle = WindowStyle.None;
        }
        void ns_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            arAdd = CallBackHandler.client.BeginGetAuctionItem(GetAuctionItemCallBack, CallBackHandler.client);
        }
        void ns_Navigated(object sender, NavigationEventArgs e)
        {
            ns.Navigating -= ns_Navigating;
            ns.Navigated -= ns_Navigated;
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            currentUser = (e.ExtraData as RealAuctionServiceRef.UserItem);
            CurrentUserItem.DataContext = currentUser;
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
        }
        private void AddGrouping(object sender, RoutedEventArgs e)
        {
            PropertyGroupDescription groupDescription = new PropertyGroupDescription();
            groupDescription.PropertyName = "Category";
            listingDataView.GroupDescriptions.Add(groupDescription);
        }
        private void RemoveGrouping(object sender, RoutedEventArgs e)
        {
            listingDataView.GroupDescriptions.Clear();
        }
        private void RemoveFiltering(object sender, RoutedEventArgs e)
        {

        }
        private void AddFiltering(object sender, RoutedEventArgs e)
        {

        }
        private void AddSorting(object sender, RoutedEventArgs e)
        {
            listingDataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("Category.category", System.ComponentModel.ListSortDirection.Ascending));
            listingDataView.SortDescriptions.Add(new System.ComponentModel.SortDescription("AuctionItem.startDate", System.ComponentModel.ListSortDirection.Ascending));
        }
        private void RemoveSorting(object sender, RoutedEventArgs e)
        {
            listingDataView.SortDescriptions.Clear();
        }
        private void OpenAddProductWindow(object sender, RoutedEventArgs e)
        {
            ProductAddAuction pai = new ProductAddAuction();
            NavigationService.LoadCompleted += pai.NavigationService_LoadCompleted;
            NavigationService.Navigate(pai, currentUser);
        }
        private void currentChoiceAuctionItemClick(object sender, RoutedEventArgs e)
        {
            
        }
        private void Master_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            auctionItem = (RealAuctionServiceRef.AuctionItem)Master.Items.CurrentItem;
        }
        private void Master_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BidItemCurrent ab = new BidItemCurrent();
            NavigationService.LoadCompleted += ab.NavigationService_LoadCompleted;
            List<object> lobj = new List<object>();
            lobj.Add(currentUser);
            lobj.Add(auctionItem);

            NavigationService.Navigate(ab, lobj);
        }

        private void LeavedAuction(object sender, RoutedEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
                CallBackHandler.client.LeavedAuctionNavigation(currentUser);
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
    public class CallBackHandler : WpfEfSampleApp.RealAuctionServiceRef.IAuctionServiceCallback
    {
        static InstanceContext context = new InstanceContext(new CallBackHandler());
        public static RealAuctionServiceRef.AuctionServiceClient client = new AuctionServiceClient(context);
        public static AuctionItemView mpage = null;
        public static Regestration rpage = null;
        public static BidItemCurrent bpage = null;
        public IAsyncResult BeginRefreshAuctionItems(AsyncCallback acb, object obj)
        {
            IAsyncResult ar = client.BeginGetAuctionItem(mpage.GetAuctionItemCallBack, client);
            return ar;
        }
        public void EndRefreshAuctionItems(IAsyncResult ar)
        {

        }
        public void RefreshAuctionItems()
        {
            mpage.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
               (ThreadStart)delegate()
               {
                   mpage.arAdd = client.BeginGetAuctionItem(mpage.GetAuctionItemCallBack, client);
               });
        }
        public IAsyncResult BeginRefreshBidItem(AsyncCallback acb, object obj)
        {
            IAsyncResult ar = client.BeginGetCurrentBid(bpage.idAItem, bpage.GetCurrentBidCallBack, client);
            return ar;

        }
        public void EndRefreshBidItem(IAsyncResult ar)
        { }
        public void RefreshBidItem()
        {
            bpage.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    bpage.arAdd = client.BeginGetCurrentBid(bpage.idAItem, bpage.GetCurrentBidCallBack, client);
                    bpage.GetBidTime();
                });
        }
        public IAsyncResult BeginRefreshPostsUItem(AsyncCallback acb, object obj)
        {
            IAsyncResult ar = client.BeginGetPostsUserItem(bpage.GetPostsUItemCallBack, client);
            return ar;
        }
        public void EndRefreshPostsUItem(IAsyncResult ar)
        { }
        public void RefreshPostsUItem()
        {
            bpage.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
               (ThreadStart)delegate()
               {
                   bpage.arAdd = client.BeginGetPostsUserItem(bpage.GetPostsUItemCallBack, client);
               });
        }
        public IAsyncResult BeginRefreshBidItems(AsyncCallback acb, object obj)
        {
            IAsyncResult ar = client.BeginGetBidsItem(bpage.idAItem, bpage.GetBidsItemCallBack, client);
            return ar;
        }
        public void EndRefreshBidItems(IAsyncResult ar)
        { }
        public void RefreshBidItems()
        {
            bpage.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
              (ThreadStart)delegate()
              {
                  bpage.arAdd = client.BeginGetBidsItem(bpage.idAItem, bpage.GetBidsItemCallBack, client);
              });
        }
    }
}
