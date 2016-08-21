using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFServices.RealAuctionEntities
{
    public class AuctionItemEntity
    {
        public int AuctionItemID { set; get; }
        public string Description { set; get; }
        public int StartPrice { set; get; }
        public int BuyNowPrice { set; get; }
        public DateTime StartDate { set; get; }
        public string Category { set; get; }
        //public  int CategoryID { set; get;}
        public UserItemEntity Owner { set; get; }
        public byte[] ImageAuctionItem { set; get; }
        public string SpecialFeature { set; get; }
        //public SpecialFeatureEntity SpecialFeatures { set; get; }
        public string InfoEntity { set; get; }
        public int SaleFlagEntity { set; get; }
        public List<BidEntity> Bids { set; get; }
    }
}
