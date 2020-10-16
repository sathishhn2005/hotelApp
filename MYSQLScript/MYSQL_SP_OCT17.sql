DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_DeleteBanners`(
in CompID int
)
BEGIN
delete from tb_uploadbanner where CompanyID=CompID;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_DeleteFoodProducts`(
in CompID int
)
BEGIN
delete from tb_foodcategory where CompanyID=CompID;
delete from  tb_foodproductcategory where CompanyID=CompID;
delete from  tb_foodproducts where CompanyID=CompID;
delete from  tb_imagesource where CompanyID=CompID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetBanner`(
in CompID int
)
BEGIN
select CompanyID, ImageSource from TB_UploadBanner where CompanyID=CompID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetBillingDetails`(in CompnID long)
BEGIN

declare TaxAmt int;
set TaxAmt=(select distinct Tax FROM TB_ADMINSUBSCRIPTIONPAYMENTDETAILS);
select TaxAmt as Tax,FP.Price as UnitPrice,DOB,PlaceOrderId,PO.FoodProductId,FoodName,PO.CompanyID,PO.CustomerID,PO.CustomerName,PO.PhoneNumber,PO.TotalAmount,PO.Quantity,PO.Tax,PO.TableNo,PO.RazorOrderDetailsId
,PO.RazorPaymentId,PaymentStatus,PaymentType,PO.Comments,'pending' as Status,PO.CreatedAt as CreatedAt,PO.Comments as Comments
 From tb_placeorder PO
Inner join tb_foodproducts FP ON FP.FoodProductId=PO.FoodProductId and FP.CompanyID=PO.CompanyID
Inner join tb_customers C ON C.CustomerId=PO.CustomerID and C.CompanyID=PO.CompanyID
where PO.CompanyID=CompnID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetFoodProducts`(in CompnID long)
BEGIN
select FPC.CompanyID,FPC.FoodProductId,FPC.FoodCategoryId,CategoryName,FoodName,IMS.ImageSource,FP.Price,FP.Description,IMS.Type  From tb_foodproductcategory FPC
inner join TB_FOODCATEGORY FC on FC.FoodCategoryId=FPC.FoodCategoryId AND FC.CompanyID=FPC.CompanyID
inner join tb_foodproducts FP on FP.FoodProductId=FPC.FoodProductId and FP.CompanyID=FPC.CompanyID
INNER JOIN TB_IMAGESOURCE IMS on IMS.CompanyID=FPC.CompanyID and IMS.ProductId=FPC.FoodProductId
where FPC.CompanyID=CompnID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetOrderDetails`(in CompnID long)
BEGIN
declare TaxAmt int;
set TaxAmt=(select distinct Tax FROM TB_ADMINSUBSCRIPTIONPAYMENTDETAILS where CompanyID=CompnID);
select TaxAmt as Tax,FP.Price as UnitPrice,DOB,PlaceOrderId,PO.FoodProductId,FoodName,PO.CompanyID,PO.CustomerID,PO.CustomerName,PO.PhoneNumber,PO.TotalAmount,PO.Quantity,PO.Tax,PO.TableNo,PO.RazorOrderDetailsId
,PO.RazorPaymentId,PaymentStatus,PaymentType,PO.Comments,Status,PO.CreatedAt as CreatedAt,PO.Comments as Comments
 From tb_placeorder PO
