using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWCFServices.RealAuctionEntities;

namespace MyWCFServices.RealAuctionDAL
{
    public class AuctionDAL
    {
        //получить пользователя
        public UserItemEntity GetUserItem(string name, string pass)
        {
            AuctionEntities AEntities = new AuctionEntities();
            UserItems useritem = (from p in AEntities.UserItems
                                  where p.Name == name && p.UPassword == pass
                                  select p).FirstOrDefault();
            UserItemEntity userItemEntity = null;
            if (useritem != null)
            {
                userItemEntity = new UserItemEntity()
                {
                    UserItemId = useritem.UserItemId,
                    Name = useritem.Name,
                    UPassword = useritem.UPassword,
                    Email = useritem.Email,
                    Rating = (int)useritem.Rating,
                    UserImage = useritem.UserImage
                };
            }
            AEntities.Dispose();
            return userItemEntity;
        }
        //получить текущую  ставку
        public BidEntity GetBidItem(int id)
        {
            AuctionEntities AEntities = new AuctionEntities();      
            DateTime curDate=DateTime.Now.Date;
            var bidsItems = AEntities.Bids
                .Where(x => x.AuctionItemID == id)
                .ToList()
                .Where(x =>
                    DateTime.Compare(x.CurrentDate.Value.Date, curDate.Date) == 0)
                    .ToList();
            Bids currentBid=null;
            BidEntity currentBidEntity = null;
            if (bidsItems.Count() != 0)
            {
                DateTime date = (DateTime)bidsItems.Max(x => x.CurrentDate);
                currentBid = bidsItems.Where(x => x.CurrentDate == date).First();
                currentBidEntity = new BidEntity()
                {
                    BidId = (int)currentBid.BidId,
                    Amount = (int)currentBid.Amount,
                    currentUser = new UserItemEntity()
                    {
                        UserItemId = currentBid.UserItems.UserItemId,
                        Email = currentBid.UserItems.Email,
                        Name = currentBid.UserItems.Name,
                        UPassword = currentBid.UserItems.UPassword,
                        Rating = (int)currentBid.UserItems.Rating,
                        UserImage = (byte[])currentBid.UserItems.UserImage
                    },
                    AuctionItemID = (int)currentBid.AuctionItemID,
                    CurrentDate = (DateTime)currentBid.CurrentDate
                };
            }
            else
            {

                currentBidEntity = new BidEntity()
                {
                    Amount = 0,
                    currentUser = new UserItemEntity()
                };
            }
            AEntities.Dispose();
            return currentBidEntity;
            
        }
        //добавление пользователя
        public bool InsertUserItem(UserItems userItem)
        {
            AuctionEntities AEntities = new AuctionEntities();
            UserItems useritemexists = (from p in AEntities.UserItems
                                        where p.Name == userItem.Name &&
                                        p.UPassword == userItem.UPassword
                                        select p).FirstOrDefault();
            if (useritemexists == null)
            {
                AEntities.UserItems.Add(userItem);
                AEntities.SaveChanges();
                return true;
            }
            else
                return false;
        }
        //добавление лота
        public bool InsertAuctionItem(AuctionItems auctionItem, string itemcategory, string itemfeature, int userkey)
        {

            AuctionEntities AEntities = new AuctionEntities();
            var owneruser = AEntities.UserItems.Find(userkey);
            var keycategory = (from c in AEntities.Categores
                               where c.Category == itemcategory
                               select c).FirstOrDefault();
            int primarykeyc = keycategory.CategoryId;
            var category = AEntities.Categores.Find(primarykeyc);
            category.AuctionItems.Add(auctionItem);

            var keyspecial = (from s in AEntities.SpecialFeaturesEntryForms
                              where s.Feature == itemfeature
                              select s).FirstOrDefault();
            int primarykeys = keyspecial.SpecialFeatureId;
            var specialfeature = AEntities.SpecialFeaturesEntryForms.Find(primarykeys);
            specialfeature.AuctionItems.Add(auctionItem);
            owneruser.AuctionItems.Add(auctionItem);
            AEntities.SaveChanges();
            return true;
        }
        //отправка поста
        public bool InsertPostUserItem(PostsUItems postsUItem,int userId)
        {
            AuctionEntities AEntites = new AuctionEntities();
            var said = AEntites.UserItems.Find(userId);
            said.PostsUItems.Add(postsUItem);
            AEntites.SaveChanges();
            return true; 
        }

