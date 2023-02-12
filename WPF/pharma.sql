create database pharmacy

use pharmacy;
GO
create table products
(
product_id int identity primary key,
product_name nvarchar(50) not null,
chemical_name nvarchar(50),
quantity int not null,
type nvarchar(50) not null,
production_date date not null,
expire_date date not null,
price float not null,
discount float
);

create table Clients
(
client_id int identity primary key,
client_name nvarchar(50) not null,
phone varchar(15) not null,
address nvarchar(50) not null
); 

create table suppliers
(
supplier_id int identity primary key,
supplier_name nvarchar(50) not null,
phone varchar(15) not null,
address nvarchar(50) not null
);  

create table purchases
(
supplier_id int references suppliers(supplier_id),
product_id int references products(product_id),
date date not null
);

create table sales
(
product_num int not null,
client_id int references Clients(client_id),
product_id int references products(product_id),
date date not null
);

create table store
(
product_id int references products(product_id)
);