Inner join tb_foodproducts FP ON FP.FoodProductId=PO.FoodProductId and FP.CompanyID=PO.CompanyID
Inner join tb_customers C ON C.CustomerId=PO.CustomerID and C.CompanyID=PO.CompanyID
where PO.CompanyID=CompnID order by PO.CreatedAt desc;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetRazorPayKey`(
in CompnID bigint
)
BEGIN
Select   RazorPayInfoId ,  CompanyID ,  keyId ,  keySecret from tb_razorpayinfo where CompanyID=CompnID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetSubPaymentHistory`(in CompnId bigint)
BEGIN
SELECT AdminSubscriptionHistoryId,    AdminSubscriptionId,    CompanyID,    StartDate,    EndDate,    TotalDays,    Price,
    Tax,    TotalAmount,    PaymentType,    PaymentStatus,    Comments,CreatedAt as AccRegisteredDate FROM tb_adminsubscriptionpaymentdetails_history where CompanyID=CompnId;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_GetSubscriptionSA`(
in SAUserName VARCHAR(50),
in Flag int
)
BEGIN
if (flag=1) then
begin

SELECT AdminSubscriptionId,    A.CompanyID,    StartDate,    EndDate,    TotalDays,    Price,    Tax,    TotalAmount,    PaymentType,    PaymentStatus,Comments,
CompanyName,AdminName,PhoneNumber,Address,UserName,Password,AccRegisteredDate,Cost,Status
FROM tb_adminsubscriptionpaymentdetails SA
Inner join TB_Admin A on A.CompanyID=SA.CompanyID;
end;
else if(flag=2) then
select CompanyId as CompanyID,CompanyName,AdminName,PhoneNumber,Address,UserName,Password,AccRegisteredDate,Cost,Status from tb_admin;
end if;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertAdmin`(
in Addre varchar(200),
in CompName varchar(40),
in CName varchar(40),
in CPDay Bigint,
in Commnt varchar(40),

in CompStatus varchar(40),
in PhNo varchar(40),
in Uname varchar(40),
in Pswd VARCHAR(40),
in AccRegDate DATETIME,

in Amt BIGINT,
in TotDays int,
in PStatus varchar(40),
in Edate datetime,
in Sdate datetime,

in Ptype varchar(40),
CompId bigint
)
BEGIN
Declare IsExists int;
Declare _ReturnValue int ;
set IsExists=(Select count(*) from TB_Admin WHERE CompanyName=CompName and username=Uname and Password=Pswd);
if IsExists=0 then
Insert into TB_Admin(CompanyName,AdminName,PhoneNumber,Address,UserName,Password,AccRegisteredDate,Cost,Status)
select CompName,CName,PhNo,Addre,Uname,Pswd,AccRegDate,Amt,CompStatus;
set _ReturnValue=(select last_insert_id());
insert into tb_adminsubscriptionpaymentdetails(CompanyID,StartDate,EndDate,TotalDays,Price,TotalAmount,PaymentType,PaymentStatus,Comments)
select _ReturnValue,Sdate,Edate,TotDays,CPDay,Amt,Ptype,PStatus,Commnt;
SELECT A.CompanyID,    CompanyName,    AdminName,    PhoneNumber,    Address,    UserName,    Password,    AccRegisteredDate,    Cost,    Status,    Logo,
AdminSubscriptionId,  StartDate,    EndDate,    TotalDays,    Price,    Tax,    TotalAmount,    PaymentType,    PaymentStatus,    Comments FROM TB_Admin A

Inner join tb_adminsubscriptionpaymentdetails TAS on TAS.CompanyId=A.CompanyId;
else 
set _ReturnValue=0;
UPDATE tb_admin SET AdminName =CName ,CompanyName=CompName,PhoneNumber = PhNo,Address =Addre ,UserName =Uname ,Password = Pswd,Cost =Amt ,
Status =CompStatus 	
WHERE CompanyID = CompId;

UPDATE tb_adminsubscriptionpaymentdetails SET StartDate = Sdate,EndDate =Edate,Price = CPDay,
TotalAmount =Amt,PaymentType = Ptype,PaymentStatus = PStatus,Comments = Commnt WHERE CompanyID = CompId;

SELECT A.CompanyID,    CompanyName,    AdminName,    PhoneNumber,    Address,    UserName,    Password,    AccRegisteredDate,    Cost,    Status,    Logo,
AdminSubscriptionId,  StartDate,    EndDate,    TotalDays,    Price,    Tax,    TotalAmount,    PaymentType,    PaymentStatus,    Comments FROM TB_Admin A

