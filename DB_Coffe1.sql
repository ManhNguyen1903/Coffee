CREATE database DB_Coffee1

CREATE TABLE Role (
  id int PRIMARY KEY IDENTITY(1, 1),
  name nvarchar(50)
)
GO

CREATE TABLE Account (
  id int PRIMARY KEY IDENTITY(1, 1),
  fullname nvarchar(100),
  password varchar(50),
  role_id int
)
GO

CREATE TABLE Employee (
  id int PRIMARY KEY IDENTITY(1, 1),
  fullname nvarchar(50),
  time nvarchar(20),
  salary nvarchar(100),
  phonenumber varchar(10),
  banknumber varchar(50),
  bankname varchar(50)
)
GO


CREATE TABLE Product (
  id int PRIMARY KEY IDENTITY(1, 1),
  category_id int,
  title nvarchar(250),
  price int,
  discount int,
  thumbnail varchar(500)
)
GO

CREATE TABLE Category (
  id int PRIMARY KEY IDENTITY(1, 1),
  name nvarchar(100)
)
GO

CREATE TABLE Galery (
  id int PRIMARY KEY IDENTITY(1, 1),
  product_id int,
  thumbnail varchar(500)
)
GO


CREATE TABLE TableFood (
  id int PRIMARY KEY IDENTITY(1, 1),
  tablename nvarchar(50),
  status nvarchar(50)
)
GO

Create table Bill(
	id int PRIMARY KEY IDENTITY(1, 1),
	DateCheckIn Date not null default GETDATE(), 
	DateCheckOut Date,
	idTable int not null,
	status int not null default 0
)
create table BillInfo(
	id int PRIMARY KEY IDENTITY(1, 1),
	idBill int not null,
	idFood int not null,
	count int not null default 0
)

ALTER TABLE Users ADD FOREIGN KEY (role_id) REFERENCES Role (id)
GO

ALTER TABLE Order_Details ADD FOREIGN KEY (product_id) REFERENCES Product (id)
GO

ALTER TABLE Galery ADD FOREIGN KEY (product_id) REFERENCES Product (id)
GO

ALTER TABLE Order_Details ADD FOREIGN KEY (order_id) REFERENCES Orders (id)
GO

ALTER TABLE Product ADD FOREIGN KEY (category_id) REFERENCES Category (id)
GO

SELECT* FROM Category

CREATE PROCEDURE USP_GetAccountbyUserName
@userName varchar(50)
As 
	begin 
	Select * from Account where username = @userName
	end
GO

EXEC USP_GetAccountbyUserName @userName = 'manh'

select * from Account where username = '1' and password ='1';

DECLARE @i INT = 0
while @i <=20
begin 
	insert TableFood ( tablename)VALUES (N'Bàn ' + cast(@i as nvarchar(100)))
	set @i = @i +1
end

create proc USP_GetTableList
as select * from TableFood
go

exec USP_GetTableList

insert Bill(DateCheckIn,DateCheckOut,idTable,status)
values (GETDATE(),GETDATE(),2,0)

select * from Bill where idTable = 2 and status = 0
SELECT * FROM BillInfo WHERE idBill = 2

SELECT Product.name,BillInfo.count,Product.price,Product.price*BillInfo.count as totalPrice
FROM Bill,BillInfo,Product
WHERE BillInfo.idBill = Bill.id and BillInfo.idFood = Product.id and Bill.status=0
and Bill.idTable = 1

select * from Product where category_id = 1;
select * from Bill

create PROC USP_InsertBill
@idTable INT
AS
begin
	insert Bill(
      DateCheckIn
      ,DateCheckOut
      ,idTable
      ,status
	  )
values (getdate(),
		NUll,
		@idTable,
		0
		)
end
go

exec USP_InsertBill 16

alter  PROCEDURE USP_InsertBillInfo
    @idBill INT,
    @idFood INT,
    @count INT
AS
BEGIN
    INSERT INTO BillInfo(idBill, idFood, count)
    VALUES(@idBill, @idFood, @count)
END


alter PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
begin
		declare @isExitsBillInfo INT
		declare @foodCount INT = 1

		select @isExitsBillInfo = id, @foodcount = b.count 
		from BillInfo as b
		where idBill = @idBill and idFood = @idFood

		if(@isExitsBillInfo > 0)
		begin
			declare @newcount int = @foodcount + @count
			if(@newcount > 0)
				Update BillInfo Set count = @foodcount + @count where idFood = @idFood and idBill = @idBill
			else
				Delete BillInfo where idBill = @idBill and idFood = @idFood
		end
		else
		begin
			insert BillInfo
			(idBill,idFood,count)
			values (@idBill,
					@idFood,
					@count
					)
		end
end
go

exec USP_InsertBillInfo 1,1,1

select MAX(id) from Bill

