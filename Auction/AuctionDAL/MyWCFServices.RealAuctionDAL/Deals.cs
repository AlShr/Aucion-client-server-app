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
    
    public partial class Deals
    {
        public int DealId { get; set; }
        public int BidId { get; set; }
        public int AuctionItemID { get; set; }
        public Nullable<System.DateTime> CurrentTime { get; set; }
        public int UserItemId { get; set; }
    
        public virtual AuctionItems AuctionItems { get; set; }
        public virtual Bids Bids { get; set; }
        public virtual UserItems UserItems { get; set; }
    }
}
