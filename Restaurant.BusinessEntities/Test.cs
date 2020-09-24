using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class Test
    {

        private long foodProductId = 0;

        private string foodName = string.Empty;
        private long companyID = 0;
        private long customerId = 0;
        private long totalAmount = 0;

        private int quantity = 0;
        private string phoneNumber = string.Empty;
        private int tableNo = 0;
        private string customerName = string.Empty;


        public long CompanyID { get { return companyID; } set { companyID = value; } }
        public long CustomerId { get { return customerId; } set { customerId = value; } }
        public long TotalAmount { get { return totalAmount; } set { totalAmount = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string FoodName { get { return foodName; } set { foodName = value; } }

        public int TableNo { get { return tableNo; } set { tableNo = value; } }
        public string CustomerName { get { return customerName; } set { customerName = value; } }
        

        public long FoodProductId { get { return foodProductId; } set { foodProductId = value; } }
        
    }
}
