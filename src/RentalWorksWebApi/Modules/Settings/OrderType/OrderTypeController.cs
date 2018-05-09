using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace WebApi.Modules.Settings.OrderType
{
    [Route("api/v1/[controller]")]
    public class OrderTypeController : AppDataController
    {
        public OrderTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001/showfields
        [HttpGet("{id}/showfields")]
        public async Task<IActionResult> GetShowFieldsAsync([FromRoute]string id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderTypeLogic l = new OrderTypeLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                if (await l.LoadAsync<OrderTypeLogic>(ids))
                {

                    StringBuilder showFields = new StringBuilder();

                    if (l.CombineActivityTabs.Value)
                    {
                    showFields.Append("RecTypeDisplay,");
                    }

                    //rental
                    showFields.Append("RentalQuantityOrdered,"); 
                    if (l.RentalShowICode.Value) { showFields.Append("RentalICode,"); }
                    if (l.RentalShowDescription.Value) { showFields.Append("RentalDescription,"); }
                    if ((!l.RentalShowICode.Value) && (!l.RentalShowDescription.Value)) { showFields.Append("RentalICode,"); }
                    if (l.RentalShowFromDate.Value) { showFields.Append("RentalFromDate,"); }
                    if (l.RentalShowToDate.Value) { showFields.Append("RentalToDate,"); }
                    if (l.RentalShowBillablePeriods.Value) { showFields.Append("RentalBillablePeriods,"); }
                    if (l.RentalShowSubQuantity.Value) { showFields.Append("RentalSubQuantity,"); }
                    if (l.RentalShowAvailableQuantity.Value) { showFields.Append("RentalAvailableQuantity,"); }
                    if (l.RentalShowRate.Value) { showFields.Append("RentalRate,"); }
                    if (l.RentalShowDaysPerWeek.Value) { showFields.Append("RentalDaysPerWeek,"); }
                    if (l.RentalShowDiscountPercent.Value) { showFields.Append("RentalDiscountPercent,"); }
                    if (l.RentalShowPeriodDiscountAmount.Value) { showFields.Append("RentalPeriodDiscountAmount,"); }
                    if (l.RentalShowPeriodExtended.Value) { showFields.Append("RentalPeriodExtended,"); }
                    if (l.RentalShowTaxable.Value) { showFields.Append("RentalTaxable,"); }
                    if (l.RentalShowWarehouse.Value) { showFields.Append("RentalWarehouse,"); }
                    if (l.RentalShowReturnToWarehouse.Value) { showFields.Append("RentalReturnToWarehouse,"); }
                    if (l.RentalShowNotes.Value) { showFields.Append("RentalNotes,"); }

                    //sales
                    showFields.Append("SalesQuantityOrdered,");
                    if (l.SalesShowICode.Value) { showFields.Append("SalesICode,"); }
                    if (l.SalesShowDescription.Value) { showFields.Append("SalesDescription,"); }
                    if ((!l.SalesShowICode.Value) && (!l.SalesShowDescription.Value)) { showFields.Append("SalesICode,"); }
                    if (l.SalesShowPickDate.Value) { showFields.Append("SalesPickDate,"); }
                    if (l.SalesShowSubQuantity.Value) { showFields.Append("SalesSubQuantity,"); }
                    if (l.SalesShowAvailableQuantity.Value) { showFields.Append("SalesAvailableQuantity,"); }
                    if (l.SalesShowRate.Value) { showFields.Append("SalesRate,"); }
                    if (l.SalesShowDiscountPercent.Value) { showFields.Append("SalesDiscountPercent,"); }
                    if (l.SalesShowPeriodDiscountAmount.Value) { showFields.Append("SalesPeriodDiscountAmount,"); }
                    if (l.SalesShowPeriodExtended.Value) { showFields.Append("SalesPeriodExtended,"); }
                    if (l.SalesShowTaxable.Value) { showFields.Append("SalesTaxable,"); }
                    if (l.SalesShowWarehouse.Value) { showFields.Append("SalesWarehouse,"); }
                    if (l.SalesShowNotes.Value) { showFields.Append("SalesNotes,"); }

                    return new OkObjectResult(showFields);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeLogic l)
        {
            return await DoPostAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}