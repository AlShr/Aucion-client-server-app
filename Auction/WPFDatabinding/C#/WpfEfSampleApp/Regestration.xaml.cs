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
using System.ServiceModel;
using WpfEfSampleApp.RealAuctionServiceRef;

namespace WpfEfSampleApp
{
    /// <summary>
    /// Логика взаимодействия для Regestration.xaml
    /// </summary>
    public partial class Regestration : Page
    {
        //AuctionEntities context = null;
        NavigationService ns;
        public IAsyncResult arAdd;
        RealAuctionServiceRef.AuctionServiceClient client;
        public Regestration()
        {
            InitializeComponent();
           // client = new AuctionServiceClient();
            CallBackHandler.rpage = this;
        }
        public void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            NavigationService.LoadCompleted -= NavigationService_LoadCompleted;
        }
        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream filestream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[filestream.Length];
            filestream.Read(buffer, 0, (int)filestream.Length);
            filestream.Close();
            return buffer;
        }

        private void Click_AddUserImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] imageSource=ImageToBinary(filename);
                ((RealAuctionServiceRef.UserItem)(this.DataContext)).UserImage = imageSource;
            }

        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((RealAuctionServiceRef.UserItem)(this.DataContext)).Upassword = ((PasswordBox)sender).Password;
        }

        private void Regestration_Loaded(object sender, RoutedEventArgs e)
        {
            ns = this.NavigationService;
            ns.Navigating += new NavigatingCancelEventHandler(ns_Navigating);
            ns.Navigated += new NavigatedEventHandler(ns_Navigated);
            var parrentWindow = this.Parent as NavigationWindow;
            if (parrentWindow != null)
                parrentWindow.WindowStyle = WindowStyle.None;
            this.DataContext = new RealAuctionServiceRef.UserItem { Name = "", Upassword = "" };
        }
        void ns_Navigating(object sender, NavigatingCancelEventArgs e)
        { }
        void ns_Navigated(object sender, NavigationEventArgs e)
        {
            ns.Navigating -= ns_Navigating;
            ns.Navigated -= ns_Navigated;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            RealAuctionServiceRef.UserItem item = (RealAuctionServiceRef.UserItem)this.DataContext;
            bool flag=CallBackHandler.client.InsertUserItem(item);
            /*
            context = new AuctionEntities();      
            context.AddToUserItems(item);
            context.SaveChanges();*/
            if (flag == true)
            {
                AuctionItemView view = new AuctionItemView();
                NavigationService.LoadCompleted += view.NavigationService_LoadCompleted;
                NavigationService.Navigate(view, item);
            }
            
        }

        private void LeavedAuction(object sender, RoutedEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {

                parentWindow.Close();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parentWindow = this.Parent as NavigationWindow;
            if (parentWindow != null)
            {
                parentWindow.DragMove();
            }
        }
    }

}
