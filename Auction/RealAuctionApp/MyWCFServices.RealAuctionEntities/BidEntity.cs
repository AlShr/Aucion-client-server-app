using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFServices.RealAuctionEntities
{
    public class BidEntity
    {
        public int BidId { set; get; }
        public UserItemEntity currentUser { set; get; }
        public int Amount { set; get; }
        public int AuctionItemID { set; get; }
        public DateTime CurrentDate { set; get; }
    }
}
