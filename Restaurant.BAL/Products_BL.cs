using Restaurant.BusinessEntities;
using Restaurant.DAL;
using System;
using System.Transactions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using Razorpay.Api;
using System.ComponentModel;

namespace Restaurant.BAL
{
    public class Products_BL
    {
        FoodProducts_DAL objDAL = new FoodProducts_DAL();
        public long GetMaster(List<CustRegister> obj, out List<FoodProducts> lstFProducts)
        {
            long returnCode = -1;

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    returnCode = objDAL.GetFoodProducts(obj, out lstFProducts, obj[0].CompanyID);
                    transactionScope.Complete();
                    transactionScope.Dispose();

                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }

                return returnCode;
            }
        }
        public long GetMasterAdmin(long CompId, out List<FoodProducts> lstFProducts)
        {
            long returnCode = -1;

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    returnCode = objDAL.GetFoodProductAdmin(CompId, out lstFProducts);
                    transactionScope.Complete();
                    transactionScope.Dispose();

                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }

                return returnCode;
            }
        }
        public long InsertUser(LoginSignUp obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.InsertUser_SuperAdmin(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long UpdatePswdSuperAdmin(LoginSignUp obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.UpdatePswdSA(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }

        public List<Cart> InsertCustOrder(List<Cart> obj)
        {
            List<Cart> lst = new List<Cart>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.InsertCustOrder(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        private static string ReceiptGeneration()
        {
            int randval;
            string n;
            StringBuilder op = new StringBuilder();
            RandomNumberGenerator obj = RandomNumberGenerator.Create();
            byte[] val = new byte[4];
            obj.GetBytes(val);
            randval = BitConverter.ToInt32(val, 0);
            if (randval < 0)
            {
                randval *= -1;
            }
            n = randval.ToString();
            int i = n.Length - 1;
            int c = 1;
            while (c <= 6)
            {
                op.Append(n[i]);
                c += 1;
                i -= 1;
            }
            obj.Dispose();
            return op.ToString();
        }
        public List<Billing> PlaceOrder(List<Billing> obj)
        {
            List<Billing> lst = new List<Billing>();
            long TotalAmt = 0;
            string OrderId = string.Empty;
            string Receipt = string.Empty;
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                foreach (var item in obj)
                {
                    TotalAmt += item.TotalAmount;
                }
                Receipt = ReceiptGeneration();
                if (obj[0].PaymentType.Equals("ONLINE"))
                {


                    Dictionary<string, object> input = new Dictionary<string, object>
                    {
                        { "amount",TotalAmt},
                        { "currency", "INR" },
                        { "receipt",Receipt },
                        { "payment_capture", 1 }
                    };

                    objDAL.GetRazorPayKeys(obj[0].CompanyID, out string key, out string secret);
                    RazorpayClient client = new RazorpayClient(key, secret);

                    Order order = client.Order.Create(input);

                    OrderId = order["id"].ToString();

                    lst = objDAL.SavePlaceOrder(obj, OrderId);
                }
                else
                {
                    lst = objDAL.SavePlaceOrder(obj, Receipt);
                }
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }

        public List<CustRegister> InsertCustomer(CustRegister obj)
        {
            List<CustRegister> lst = new List<CustRegister>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {


                if (!string.IsNullOrEmpty(obj.PhoneNumber))
                {
                    string MobileNumber = obj.PhoneNumber;
                    string url = "http://2factor.in/API/V1/048ac9fe-e85a-11ea-9fa5-0200cd936042/SMS/" + MobileNumber + '/' + "AUTOGEN/HotelApp";
                    var responseString = JObject.Parse(Response(url));
                    string Status = responseString["Status"].ToString();
                    string Details = responseString["Details"].ToString();
                    obj.Details = Details;
                    string[] response = { Status, Details };
                }
                lst = objDAL.InsertCust(obj);
                lst[0].Details = obj.Details;
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public string VerifyOTP(string Details, long EnteredOTP)
        {
            string Status = string.Empty;
            string responseString = string.Empty;
            try
            {
                string url = "http://2factor.in/API/V1/048ac9fe-e85a-11ea-9fa5-0200cd936042/SMS/" + "VERIFY/" + Details + '/' + EnteredOTP;
                responseString = Response(url);
                if (!string.IsNullOrEmpty(responseString))
                {
                    var res = JObject.Parse(responseString);
                    Status = res["Status"].ToString();
                    Details = res["Details"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Status;
        }
        private string Response(string URL)
        {
            string responseString = string.Empty;
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(URL);
                httpWReq.Method = "GET";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                if (response != null)
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                responseString = "";

            }
            return responseString;
        }

        public List<FoodProducts> InsertProducts(List<FoodProducts> lstFP)
        {
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lstFoodProducts = objDAL.InsertFoodProducts(lstFP);
                //var model = JsonConvert.DeserializeObject(obj);

                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lstFoodProducts;
        }
        public List<FoodProducts> BannerUpload(List<FoodProducts> lstFP)
        {
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lstFoodProducts = objDAL.UploadBanner(lstFP);
                //var model = JsonConvert.DeserializeObject(obj);

                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lstFoodProducts;
        }
        public long TaxUpdate(Cart obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.UpdateTax(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long RenewalSubscription(Renewal obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                obj.StartDate = Convert.ToDateTime(obj.Sdate);
                obj.EndDate = Convert.ToDateTime(obj.Edate);
                // obj.StartDate = obj.EndDate;
                returnCode = objDAL.Renewal(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public List<Billing> CompanyBillingDetails(long CompanyId)
        {
            List<Billing> lst = new List<Billing>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.GetBillingDetails(CompanyId);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public List<Admin> IsUserExists(LoginSignUp obj)
        {
            List<Admin> lst = new List<Admin>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.IsUserExists(obj);
                //  lst[0].TotalDays = Convert.ToInt32((lst[0].EndDate - lst[0].StartDate).TotalDays);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public List<Admin> AdminRegister(Admin obj)
        {
            List<Admin> lst = new List<Admin>();
            obj.StartDate = Convert.ToDateTime(obj.SDate);
            obj.EndDate = Convert.ToDateTime(obj.EDate);
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.RegisterAdmin(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public long UpdateSubscription(Admin obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.UpdateSubAdmin(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public List<Admin> GetSASubscription(string strname, int flag)
        {
            List<Admin> lst = new List<Admin>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.GetSubscriptionSA(strname, flag);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public long SAUserExists(LoginSignUp obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.IsSuperAdminExists(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long GetOrderedDetails(long CompnId, int flag, out List<Billing> lstOrderedDetails, out Billing obj)
        {
            long returnCode = -1;
            obj = new Billing();
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.GetOrderDetails(CompnId, flag, out lstOrderedDetails);

                if (lstOrderedDetails.Count > 0)
                {
                    foreach (var item in lstOrderedDetails)
                    {
                        obj.TotalOrders = lstOrderedDetails.Count;
                        obj.TotalQtySales += item.Quantity;
                        obj.TotalRevenue += item.UnitPrice * item.Quantity ;
                    }

                }
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long SubPause(Admin obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.PauseSub(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public List<FoodProducts> GetBanners(long CompanyId)
        {
            List<FoodProducts> lst = new List<FoodProducts>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.GetBanners(CompanyId);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return lst;
        }
        public long GetPaymentHist(out List<Admin> lst, long CompanyId)
        {
            long returnCode = -1;
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.GetPaymentHistory(out lst, CompanyId);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long UpdatePHis(Admin obj)
        {
            long returnCode = -1;
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.UpdatePaymentHis(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        public long UpdatePlaceOrder(Billing obj)
        {
            long returnCode = -1;
            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.UpdatePO(obj);
                transactionScope.Complete();
                transactionScope.Dispose();

            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw ex;
            }

            return returnCode;
        }
        
    }
}
