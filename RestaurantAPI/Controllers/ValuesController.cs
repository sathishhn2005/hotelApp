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
        public IActionResult PostCustomerAndOrder([FromBody] LoginSignUp obj)
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
            if (returnCode > 0)
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
        public IActionResult InsertFoodProducts([FromBody] FoodProducts obj)
        {

            long returnCode = -1;
            try
            {
                returnCode = objBAL.InsertProducts(obj);
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
        public IActionResult Renewal(Renewal obj)
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

    }
}
