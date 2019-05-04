CREATE DATABASE SkateShopDb
GO

CREATE SCHEMA SkateShop
GO

--  Creating tables --

CREATE TABLE Location (
    LocationID INT IDENTITY,
    Address NVARCHAR(200) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_LocationID PRIMARY KEY(LocationID)
)

CREATE TABLE Customer (
    CustomerID INT IDENTITY,
    LocationID INT NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_CustomerID PRIMARY KEY(CustomerID)
)

CREATE TABLE [Order] (
    OrderID INT IDENTITY,
    CustomerID INT NOT NULL,
    LocationID INT NOT NULL,
    Date DATETIME NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT PK_OrderID PRIMARY KEY(OrderID)
)

CREATE TABLE OrderItem (
    OrderItemID INT IDENTITY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    CONSTRAINT PK_OrderItemID PRIMARY KEY(OrderItemID)
)

CREATE TABLE Product (
    ProductID INT IDENTITY,
    Name NVARCHAR(200) NOT NULL,
    Price MONEY NOT NULL,
    CONSTRAINT PK_ProductID PRIMARY KEY(ProductID)
)

CREATE TABLE InventoryItem (
    InventoryItemID INT IDENTITY,
    ProductID INT NOT NULL,
    LocationID INT NOT NULL,
    CONSTRAINT PK_InventoryItemID PRIMARY KEY(InventoryItemID)
)

--  Establishing foreign key relationships  --

ALTER TABLE Customer ADD
    CONSTRAINT FK_LocationID FOREIGN KEY(LocationID) REFERENCES Location(LocationID)

ALTER TABLE [Order] ADD
    CONSTRAINT FK_CustomerID FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_LocationID FOREIGN KEY(LocationID) REFERENCES Location(LocationID)

ALTER TABLE OrderItem ADD 
    CONSTRAINT FK_OrderID FOREIGN KEY(OrderID) REFERENCES [Order](OrderID),
    CONSTRAINT FK_ProductID FOREIGN KEY(ProductID) REFERENCES Product(ProductID)

ALTER TABLE InventoryItem ADD
    CONSTRAINT FK_ProductID FOREIGN KEY(ProductID) REFERENCES Product(ProductID),
    CONSTRAINT FK_LocationID FOREIGN KEY(LocationID) REFERENCES Location(LocationID)