        public PostsUItemEntity GetPostUserItem(int userId)
        {
           AuctionEntities AEntites=new AuctionEntities();
            var post=(from p in AEntites.PostsUItems
                        where p.UserItemId==userId
                          select p).Last();
            PostsUItems poUI = post;
            PostsUItemEntity postUIEntity = new PostsUItemEntity()
            {
                Post = poUI.Post,
                Said = new UserItemEntity() 
                {
                    Name=poUI.UserItems.Name,
                    UserImage=poUI.UserItems.UserImage
                }
            };
            return postUIEntity;
        }
        //поставить ставку
        public bool InsertBidItem(Bids bidItem, int keyAItem, int keyUItem)
        {
            AuctionEntities AEntites = new AuctionEntities();
            var auctionItem = AEntites.AuctionItems.Find(keyAItem);
            var userItem = AEntites.UserItems.Find(keyUItem);
            userItem.Bids.Add(bidItem);
            auctionItem.Bids.Add(bidItem);
            AEntites.SaveChanges();
            auctionItem = (from a in AEntites.AuctionItems
                               where a.AuctionItemId == keyAItem
                               select a).FirstOrDefault();           
            AEntites.SaveChanges();
            return true;
        }
        //покупка
        public bool ExecuteDealItem(Deals dealsItem, int  keyBItem, int keyAItem, int keyUItem)
        {
            AuctionEntities AEntites = new AuctionEntities();
            var auctionItem = AEntites.AuctionItems.Find(keyAItem);
            auctionItem.SaleFlag = 1;
            var userItem = AEntites.UserItems.Find(keyUItem);
            var bidItem = AEntites.Bids.Find(keyBItem);
            userItem.Deals.Add(dealsItem);
            auctionItem.Deals.Add(dealsItem);
            bidItem.Deals.Add(dealsItem);
            AEntites.SaveChanges();
            return true;
        }
        //список лотов
        public List<AuctionItemEntity> GetAuctionItem()
        {
            AuctionEntities AEntities = new AuctionEntities();
            IQueryable<AuctionItems> auctionItems = AEntities.AuctionItems;
            auctionItems = from a in AEntities.AuctionItems
                               .Include("UserItems")
                               .Include("Categores")
                               .Include("SpecialFeaturesEntryForms")
                               .Include("Bids")
                               .Where(x=>x.SaleFlag==0)
                           select a;

            List<AuctionItemEntity> auctionItemEntities = new List<AuctionItemEntity>();
            AuctionItemEntity auctionItemEntity = null;
            if (auctionItems != null)
            {
                foreach (AuctionItems auctionItem in auctionItems)
                {
                    if (auctionItem != null)
                    {
                        auctionItemEntity = new AuctionItemEntity()
                        {
                            AuctionItemID = auctionItem.AuctionItemId,
                            Description = auctionItem.Discription,
                            StartPrice = (int)auctionItem.StartPrice,
                            BuyNowPrice=(int)auctionItem.BuyNowPrice,
                            InfoEntity=auctionItem.Info,
                            StartDate = (DateTime)auctionItem.StartDate,
                            Category = auctionItem.Categores.Category,
                            Owner = new UserItemEntity()
                            {
                                UserItemId = auctionItem.UserItems.UserItemId,
                                Email = auctionItem.UserItems.Email,
                                Name = auctionItem.UserItems.Name,
                                UPassword = auctionItem.UserItems.UPassword,
                                Rating = (int)auctionItem.UserItems.Rating,

                                UserImage = (byte[])auctionItem.UserItems.UserImage
                            },
                            ImageAuctionItem = auctionItem.ImageAuctionItem,
                            SpecialFeature = auctionItem.SpecialFeaturesEntryForms.Feature,
                            Bids = new List<BidEntity>()
                            {

                            }
                        };
                        auctionItemEntities.Add(auctionItemEntity);
                    }
                }
            }
            AEntities.Dispose();
            return auctionItemEntities;
        }

