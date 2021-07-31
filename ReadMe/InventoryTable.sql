  create table Inventory(
  id int identity(1,1) primary key,
  name varchar(40),
  description text,
  price int,
  serialnumber varchar(10),
  manufacturingdate date,
  expirydate date
  );

insert into Inventory values('Milk', 'Milk just milk', 24, 'P94FE0NECW', '2021-06-28', '2021-07-29');
insert into Inventory values('Tea', 'Tea bag', 20, 'P94GE0NECW', '2021-06-28', '2021-07-29');
insert into Inventory values('Eggs', 'White eggs', 36, 'P92FE0NECW', '2021-06-28', '2021-07-29');
insert into Inventory values('Tea1', 'Tea bag', 20, 'P91FE0NECW', '2021-06-28', '2021-07-29');
insert into Inventory values('Tea2', 'Tea bag', 20, 'P94FE0NEDW', '2021-06-28', '2021-07-29');
insert into Inventory values('Tea3', 'Tea bag', 20, 'P94FE0NERW', '2021-06-28', '2021-07-29');