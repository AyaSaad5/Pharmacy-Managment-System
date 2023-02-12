use pharmacy;
GO
 

create table الشركة_المنتجة
(
م int primary key,
اسم_الشركة nvarchar(50) not null,
رقم_التليفون varchar(15) not null,
العنوان nvarchar(50) not null
);  

create table المشتريات
(
كود_الشركة int references الشركة_المنتجة(م),
كود_الصنف int references الأصناف(كود_الصنف),
التاريخ date not null
);

create table العملاء
(
كود_العميل int primary key,
اسم_العميل nvarchar(50) not null,
رقم_التليفون varchar(15) not null,
العنوان nvarchar(50) not null
);  

create table المبيعات
(
رقم_الطلب int not null,
كود_العميل int references العملاء(كود_العميل),
كود_الصنف int references الأصناف(كود_الصنف),
التاريخ date not null
);

create table المخازن
(
كود_الصنف int references الأصناف(كود_الصنف)
);
use pharmacy;
GO