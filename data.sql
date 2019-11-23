use ApartmentManagement
go
-----------------------------
--Complex
insert into dbo.Complex values ('complex 1','123 NDC street, Ward 10, District 1','HCM','5','07123456');
insert into dbo.Complex values ('complex 2','200 BTD street, Ward 2, District 7','HCM','2','07134457');
insert into dbo.Complex values ('complex 3','1 ASC street, Ward 6, District 3','HCM','3','07143457');
--Department
alter table dbo.Department
nocheck constraint FK_Department_Staff
go
insert into dbo.Department values (1,'Security','')
insert into dbo.Department values (2,'Logistic','')
go
alter table dbo.Department
check constraint FK_Department_Staff
go
--Staff
insert into dbo.Staff values (1001,'Nguyen Van A','1992-1-11','123 ABC street, Ward 6, District 2','HCM','035289745',1,1,'2018-2-4','')
insert into dbo.Staff values (2001,'Nguyen Van B','1995-3-3','456 ABC street, Ward 2, District 3','HCM','035245341',2,1,'2017-3-10','')


select * from dbo.Complex
select * from dbo.Department
select * from dbo.Staff