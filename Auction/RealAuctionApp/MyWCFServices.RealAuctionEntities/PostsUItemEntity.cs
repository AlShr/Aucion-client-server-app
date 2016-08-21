using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFServices.RealAuctionEntities
{
    public class PostsUItemEntity
    {
        public int PostUItemID { get; set; }
        public string Post { set; get; }
        public DateTime PostTime { set; get; }
        public UserItemEntity Said { set; get; }            
    }
}
