create database InventoryManagement;

--Run in Products db

Create table Products (
	ProductId int identity(1,1) PRIMARY KEY,
	ProductName varchar(50),
	MerchantId varchar(50),
	ProductDesc varchar(200),
	CategoryId int,
	ProductTag varchar(20),
	ProductQuantity int,
	ProductPrice decimal,
	ProductDiscount decimal
);
Create table ProductImages (
	ProductId int foreign key (ProductId) REFERENCES Products(ProductId),
	ImageUrl nvarchar(255), 
	primary key (ProductId,ImageUrl)
);

--Run in Sizes Db
create table Sizes(
	SizeIndex int PRIMARY KEY,
	ProductId int,
	Size varchar(10),
	SizePrice decimal 
);

--Run in Category db
create table Categories(
	CategoryId int identity(1,1) PRIMARY KEY,
	Category varchar(25),	
);