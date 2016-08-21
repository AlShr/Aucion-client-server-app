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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfEfSampleApp.RealAuctionServiceRef;
using System.Threading;
using System.Collections.ObjectModel;
namespace WpfEfSampleApp
{
    /// <summary>
    /// Логика взаимодействия для ProductAddAuction.xaml
    /// </summary>
    public partial class ProductAddAuction : Page
    {
        RealAuctionServiceRef.UserItem  currentUser;
        RealAuctionServiceRef.AuctionItem auctionCurrentItem;
        IAsyncResult arAdd;
        public ProductAddAuction()
        {
            InitializeComponent();
        }
        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream filestream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[filestream.Length];
            filestream.Read(buffer, 0, (int)filestream.Length);
            filestream.Close();
            return buffer;
        }
        private void Click_AddAuctionItemImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] imageSource = ImageToBinary(filename);
               ((RealAuctionServiceRef.AuctionItem)(this.DataContext)).ImageAuctionItem = imageSource;
            }
        }

        private void SubmitProduct(object sender, RoutedEventArgs e)
        {
            auctionCurrentItem = (RealAuctionServiceRef.AuctionItem)this.DataContext;
            int buyNowPrice = (int)(auctionCurrentItem.StartPrice * 1.45);
            auctionCurrentItem.BuyNowPrice = buyNowPrice;
            arAdd = CallBackHandler.client.BeginInsertAuctionItem(auctionCurrentItem, AddAuctionCallBack, CallBackHandler.client);
        
        }
        //callback добавления лота
        void AddAuctionCallBack(IAsyncResult ar)
        {           
           bool flag = ((AuctionServiceClient)ar.AsyncState).EndInsertAuctionItem(ar);
           this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate()
                {
                    Novigation(flag);
                });

        }
        public void Novigation(bool flag)
        {

                AuctionItemView aiv = new AuctionItemView();
                NavigationService.LoadCompleted += aiv.NavigationService_LoadCompleted;
                NavigationService.Navigate(aiv, currentUser);
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            currentUser = (e.ExtraData as RealAuctionServiceRef.UserItem);
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
        }
        
        private void ProductAddAuctionItem(object sender, RoutedEventArgs e)
        {
            var categorycurrent = new RealAuctionServiceRef.CategoryItem { Category = "" };
            CategoryEntyForm.DataContext=categorycurrent;
            var specialcurrentitem = new RealAuctionServiceRef.SpecialFeatureItem() { Feature = "None" };
            var currentBid = new RealAuctionServiceRef.Bid()
            {
                Amount = 0,
                CurrentUseritem=currentUser
            };
            SpecialfeaturesEntryForm.DataContext = specialcurrentitem;
            this.DataContext = new RealAuctionServiceRef.AuctionItem() 
            { 
                Description = "Type your description here",
                StartPrice = 0, 
                BuyNowPrice=0,
                StartDate = DateTime.Now,
                Category=categorycurrent.Category,
                Owner=currentUser,
                SpecialFeatures=specialcurrentitem.Feature,
                Info="",
                Bids=new ObservableCollection<Bid>()
                { new Bid(){ 
                    Amount=0,
                    CurrentUseritem=currentUser
                }                
                }.ToArray()
            };
            var parrentWindow = this.Parent as NavigationWindow;
            if (parrentWindow != null)
                parrentWindow.WindowStyle = WindowStyle.None;
        }
        
        private void CategoryEntyForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ((RealAuctionServiceRef.AuctionItem)this.DataContext).Category = ((ComboBox)sender).SelectedValue.ToString();
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
}
