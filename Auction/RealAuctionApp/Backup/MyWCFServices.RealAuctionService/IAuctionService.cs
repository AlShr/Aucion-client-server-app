using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MyWCFServices.RealAuctionService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IClientCallback), SessionMode = SessionMode.Required)]
    public interface IAuctionService
    {
        [OperationContract]
        UserItem GetUserItem(string name, string pass);
        [OperationContract]
        List<AuctionItem> GetAuctionItem();
        [OperationContract]
        bool InsertUserItem(UserItem userItem);
        [OperationContract]
        bool InsertAuctionItem(AuctionItem auctionItem);
        [OperationContract]
        CategoryItem GetCategory(string category);
        [OperationContract]
        SpecialFeatureItem GetSpecialFeature(string feature);
        [OperationContract]
        List<PostsUItem> GetPostsUserItem();
        [OperationContract]
        List<Bid> GetBidsItem(int auctionItemID);
        [OperationContract]
        bool InsertPostUserItem(PostsUItem postsUItem);
        [OperationContract]
        bool InsertBidsItem(Bid bidItem, AuctionItem auctionItem,UserItem userItem);
        [OperationContract]
        Bid GetCurrentBid(int id);
        [OperationContract]
        bool ExecuteDealItem(DealsItem dealsItem, Bid bidItem, AuctionItem auctionItem, UserItem userItem);
        [OperationContract]
        bool UserItemJoined(AuctionItem a, UserItem u);
        [OperationContract]
        void LeavedAuctionNavigation(UserItem u);

        // TODO: Добавьте здесь операции служб
    }
    public interface IClientCallback
    {
        [OperationContract]
        void RefreshAuctionItems();
        [OperationContract]
        void RefreshBidItem();
        [OperationContract]
        void RefreshPostsUItem();
        [OperationContract]
        void RefreshBidItems();
    }

    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "MyWCFServices.RealAuctionService.ContractType".
    [DataContract]
    public class UserItem : INotifyPropertyChanged
    {
        private string name;
        private string upassword;
        private int rating;
        private byte[] userimage;
        private string email;
        [DataMember]
        public int UserItemID { set; get; }
        [DataMember]
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }
        [DataMember]
        public string Upassword
        {
            get { return this.upassword; }
            set
            {
                this.upassword = value;
                OnPropertyChanged("Upassword");
            }
        }
        [DataMember]
        public int Rating
        {
            get { return this.rating; }
            set
            {
                this.rating = value;
                OnPropertyChanged("Rating");
            }
        }
        [DataMember]
        public byte[] UserImage
        {
            get { return this.userimage; }
            set
            {
                this.userimage = value;
                OnPropertyChanged("UserImage");
            }
        }
        [DataMember]
        public string Email
        {
            get { return this.email; }
            set
            {
                this.email = value;
                OnPropertyChanged("Email");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    [DataContract]
    public class Bid : INotifyPropertyChanged
    {
        private UserItem currentUseritem;
        private int amount;
        private int auctionItemID;
        private DateTime currentDate;
        [DataMember]
        public int Amount
        {
            get { return amount; }
            set
            {
                this.amount = value;
                OnPropertyChanged("Amount");
            }
        }
        [DataMember]
        public int BidID { set; get; }
        [DataMember]
        public UserItem CurrentUseritem
        {
            get { return currentUseritem; }
            set
            {
                this.currentUseritem = value;
                OnPropertyChanged("CurrentUseritem");
            }
        }
        [DataMember]
        public int AuctionItemID
        {
            get { return auctionItemID; }
            set 
            {
                this.auctionItemID = value;
                OnPropertyChanged("AuctionItemID");
            }
        }
        [DataMember]
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                this.currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }
        //int IComparable<Bid>.CompareTo(Bid other)
        //{
        //    int amountThis = this.amount;
        //    int amountOther = other.Amount;
        //    if (amountOther > amountThis) return -1;
        //    else if (amountOther == amountThis) return 0;
        //    else return 1;
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    [DataContract]
    public class AuctionItem : INotifyPropertyChanged
    {
        private string description;
        private int startPrice;
        private int buyNowPrice;
        private DateTime startDate;
        private string category;
        private UserItem owner;
        private byte[] imageAuctionItem;
        private string specialFeatures;
        private string info;
        private int saleFlag;
        private ObservableCollection<Bid> bids;
        [DataMember]
        public int AuctionItemID { set; get; }
        [DataMember]
        public string Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                OnPropertyChanged("Description");
            }
        }
        [DataMember]
        public int StartPrice
        {
            get { return this.startPrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price must be positive");
                }
                this.startPrice = value;
                OnPropertyChanged("StartPrice");
                OnPropertyChanged("CurrentPrice");
            }
        }
        [DataMember]
        public int BuyNowPrice
        {
            get { return this.buyNowPrice; }
            set 
            {
                this.buyNowPrice = value;
                OnPropertyChanged("BuyNowPrice");
            }
        }
        [DataMember]
        public DateTime StartDate
        {
            get { return this.startDate; }
            set
            {
                this.startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        [DataMember]
        public UserItem Owner
        {
            get { return owner; }
            set
            {
                this.owner = value;
                OnPropertyChanged("Owner");
            }
        }
        [DataMember]
        public string Category
        {
            get { return this.category; }
            set
            {
                this.category = value;
                OnPropertyChanged("Category");
            }
        }
        [DataMember]
        public string SpecialFeatures
        {
            get { return this.specialFeatures; }
            set
            {
                this.specialFeatures = value;
                OnPropertyChanged("SpecialFeatures");
            }
        }
        [DataMember]
        public byte[] ImageAuctionItem
        {
            get { return this.imageAuctionItem; }
            set
            {
                this.imageAuctionItem = value;
                OnPropertyChanged("ImageAuctionItem");
            }
        }
        [DataMember]
        public string Info
        {
            get { return this.info; }
            set 
            {
                this.info = value;
                OnPropertyChanged("Info");
            }
        }
        [DataMember]
        public int SaleFlag
        {
            get { return this.saleFlag; }
            set
            {
                this.saleFlag = value;
                OnPropertyChanged("SaleFlag");
            }
        }
        [DataMember]
        public ObservableCollection<Bid> Bids
        {
            get { return new ObservableCollection<Bid>(this.bids); }
            set
            {

                this.bids = value;
                OnPropertyChanged("Bids");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
    [DataContract]
    public class CategoryItem : INotifyPropertyChanged
    {
        private string category;
        [DataMember]
        public int CategoryID { set; get; }
        [DataMember]
        public string Category
        {
            get { return this.category; }
            set
            {
                this.category = value;
                OnPropertyChanged("Category");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
    [DataContract]
    public class SpecialFeatureItem 
    {
        private string feature;
        [DataMember]
        public int SpecialFeatureID { set; get; }
        [DataMember]
        public string Feature
        {
            get { return this.feature; }
            set
            {
                this.feature = value;
                // OnPropertyChanged("Feature");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    [DataContract]
    public class PostsUItem:INotifyPropertyChanged
    {
        string post;
        DateTime postTime;
        UserItem said;
        [DataMember]
        public int PostUItemID { get; set; }
        [DataMember]
        public string Post
        {
            get{ return this.post; }
            set 
            {
                this.post = value;
                OnPropertyChanged("Post");
            }
        }
        [DataMember]
        public DateTime PostTime
        {
            get { return this.postTime; }
            set
            {
                this.postTime = value;
                OnPropertyChanged("PostTime");
            }

        }
        [DataMember]
        public UserItem Said
        {
            get { return said; }
            set
            {
                this.said = value;
                OnPropertyChanged("Said");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    [DataContract]
    public class DealsItem:INotifyPropertyChanged
    {
        Bid bidItem;
        AuctionItem auctionItem;
        DateTime currentTime;
        int userItemId;
        [DataMember]
        public int DealId { set; get; }
        [DataMember]
        public Bid BidItem
        {
            get { return this.bidItem; }
            set 
            {
                this.bidItem = value;
                OnPropertyChanged("BidItem");
            }
        }
        [DataMember]
        public AuctionItem AuctionItem
        {
            get { return this.auctionItem; }
            set 
            {
                this.auctionItem = value;
                OnPropertyChanged("AuctionItem");
            }
        }
        [DataMember]
        public DateTime CurrentTime
        {
            get { return this.currentTime; }
            set 
            {
                this.currentTime=value;
                OnPropertyChanged("CurrentTime");
            }
        }
        [DataMember]
        public int UserItemId
        {
            get { return this.userItemId; }
            set 
            {
                this.userItemId = value;
                OnPropertyChanged("UserItemId");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