Inner join tb_adminsubscriptionpaymentdetails TAS on TAS.CompanyId=A.CompanyId;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertCustCart`(
in FoodProdid bigint,
in CompID bigint,
in CustID bigint,
in TotalAmt bigint,
in Discnt int,
in U_Price int,
in Qty int,
in TaxPer int,
in TableId int

)
BEGIN
DECLARE _ReturnValue bigint;
Insert into TB_OrderDetails (FoodProductId,CompanyID,CustomerID,TotalAmount,Discount,UnitPrice,Quantity,Tax,TableNo)
select FoodProdid,CompID,CustID,TotalAmt,Discnt,U_Price,Qty,TaxPer,TableId; 
set _ReturnValue=(select last_insert_id());
if _ReturnValue>0 then
Select OrderDetailsId,FoodProductId,CompanyID,CustomerID,TotalAmount,Discount,UnitPrice,Quantity,Tax,TableNo from 
TB_OrderDetails where OrderDetailsId=_ReturnValue;
end if;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertCustomer`(
in CompID bigint,
in OTP bigint,
in SerialNo varchar(50),
in Name varchar(40),
in PhoneNumber varchar(40),
in Address varchar(200),
in DOB varchar(40)


)
BEGIN
Declare _ReturnValue INT;
Declare _taxAmt int;
Insert into TB_Customers (CompanyID,OTP ,SerialNo ,Name ,PhoneNumber ,Address ,DOB)
select CompID,OTP ,SerialNo ,Name ,PhoneNumber ,Address ,DOB; 
set _ReturnValue=(select last_insert_id());
if _ReturnValue>0 then
set _taxAmt =(SELECT distinct Tax from TB_ADMINSUBSCRIPTIONPAYMENTDETAILS A where A.CompanyID=CompID);

Select CompanyID,CustomerId,OTP,PhoneNumber,SerialNo,Name,_taxAmt as Tax,Address from 
Tb_customers where CustomerId=_ReturnValue;

