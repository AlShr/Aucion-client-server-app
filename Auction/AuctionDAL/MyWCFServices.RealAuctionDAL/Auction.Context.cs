﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AuctionEntities : DbContext
    {
        public AuctionEntities()
            : base("name=AuctionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AuctionItems> AuctionItems { get; set; }
        public DbSet<Bids> Bids { get; set; }
        public DbSet<Categores> Categores { get; set; }
        public DbSet<Deals> Deals { get; set; }
        public DbSet<PostsUItems> PostsUItems { get; set; }
        public DbSet<SpecialFeaturesEntryForms> SpecialFeaturesEntryForms { get; set; }
        public DbSet<UserItems> UserItems { get; set; }
    }
}
