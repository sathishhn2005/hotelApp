using Restaurant.BusinessEntities;
using Restaurant.DAL;
using System;
using System.Transactions;
using System.Collections.Generic;

namespace Restaurant.BAL
{
    public class Products_BL
    {
        FoodProducts_DAL objDAL = new FoodProducts_DAL();
        public long GetMaster(long CompanyID, out List<FoodProducts> lstFProducts)
        {
            long returnCode = -1;

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    returnCode = objDAL.GetFoodProducts(CompanyID, out lstFProducts);
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

        public long InsertCustOrder(out List<Cart> obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.InsertCustOrder(out obj);
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
        public List<CustRegister> InsertCustomer(CustRegister obj)
        {
            List<CustRegister> lst = new List<CustRegister>();

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                lst = objDAL.InsertCust(obj);
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
        
        public long InsertProducts(FoodProducts obj)
        {
            long returnCode = -1;

            using TransactionScope transactionScope = new TransactionScope();
            try
            {
                returnCode = objDAL.InsertFoodProducts(obj);
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


    }
}