end if;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertFoodProducts`(
in CompID int,
in PriceAmt int,
in Foodnam varchar(40),
in CatgryName varchar(40),
in Descr varchar(1000),
in ImageSou longblob,
in ImgSourceType varchar(50)
)
BEGIN

declare FoodProduId bigint;
declare FoodCatgryId bigint;
Declare IsExists int;




set IsExists=(Select count(*) from tb_foodcategory WHERE CompanyID=CompID and CategoryName=CatgryName);
if IsExists=0 then

Insert into tb_foodcategory(CompanyID,CategoryName,Description)
select CompID,CatgryName,Descr;
set FoodCatgryId=(select last_insert_id());
else
set FoodCatgryId=(Select distinct FoodCategoryId from tb_foodcategory WHERE CompanyID=CompID and CategoryName=CatgryName);
end if;


set IsExists=(Select count(*) from tb_Foodproducts WHERE CompanyID=CompID and FoodName=Foodnam);
if IsExists=0 then

Insert into tb_Foodproducts(CompanyID ,Price,FoodName,Description )
select CompID ,PriceAmt,Foodnam ,Descr;
set FoodProduId=(select last_insert_id());
else
set FoodProduId=(Select distinct FoodProductId from tb_Foodproducts WHERE CompanyID=CompID and FoodName=Foodnam);
end if;

set IsExists=(Select count(*) from tb_ImageSource WHERE CompanyID=CompID and ProductId=FoodProduId);
if IsExists!=4 then

insert into tb_ImageSource(CompanyID,ProductId,ImageSource,Type)
select CompID,FoodProduId,ImageSou,ImgSourceType;
end if;


set IsExists=(Select count(*) from TB_FOODPRODUCTCATEGORY WHERE CompanyID=CompID and FoodProductId=FoodProduId and FoodCategoryId=FoodCatgryId);
if IsExists=0 then
Insert into TB_FOODPRODUCTCATEGORY(CompanyID,FoodProductId,FoodCategoryId)
select CompID,FoodProduId,FoodCatgryId;
end if;
select FPC.CompanyID,FPC.FoodProductId,FPC.FoodCategoryId,CategoryName,FoodName,IMS.ImageSource,FP.Price,FP.Description,IMS.Type  From tb_foodproductcategory FPC
inner join TB_FOODCATEGORY FC on FC.FoodCategoryId=FPC.FoodCategoryId AND FC.CompanyID=FPC.CompanyID
inner join tb_foodproducts FP on FP.FoodProductId=FPC.FoodProductId and FP.CompanyID=FPC.CompanyID
INNER JOIN TB_IMAGESOURCE IMS on IMS.CompanyID=FPC.CompanyID and IMS.ProductId=FPC.FoodProductId
where FPC.CompanyID=CompID;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertPlaceOrder`(
in FoodProdid bigint,
in CompID bigint,
in CustID bigint,
in CustName varchar(50),
in PhoneNo varchar(50),
in Cmnt varchar(250),
in TotalAmt bigint,
in Qty int,
in TaxPer int,
in TableId int,
in PaymentType varchar(50),
in OrderId varchar(100),
in Addr varchar(150)

)
BEGIN
DECLARE _ReturnValue bigint;
Declare CompName varchar(50);
DECLARE Logoo longblob;
Declare CreatedAtDate datetime;
set CreatedAtDate=NOW();
set Logoo=(Select distinct Logo from TB_Admin where CompanyID=CompID);
set CompName=(Select DISTINCT CompanyName from TB_Admin where CompanyID=CompID);
Insert into TB_PlaceOrder (FoodProductId,CompanyID,CustomerID,CustomerName,PhoneNumber,TotalAmount,Quantity,Tax,TableNo,PaymentType,
RazorOrderDetailsId,Comments,Address,CreatedAt)
select FoodProdid,CompID,CustID,CustName,PhoneNo,TotalAmt,Qty,TaxPer,TableId,PaymentType,OrderId,Cmnt,Addr,CreatedAtDate; 
set _ReturnValue=(select last_insert_id());
if _ReturnValue>0 then
Select CompName as CompanyName ,'Online Payment' as Description,Logoo as Logo,PlaceOrderId,FoodProductId,CompanyID,CustomerID,CustomerName,PhoneNumber,TotalAmount,Quantity,Tax,TableNo,PaymentType,
RazorOrderDetailsId,Comments,Address,CreatedAt from TB_PlaceOrder where PlaceOrderId=_ReturnValue;
end if;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_InsertUsers_SuperAdmin`(
in SAName varchar(100),
in UserName varchar(100),
in Pswd varchar(40),
in ConfirmPswd varchar(40),
in PhNo varchar(40),

OUT _ReturnValue INT

)
BEGIN
Declare IsExists int;
set IsExists=(Select count(*) from TB_USERS WHERE User_Name=UserName);
if IsExists=0 then
Insert into TB_USERS (name,user_name,password,phonenumber)
select SAName,UserName,Pswd,PhNo;
set _ReturnValue=(select last_insert_id());
else
set _ReturnValue=0;
end if;

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_IsSAUserExits`(
in UserN nvarchar(50),
in Pswd nvarchar(50)
)
BEGIN
select User_Id,User_Name as UserName,Name,PhoneNumber From TB_USERS where User_Name=UserN and Password=Pswd;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_IsUserExits`(
in UserN nvarchar(50),
in Pswd nvarchar(50)
)
BEGIN
declare CId bigint;
set CId=(Select distinct CompanyId from tb_admin where UserName=UserN and Password=Pswd and Status='Active');
if(Cid>0) then
select UserName,A.CompanyID as CompanyID,AdminSubscriptionId,StartDate,EndDate,TotalDays,Price,Tax,TotalAmount,PaymentType,PaymentStatus,Comments 
from TB_Admin A
Inner join TB_ADMINSUBSCRIPTIONPAYMENTDETAILS ASPD ON ASPD.CompanyId=A.CompanyId
where UserName=UserN and Password=Pswd and A.CompanyId=CId and A.Status='Active';
end if;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PauseSubscription`(
in CompID bigint,
in PauseStatus varchar(50),
OUT _ReturnValue INT
)
BEGIN
UPDATE TB_ADMIN SET Status =PauseStatus WHERE CompanyId= CompID;
set _ReturnValue=(select row_count());
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_Renewal`(
in CompnId bigint,
in SDate datetime,
in EDate datetime,
in TotDays varchar(30),
in PriceAmt bigint,
in TaxAmt int,
in TotalAmt bigint,
out _ReturnValue int

)
BEGIN
Declare IsExists int;
set IsExists=(Select count(*) from TB_AdminSubscriptionPaymentDetails WHERE CompanyId=CompnId);
if IsExists=0 then
Insert into TB_AdminSubscriptionPaymentDetails (CompanyId ,StartDate,EndDate,TotalDays,Price,Tax,TotalAmount)
select CompnId SDate,EDate,TotDays,PriceAmt,TaxAmt,TotalAmt;
set _ReturnValue=(select last_insert_id());
Insert into tb_adminsubscriptionpaymentdetails_history (AdminSubscriptionId,CompanyId ,StartDate,EndDate,TotalDays,Price,Tax,TotalAmount)
select _ReturnValue,CompnId SDate,EDate,TotDays,PriceAmt,TaxAmt,TotalAmt;
else 
UPDATE TB_ADMINSUBSCRIPTIONPAYMENTDETAILS SET  StartDate=SDate,EndDate=EDate,TotalDays=TotDays,Price=PriceAmt,Tax=TaxAmt,TotalAmount=TotalAmt 
WHERE CompanyId= CompnId;
set _ReturnValue=(select row_count());
Insert into tb_adminsubscriptionpaymentdetails_history (AdminSubscriptionId,CompanyId ,StartDate,EndDate,TotalDays,Price,Tax,TotalAmount,PaymentType)
select AdminSubscriptionId,CompanyId ,StartDate,EndDate,TotalDays,Price,Tax,TotalAmount,'Cash' from TB_AdminSubscriptionPaymentDetails where CompanyId= CompnId;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_SubscriptionUpdate`(
in CompanyId bigint,
in PaymentType varchar(50),
in Comments nvarchar(200),
in TotalAmount bigint,
out _ReturnValue int

)
BEGIN
Update TB_AdminSubscriptionPaymentDetails set PaymentType=PaymentType and TotalAmount=TotalAmount and Comments=Comments and PaymentStatus='Completed'
where AdminSubscriptionId=AdminSubscriptionId AND CompanyId=CompanyId;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_UpdatePaymentHistory`(
in PymntStatus varchar(50),
in ASHisId bigint,
out _ReturnValue int
)
begin
update TB_ADMINSUBSCRIPTIONPAYMENTDETAILS_HISTORY set PaymentStatus=PymntStatus where AdminSubscriptionHistoryId=ASHisId;
set _ReturnValue=(select row_count());
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_UpdatePO`(
in OrderId varchar(100),
in POStatus varchar(50),
OUT _ReturnValue INT
)
BEGIN
update tb_placeorder set Status=POStatus WHERE RazorOrderDetailsId=OrderId;
set _ReturnValue=(select row_count());
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_UpdateTax`(
in CompnId bigint,
in Taxpercent nvarchar(50),
out _ReturnValue int
)
BEGIN
UPDATE tb_adminsubscriptionpaymentdetails SET Tax =Taxpercent WHERE CompanyId= CompnId;
set _ReturnValue=(select row_count());

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_Update_SAPassword`(

in user_name varchar(100),
in password varchar(40),
in confirmpassword varchar(40),

OUT _ReturnValue INT

)
BEGIN
update tb_users set password=password where username=user_name;
set _ReturnValue=(select row_count());
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_UploadBanner`(
in CompID int,
in ImageSou longblob
)
BEGIN

Declare IsExists int;
set IsExists=(Select count(*) from TB_UploadBanner WHERE CompanyID=CompID );
if IsExists!=6 then

insert into TB_UploadBanner(CompanyID,ImageSource)
select CompID,ImageSou;
end if;
select CompanyID,ImageSource from TB_UploadBanner where CompanyID=CompID;

END$$
DELIMITER ;
