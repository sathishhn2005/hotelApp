using MySql.Data.MySqlClient;
using Newtonsoft.Json.Converters;
using Restaurant.BusinessEntities;
using Restaurant.Utilty;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Restaurant.DAL
{
    public class FoodProducts_DAL
    {
        SQLHelper sqlHelper = new SQLHelper();
        public long GetFoodProducts(List<CustRegister> obj, out List<FoodProducts> lstProducts, long CompanyID)
        {
            long returnCode = -1;
            //  CompanyID = 1;
            lstProducts = new List<FoodProducts>();


            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompnID", CompanyID)
                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetFoodProducts");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(ds.Tables[0], out lstProducts);
                    returnCode = 1;
                }
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long InsertUser_SuperAdmin(LoginSignUp objInsertUser)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("SAName",objInsertUser.Name),
                    new MySqlParameter("UserName",objInsertUser.UserName),
                    new MySqlParameter("Pswd", objInsertUser.Password),
                    new MySqlParameter("ConfirmPswd", objInsertUser.ConfirmPassword),
                    new MySqlParameter("PhNo", objInsertUser.PhoneNumber)
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_InsertUsers_SuperAdmin");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long UpdatePswdSA(LoginSignUp objInsertUser)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("user_name",objInsertUser.UserName),
                    new MySqlParameter("password", objInsertUser.Password),
                    new MySqlParameter("confirmpassword", objInsertUser.ConfirmPassword),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_Update_SAPassword");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FoodProducts> InsertFoodProducts(List<FoodProducts> lstFP)
        {
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            long CompId = lstFP[0].CompanyID;
            if (CompId > 0)
            {
                List<MySqlParameter> param = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",CompId),

                };
                var deletePut = sqlHelper.executeSP<DataSet>(param, "SP_DeleteFoodProducts");
                try
                {
                    foreach (var item in lstFP)
                    {
                        List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",item.CompanyID),
                    new MySqlParameter("PriceAmt",item.Price),
                    new MySqlParameter("Foodnam",item.FoodName),
                    new MySqlParameter("CatgryName", item.CategoryName),
                    new MySqlParameter("Descr", item.Description),
                    new MySqlParameter("ImageSou", item.ImageSource),
                    new MySqlParameter("ImgSourceType", item.Type),

                };

                        DataSet output = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertFoodProducts");
                        if (output.Tables[0].Rows.Count > 0)
                        {
                            DTtoListConverter.ConvertTo(output.Tables[0], out lstFoodProducts);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return lstFoodProducts;
        }
        public List<CustRegister> InsertCust(CustRegister objInsertCust)
        {
            List<CustRegister> lst = new List<CustRegister>();

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",objInsertCust.CompanyID),
                    new MySqlParameter("OTP",objInsertCust.OTP),
                    new MySqlParameter("SerialNo",objInsertCust.SerialNo),
                    new MySqlParameter("Name", objInsertCust.Name),
                    new MySqlParameter("PhoneNumber", objInsertCust.PhoneNumber),
                    new MySqlParameter("Address", objInsertCust.Address),
                    new MySqlParameter("DOB", objInsertCust.DOB),
                };

                DataSet output = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertCustomer");
                if (output.Tables[0].Rows.Count > 0)
                {
                    //lst = (from DataRow dr in output.Tables[0].Rows
                    //       select new CustRegister()
                    //       {

                    //           CompanyID = (long)dr["CompanyId"],
                    //           CustomerId = (long)dr["CustomerId"],
                    //           OTP = (long)dr["OTP"],
                    //           PhoneNumber = dr["PhoneNumber"].ToString(),
                    //           SerialNo = dr["SerialNo"].ToString(),
                    //           Name = dr["Name"].ToString(),
                    //           Tax = (int)dr["Tax"],
                    //       }).ToList();
                    DTtoListConverter.ConvertTo(output.Tables[0], out lst);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public List<Cart> InsertCustOrder(List<Cart> lstCustOrders)
        {
            List<Cart> lst = new List<Cart>();

            try
            {
                DataSet ds = new DataSet();
                foreach (var item in lstCustOrders)
                {
                    List<MySqlParameter> parameters = new List<MySqlParameter>
                    {
                        new MySqlParameter("FoodProdid",item.FoodProductId),
                    new MySqlParameter("CompID",item.CompanyID),
                    new MySqlParameter("CustID",item.CustomerID),
                    new MySqlParameter("TotalAmt", item.TotalAmount),
                    new MySqlParameter("Discnt", item.Discount),
                    new MySqlParameter("U_Price", item.UnitPrice),
                    new MySqlParameter("Qty",item.Quantity),
                    new MySqlParameter("TaxPer",item.Tax),
                    new MySqlParameter("TableId",item.TableNo),
                    };

                    ds = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertCustCart");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lst = (from DataRow dr in ds.Tables[0].Rows
                               select new Cart()
                               {
                                   OrderDetailsId = Convert.ToInt64(dr["OrderDetailsId"]),
                                   CompanyID = Convert.ToInt32(dr["CompanyID"]),
                                   CustomerID = Convert.ToInt64(dr["CustomerID"]),
                                   FoodProductId = Convert.ToInt64(dr["FoodProductId"]),
                                   TotalAmount = Convert.ToInt64(dr["TotalAmount"]),
                                   Discount = Convert.ToInt32(dr["Discount"]),
                                   UnitPrice = Convert.ToInt32(dr["UnitPrice"]),
                                   Quantity = Convert.ToInt32(dr["Quantity"]),
                                   Tax = Convert.ToInt32(dr["Tax"]),
                                   TableNo = Convert.ToInt32(dr["TableNo"]),

                               }).ToList();

                    }
                    else
                        return lst;
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long UpdateTax(Cart objTax)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompnId",objTax.CompanyID),
                    new MySqlParameter("Taxpercent",objTax.Tax),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_UpdateTax");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long Renewal(Renewal objRenewal)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompnId",objRenewal.CompanyID),
                    new MySqlParameter("SDate",objRenewal.StartDate),
                    new MySqlParameter("EDate",objRenewal.EndDate),
                    new MySqlParameter("TotDays",objRenewal.TotalDays),
                    new MySqlParameter("PriceAmt",objRenewal.Price),
                    new MySqlParameter("TaxAmt",objRenewal.Tax),
                    new MySqlParameter("TotalAmt",objRenewal.TotalAmount),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_Renewal");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public List<Billing> GetBillingDetails(long CompanyId)
        {
            List<Billing> lst = new List<Billing>();

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompanyID",CompanyId),

                };

                var output = sqlHelper.executeSP<DataSet>(parameters, "SP_GetBillingDetails");
                if (output.Tables[0].Rows.Count > 0)
                {
                    lst = (from DataRow dr in output.Tables[0].Rows
                           select new Billing()
                           {

                               TableNo = (int)dr["CompanyId"],
                               CustomerName = dr["CustomerName"].ToString(),
                               CompanyID = (long)dr["CompanyID"],
                               CustomerId = (long)dr["CustomerId"],
                               CompanyName = dr["CompanyName"].ToString(),
                               PhoneNumber = (long)dr["PhoneNumber"],
                               OrderDetails = dr["OrderDetails"].ToString(),
                               TotalAmount = (long)dr["TotalAmount"],
                               Tax = (int)dr["Tax"],
                           }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public List<Admin> IsUserExists(LoginSignUp obj)
        {
            List<Admin> lst = new List<Admin>();

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("UserN",obj.UserName),
                    new MySqlParameter("Pswd",obj.Password),


                };

                DataSet ds = sqlHelper.executeSP<DataSet>(parameters, "SP_IsUserExits");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DTtoListConverter.ConvertTo(ds.Tables[0], out lst);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<Admin> RegisterAdmin(Admin item)
        {
            List<Admin> lst = new List<Admin>();
            DataSet ds = new DataSet();
            try
            {
                //int Tdays = Convert.ToInt32(item.EndDate - item.StartDate);
                TimeSpan difference = item.EndDate - item.StartDate;
                var Tdays = difference.TotalDays;
                string Status = string.Empty;
                if (item.Pause.Equals("false"))
                    Status = "InActive";
                else
                    Status = "Active";
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    
                    new MySqlParameter("Addre", item.Address),
                    new MySqlParameter("Commnt", item.Comments),
                    new MySqlParameter("CompName", item.CompanyName),
                    new MySqlParameter("CPDay", item.CostPerDay),
                    new MySqlParameter("CName", item.CustomerName),

                    new MySqlParameter("CompStatus", Status),
                    new MySqlParameter("PhNo", item.PhoneNumber),
                    new MySqlParameter("Pswd", item.Password),
                    new MySqlParameter("Uname", item.UserName),
                    new MySqlParameter("AccRegDate", DateTime.Now),

                    new MySqlParameter("Amt", item.TotalAmount),
                    new MySqlParameter("TotDays", Tdays),
                    new MySqlParameter("PStatus", item.DrCr),
                    new MySqlParameter("Edate", item.EndDate),
                    new MySqlParameter("Sdate", item.StartDate),

                    new MySqlParameter("Pausee", item.Pause),
                    new MySqlParameter("PType", item.Payments),
                    new MySqlParameter("CompId", item.CompanyID),


                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertAdmin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(ds.Tables[0], out lst);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public long UpdateSubAdmin(Admin obj)
        {
            long returnCode = -1;

            try
            {

                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompanyId",obj.CompanyID),
                    new MySqlParameter("AdminSubscriptionId ", obj.AdminSubscriptionId),
                    new MySqlParameter("PaymentType",obj.PaymentType),
                    new MySqlParameter("Comments", obj.Comments),
                    new MySqlParameter("TotalAmount", obj.TotalAmount),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_SubscriptionUpdate");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public List<Admin> GetSubscriptionSA(string SAUserName, int flag)
        {
            if (SAUserName == null)
                SAUserName = "";
            List<Admin> lst = new List<Admin>();
            DataSet ds = new DataSet();
            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("SAUserName",SAUserName),
                    new MySqlParameter("Flag",flag),

                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetSubscriptionSA");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(ds.Tables[0], out lst);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public long IsSuperAdminExists(LoginSignUp obj)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("UserN",obj.UserName),
                    new MySqlParameter("Pswd",obj.Password),


                };

                var output = sqlHelper.executeSP<DataSet>(parameters, "SP_IsSAUserExits");
                if (output.Tables[0].Rows.Count > 0)
                {
                    returnCode = 1;
                }
                else
                    returnCode = 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public List<Billing> SavePlaceOrder(List<Billing> lstCustOrders, string OrderId)
        {
            List<Billing> lst = new List<Billing>();

            try
            {
                DataSet ds = new DataSet();
                foreach (var item in lstCustOrders)
                {

                    List<MySqlParameter> parameters = new List<MySqlParameter>
                    {
                        new MySqlParameter("FoodProdid",item.FoodProductId),
                    new MySqlParameter("CompID",item.CompanyID),
                    new MySqlParameter("CustID",item.CustomerId),
                    new MySqlParameter("CustName",item.CustomerName),
                    new MySqlParameter("PhoneNo",item.PhoneNumber),

                    new MySqlParameter("TotalAmt", item.TotalAmount),
                    new MySqlParameter("Qty", item.Quantity),
                    new MySqlParameter("TaxPer",item.Tax),
                    new MySqlParameter("TableId",item.TableNo),
                    new MySqlParameter("PaymentType",item.PaymentType),
                    new MySqlParameter("OrderId",OrderId),
                    new MySqlParameter("Addr",item.Address),
                    new MySqlParameter("Cmnt",item.Comments??"No comments entered"),
                    

                    };

                    ds = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertPlaceOrder");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DTtoListConverter.ConvertTo(ds.Tables[0], out lst);

                    }
                    else
                        return lst;
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }

        public long GetOrderDetails(long CompId, int flag, out List<Billing> lstOrderedDetails)
        {

            long returnCode = -1;
            lstOrderedDetails = new List<Billing>();
            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                    {

                    new MySqlParameter("CompnID",CompId),
                    };
                if (flag.Equals(1))
                    ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetOrderDetails");
                else if (flag.Equals(2))
                    ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetBillingDetails");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(ds.Tables[0], out lstOrderedDetails);
                    return returnCode = 1;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public long GetFoodProductAdmin(long CompId, out List<FoodProducts> lstProducts)
        {
            long returnCode = -1;

            lstProducts = new List<FoodProducts>();
            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompnID", CompId)
                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetFoodProducts");
                if (ds.Tables[0].Rows.Count > 0)
                {


                    DTtoListConverter.ConvertTo(ds.Tables[0], out lstProducts);
                    returnCode = 1;
                }
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long GetRazorPayKeys(long CompId, out string key, out string secret)
        {

            long returnCode = 0;
            key = string.Empty;
            secret = string.Empty;
            List<Billing> lst = new List<Billing>();
            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                    {

                    new MySqlParameter("CompnID",CompId),
                    };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetRazorPayKey");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    key = ds.Tables[0].Rows[0]["keyId"].ToString();
                    secret = ds.Tables[0].Rows[0]["keySecret"].ToString();
                    //DTtoListConverter.ConvertTo(ds.Tables[0], out lstProducts);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public long PauseSub(Admin obj)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",obj.CompanyID),
                    new MySqlParameter("PauseStatus",obj.Status),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_PauseSubscription");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public List<FoodProducts> UploadBanner(List<FoodProducts> lstFP)
        {
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            long CompId = lstFP[0].CompanyID;
            List<MySqlParameter> paramss = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",CompId),

                };
            var deletePut = sqlHelper.executeSP<DataSet>(paramss, "SP_DeleteBanners");
            if (CompId > 0)
            {

                try
                {
                    foreach (var item in lstFP)
                    {
                        List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",item.CompanyID),
                    new MySqlParameter("ImageSou", item.ImageSource),
                };

                        DataSet output = sqlHelper.executeSP<DataSet>(parameters, "SP_UploadBanner");
                        if (output.Tables[0].Rows.Count > 0)
                        {
                            DTtoListConverter.ConvertTo(output.Tables[0], out lstFoodProducts);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return lstFoodProducts;
        }
        public List<FoodProducts> GetBanners(long CompanyId)
        {
            List<FoodProducts> lst = new List<FoodProducts>();

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompID",CompanyId),

                };

                DataSet output = sqlHelper.executeSP<DataSet>(parameters, "SP_GetBanner");
                if (output.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(output.Tables[0], out lst);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public long GetPaymentHistory(out List<Admin> lst, long CompanyID)
        {
            long returnCode = -1;
            
            lst = new List<Admin>();
            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompnID", CompanyID)
                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetSubPaymentHistory");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DTtoListConverter.ConvertTo(ds.Tables[0], out lst);
                    returnCode = 1;
                }
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long UpdatePaymentHis(Admin obj)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("PymntStatus",obj.PaymentStatus),
                    new MySqlParameter("ASHisId",obj.AdminSubscriptionHistoryId)
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_UpdatePaymentHistory");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
        public long UpdatePO(Billing obj)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("OrderId",obj.RazorOrderDetailsId),
                    new MySqlParameter("POStatus",obj.Status),

                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_UpdatePO");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return returnCode;
        }
    }
}
