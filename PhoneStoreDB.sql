-- create database phonestore;
-- use phonestore;
-- CREATE TABLE Phones(
-- Phone_ID INT AUTO_INCREMENT,  -- AUTO_INCREMENT
-- Phone_Name VARCHAR (50) NOT NULL,
-- Brand VARCHAR(50) NOT NULL,
-- CPUnit VARCHAR(50),
-- RAM VARCHAR(50),
-- DiscountPrice DECIMAL default '0',
-- Price DECIMAL NOT NULL,
-- Battery_Capacity VARCHAR(50),
-- OS VARCHAR(50) NOT NULL,
-- Sim_Slot VARCHAR(50),
-- Screen_Hz VARCHAR(50),
-- Screen_Resolution VARCHAR(50),
-- ROM VARCHAR(50),
-- StorageMemory VARCHAR(50),
-- Mobile_Network VARCHAR(50),
-- Quantity int not null,
-- Phone_Size VARCHAR(50),
-- PRIMARY KEY (Phone_ID)
-- )engine = InnoDB;

-- insert into phones(phone_id, phone_name, brand, price, os, quantity) 
-- value('1', 'samsung s21', 'samsung', '14000000', 'android', '10');



-- delimiter $$
-- create trigger before_update_on_phones
-- before update on phones
-- for each row
-- begin
-- if(new.quantity <0) then signal sqlstate '02212' set message_text = 'Quantity of phone cannot less than 0';
-- end if;
-- end$$
-- delimiter ;

-- CREATE TABLE Customers(
-- Customer_ID INT auto_increment,
-- Customer_Name VARCHAR(50),
-- Job VARCHAR(50),
-- Phone_Number VARCHAR(15) NOT NULL,
-- Address VARCHAR(100),
-- PRIMARY KEY (Customer_ID)
-- )engine = InnoDB;
-- insert into customers(customer_name, job, phone_number, address)value('Thay ong noi', 'Teacher', '0969696969', 'aaa');
-- insert into customers(customer_name, job, phone_number, address)value('Khanh Sky', 'Student', '0969696969', 'bbb');

-- CREATE TABLE Staffs(
-- Staff_ID INT auto_increment,
-- Staff_Name VARCHAR(50) NOT NULL,
-- User_Name VARCHAR(50) NOT NULL UNIQUE,
-- Password VARCHAR(50) NOT NULL,
-- Address VARCHAR(100),
-- Title_ID INT NOT NULL,
-- PRIMARY KEY (Staff_ID)
-- )engine = InnoDB;
-- insert into staffs(staff_id, staff_name, user_name, password, title_id) 
-- value('0', 'default', 'default', 'default', '2');
-- insert into staffs(staff_name, user_name, password, title_id) 
-- value('Truong con', 'truongcon69', '12345xyz', '1');
-- insert into staffs(staff_name, user_name, password, title_id) 
-- value('Dau cat moi', 'catmoi69', '12345xyz', '2');

-- CREATE TABLE Orders(
-- Order_ID INT auto_increment,
-- Customer_ID INT,
-- Seller_ID INT,
-- Accountant_ID INT default '0',
-- Order_Date DATETIME NOT NULL default current_timestamp(),
-- Order_Note VARCHAR(100),
-- Order_Status VARCHAR(50) NOT NULL default 'Processing',
-- PaymentMethod VARCHAR(50) default 'not have',
-- FOREIGN KEY (Customer_ID) REFERENCES Customers(Customer_ID),
-- FOREIGN KEY (Seller_ID) REFERENCES Staffs(Staff_ID),
-- FOREIGN KEY (Accountant_ID) REFERENCES Staffs(Staff_ID),
-- PRIMARY KEY (Order_ID)
-- )engine = InnoDB;

-- create table orderdetails(
-- order_id int not null,
-- phone_id int not null,
-- phone_imei varchar(20) not null,
-- constraint fk_order_id_od foreign key(order_id) references orders(order_id),
-- constraint fk_phone_id_od foreign key(phone_id) references phones(phone_id),
-- constraint pk_orderdetails primary key(order_id, phone_id, phone_imei)
-- )engine = InnoDB;

-- delimiter $$
-- create trigger after_insert_on_orderdetails 
-- after insert on orderdetails
-- for each row
-- begin
-- update phones set quantity = quantity -1 where phone_id = new.phone_id;
-- end$$
-- delimiter ;


