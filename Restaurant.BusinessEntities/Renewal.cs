using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class Renewal
    {
        
        private long companyID = 0;
        private DateTime startDate = DateTime.MaxValue;
        private DateTime endDate = DateTime.MaxValue;
        private int totalDays = 0;
        private int tax = 0;
        private long totalAmount = 0;
        private long price = 0;

        /// <summary>
        /// Gets or sets the IsDiscountable value.
        /// </summary>
       
        public int Tax
        {
            get { return tax; }
            set { tax = value; }
        }
        public long Price
        {
            get { return price; }
            set { price = value; }
        }
        public long CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        public long TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public int TotalDays
        {
            get { return totalDays; }
            set { totalDays = value; }
        }



    }
}
