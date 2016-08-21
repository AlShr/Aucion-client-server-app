using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWCFServices.RealAuctionDAL;
using MyWCFServices.RealAuctionEntities;

namespace MyWCFServices.RealAuctionLogic
{
    public class AuctionLogic
    {
        AuctionDAL auctionDAL = new AuctionDAL();

        public UserItemEntity GetUserItem(string name, string pass)
        {
            return auctionDAL.GetUserItem(name, pass);
        }
        public PostsUItemEntity GetPostUserItem(int userId)
        {
            return auctionDAL.GetPostUserItem(userId);
        }
        public List<AuctionItemEntity> GetAuctionItem()
        {
            return auctionDAL.GetAuctionItem();
        }
        public List<PostsUItemEntity> GetPostUserItem()
        {
            return auctionDAL.GetPostsUserItem();
        }
        public List<BidEntity> GetBidsItems(int auctionItemID)
        {
            return auctionDAL.GetBidsItem(auctionItemID);
        }
        public BidEntity GetBidItem(int id)
        {
            return auctionDAL.GetBidItem(id);
        }
        public bool InsertUserItem(UserItemEntity userItemEntity)
        {
            UserItems userItem = new UserItems();
            userItem.UserItemId = (int)userItemEntity.UserItemId;
            userItem.Name = userItemEntity.Name;
            userItem.UPassword = userItemEntity.UPassword;
            userItem.Email = userItemEntity.Email;
            userItem.Rating = (int)userItemEntity.Rating;
            userItem.UserImage = userItemEntity.UserImage;
            return auctionDAL.InsertUserItem(userItem);
        }
        public bool InsertAuctionItem(AuctionItemEntity auctionItemEntity)
        {
            AuctionItems auctionItem = new AuctionItems();
            auctionItem.AuctionItemId = auctionItemEntity.AuctionItemID;
            auctionItem.Discription = auctionItemEntity.Description;
            auctionItem.StartPrice = (int)auctionItemEntity.StartPrice;
            auctionItem.StartDate = (DateTime)auctionItemEntity.StartDate;
            auctionItem.ImageAuctionItem = auctionItemEntity.ImageAuctionItem;
            string category = auctionItemEntity.Category;
            int user = auctionItemEntity.Owner.UserItemId;
            string specialfeature = auctionItemEntity.SpecialFeature;
            auctionItem.Info = auctionItemEntity.InfoEntity;
            auctionItem.BuyNowPrice = auctionItemEntity.BuyNowPrice;
            auctionItem.SaleFlag = 0;
            return auctionDAL.InsertAuctionItem(auctionItem, category, specialfeature,user);
        }
        public bool InsertPostUserItem(PostsUItemEntity postsUItemEntity)
        {
            PostsUItems postsUItem = new PostsUItems();
            postsUItem.PostUItemId = postsUItemEntity.PostUItemID;
            postsUItem.Post = postsUItemEntity.Post;
            postsUItem.PostTime = postsUItemEntity.PostTime;
            int userId = postsUItemEntity.Said.UserItemId;
            return auctionDAL.InsertPostUserItem(postsUItem, userId);
        }
        public bool InsertBidItem(BidEntity bidEntity, int keyAItem, int keyUItem)
        {
            Bids bidsItem = new Bids();
            bidsItem.Amount = bidEntity.Amount;
            bidsItem.CurrentDate = bidEntity.CurrentDate;
            return auctionDAL.InsertBidItem(bidsItem, keyAItem, keyUItem);
        }
        public bool ExecuteDealItem(DealEntity dealEntity, int keyBItem, int keyAItem, int keyUItem)
        {
            Deals dealItem = new Deals();
            dealItem.CurrentTime = dealEntity.CurrentTime;
            return auctionDAL.ExecuteDealItem(dealItem, keyBItem, keyAItem, keyUItem);
        }

    }
}
