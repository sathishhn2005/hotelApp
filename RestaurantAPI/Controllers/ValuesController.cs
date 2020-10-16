using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Restaurant.BAL;
using Restaurant.BusinessEntities;
using Restaurant.Utilty;



namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private SQLHelper _myClass;
        public ValuesController(SQLHelper myClass)
        {
            _myClass = myClass;
        }


        Products_BL objBAL = new Products_BL();

        [HttpPost]
        [Route("[action]")]
        public IActionResult SuperAdminInsert([FromBody] LoginSignUp obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.InsertUser(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode >= 0)
                return Ok(returnCode);

            else
                return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateSAPassword([FromBody] LoginSignUp obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.UpdatePswdSuperAdmin(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(returnCode);

            else
                return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertCust([FromBody] CustRegister obj)
        {
            _ = new List<CustRegister>();
            List<CustRegister> lst;
            try
            {
                lst = objBAL.InsertCustomer(obj);
                //var client = new RestClient("http://2factor.in/API/V1/293832-67745-11e5-88de-5600000c6b13/SMS/991991199/AUTOGEN");
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("content-type", "application/x-www-form-urlencoded");
                //IRestResponse response = client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertCustCart([FromBody] List<Cart> obj)
        {

            List<Cart> lst = new List<Cart>();
            try
            {
                lst = objBAL.InsertCustOrder(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertFoodProducts([FromBody] List<FoodProducts> obj)
        {
            // JavaScriptSerializer js = new JavaScriptSerializer().Deserialize<FoodProducts>(obj);
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            try
            {
                lstFoodProducts = objBAL.InsertProducts(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lstFoodProducts.Count > 0)
                return Ok(lstFoodProducts);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateTax(Cart obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.TaxUpdate(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(returnCode);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Renew([FromBody] Renewal obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.RenewalSubscription(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(returnCode);

            else
                return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult BillingDetails(long CompanyId)
        {

            List<Billing> lst = new List<Billing>();
            try
            {
                lst = objBAL.CompanyBillingDetails(CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult IsUserExists(LoginSignUp obj)
        {

            List<Admin> lst = new List<Admin>();
            try
            {
                if (!string.IsNullOrEmpty(obj.UserName) && !string.IsNullOrEmpty(obj.Password))
                    lst = objBAL.IsUserExists(obj);
                else
                    return NotFound("Username & Password not entered");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult AdminReg([FromBody] Admin obj)
        {
            List<Admin> lst = new List<Admin>();
            try
            {
                lst = objBAL.AdminRegister(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
            //return Ok(obj);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult SubAdminUpdate([FromBody] Admin obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.UpdateSubscription(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(returnCode);

            else
                return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetSubSA(string SAName)
        {
            List<Admin> lstSubscriptionInfo = new List<Admin>();
            List<Admin> lst = new List<Admin>();
            long returnCode = 1;
            object[] arr = new object[2];
            try
            {
                lstSubscriptionInfo = objBAL.GetSASubscription(SAName, 1);
                lst = objBAL.GetSASubscription(SAName, 2);
                arr[0] = lstSubscriptionInfo;
                arr[1] = lst;
                returnCode = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(arr);

            else
                return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult OTPVerification([FromBody] CustRegister obj)
        {

            if (!string.IsNullOrEmpty(obj.Details) && obj.OTP > 0)
            {
                string Details = obj.Details;
                long EnteredOTP = obj.OTP;
                string response = objBAL.VerifyOTP(Details, EnteredOTP);
                if (response.Length > 0)
                    return Ok(response);
                else
                    return BadRequest();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult IsSAUserExists([FromBody] LoginSignUp obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.SAUserExists(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode >= 0)
                return Ok(returnCode);

            else
                return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetAllProducts([FromBody] List<CustRegister> obj)
        {
            List<FoodProducts> lstProducts = new List<FoodProducts>();
            long returnCode = -1;
            object[] arr = new object[2];
            try
            {
                returnCode = objBAL.GetMaster(obj, out lstProducts);
                arr[0] = obj;
                arr[1] = lstProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(arr);

            else
                return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllProductsAdmin(long CompId)
        {
            List<FoodProducts> lstProducts = new List<FoodProducts>();
            long returnCode = -1;
            try
            {
                returnCode = objBAL.GetMasterAdmin(CompId, out lstProducts);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(lstProducts);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult PlaceOrder([FromBody] List<Billing> objj)
        {
            List<Billing> lstPlaceOrder = new List<Billing>();

            try
            {
                lstPlaceOrder = objBAL.PlaceOrder(objj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lstPlaceOrder.Count > 0)
                return Ok(lstPlaceOrder);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetOrderedDetails([FromBody] Billing obj)
        {
            try
            {
                objBAL.GetOrderedDetails(obj.CompanyID, obj.Flag, out List<Billing> lstOrderedDetails, out Billing objj);
                if (obj.Flag.Equals(1))
                {
                    object[] arr = new object[2];

                    arr[0] = lstOrderedDetails;
                    arr[1] = objj;

                    if (lstOrderedDetails.Count > 0)
                        return Ok(arr);

                    else
                        return NotFound();
                }
                else
                {
                    if (lstOrderedDetails.Count > 0)
                        return Ok(lstOrderedDetails);

                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult SubscriptionPause([FromBody] Admin obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.SubPause(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode >= 0)
                return Ok(returnCode);

            else
                return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UploadBanner([FromBody] List<FoodProducts> obj)
        {
            // JavaScriptSerializer js = new JavaScriptSerializer().Deserialize<FoodProducts>(obj);
            List<FoodProducts> lstFoodProducts = new List<FoodProducts>();

            try
            {
                lstFoodProducts = objBAL.BannerUpload(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lstFoodProducts.Count > 0)
                return Ok(lstFoodProducts);

            else
                return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetBanner(long CompanyId)
        {

            List<FoodProducts> lst = new List<FoodProducts>();
            try
            {
                lst = objBAL.GetBanners(CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetHistoryPayment(long CompanyId)
        {

            List<Admin> lst = new List<Admin>();
            try
            {
                objBAL.GetPaymentHist(out lst, CompanyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lst.Count > 0)
                return Ok(lst);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdatePaymntHis(Admin obj)
        {
            long returnCode = -1;
            try
            {
                returnCode = objBAL.UpdatePHis(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(returnCode);

            else
                return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateOrderDetails([FromBody] Billing obj)
        {
            try
            {
                long returnCode = objBAL.UpdatePlaceOrder(obj);
                objBAL.GetOrderedDetails(obj.CompanyID, obj.Flag, out List<Billing> lstOrderedDetails, out Billing objj);
                if (obj.Flag.Equals(1))
                {
                    object[] arr = new object[2];

                    arr[0] = lstOrderedDetails;
                    arr[1] = objj;

                    if (lstOrderedDetails.Count > 0)
                        return Ok(arr);

                    else
                        return NotFound();
                }
                else
                {
                    if (lstOrderedDetails.Count > 0)
                        return Ok(lstOrderedDetails);

                    else
                        return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
