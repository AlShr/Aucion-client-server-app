using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyWCFServices.RealAuctionEntities;
using MyWCFServices.RealAuctionLogic;

namespace MyWCFServices.RealAuctionService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuctionService : IAuctionService
    {
        AuctionLogic auctionLogic = new AuctionLogic();
        static Dictionary<UserItem, IClientCallback> clients = new Dictionary<UserItem, IClientCallback>();
        
        static Dictionary<AuctionItem, List<UserItem>> auctionclient =
            new Dictionary<AuctionItem, List<UserItem>>();
       
        

        public IClientCallback CurrentCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IClientCallback>();
            }
        }
        private bool SearchClientsbyName(string name)
        {
            foreach (UserItem u in clients.Keys)
            {
                if (u.Name == name)
                {
                    return true;
                }
                
            }
            return false;
        }
        public void LeavedAuctionNavigation(UserItem user)
        {
            if (SearchClientsbyName(user.Name))
            {
                clients.Remove(user);
            }     
        }
        private bool SearchAuctionClientbyName(int auctionItemID)
        {
            foreach (AuctionItem a in auctionclient.Keys)
            {
                if (a.AuctionItemID == auctionItemID)
                {
                    return true;
                }
            }
            return false;
        }
        public UserItem GetUserItem(string name, string pass)
        {
            UserItemEntity userItemEntity = auctionLogic.GetUserItem(name, pass);
            UserItem userItem = null;        
            if (userItemEntity != null)
            {
                userItem = new UserItem();
                TranslateUserItemEntityToUserItemContractData(userItemEntity, userItem);
                if (!clients.ContainsValue(CurrentCallback) &&
                    !SearchClientsbyName(userItem.Name))
                {
                    clients.Add(userItem,CurrentCallback);
                }
            }
            return userItem;
        }
        
       //добавление  на  торги по лоту
        public bool UserItemJoined(AuctionItem a,UserItem u)
        {
            if (!SearchAuctionClientbyName(a.AuctionItemID))
            {
                if (SearchClientsbyName(u.Name))
                {
                    List<UserItem> userList = new List<UserItem>();
                    userList.Add(u);
                    auctionclient.Add(a, userList);
                }
            }
            else
            {
                auctionclient[a].Add(u);
            }
            PostsUItem postuItem = new PostsUItem()
            {
                Post = u.Name + " присоединился",
                Said = new UserItem
                {

                    UserItemID = u.UserItemID
                },
                PostTime = DateTime.Now
            };
            PostsUItemEntity postUEntity = new PostsUItemEntity();
            TranslatePostsUItemContractDataToPostsUItemEntity(postUEntity, postuItem);
            foreach (UserItem key in auctionclient[a])
            {
                IClientCallback callback = clients[key];
                try
                {
                   // callback.RefreshAuctionItems();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public PostsUItem GetPostUserItem(int userId)
        {
            PostsUItemEntity postsUItemEntity = auctionLogic.GetPostUserItem(userId);
            PostsUItem postUItem = null;
            if (postsUItemEntity != null)
            {
                postUItem = new PostsUItem();
                TranslatePostsUItemContractDataToPostsUItemEntity(postsUItemEntity, postUItem);
            }
            return postUItem;
        }
        public bool InsertUserItem(UserItem userItem)
        {
            UserItemEntity userItemEntity = new UserItemEntity();
            TranslateUserItemContractDataToUserItemEntity(userItem, userItemEntity);
            bool flag = auctionLogic.InsertUserItem(userItemEntity);
            if (flag == true)
            {
                if (!clients.ContainsValue(CurrentCallback) &&
                    !SearchClientsbyName(userItem.Name))
                {
                    clients.Add(userItem, CurrentCallback);
                }
            }
            return flag;

        }
        public Bid GetCurrentBid(int id)
        {
            BidEntity bidItemEntity = auctionLogic.GetBidItem(id);
            Bid bidItem = null;
            if (bidItemEntity != null)
            {
                bidItem = new Bid();
                TranslateBidItemEntityToBidItemContractData(bidItemEntity, bidItem);
            }
            return bidItem;
        }
        private void TranslateUserItemEntityToUserItemContractData(UserItemEntity userItemEntity, UserItem userItem)
        {
            userItem.UserItemID = userItemEntity.UserItemId;
            userItem.Email = userItemEntity.Email;
            userItem.Name = userItemEntity.Name;
            userItem.Upassword = userItemEntity.UPassword;
            userItem.Rating = userItemEntity.Rating;
            userItem.UserImage = userItemEntity.UserImage;
        }
        private void TranslateUserItemContractDataToUserItemEntity(UserItem userItem, UserItemEntity userItemEntity)
        {
            userItemEntity.UserItemId = userItem.UserItemID;
            userItemEntity.Email = userItem.Email;
            userItemEntity.Name = userItem.Name;
            userItemEntity.UPassword = userItem.Upassword;
            userItemEntity.Rating = userItem.Rating;
            userItemEntity.UserImage = userItem.UserImage;
        }
        public List<AuctionItem> GetAuctionItem()
        {
            List<AuctionItemEntity> auctionItemEntites = auctionLogic.GetAuctionItem();
            List<AuctionItem> auctionItems = new List<AuctionItem>();
            AuctionItem auctionItem = null;
            foreach (AuctionItemEntity auctionItemEntity in auctionItemEntites)
            {
                auctionItem = new AuctionItem();
                TranslateAuctionItemEntityToAuctionItemContractData(auctionItemEntity, auctionItem);
                auctionItems.Add(auctionItem);
            }
            return auctionItems;

        }
        public List<PostsUItem> GetPostsUserItem()
        {
            List<PostsUItemEntity> postsUItemsEntites = auctionLogic.GetPostUserItem();
            List<PostsUItem> postUItems = new List<PostsUItem>();
            PostsUItem postsUItem = null;
            foreach (PostsUItemEntity postsUItemEntity in postsUItemsEntites)
            {
                postsUItem = new PostsUItem();
                TranslatePostsUItemEntityToPostsUItemContractData(postsUItemEntity, postsUItem);
                postUItems.Add(postsUItem);
            }
            return postUItems;
        }
        public List<Bid> GetBidsItem(int auctionItemID)
        {
            List<BidEntity> bidEntites = auctionLogic.GetBidsItems(auctionItemID);
            List<Bid> bidItems = new List<Bid>();
            Bid bidItem = null;
            foreach(BidEntity bidEntity in  bidEntites)
            {
                bidItem = new Bid();
                TranslateBidItemEntityToBidItemContractData(bidEntity, bidItem);
                bidItems.Add(bidItem);
            }
            return bidItems;
        }
        public bool InsertAuctionItem(AuctionItem auctionItem)
        {
            AuctionItemEntity auctionItemEntity = new AuctionItemEntity();
            TranslateAuctionItemContractDataToAuctionItemEntity(auctionItemEntity, auctionItem);
            bool flag = auctionLogic.InsertAuctionItem(auctionItemEntity);
            if (flag == true)
            {
                foreach (UserItem key in clients.Keys)
                {
                    IClientCallback callback = clients[key];
                    try
                    {
                        callback.RefreshAuctionItems();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return flag;
        }
        public bool InsertPostUserItem(PostsUItem postsUItem)
        {
            PostsUItemEntity postsUItemEntity = new PostsUItemEntity();
            TranslatePostsUItemContractDataToPostsUItemEntity(postsUItemEntity, postsUItem);
            bool flag = auctionLogic.InsertPostUserItem(postsUItemEntity);
            if (flag == true)
            {
                foreach (UserItem key in clients.Keys)
                {
                    IClientCallback callback = clients[key];
                    try
                    {
                        callback.RefreshPostsUItem();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return flag;
        }
        public bool InsertBidsItem(Bid currentBid, AuctionItem auctionItem,UserItem userItem )
        {
            BidEntity bidEntity = new BidEntity();
            TranslateBidItemContractDataToBidItemEntity(bidEntity, currentBid);
            int keyAItem=auctionItem.AuctionItemID;
	        int keyUItem=userItem.UserItemID;
            bool flag = auctionLogic.InsertBidItem(bidEntity, keyAItem,keyUItem);
            if (flag == true)
            {
                foreach (UserItem key in clients.Keys)
                {
                    IClientCallback callback = clients[key];
                    try
                    {
                        callback.RefreshBidItem();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return flag;

        }
        public bool ExecuteDealItem(DealsItem dealsItem, Bid bidItem, AuctionItem auctionItem, UserItem userItem)
        {
            DealEntity dealEntity = new DealEntity();
            TranslateDealsItemContractDataToDealItemEntity(dealEntity, dealsItem);
            int keyAItem = auctionItem.AuctionItemID;
            int keyUItem = userItem.UserItemID;
            int keyBItem = bidItem.BidID;
            dealEntity.CurrentTime = DateTime.Now;
            bool flag = auctionLogic.ExecuteDealItem(dealEntity, keyBItem, keyAItem, keyUItem);
            if (flag == true)
            {
                foreach (UserItem key in clients.Keys)
                {
                    IClientCallback callback = clients[key];
                    try
                    {
                        callback.RefreshBidItems();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return flag;
        }
        private void TranslateAuctionItemEntityToAuctionItemContractData(AuctionItemEntity auctionItemEntity, AuctionItem auctionItem)
        {
            auctionItem.AuctionItemID = auctionItemEntity.AuctionItemID;
            auctionItem.Description = auctionItemEntity.Description;
            auctionItem.StartPrice = auctionItemEntity.StartPrice;
            auctionItem.BuyNowPrice = auctionItemEntity.BuyNowPrice;
            auctionItem.StartDate = auctionItemEntity.StartDate;
            auctionItem.Category = auctionItemEntity.Category;
            auctionItem.Owner = new UserItem()
            {
                UserItemID = auctionItemEntity.Owner.UserItemId,
                Email = auctionItemEntity.Owner.Email,
                Name = auctionItemEntity.Owner.Name,
                Upassword = auctionItemEntity.Owner.UPassword,
                Rating = auctionItemEntity.Owner.Rating,
                UserImage = auctionItemEntity.Owner.UserImage
            };
            auctionItem.ImageAuctionItem = auctionItemEntity.ImageAuctionItem;
            auctionItem.SpecialFeatures = auctionItemEntity.SpecialFeature;
            auctionItem.Info = auctionItemEntity.InfoEntity;
            auctionItem.SaleFlag = auctionItemEntity.SaleFlagEntity;
            auctionItem.Bids = new System.Collections.ObjectModel.ObservableCollection<Bid>();
            if (auctionItemEntity.Bids.Count != 0)
            {
                foreach (BidEntity bidEntity in auctionItemEntity.Bids.ToList())
                {
                    auctionItem.Bids.Add(
                        new Bid
                        {
                            BidID = bidEntity.BidId,
                            Amount = bidEntity.Amount,
                            CurrentUseritem = new UserItem()
                            {
                                UserItemID = bidEntity.currentUser.UserItemId,
                                Name = bidEntity.currentUser.Name,
                                Email = bidEntity.currentUser.Email,
                                Upassword = bidEntity.currentUser.UPassword,
                                Rating = bidEntity.currentUser.Rating,
                                UserImage = bidEntity.currentUser.UserImage
                            }
                        });

                }
            }
        }
        private void TranslateAuctionItemContractDataToAuctionItemEntity(AuctionItemEntity auctionItemEntity, AuctionItem auctionItem)
        {
            auctionItemEntity.AuctionItemID = auctionItem.AuctionItemID;
            auctionItemEntity.Description = auctionItem.Description;
            auctionItemEntity.StartPrice = auctionItem.StartPrice;
            auctionItemEntity.BuyNowPrice = auctionItem.BuyNowPrice;
            auctionItemEntity.StartDate = auctionItem.StartDate;
            auctionItemEntity.Category = auctionItem.Category;
            auctionItemEntity.Owner = new UserItemEntity()
            {
                UserItemId = auctionItem.Owner.UserItemID,
                Email = auctionItem.Owner.Email,
                Name = auctionItem.Owner.Name,
                UPassword = auctionItem.Owner.Upassword,
                Rating = auctionItem.Owner.Rating,
                UserImage = auctionItem.Owner.UserImage
            };
            auctionItemEntity.ImageAuctionItem = auctionItem.ImageAuctionItem;
            auctionItemEntity.SpecialFeature = auctionItem.SpecialFeatures;
            auctionItemEntity.InfoEntity = auctionItem.Info;
            auctionItemEntity.SaleFlagEntity = auctionItem.SaleFlag;
            auctionItemEntity.Bids = new List<BidEntity>();
            if (auctionItem.Bids.Count != 0)
            {
                foreach (Bid bid in auctionItem.Bids)
                {
                    auctionItemEntity.Bids.Add(
                        new BidEntity
                        {
                            BidId = bid.BidID,
                            Amount = bid.Amount,
                            currentUser = new UserItemEntity()
                            {
                                UserItemId = bid.CurrentUseritem.UserItemID,
                                Name = bid.CurrentUseritem.Name,
                                Email = bid.CurrentUseritem.Email,
                                UPassword = bid.CurrentUseritem.Upassword,
                                Rating = bid.CurrentUseritem.Rating,
                                UserImage = bid.CurrentUseritem.UserImage
                            }
                        });
                }
            }
        }
        private void TranslatePostsUItemEntityToPostsUItemContractData(PostsUItemEntity postsUItemEntity, PostsUItem postsUItem)
        {
            postsUItem.PostUItemID = postsUItemEntity.PostUItemID;
            postsUItem.Post = postsUItemEntity.Post;
            postsUItem.PostTime = postsUItemEntity.PostTime;
            postsUItem.Said=new UserItem()
            {
                UserItemID = postsUItemEntity.Said.UserItemId,
                Email = postsUItemEntity.Said.Email,
                Name = postsUItemEntity.Said.Name,
                Upassword = postsUItemEntity.Said.UPassword,
                Rating = postsUItemEntity.Said.Rating,
                UserImage = postsUItemEntity.Said.UserImage
            };
        }
        private void TranslatePostsUItemContractDataToPostsUItemEntity(PostsUItemEntity postsUItemEntity, PostsUItem postsUItem)
        {
            postsUItemEntity.PostUItemID = postsUItem.PostUItemID;
            postsUItemEntity.Post = postsUItem.Post;
            postsUItemEntity.PostTime = postsUItem.PostTime;
            postsUItemEntity.Said = new UserItemEntity()
            {                
                UserItemId = postsUItem.Said.UserItemID,
                Email = postsUItem.Said.Email,
                Name = postsUItem.Said.Name,
                UPassword = postsUItem.Said.Upassword,
                Rating = postsUItem.Said.Rating,
                UserImage = postsUItem.Said.UserImage
            };
        }
        private void TranslateBidItemEntityToBidItemContractData(BidEntity bidItemEntity, Bid bidItem)
        {
            bidItem.BidID = bidItemEntity.BidId;           
            bidItem.CurrentUseritem = new UserItem()
            {
                UserItemID = bidItemEntity.currentUser.UserItemId,
                Email = bidItemEntity.currentUser.Email,
                Name = bidItemEntity.currentUser.Name,
                Upassword = bidItemEntity.currentUser.UPassword,
                Rating = bidItemEntity.currentUser.Rating,
                UserImage = bidItemEntity.currentUser.UserImage
            };
             
            bidItem.AuctionItemID = bidItemEntity.AuctionItemID;
            bidItem.Amount=bidItemEntity.Amount;
            bidItem.CurrentDate = bidItemEntity.CurrentDate;
        }
        private void TranslateBidItemContractDataToBidItemEntity(BidEntity bidItemEntity, Bid bidItem)
        {
            bidItemEntity.BidId = bidItem.BidID;
            /*
            bidItemEntity.currentUser= new UserItemEntity()
            {
                UserItemId = bidItem.CurrentUseritem.UserItemID,
                Email = bidItem.CurrentUseritem.Email,
                Name = bidItem.CurrentUseritem.Name,
                UPassword = bidItem.CurrentUseritem.Upassword,
                Rating = bidItem.CurrentUseritem.Rating,
                UserImage = bidItem.CurrentUseritem.UserImage
            };
             */
            
            bidItemEntity.Amount = bidItem.Amount;
            bidItemEntity.CurrentDate = bidItem.CurrentDate;
        }
        public void TranslateDealsItemContractDataToDealItemEntity(DealEntity dealItemEntity, DealsItem dealItem)
        {
            dealItemEntity.DealID = dealItem.DealId;
            dealItemEntity.CurrentTime = dealItem.CurrentTime;
        }
        private void TranslateDealItemEntityToDealstemContractData(DealEntity dealItemEntity, DealsItem dealItem)
        {
            dealItem.DealId = dealItemEntity.DealID;
            dealItem.BidItem = new Bid()
            {
                CurrentDate = dealItemEntity.bidEntity.CurrentDate,
                Amount = dealItemEntity.bidEntity.Amount,
                CurrentUseritem = new UserItem() 
                {
                    Name=dealItemEntity.bidEntity.currentUser.Name,
                    Email=dealItemEntity.bidEntity.currentUser.Email,
                    UserImage=dealItemEntity.bidEntity.currentUser.UserImage
                }
            };
            dealItem.CurrentTime = dealItemEntity.CurrentTime;
            dealItem.AuctionItem = new AuctionItem()
            {
                Description = dealItemEntity.auctionItemEntity.Description,
                Category = dealItemEntity.auctionItemEntity.Category,
                ImageAuctionItem = dealItemEntity.auctionItemEntity.ImageAuctionItem,
                Owner = new UserItem()
                {
                    Name = dealItemEntity.auctionItemEntity.Owner.Name,
                    Email = dealItemEntity.auctionItemEntity.Owner.Email,
                    UserImage = dealItemEntity.auctionItemEntity.Owner.UserImage,
                },
                Info=dealItemEntity.auctionItemEntity.InfoEntity,
                StartDate=dealItemEntity.auctionItemEntity.StartDate,
                StartPrice=dealItemEntity.auctionItemEntity.StartPrice,
                BuyNowPrice=dealItemEntity.auctionItemEntity.BuyNowPrice
            };

        }
        public CategoryItem GetCategory(string category)
        {
            CategoryItem categores = new CategoryItem();
            categores.Category = category;
            return categores;
        }
        public SpecialFeatureItem GetSpecialFeature(string feature)
        {
            SpecialFeatureItem special = new SpecialFeatureItem();
            special.Feature = feature;
            return special;
        }

   
    }
}
