using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    
    public class Admin
    {
      
        private long adminId = 0;
        private long companyID = 0;
        private long cost = 0;
        private long adminSubscriptionId = 0;
        private string phoneNumber = string.Empty;
        private string address = string.Empty;
        private string userName = string.Empty;
        private string password = string.Empty;
        private string companyName = string.Empty;
        private string adminName = string.Empty;
        private string customerName = string.Empty;
        private string drCr = string.Empty;
        private string payments = string.Empty;
        private long costPerDay = 0;
        private string status = string.Empty;
        private DateTime accRegisteredDate = DateTime.MaxValue;
        private DateTime startDate = DateTime.MaxValue;
        private DateTime endDate = DateTime.MaxValue;
        private int totalDays = 0;
        private long price = 0;
        private long totalAmount = 0;
        private int tax = 0;
        private string paymentType = string.Empty;
        private bool pause;
        
        private string paymentStatus = string.Empty;
        private string comments = string.Empty;

        public long AdminId { get { return adminId; } set { adminId = value; } }
        public long CompanyID { get { return companyID; } set { companyID = value; } }
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        public string AdminName { get { return adminName; } set { adminName = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public string Password { get { return password; } set { password = value; } }
        public DateTime AccRegisteredDate { get { return accRegisteredDate; } set { accRegisteredDate = value; } }
        public long Cost { get { return cost; } set { cost = value; } }
        public string Status { get { return status; } set { status = value; } }
        public long AdminSubscriptionId { get { return adminSubscriptionId; } set { adminSubscriptionId = value; } }
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public int TotalDays { get { return totalDays; } set { totalDays = value; } }
        public long Price { get { return price; } set { price = value; } }
        public int Tax { get { return tax; } set { tax = value; } }
        public long TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public string PaymentType { get { return paymentType; } set { paymentType = value; } }
        public string PaymentStatus { get { return paymentStatus; } set { paymentStatus = value; } }
        public string Comments { get { return comments; } set { comments = value; } }
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        public string DrCr { get { return drCr; } set { drCr = value; } }
        public string Payments { get { return payments; } set { payments = value; } }
        public long CostPerDay { get { return costPerDay; } set { costPerDay = value; } }
       
        public bool Pause { get => pause; set => pause = value; }
    }
}
