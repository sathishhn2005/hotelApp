CREATE TABLE `tb_admin` (
  `CompanyID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(50) DEFAULT NULL,
  `AdminName` varchar(40) DEFAULT NULL,
  `PhoneNumber` varchar(30) DEFAULT NULL,
  `Address` varchar(40) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `AccRegisteredDate` datetime DEFAULT NULL,
  `Cost` bigint(20) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `Logo` longblob,
  PRIMARY KEY (`CompanyID`)
) 

CREATE TABLE `tb_adminsubscriptionpaymentdetails` (
  `AdminSubscriptionId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `TotalDays` int(11) DEFAULT NULL,
  `Price` bigint(20) DEFAULT NULL,
  `Tax` int(11) DEFAULT NULL,
  `TotalAmount` bigint(20) DEFAULT NULL,
  `PaymentType` varchar(50) DEFAULT NULL,
  `PaymentStatus` varchar(50) DEFAULT NULL,
  `Comments` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`AdminSubscriptionId`)
); 

CREATE TABLE `tb_customers` (
  `CustomerId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `OTP` bigint(20) DEFAULT NULL,
  `SerialNo` varchar(50) DEFAULT NULL,
  `Name` varchar(40) DEFAULT NULL,
  `PhoneNumber` varchar(30) DEFAULT NULL,
  `Address` varchar(40) DEFAULT NULL,
  `DOB` varchar(50) DEFAULT NULL,
  `RegisteredDate` datetime DEFAULT NULL,
  PRIMARY KEY (`CustomerId`)
);

CREATE TABLE `tb_foodcategory` (
  `FoodCategoryId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `CategoryName` varchar(50) DEFAULT NULL,
  `Description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`FoodCategoryId`)
) ;

CREATE TABLE `tb_foodproductcategory` (
  `FoodProductCategoryId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `FoodProductId` bigint(20) DEFAULT NULL,
  `FoodCategoryId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`FoodProductCategoryId`)
) ;

CREATE TABLE `tb_foodproducts` (
  `FoodProductId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `FoodName` varchar(50) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `ImageSource` longblob,
  `ImagePath` varchar(50) DEFAULT NULL,
  `Price` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`FoodProductId`)
) ;

CREATE TABLE `tb_imagesource` (
  `ImageSourceId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `CategoryId` bigint(20) DEFAULT NULL,
  `ProductId` varchar(50) DEFAULT NULL,
  `ImageSource` longblob,
  `Type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ImageSourceId`)
) ;

CREATE TABLE `tb_orderdetails` (
  `OrderDetailsId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FoodProductId` bigint(20) DEFAULT NULL,
  `CompanyID` bigint(20) DEFAULT NULL,
  `CustomerID` bigint(20) DEFAULT NULL,
  `TableNo` int(11) DEFAULT NULL,
  `CName` varchar(100) DEFAULT NULL,
  `FoodName` varchar(100) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `PhoneNo` varchar(100) DEFAULT NULL,
  `DOB` varchar(100) DEFAULT NULL,
  `TotalAmount` bigint(20) DEFAULT NULL,
  `Comments` varchar(1000) DEFAULT NULL,
  `Status` varchar(100) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`OrderDetailsId`)
) ;

CREATE TABLE `tb_placeorder` (
  `PlaceOrderId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FoodProductId` bigint(20) DEFAULT NULL,
  `CompanyID` bigint(20) DEFAULT NULL,
  `CustomerID` bigint(20) DEFAULT NULL,
  `CustomerName` varchar(20) DEFAULT NULL,
  `PhoneNumber` bigint(20) DEFAULT NULL,
  `TotalAmount` bigint(20) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `Tax` int(11) DEFAULT NULL,
  `TableNo` int(11) DEFAULT NULL,
  `RazorPaymentId` varchar(100) DEFAULT NULL,
  `RazorOrderDetailsId` varchar(100) DEFAULT NULL,
  `PaymentStatus` varchar(100) DEFAULT NULL,
  `PaymentType` varchar(100) DEFAULT NULL,
  `Comments` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`PlaceOrderId`)
) ;

CREATE TABLE `tb_razorpayinfo` (
  `RazorPayInfoId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `keyId` varchar(100) DEFAULT NULL,
  `keySecret` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`RazorPayInfoId`)
) ;

CREATE TABLE `tb_uploadbanner` (
  `ImageSourceId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyID` bigint(20) DEFAULT NULL,
  `ImageSource` longblob,
  PRIMARY KEY (`ImageSourceId`)
) ;

CREATE TABLE `tb_users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(100) NOT NULL,
  `password` varchar(40) NOT NULL,
  `confirmpassword` varchar(40) DEFAULT NULL,
  `phonenumber` varchar(40) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
);
