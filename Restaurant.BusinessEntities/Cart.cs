using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class Cart
    {
        private long billingID = 0;
        private long companyID = 0;
        private long customerID = 0;
        private long foodID = 0;
        private long totalAmount = 0;
        private int discount = 0;
        private int unitPrice = 0;
        private int quantity = 0;
        private int tax = 0;
        private int tableNo = 0;

        public long BillingID { get { return billingID; } set { billingID = value; } }
        public long CompanyID { get { return companyID; } set { companyID = value; } }
        public long CustomerID { get { return customerID; } set { customerID = value; } }
        public long FoodID { get { return foodID; } set { foodID = value; } }
        public long TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public int Discount { get { return discount; } set { discount = value; } }
        public int UnitPrice { get { return unitPrice; } set { unitPrice = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public int Tax { get { return tax; } set { tax = value; } }
        public int TableNo { get { return tableNo; } set { tableNo = value; } }


    }
}
