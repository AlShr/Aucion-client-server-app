using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFServices.RealAuctionEntities
{
    public class DealEntity
    {
        public int DealID { set; get; }
        public BidEntity bidEntity { set; get; }
        public AuctionItemEntity auctionItemEntity { set; get; }
        public DateTime CurrentTime { set; get; }
        public int UserItemID { set; get; }
    }
}
