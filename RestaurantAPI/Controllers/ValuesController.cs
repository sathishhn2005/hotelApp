using System;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("[action]")]
        public ActionResult GetProducts(long OrgId)
        {
            long returnCode = -1;
            List<FoodProducts> lstProducts;
            try
            {
                returnCode = objBAL.GetMaster(OrgId, out lstProducts);
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

            long returnCode = -1;
            try
            {
                returnCode = objBAL.InsertCustOrder(out obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (returnCode > 0)
                return Ok(obj);

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
        public IActionResult Renew([FromBody]Renewal obj)
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

            long returnCode = -1;
            try
            {
                returnCode = objBAL.AdminRegister(obj);
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


            List<Admin> lst = new List<Admin>();
            try
            {
                lst = objBAL.GetSASubscription(SAName);
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
    }
}
