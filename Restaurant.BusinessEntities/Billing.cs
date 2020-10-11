using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class Billing
    {

        private long billingID = 0;
        private long orderDetailsId = 0;
        private long placeOrderId = 0;
        private long foodProductId = 0;

        private string razorPaymentId = string.Empty;
        private string description = string.Empty;

        private string razorOrderDetailsId = string.Empty;
        private string address = string.Empty;
        private string paymentStatus = string.Empty;
        private string paymentType = string.Empty;
        private string foodName = string.Empty;
        private long companyID = 0;
        private long customerId = 0;
        private long totalAmount = 0;
        private long totalOrders = 0;
        private long totalQtySales = 0;
        private long totalRevenue = 0;
        private int discount = 0;
        private long unitPrice = 0;
        private int quantity = 0;
        private long phoneNumber = 0;
        private int tax = 0;
        private int flag = 0;
        private int tableNo = 0;
        private string customerName = string.Empty;
        private string companyName = string.Empty;
        private string orderDetails = string.Empty;
        private string comments = string.Empty;
        private string status = string.Empty;
        private string dOB = string.Empty;

        private string createdBy = String.Empty;
        
        private DateTime createdAt = DateTime.MaxValue;
        private string modifiedBy = String.Empty;
        private DateTime modifiedAt = DateTime.MaxValue;
        private byte[] logo;



        public byte[] Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        public long BillingID { get { return billingID; } set { billingID = value; } }
        public long OrderDetailsId { get { return orderDetailsId; } set { orderDetailsId = value; } }
        public long CompanyID { get { return companyID; } set { companyID = value; } }
        public long CustomerId { get { return customerId; } set { customerId = value; } }
        public long TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public int Discount { get { return discount; } set { discount = value; } }
        public long UnitPrice { get { return unitPrice; } set { unitPrice = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public long PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string FoodName { get { return foodName; } set { foodName = value; } }

        public int Tax { get { return tax; } set { tax = value; } }
        public int Flag { get { return flag; } set { flag = value; } }
        public int TableNo { get { return tableNo; } set { tableNo = value; } }
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        public string OrderDetails { get { return orderDetails; } set { orderDetails = value; } }
        public string Comments { get { return comments; } set { comments = value; } }
        public string Status { get { return status; } set { status = value; } }
        public string DOB { get { return dOB; } set { dOB = value; } }
        public string RazorPaymentId { get { return razorPaymentId; } set { razorPaymentId = value; } }
        public string RazorOrderDetailsId { get { return razorOrderDetailsId; } set { razorOrderDetailsId = value; } }
        public string PaymentStatus { get { return paymentStatus; } set { paymentStatus = value; } }
        public string PaymentType { get { return paymentType; } set { paymentType = value; } }
        public long FoodProductId { get { return foodProductId; } set { foodProductId = value; } }
        public long TotalOrders { get { return totalOrders; } set { totalOrders = value; } }
        public long TotalQtySales { get { return totalQtySales; } set { totalQtySales = value; } }
        public long TotalRevenue { get { return totalRevenue; } set { totalRevenue = value; } }
        public long PlaceOrderId { get { return placeOrderId; } set { placeOrderId = value; } }
        public string CreatedBy { get { return createdBy; } set { createdBy = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime CreatedAt { get { return createdAt; } set { createdAt = value; } }
        public string ModifiedBy { get { return modifiedBy; } set { modifiedBy = value; } }
        public string Address { get { return address; } set { address = value; } }
        public DateTime ModifiedAt { get { return modifiedAt; } set { modifiedAt = value; } }

    }
}
