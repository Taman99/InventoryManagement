create database InventoryManagement;

Create table Products (
	ProductId int PRIMARY KEY,
	ProductName varchar(50),
	MerchantId varchar(50),
	ProductDesc varchar(200),
	ImageUrl1 varchar(255),
	ImageUrl2 varchar(255),
	ImageUrl3 varchar(255),
	ImageUrl4 varchar(255),
	ImageUrl5 varchar(255),
	ImageUrl6 varchar(255),	
	CategoryId int,
	ProductTag varchar(20),
	ProductQuantity int,
	ProductPrice decimal,
	ProductDiscount decimal,	
	SizesExist BIT
);

create table Sizes(
	SizeId int identity(1,1) PRIMARY KEY,
	ProductId int, 		
	SizeName varchar(10),
	SizePrice decimal 
);


create table Categories(
	CategoryId int identity(1,1) PRIMARY KEY,
	CategoryName varchar(25),	
);


CREATE TABLE [UserProfile](
	[UserId] [varchar](36) NOT NULL,
	[UserFirstName] [varchar](30) NULL,
	[UserLastName] [varchar](30) NULL,
	[CompanyName] [varchar](50) NULL,
	[ProfilePictureUrl] [varchar](255) NULL,
	[UserEmail] [varchar](50) NULL,
)

Alter table userprofile
add UserPhoneNo varchar(10),
	Gender varchar(6),
	UserAddress varchar(150),
	UserState varchar(30),
	UserCountry varchar(20)
	
Alter table categories
add UserId varchar(50)

