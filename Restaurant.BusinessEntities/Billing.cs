using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class Billing
    {
        private long billingID = 0;
        private long companyID = 0;
        private long customerId = 0;
        private long totalAmount = 0;
        private int discount = 0;
        private int unitPrice = 0;
        private int quantity = 0;
        private string phoneNumber = string.Empty;
        private int tax = 0;
        private int tableNo = 0;
        private string customerName = string.Empty;
        private string companyName = string.Empty;
        private string orderDetails = string.Empty;
        private string comments = string.Empty;
        private string status = string.Empty;
        private DateTime dOB = DateTime.MaxValue;

        private string createdBy = String.Empty;
        private DateTime createdAt = DateTime.MaxValue;
        private string modifiedBy = String.Empty;
        private DateTime modifiedAt = DateTime.MaxValue;

        public long BillingID { get { return billingID; } set { billingID = value; } }
        public long CompanyID { get { return companyID; } set { companyID = value; } }
        public long CustomerId { get { return customerId; } set { customerId = value; } }
        public long TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public int Discount { get { return discount; } set { discount = value; } }
        public int UnitPrice { get { return unitPrice; } set { unitPrice = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public int Tax { get { return tax; } set { tax = value; } }
        public int TableNo { get { return tableNo; } set { tableNo = value; } }
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        public string OrderDetails { get { return orderDetails; } set { orderDetails = value; } }
        public string Comments { get { return comments; } set { comments = value; } }
        public string Status { get { return status; } set { status = value; } }
        public DateTime DOB { get { return dOB; } set { dOB = value; } }



        public string CreatedBy { get { return createdBy; } set { createdBy = value; } }
        public DateTime CreatedAt { get { return createdAt; } set { createdAt = value; } }
        public string ModifiedBy { get { return modifiedBy; } set { modifiedBy = value; } }
        public DateTime ModifiedAt { get { return modifiedAt; } set { modifiedAt = value; } }
    }
}