        //список постов
        public List<PostsUItemEntity> GetPostsUserItem()
        {
            PostsUItemEntity postsUItemEntity;
            AuctionEntities AEntities = new AuctionEntities();
            DateTime curDate = DateTime.Now.Date;        
            var postsUItems = (from p in AEntities.PostsUItems
                                                  .Include("UserItems")
                               select p).ToList()
                                                .Where(x =>
                    DateTime.Compare(x.PostTime.Value.Date, curDate.Date) == 0)
                    .ToList();
            List<PostsUItemEntity> postsUItemsEntites = new List<PostsUItemEntity>();
            if (postsUItems != null)
            {
                foreach (PostsUItems postsUItem in postsUItems)
                {
                    if (postsUItem != null)
                    {
                        postsUItemEntity = new PostsUItemEntity()
                        {
                            PostUItemID = postsUItem.UserItemId,
                            Post = postsUItem.Post,
                            PostTime = (DateTime)postsUItem.PostTime,
                            Said = new UserItemEntity()
                            {
                                UserItemId = postsUItem.UserItems.UserItemId,
                                Email = postsUItem.UserItems.Email,
                                Name = postsUItem.UserItems.Name,
                                UPassword = postsUItem.UserItems.UPassword,
                                Rating = (int)postsUItem.UserItems.Rating,
                                UserImage = (byte[])postsUItem.UserItems.UserImage
                            }
                        };
                        postsUItemsEntites.Add(postsUItemEntity);
                    }
                }
            }
            AEntities.Dispose();
            return postsUItemsEntites;
        }
        //список ставок
        public List<BidEntity> GetBidsItem(int auctionItemID)
        {   
            AuctionEntities AEntities = new AuctionEntities();
            DateTime curDate = DateTime.Now.Date;
            var bidsItems = AEntities.Bids
                .Where(x => x.AuctionItemID == auctionItemID)
                .ToList()
                .Where(x =>
                    DateTime.Compare(x.CurrentDate.Value.Date, curDate.Date) == 0)
                    .ToList();            
            List<BidEntity> bidEntites = new List<BidEntity>();
            BidEntity bidEntity = null;
            BidEntity currentBidEntity = null;
            if (bidsItems.Count()!= 0)
            {
                foreach (Bids bidItem in bidsItems)
                {
                    if (bidItem != null)
                    {
                        bidEntity=new BidEntity()
                        {
                            BidId=(int)bidItem.BidId,
                            Amount=(int)bidItem.Amount,
                            currentUser=new UserItemEntity()
                            {
                                UserItemId = bidItem.UserItems.UserItemId,
                                Email = bidItem.UserItems.Email,
                                Name = bidItem.UserItems.Name,
                                UPassword = bidItem.UserItems.UPassword,
                                Rating = (int)bidItem.UserItems.Rating,
                                UserImage = (byte[])bidItem.UserItems.UserImage
                            },
                            AuctionItemID=(int)bidItem.AuctionItemID
                        };
                        bidEntites.Add(bidEntity);
                    }
                }
                
            }
            else
            {
                currentBidEntity = new BidEntity()
                {
                    Amount = 0,
                    currentUser = new UserItemEntity()
                };
                bidEntites.Add(currentBidEntity);
            }
            AEntities.Dispose();
            return bidEntites;           
        }
    }
}
