use SocialActive
go
CREATE TABLE [dbo].[Blogs] ( 
    [BlogId] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR (200) NULL, 
    [Url]  NVARCHAR (200) NULL, 
    CONSTRAINT [PK_dbo.Blogs] PRIMARY KEY CLUSTERED ([BlogId] ASC) 
); 
 
CREATE TABLE [dbo].[Posts] ( 
    [PostId] INT IDENTITY (1, 1) NOT NULL, 
    [Title] NVARCHAR (200) NULL, 
    [Content] NTEXT NULL, 
    [BlogId] INT NOT NULL, 
    CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED ([PostId] ASC), 
    CONSTRAINT [FK_dbo.Posts_dbo.Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blogs] ([BlogId]) ON DELETE CASCADE 
);
use pp2022
go
CREATE TABLE [dbo].[UserItems] ( 
    [UserItemId] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR (200) NULL, 
    [UPassword]  NVARCHAR (200) NULL, 
    [Rating] INT NULL,
    [Email] NVARCHAR(200) NULL,
    [UserImage] VARBINARY(MAX),
    CONSTRAINT [PK_dbo.UserItems] PRIMARY KEY CLUSTERED ([UserItemId] ASC)
); 
CREATE TABLE [dbo].[Bids] ( 
    [BidId] INT IDENTITY (1, 1) NOT NULL, 
    [Amount] INT NULL,
    [UserItemId] INT NOT NULL,
    [AuctionItemID] INT NOT NULL,
    CONSTRAINT [PK_dbo.Bids] PRIMARY KEY CLUSTERED ([BidId] ASC),
    CONSTRAINT [FK_dbo.Bids_dbo.Bids_BidId] FOREIGN KEY ([UserItemId]) REFERENCES [dbo].[UserItems] ([UserItemId]) ON DELETE CASCADE,   
	CONSTRAINT [FK_dbo.Bids_dbo.AuctionItems_AuctionItemID] FOREIGN KEY ([AuctionItemId]) REFERENCES [dbo].[AuctionItems] ([AuctionItemId])
); 
CREATE TABLE [dbo].[Categores] ( 
    [CategoryId] INT IDENTITY (1, 1) NOT NULL, 
    [Category] NVARCHAR(200) NULL,
    CONSTRAINT [PK_dbo.Categores] PRIMARY KEY CLUSTERED ([CategoryId] ASC)  
);
CREATE TABLE [dbo].[SpecialFeaturesEntryForms] ( 
    [SpecialFeatureId] INT IDENTITY (1, 1) NOT NULL, 
    [Feature] NVARCHAR(200) NULL,
    CONSTRAINT [PK_dbo.SpecialFeaturesEntryForms] PRIMARY KEY CLUSTERED ([SpecialFeatureId] ASC)  
); 
CREATE TABLE [dbo].[AuctionItems] ( 
    [AuctionItemId] INT IDENTITY (1, 1) NOT NULL, 
    [Discription] NVARCHAR(200) NULL,
    [StartPrice] INT NOT NULL,
    [StartDate] DATETIME NOT NULL,
    [ImageAuctionItem] VARBINARY(MAX),
    [UserItemId] INT NOT NULL,
    [CategoryId] INT NOT NULL,
    [SpecialFeatureId]INT NOT NULL,
    [Info] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_dbo.AuctionItems] PRIMARY KEY CLUSTERED ([AuctionItemId] ASC),
    CONSTRAINT [FK_dbo.AuctionItems_dbo.UserItems_UserItemId] FOREIGN KEY ([UserItemId]) REFERENCES [dbo].[UserItems] ([UserItemId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AuctionItems_dbo.Categores_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categores] ([CategoryId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AuctionItems_dbo.SpecialFeatures_SpecialFeatureId] FOREIGN KEY ([SpecialFeatureId]) REFERENCES [dbo].[SpecialFeaturesEntryForms] ([SpecialFeatureId]) ON DELETE CASCADE            

);
CREATE TABLE [dbo].[PostsUItems] ( 
    [PostUItemId] INT IDENTITY (1, 1) NOT NULL, 
    [Post] NVARCHAR(200) NULL,
    [PostTime] DATETIME NULL,
    [UserItemId]INT NOT NULL,
    CONSTRAINT [PK_dbo.PostsUItems] PRIMARY KEY CLUSTERED ([PostUItemId] ASC),  
    CONSTRAINT [FK_dbo.PostsUItems_dbo.UserItems_UserItemId] FOREIGN KEY ([UserItemId]) REFERENCES [dbo].[UserItems] ([UserItemId]) ON DELETE CASCADE,
); 

ALTER TABLE Bids ADD  AuctionItemID int not null
go
alter table Bids
add CONSTRAINT [FK_dbo.Bids_dbo.AuctionItems_AuctionItemId] 
FOREIGN KEY ([UserItemId]) REFERENCES [dbo].[AuctionItems] ([AuctionItemId])  
go
GO
ALTER TABLE AuctionItems ADD Info NVARCHAR (max) NULL
use Auction

alter table Bids 
add CurrentDate Datetime null

alter table [AuctionItems]
drop constraint[FK_dbo.AuctionItems_dbo.SpecialFeatures_SpecialFeatureId]
alter table [AuctionItems]
drop constraint[FK_dbo.AuctionItems_dbo.Categores_CategoryId] 
alter table [AuctionItems]
drop constraint[FK_dbo.AuctionItems_dbo.UserItems_UserItemId]
alter table [Bids]
drop constraint[FK_dbo.Bids_dbo.AuctionItems_AuctionItemID]
alter table [PostsUItems]
drop constraint[FK_dbo.PostsUItems_dbo.UserItems_UserItemId]
alter table [Bids]
drop constraint[FK_dbo.Bids_dbo.Bids_BidId]
alter table [AuctionItems]
drop constraint[PK_dbo.AuctionItems]
alter table [SpecialFeaturesEntryForms]
drop constraint[PK_dbo.SpecialFeaturesEntryForms]
alter table [Categores]
drop constraint[PK_dbo.Categores]
alter table [Bids]
drop constraint[PK_dbo.Bids]
alter table [UserItems]
drop constraint[PK_dbo.UserItems]
alter table [PostsUItems]
drop constraint[PK_dbo.PostsUItems]

go
sp_help [UserItems]
go
sp_help [PostsUItems]
go
sp_help [Bids]
go
sp_help [Categores]
go
sp_help [SpecialFeaturesEntryForms]
go
sp_help [AuctionItems]
go

drop table [UserItems]
go
drop table [PostsUItems]
go
drop table [Bids]
go
drop table [Categores]
go
drop table [SpecialFeaturesEntryForms]
go
drop table [AuctionItems]