exec USP_InsertBillInfo 5,2,10
USP_InsertBillInfo 1,2,18

select BillInfo.* from BillInfo where idBill=15
exec USP_InsertBillInfo 1025, 1, 1

create trigger UTG_UpdateBillInfo
On BillInfo FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = idBill FROM Inserted
	DECLARE @idTable int
	select @idTable = idTable From Bill where id = @idBill and status = 0
	update TableFood set status = N'Có người' where id = @idTable	

END

alter trigger UTG_UpdateBill
on Bill for update
as
begin
	declare @idBill int
	select @idBill = id from inserted
	declare @idTable int
	select @idTable = idTable from Bill where id = @idBill

	declare @count int = 0
	select @count = COUNT(*) from Bill where idTable = @idTable and status = 0

	if(@count = 0)
	update TableFood set status = N'Trống' where id = @idTable
end

delete BillInfo
delete Bill

select * from Product
select product.id as N'Số thứ tự ', Product.name as N'Tên sản phẩm', Category.name as N'Loại',Product.price as N'Giá'  
from Product, Category 
where Product.category_id = Category.id



alter PROC USP_InsertProduct
@name nvarchar(100), @category_id int, @price float

as 
begin
INSERT Product ( name, category_id, price) 
VALUES (@name, @category_id, @price)
end


alter PROC USP_UpdateProduct
@name nvarchar(100), @category_id int, @price float, @idProduct int,
@status nvarchar(100)
as 
begin
Update Product
set name = @name, category_id=@category_id,price=@price,status = @status
where Product.id = @idProduct
end

exec USP_UpdateProduct  N'mực 1 nắng ',1,1000000,1


select p.id, p.name,c.name,price
from Product as p, Category as c
where p.category_id =c.id

select* from Category


ALTER TABLE Product
add status nvarchar(100) default N'Còn hàng'
DROP COLUMN thumbnail;

update Product
set status = N'Còn hàng'

select*
from Product
where category_id = 1 and status = N'Còn hàng'


alter PROC USP_InsertCategory
@name nvarchar(100)
as 
begin
INSERT Category( name) 
VALUES (@name)
end

create PROC USP_UpdateCategory
@name nvarchar(100),@idCategory int
as 
begin
Update Category
set name = @name
where Category.id = @idCategory
end

exec USP_InsertCategory N'Nước mắm'

create PROC USP_DeleteCategory
@idCategory int
as 
begin
delete Category
where Category.id = @idCategory
end

create PROC USP_DeleteProduct
@idProduct int
as 
begin
delete Product
where Product.id = @idProduct
end

exec USP_DeleteCategory 6

select * from TableFood

alter PROC USP_InsertTable
@name nvarchar(100), @status nvarchar(100)
as 
begin
INSERT TableFood( tablename,status)
VALUES (@name, @status)
end

exec USP_InsertTable N'Bàn test', N'Trống'

alter PROC USP_UpdateTable
@name nvarchar(100),@idTable int, @status nvarchar(100) 
as 
begin
Update TableFood
set tablename = @name, status = @status 
where  id = @idTable
end

select * from Employee

create PROC USP_DeleteTable
@idTable int
as 
begin
delete TableFood
where TableFood.id = @idTable
end

select * from Bill

delete Bill
where Bill.idTable = 30

exec USP_DeleteTable 30

select * from Employee
-- nfjikwefiwehbkfnhkujweqnhbfkujhnqeuwhnfou

create PROC USP_InsertEmployee
	@fullname nvarchar(50),
  @time nvarchar(20),
  @salary nvarchar(100),
  @phonenumber varchar(10),
  @banknumber varchar(50),
  @bankname varchar(50)
as 
begin
INSERT Employee([fullname]
      ,[time]
      ,[salary]
      ,[phonenumber]
      ,[banknumber]
      ,[bankname])
VALUES (@fullname,@time,@salary,@phonenumber,@banknumber,@bankname)
end

exec USP_InsertEmployee N'Nguyễn Hà', N'Ca sáng',N'19k/h','0365473322', '0365473322', MB
delete Employee

create PROC USP_UpdateEmployee
	@fullname nvarchar(50),
  @time nvarchar(20),
  @salary nvarchar(100),
  @phonenumber varchar(10),
  @banknumber varchar(50),
  @bankname varchar(50),
  @idEmployee int
as 
begin
Update Employee
set		[fullname] = @fullname
      ,[time] = @time
      ,[salary] = @salary
      ,[phonenumber] = @phonenumber
      ,[banknumber] = @banknumber
      ,[bankname] = @bankname
where  id = @idEmployee
end

exec USP_UpdateEmployee N'Nguyễn Hà', N'Ca sáng',N'19k/h','0365473322', '0365473322', MVPC,1

alter PROC USP_DeleteEmployee
@idEmployee int
as 
begin
delete Employee
where Employee.id = @idEmployee
end