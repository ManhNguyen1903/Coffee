CREATE database DB_Coffee

CREATE TABLE Role (
  id int PRIMARY KEY IDENTITY(1, 1),
  name varchar(20)
)
GO

CREATE TABLE Users (
  id int PRIMARY KEY IDENTITY(1, 1),
  fullname varchar(50),
  password varchar(32),
  role_id int
)
GO

CREATE TABLE Employee (
  user_id int PRIMARY KEY IDENTITY(1, 1),
  fullname varchar(50),
  time varchar(20),
  salary int
)
GO

CREATE TABLE Product (
  id int PRIMARY KEY IDENTITY(1, 1),
  category_id int,
  title varchar(250),
  price int,
  discount int,
  thumbnail varchar(500)
)
GO

CREATE TABLE Category (
  id int PRIMARY KEY IDENTITY(1, 1),
  name varchar(100)
)
GO

CREATE TABLE Galery (
  id int PRIMARY KEY IDENTITY(1, 1),
  product_id int,
  thumbnail varchar(500)
)
GO

CREATE TABLE Orders (
  id int PRIMARY KEY IDENTITY(1, 1),
  fullname varchar(50),
  phone_number varchar(20),
  note varchar(1000),
  order_date datetime,
  total_money int
)
GO

CREATE TABLE Order_Details (
  id int PRIMARY KEY IDENTITY(1, 1),
  order_id int,
  product_id int,
  price int,
  num int,
  total_money int
)
GO

CREATE TABLE Tables (
  id int PRIMARY KEY IDENTITY(1, 1),
  num int,
  status int
)
GO

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
