using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFServices.RealAuctionEntities
{
    public class UserItemEntity
    {
        public int UserItemId { get; set; }
        public string Name { get; set; }
        public string UPassword { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
        public byte[] UserImage { get; set; }
    }
}
