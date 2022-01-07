create database InventoryManagement;

--Run in Products db

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

--Run in Sizes Db
create table Sizes(
	SizeId int identity(1,1) PRIMARY KEY,
	ProductId int, 		
	SizeName varchar(10),
	SizePrice decimal 
);

--Run in Category db

create table Categories(
	CategoryId int identity(1,1) PRIMARY KEY,
	CategoryName varchar(25),	
);