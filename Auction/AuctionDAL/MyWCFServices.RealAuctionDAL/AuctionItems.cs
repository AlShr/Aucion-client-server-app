//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWCFServices.RealAuctionDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuctionItems
    {
        public AuctionItems()
        {
            this.Bids = new HashSet<Bids>();
            this.Deals = new HashSet<Deals>();
        }
    
        public int AuctionItemId { get; set; }
        public string Discription { get; set; }
        public int StartPrice { get; set; }
        public System.DateTime StartDate { get; set; }
        public byte[] ImageAuctionItem { get; set; }
        public int UserItemId { get; set; }
        public int CategoryId { get; set; }
        public int SpecialFeatureId { get; set; }
        public string Info { get; set; }
        public Nullable<int> BuyNowPrice { get; set; }
        public Nullable<int> SaleFlag { get; set; }
    
        public virtual Categores Categores { get; set; }
        public virtual SpecialFeaturesEntryForms SpecialFeaturesEntryForms { get; set; }
        public virtual UserItems UserItems { get; set; }
        public virtual ICollection<Bids> Bids { get; set; }
        public virtual ICollection<Deals> Deals { get; set; }
    }
}
