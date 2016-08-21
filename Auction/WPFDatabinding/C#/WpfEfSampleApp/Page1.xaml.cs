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
using System.Threading;
//using System.Data.Objects;
using System.Security.Cryptography;
using WpfEfSampleApp.RealAuctionServiceRef;
namespace WpfEfSampleApp
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        NavigationService ns;
       // AuctionEntities context = null;
        UserItem item;
        static RealAuctionServiceRef.UserItem temp;
        RealAuctionServiceRef.UserItem newitem;
        IAsyncResult arAdd;
        public Page1()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
              RealAuctionServiceRef.UserItem item = (RealAuctionServiceRef.UserItem)(this.DataContext);      
              arAdd = CallBackHandler.client.BeginGetUserItem(item.Name, item.Upassword, GetUserItemCallBack, CallBackHandler.client);
            
        }
        public void Novigation(RealAuctionServiceRef.UserItem item)
        {
            if (item != null)
            {
                AuctionItemView aiv = new AuctionItemView();
                NavigationService.LoadCompleted += aiv.NavigationService_LoadCompleted;
                NavigationService.Navigate(aiv, item);
            }
            else
            {
                Regestration reg = new Regestration();
                NavigationService.LoadCompleted += reg.NavigationService_LoadCompleted;
                NavigationService.Navigate(reg, "");
            }
        }
        void GetUserItemCallBack(IAsyncResult ar)
        {
           RealAuctionServiceRef.UserItem item  = ((AuctionServiceClient)ar.AsyncState).EndGetUserItem(ar);
           this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
               (ThreadStart)delegate()
           {
               Novigation(item);
           });
           
        }
        public static string HashString(string cleartext)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(cleartext);
            return HashBytes(clearBytes);
        }
        public static string HashBytes(byte[] clearBytes)
        {
            SHA1 hasher = SHA1.Create();
            byte[] hashBytes = hasher.ComputeHash(clearBytes);
            string hash = System.Convert.ToBase64String(hashBytes);
            hasher.Clear();
            return hash;
        }
        private void OnInit(object sender, RoutedEventArgs e)
        {
            ns = this.NavigationService;
            ns.Navigating += new NavigatingCancelEventHandler(ns_Navigating);
            ns.Navigated += new NavigatedEventHandler(ns_Navigated);
            var parrentWindow = this.Parent as NavigationWindow;
            if (parrentWindow != null)
                parrentWindow.WindowStyle = WindowStyle.None;
            this.DataContext = new  RealAuctionServiceRef.UserItem { Name = "", Upassword = "" };
        }
        void ns_Navigating(object sender, NavigatingCancelEventArgs e)
        { 
        }
        void ns_Navigated(object sender, NavigationEventArgs e)
        {
            ns.Navigating -= ns_Navigating;
            ns.Navigated -= ns_Navigated;
        }
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
           ((RealAuctionServiceRef.UserItem)(this.DataContext)).Upassword = ((PasswordBox)sender).Password;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Regestration reg = new Regestration();
            NavigationService.LoadCompleted += reg.NavigationService_LoadCompleted;
            NavigationService.Navigate(reg, "");
        }

        private void LeavedAuction(object sender, RoutedEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
                
                parentWindow.Close();
            }
        }

        private void ConnectBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
                parentWindow.DragMove();
            }
        }  
    }
}
