create database phonestore;
use phonestore;
CREATE TABLE Phones(
Phone_ID INT AUTO_INCREMENT,  -- AUTO_INCREMENT
Phone_Name VARCHAR (200) NOT NULL,
Brand VARCHAR(200) NOT NULL,
CPU VARCHAR(200),
RAM VARCHAR(2000),
DiscountPrice DECIMAL default '0',
Price DECIMAL NOT NULL,
Battery_Capacity VARCHAR(200),
OS VARCHAR(50) NOT NULL,
Sim_Slot VARCHAR(100),
Screen_Hz VARCHAR(200),
Screen_Resolution VARCHAR(200),
ROM VARCHAR(200),
StorageMemory VARCHAR(200),
Mobile_Network VARCHAR(200),
Quantity int not null,
Phone_Size VARCHAR(200),
PRIMARY KEY (Phone_ID)
)engine = InnoDB;

INSERT INTO phones (Phone_Name, Brand, CPU, RAM, DiscountPrice, Price, Battery_Capacity, OS, Sim_Slot, Screen_Hz, Screen_Resolution, ROM, StorageMemory, Mobile_Network, quantity, phone_size) 
value('Samsung Galaxy A50', 'Samsung', 'Octa-core (4x2.3 GHz Cortex-A73 & 4x1.7 GHz Cortex-A53)', '4GB', '400000', '5000000', '4,000mAh Li-Ion; 15W quick charge', 'Android', '2', '6.4" Super AMOLED; 19.5:9 aspect ratio', 'FullHD+ (1080 x 2340 px)', '5GB', '4GB of RAM + 128GB storage / 6GB of RAM + 128GB storage; Up to 512GB microSD card support', '4G', '36', '1080 x 2340 px');
INSERT INTO phones (Phone_Name, Brand, CPU, RAM, DiscountPrice, Price, Battery_Capacity, OS, Sim_Slot, Screen_Hz, Screen_Resolution, ROM, StorageMemory, Mobile_Network, quantity, phone_size) 
value('Iphone 14 Pro Max', 'Apple', 'Apple A16 Bionic', '6GB', '500000', '10000000', 'Li-Ion 4323 mAh, non-removable (16.68 Wh)', 'IOS', '2', '120Hz', '1290 x 2796 pixels, 19.5:9 ratio (~460 ppi density)', '10GB', '128GB 6GB RAM, 256GB 6GB RAM, 512GB 6GB RAM, 1TB 6GB RAM', '4G', '100', '6.7 inches, 110.2 cm2 (~88.3% screen-to-body ratio)');

SELECT * FROM phones;

delimiter $$
create trigger before_update_on_phones
before update on phones
for each row
begin
if(new.quantity <0) then signal sqlstate '02212' set message_text = 'Quantity of phone cannot less than 0';
end if;
end$$
delimiter ;

CREATE TABLE Customers(
Customer_ID INT auto_increment,
Customer_Name VARCHAR(50),
Job VARCHAR(50),
Phone_Number VARCHAR(15) NOT NULL,
Address VARCHAR(100),
PRIMARY KEY (Customer_ID)
)engine = InnoDB;
insert into customers(customer_name, job, phone_number, address)value('Nguyen Xuan Sinh', 'Teacher', '0969696969', 'aaa');
insert into customers(customer_name, job, phone_number, address)value('Nguyen Thien An', 'Student', '0969696969', 'bbb');


CREATE TABLE Staffs(
Staff_ID INT auto_increment,
Staff_Name VARCHAR(50) NOT NULL,
User_Name VARCHAR(50) NOT NULL UNIQUE,
Password VARCHAR(50) NOT NULL,
Address VARCHAR(100),
Title_ID INT NOT NULL,
PRIMARY KEY (Staff_ID)
)engine = InnoDB;
insert into staffs(address, staff_name, user_name, password, title_id) 
value('Cau The Huc	', 'Kha Banh', 'seller01', 'e99a18c428cb38d5f260853678922e03', '1');
insert into staffs(address, staff_name, user_name, password, title_id) 
value('Thap Rua', 'Truong con', 'seller02', 'e99a18c428cb38d5f260853678922e03', '1');
insert into staffs(address, staff_name, user_name, password, title_id) 
value('Chua Mot Cot', 'Dau cat moi', 'accountant01', 'e99a18c428cb38d5f260853678922e03', '2');

CREATE TABLE Orders(
Order_ID INT auto_increment,
Customer_ID INT,
Seller_ID INT,
Accountant_ID INT default '0',
Order_Date DATETIME NOT NULL default current_timestamp(),
Order_Note VARCHAR(100),
Order_Status VARCHAR(50) NOT NULL default 'Processing',
PaymentMethod VARCHAR(50) default 'not have',
FOREIGN KEY (Customer_ID) REFERENCES Customers(Customer_ID),
FOREIGN KEY (Seller_ID) REFERENCES Staffs(Staff_ID),
FOREIGN KEY (Accountant_ID) REFERENCES Staffs(Staff_ID),
PRIMARY KEY (Order_ID)
)engine = InnoDB;

create table orderdetails(
order_id int not null,
phone_id int not null,
phone_imei varchar(20) not null,
constraint fk_order_id_od foreign key(order_id) references orders(order_id),
constraint fk_phone_id_od foreign key(phone_id) references phones(phone_id),
constraint pk_orderdetails primary key(order_id, phone_id, phone_imei)
)engine = InnoDB;

delimiter $$
create trigger after_insert_on_orderdetails 
after insert on orderdetails
for each row
begin
update phones set quantity = quantity -1 where phone_id = new.phone_id;
end$$
delimiter ;


