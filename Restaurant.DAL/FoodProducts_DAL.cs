﻿using MySql.Data.MySqlClient;
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
        public long GetFoodProducts(long OrgId, out List<FoodProducts> lstProducts)
        {
            long returnCode = -1;
            lstProducts = new List<FoodProducts>();

            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("OrgId", OrgId)
                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_GetFoodProducts");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstProducts = (from DataRow dr in ds.Tables[0].Rows
                                   select new FoodProducts()
                                   {
                                       CompanyID = Convert.ToInt32(dr["CompanyID"]),
                                       FoodID = Convert.ToInt64(dr["FoodID"]),
                                       FoodName = dr["FoodName"].ToString(),
                                       CategoryID = Convert.ToInt32(dr["CategoryID"]),
                                       CategoryName = dr["CategoryName"].ToString(),
                                       Price = Convert.ToInt64(dr["Price"]),
                                       Description = dr["Description"].ToString(),
                                       TaxPercent = Convert.ToDecimal(dr["Planning"])

                                   }).ToList();
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
                    new MySqlParameter("name",objInsertUser.Name),
                    new MySqlParameter("user_name",objInsertUser.UserName),
                    new MySqlParameter("password", objInsertUser.Password),
                    new MySqlParameter("confirmpassword", objInsertUser.ConfirmPassword),
                    new MySqlParameter("phonenumber", objInsertUser.PhoneNumber)
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

        public long InsertFoodProducts(FoodProducts objFoodProducts)
        {
            long returnCode = -1;

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompanyID",objFoodProducts.CompanyID),
                    new MySqlParameter("Price",objFoodProducts.Price),
                    new MySqlParameter("FoodName",objFoodProducts.FoodName),
                    new MySqlParameter("CategoryName", objFoodProducts.CategoryName),
                    new MySqlParameter("Description", objFoodProducts.Description),
                    new MySqlParameter("ImageSource", objFoodProducts.ImageSource),
                };

                var output = sqlHelper.executeSP<int>(parameters, "SP_InsertFoodProducts");
                returnCode = Convert.ToInt64(output);
                return returnCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustRegister> InsertCust(CustRegister objInsertCust)
        {
            List<CustRegister> lst = new List<CustRegister>();

            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    new MySqlParameter("CompanyID",objInsertCust.CompanyID),
                    new MySqlParameter("OTP",objInsertCust.OTP),
                    new MySqlParameter("SerialNo",objInsertCust.SerialNo),
                    new MySqlParameter("Name", objInsertCust.Name),
                    new MySqlParameter("PhoneNumber", objInsertCust.PhoneNumber),
                    new MySqlParameter("Address", objInsertCust.Address),
                    new MySqlParameter("DOB", objInsertCust.DOB),
                };

                var output = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertCustomer");
                if (output.Tables[0].Rows.Count > 0)
                {
                    lst = (from DataRow dr in output.Tables[0].Rows
                           select new CustRegister()
                           {

                               CompanyID = (long)dr["CompanyId"],
                               CustomerId = (long)dr["CustomerId"],
                               OTP = (long)dr["OTP"],
                               PhoneNumber = dr["PhoneNumber"].ToString(),
                               SerialNo = dr["SerialNo"].ToString(),
                               Name = dr["Name"].ToString(),
                           }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public long InsertCustOrder(out List<Cart> lstCustOrders)
        {
            long returnCode = -1;


            try
            {
                DataSet ds = new DataSet();
                List<MySqlParameter> parameters = new List<MySqlParameter>
                {
                    //convert to datatable
                };

                ds = sqlHelper.executeSP<DataSet>(parameters, "SP_InsertCustCart");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstCustOrders = new List<Cart>();
                    lstCustOrders = (from DataRow dr in ds.Tables[0].Rows
                                     select new Cart()
                                     {
                                         CompanyID = Convert.ToInt32(dr["CompanyID"]),
                                         CustomerID = Convert.ToInt64(dr["CustomerID"]),
                                         FoodID = Convert.ToInt64(dr["FoodID"]),
                                         TotalAmount = Convert.ToInt64(dr["TotalAmount"]),
                                         Discount = Convert.ToInt32(dr["Discount"]),
                                         UnitPrice = Convert.ToInt32(dr["UnitPrice"]),
                                         Quantity = Convert.ToInt32(dr["Quantity"]),
                                         Tax = Convert.ToInt32(dr["Tax"]),
                                         TableNo = Convert.ToInt32(dr["TableNo"]),

                                     }).ToList();
                    returnCode = 1;
                }
                else
                    lstCustOrders = new List<Cart>();
                return returnCode;
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
                    new MySqlParameter("CompanyId",objTax.CompanyID),
                    new MySqlParameter("Tax",objTax.Tax),
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
                    new MySqlParameter("CompanyId",objRenewal.CompanyID),
                    new MySqlParameter("StartDate",objRenewal.StartDate),
                    new MySqlParameter("EndDate",objRenewal.EndDate),
                    new MySqlParameter("TotalDays",objRenewal.TotalDays),
                    new MySqlParameter("Price",objRenewal.Price),
                    new MySqlParameter("Tax",objRenewal.Tax),
                    new MySqlParameter("TotalAmount",objRenewal.TotalAmount),
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
                               PhoneNumber = dr["PhoneNumber"].ToString(),
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
    }
}
