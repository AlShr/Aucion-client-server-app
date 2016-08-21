﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFAuctionApp.RealAuctionServiceRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserItem", Namespace="http://schemas.datacontract.org/2004/07/MyWCFServices.RealAuctionService")]
    [System.SerializableAttribute()]
    public partial class UserItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] UserImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserItemIDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rating {
            get {
                return this.RatingField;
            }
            set {
                if ((this.RatingField.Equals(value) != true)) {
                    this.RatingField = value;
                    this.RaisePropertyChanged("Rating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Upassword {
            get {
                return this.UpasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.UpasswordField, value) != true)) {
                    this.UpasswordField = value;
                    this.RaisePropertyChanged("Upassword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] UserImage {
            get {
                return this.UserImageField;
            }
            set {
                if ((object.ReferenceEquals(this.UserImageField, value) != true)) {
                    this.UserImageField = value;
                    this.RaisePropertyChanged("UserImage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserItemID {
            get {
                return this.UserItemIDField;
            }
            set {
                if ((this.UserItemIDField.Equals(value) != true)) {
                    this.UserItemIDField = value;
                    this.RaisePropertyChanged("UserItemID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuctionItem", Namespace="http://schemas.datacontract.org/2004/07/MyWCFServices.RealAuctionService")]
    [System.SerializableAttribute()]
    public partial class AuctionItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AuctionItemIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFAuctionApp.RealAuctionServiceRef.Bid[] BidsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ImageAuctionItemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFAuctionApp.RealAuctionServiceRef.UserItem OwnerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecialFeaturesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StartPriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AuctionItemID {
            get {
                return this.AuctionItemIDField;
            }
            set {
                if ((this.AuctionItemIDField.Equals(value) != true)) {
                    this.AuctionItemIDField = value;
                    this.RaisePropertyChanged("AuctionItemID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFAuctionApp.RealAuctionServiceRef.Bid[] Bids {
            get {
                return this.BidsField;
            }
            set {
                if ((object.ReferenceEquals(this.BidsField, value) != true)) {
                    this.BidsField = value;
                    this.RaisePropertyChanged("Bids");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryField, value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] ImageAuctionItem {
            get {
                return this.ImageAuctionItemField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageAuctionItemField, value) != true)) {
                    this.ImageAuctionItemField = value;
                    this.RaisePropertyChanged("ImageAuctionItem");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFAuctionApp.RealAuctionServiceRef.UserItem Owner {
            get {
                return this.OwnerField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnerField, value) != true)) {
                    this.OwnerField = value;
                    this.RaisePropertyChanged("Owner");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialFeatures {
            get {
                return this.SpecialFeaturesField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecialFeaturesField, value) != true)) {
                    this.SpecialFeaturesField = value;
                    this.RaisePropertyChanged("SpecialFeatures");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartDate {
            get {
                return this.StartDateField;
            }
            set {
                if ((this.StartDateField.Equals(value) != true)) {
                    this.StartDateField = value;
                    this.RaisePropertyChanged("StartDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartPrice {
            get {
                return this.StartPriceField;
            }
            set {
                if ((this.StartPriceField.Equals(value) != true)) {
                    this.StartPriceField = value;
                    this.RaisePropertyChanged("StartPrice");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Bid", Namespace="http://schemas.datacontract.org/2004/07/MyWCFServices.RealAuctionService")]
    [System.SerializableAttribute()]
    public partial class Bid : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BidIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WPFAuctionApp.RealAuctionServiceRef.UserItem CurrentUseritemField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BidID {
            get {
                return this.BidIDField;
            }
            set {
                if ((this.BidIDField.Equals(value) != true)) {
                    this.BidIDField = value;
                    this.RaisePropertyChanged("BidID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WPFAuctionApp.RealAuctionServiceRef.UserItem CurrentUseritem {
            get {
                return this.CurrentUseritemField;
            }
            set {
                if ((object.ReferenceEquals(this.CurrentUseritemField, value) != true)) {
                    this.CurrentUseritemField = value;
                    this.RaisePropertyChanged("CurrentUseritem");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RealAuctionServiceRef.IAuctionService")]
    public interface IAuctionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/GetUserItem", ReplyAction="http://tempuri.org/IAuctionService/GetUserItemResponse")]
        WPFAuctionApp.RealAuctionServiceRef.UserItem GetUserItem(string name, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/GetUserItem", ReplyAction="http://tempuri.org/IAuctionService/GetUserItemResponse")]
        System.Threading.Tasks.Task<WPFAuctionApp.RealAuctionServiceRef.UserItem> GetUserItemAsync(string name, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/GetAuctionItem", ReplyAction="http://tempuri.org/IAuctionService/GetAuctionItemResponse")]
        WPFAuctionApp.RealAuctionServiceRef.AuctionItem[] GetAuctionItem();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/GetAuctionItem", ReplyAction="http://tempuri.org/IAuctionService/GetAuctionItemResponse")]
        System.Threading.Tasks.Task<WPFAuctionApp.RealAuctionServiceRef.AuctionItem[]> GetAuctionItemAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/InsertUserItem", ReplyAction="http://tempuri.org/IAuctionService/InsertUserItemResponse")]
        bool InsertUserItem(WPFAuctionApp.RealAuctionServiceRef.UserItem userItem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/InsertUserItem", ReplyAction="http://tempuri.org/IAuctionService/InsertUserItemResponse")]
        System.Threading.Tasks.Task<bool> InsertUserItemAsync(WPFAuctionApp.RealAuctionServiceRef.UserItem userItem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/InsertAuctionItem", ReplyAction="http://tempuri.org/IAuctionService/InsertAuctionItemResponse")]
        bool InsertAuctionItem(WPFAuctionApp.RealAuctionServiceRef.AuctionItem auctionItem);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuctionService/InsertAuctionItem", ReplyAction="http://tempuri.org/IAuctionService/InsertAuctionItemResponse")]
        System.Threading.Tasks.Task<bool> InsertAuctionItemAsync(WPFAuctionApp.RealAuctionServiceRef.AuctionItem auctionItem);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuctionServiceChannel : WPFAuctionApp.RealAuctionServiceRef.IAuctionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuctionServiceClient : System.ServiceModel.ClientBase<WPFAuctionApp.RealAuctionServiceRef.IAuctionService>, WPFAuctionApp.RealAuctionServiceRef.IAuctionService {
        
        public AuctionServiceClient() {
        }
        
        public AuctionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuctionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuctionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuctionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WPFAuctionApp.RealAuctionServiceRef.UserItem GetUserItem(string name, string pass) {
            return base.Channel.GetUserItem(name, pass);
        }
        
        public System.Threading.Tasks.Task<WPFAuctionApp.RealAuctionServiceRef.UserItem> GetUserItemAsync(string name, string pass) {
            return base.Channel.GetUserItemAsync(name, pass);
        }
        
        public WPFAuctionApp.RealAuctionServiceRef.AuctionItem[] GetAuctionItem() {
            return base.Channel.GetAuctionItem();
        }
        
        public System.Threading.Tasks.Task<WPFAuctionApp.RealAuctionServiceRef.AuctionItem[]> GetAuctionItemAsync() {
            return base.Channel.GetAuctionItemAsync();
        }
        
        public bool InsertUserItem(WPFAuctionApp.RealAuctionServiceRef.UserItem userItem) {
            return base.Channel.InsertUserItem(userItem);
        }
        
        public System.Threading.Tasks.Task<bool> InsertUserItemAsync(WPFAuctionApp.RealAuctionServiceRef.UserItem userItem) {
            return base.Channel.InsertUserItemAsync(userItem);
        }
        
        public bool InsertAuctionItem(WPFAuctionApp.RealAuctionServiceRef.AuctionItem auctionItem) {
            return base.Channel.InsertAuctionItem(auctionItem);
        }
        
        public System.Threading.Tasks.Task<bool> InsertAuctionItemAsync(WPFAuctionApp.RealAuctionServiceRef.AuctionItem auctionItem) {
            return base.Channel.InsertAuctionItemAsync(auctionItem);
        }
    }
}
