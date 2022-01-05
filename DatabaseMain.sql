create database Products;
create database Category;
create database Size;


--Run in Products db

Create table tbl_Products (
	pdt_id int identity(1,1) PRIMARY KEY,
	pdt_name varchar(50),
	user_id varchar(50),
	pdt_desc varchar(200),
	pdt_category_id int,
	pdt_tag varchar(20),
	pdt_quantity int,
	pdt_price decimal,
	pdt_discount decimal
);
Create table tbl_ProductImages (
	pdt_id int foreign key (pdt_id) REFERENCES tbl_Products(pdt_id),
	image_url nvarchar(255), 
	primary key (pdt_id,image_url)
);

--Run in Sizes Db
create table tbl_Sizes(
	size_index int PRIMARY KEY,
	pdt_id int,
	size varchar(10),
	size_price decimal 
);

--Run in Category db
create table tbl_Categories(
	category_id int identity(1,1) PRIMARY KEY,
	category varchar(25),	
);