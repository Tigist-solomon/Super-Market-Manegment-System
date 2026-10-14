USE SupermarketDB;
GO

--------------------------------------------------
-- USERS
--------------------------------------------------
CREATE TABLE Users
(
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(64) NOT NULL,
    role NVARCHAR(20) NOT NULL
);

--------------------------------------------------
-- CUSTOMER
--------------------------------------------------
CREATE TABLE Customer
(
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    phone NVARCHAR(20)
);

--------------------------------------------------
-- CATEGORY
--------------------------------------------------
CREATE TABLE Category
(
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL
);

--------------------------------------------------
-- PRODUCTS
--------------------------------------------------
CREATE TABLE Products
(
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    category_id INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,

    CONSTRAINT FK_Product_Category
    FOREIGN KEY(category_id)
    REFERENCES Category(category_id)
);

--------------------------------------------------
-- ORDERS
--------------------------------------------------
CREATE TABLE Orders
(
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NULL,
    user_id INT NULL,
    date DATETIME NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    status NVARCHAR(20) NOT NULL DEFAULT 'Pending',

    CONSTRAINT FK_Order_Customer
    FOREIGN KEY(customer_id)
    REFERENCES Customer(customer_id),

    CONSTRAINT FK_Order_User
    FOREIGN KEY(user_id)
    REFERENCES Users(user_id)
);

--------------------------------------------------
-- ORDER DETAILS
--------------------------------------------------
CREATE TABLE OrderDetails
(
    order_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    product_name NVARCHAR(100),
    price DECIMAL(10,2),
    quantity INT NOT NULL,
    subtotal DECIMAL(10,2),

    CONSTRAINT FK_OrderDetail_Order
    FOREIGN KEY(order_id)
    REFERENCES Orders(order_id),

    CONSTRAINT FK_OrderDetail_Product
    FOREIGN KEY(product_id)
    REFERENCES Products(product_id)
);

--------------------------------------------------
-- PAYMENT
--------------------------------------------------
CREATE TABLE Payment
(
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    payment_method NVARCHAR(50),
    amount DECIMAL(10,2),
    payment_date DATETIME,

    CONSTRAINT FK_Payment_Order
    FOREIGN KEY(order_id)
    REFERENCES Orders(order_id)
);

--------------------------------------------------
-- START PRODUCT IDs FROM 1001
--------------------------------------------------
DBCC CHECKIDENT ('Products', RESEED, 1000);
GO



--------------------------------------------------
-- USERS
--------------------------------------------------

INSERT INTO Users(username,password,role)
VALUES
('admin','1234','Admin'),
('cashier','1111','Cashier'),
('customer','2222','Customer');

--------------------------------------------------
-- CATEGORIES
--------------------------------------------------

INSERT INTO Category(name)
VALUES
('Food'),
('Drinks'),
('Snacks'),
('Vegetables'),
('Fruits'),
('Dairy'),
('Bakery'),
('Cleaning'),
('Cosmetics'),
('Stationery');

--------------------------------------------------
-- PRODUCTS
--------------------------------------------------

INSERT INTO Products(name,category_id,price,quantity)
VALUES
('Bread',7,20.00,50),
('Water',2,10.00,100),
('Milk',6,35.00,40),
('Rice',1,80.00,60),
('Soap',8,45.00,30);


-- More Products

INSERT INTO Products (name, category_id, price, quantity) VALUES
('Sugar', 1, 65.00, 80),
('Salt', 1, 20.00, 100),
('Cooking Oil', 1, 350.00, 40),
('Pasta', 1, 90.00, 60),
('Biscuits', 3, 35.00, 120),
('Chocolate', 3, 70.00, 90),
('Orange Juice', 2, 80.00, 50),
('Soft Drink', 2, 35.00, 100),
('Apple', 5, 15.00, 200),
('Banana', 5, 8.00, 250),
('Tomato', 4, 18.00, 150),
('Onion', 4, 25.00, 180),
('Potato', 4, 22.00, 200),
('Cheese', 6, 120.00, 30),
('Butter', 6, 95.00, 45),
('Yogurt', 6, 40.00, 60),
('Cake', 7, 180.00, 20),
('Donut', 7, 25.00, 70),
('Shampoo', 9, 180.00, 35),
('Toothpaste', 9, 75.00, 80),
('Soap', 8, 45.00, 100),
('Detergent', 8, 220.00, 40),
('Notebook', 10, 50.00, 100),
('Pen', 10, 15.00, 300),
('Pencil', 10, 8.00, 250);

SELECT * FROM Users;

SELECT * FROM Category;

SELECT * FROM Products;

SELECT * FROM Orders;

SELECT * FROM OrderDetails;

SELECT * FROM Payment;