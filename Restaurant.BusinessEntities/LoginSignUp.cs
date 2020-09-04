using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BusinessEntities
{
    public class LoginSignUp
    {

        private string name = String.Empty;
        private string phoneNumber = String.Empty;
        private string userName = String.Empty;
        private string password = String.Empty;
        private string confirmPassword = String.Empty;
        private long otp = 0;
        private long user_Id = 0;



        /// <summary>
        /// Gets or sets the IsDiscountable value.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; }
        }
        public long OTP
        {
            get { return otp; }
            set { otp = value; }
        }

        public long User_Id
        {
            get { return user_Id; }
            set { user_Id = value; }
        }
    }
}
