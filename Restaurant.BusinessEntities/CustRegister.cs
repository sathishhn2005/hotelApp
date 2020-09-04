using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class CustRegister
    {

        private string name = String.Empty;
        private long companyID = 0;
        private long customerId = 0;
        private string details = string.Empty;
        
        
        
        private string serialNo = String.Empty;
        private string address = String.Empty;
        private string phoneNumber = String.Empty;
        private string dOB = string.Empty;

        private long otp = 0;
        public long CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        /// <summary>
        /// Gets or sets the CompanyID value.
        /// </summary>
        public long CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        /// <summary>
        /// Gets or sets the DOB value.
        /// </summary>
        public string DOB
        {
            get { return dOB; }
            set { dOB = value; }
        }


        /// <summary>
        /// Gets or sets the IsDiscountable value.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the SerialNo value.
        /// </summary>
        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
        /// <summary>
        /// Gets or sets the Address value.
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
       
        public long OTP
        {
            get { return otp; }
            set { otp = value; }
        }

        public string Details
        {
            get { return details; }
            set { details = value; }
        }
      
        


    }
}
