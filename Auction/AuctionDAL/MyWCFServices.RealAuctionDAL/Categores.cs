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
    
    public partial class Categores
    {
        public Categores()
        {
            this.AuctionItems = new HashSet<AuctionItems>();
        }
    
        public int CategoryId { get; set; }
        public string Category { get; set; }
    
        public virtual ICollection<AuctionItems> AuctionItems { get; set; }
    }
}