using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Logic;



/*
QuickBooks IIF file for Invoice
----------------------------------------------
!CUST	NAME	BADDR1	BADDR2	BADDR3	PHONE1	
{{#each Customers}}
CUST	{{CustomerName}}	{{Address1}}	{{Address2}}	{{City}}, {{State}} {{ZipCode}}	{{Phone}}
{{/each}}
!TRNS	TRNSTYPE	DATE	TERMS	ACCNT	NAME	AMOUNT	DOCNUM	INVMEMO	ADDR1	ADDR2	ADDR3	ADDR4	ADDR5	PONUM	MEMO	CLASS	NAMEISTAXABLE	DUEDATE  
!SPL	TRNSTYPE	DATE	ACCNT	NAME	AMOUNT	DOCNUM	MEMO	PRICE	QNTY	INVITEM	TAXABLE	CLASS	EXTRA  
!ENDTRNS
{{#each Invoices}}
TRNS	{{InvoiceTypeForQuickBooks}}	{{InvoiceDate}}	{{PaymentTerms}}	{{AccountsReceivableAccountNumber}}	"{{Customer}}"	{{InvoiceTotal}}	{{InvoiceNumber}}	{{../BatchNumber}}	"{{Customer}}"	"{{BillToAttention}}"	"{{BillToAddress1}}"	"{{BillToAddress2}}"	"{{BillToCity}}	 {{BillToState}} {{BillToZip}}"	{{PurchaseOrderNumber}}	{{../BatchNumber}}	{{InvoiceClass}}	Y	{{InvoiceDueDate}}
{{#each Items}}
SPL	{{../InvoiceTypeForQuickBooks}}	{{../InvoiceDate}}	{{IncomeAccountNumber}}	"{{../Customer}}"	{{ExtendedNegative}}	{{../InvoiceNumber}}	"{{DescriptionWithoutDoubleQuotes}}"	{{Rate}}	{{QuantityNegative}}	{{ICode}}	{{TaxableYN}}	{{../InvoiceClass}}	
{{/each}}
{{#each Taxes}}
SPL	{{../InvoiceTypeForQuickBooks}}	{{../InvoiceDate}}	{{TaxAccountNumber1}}	"{{Vendor}}"	{{../InvoiceTaxNegative}}	{{../InvoiceNumber}}	"{{Description}}"	{{RentalTaxRate1}} %	-1	"{{Code}}"	N		AUTOSTAX
{{/each}}
ENDTRNS
{{/each}}
*/

/*
QuickBooks IIF file for Receipt
----------------------------------------------
!TRNS	TRNSTYPE	PAYMETH	DATE	ACCNT	NAME	AMOUNT	DOCNUM	MEMO	CLEAR
!SPL	TRNSTYPE	DATE	ACCNT	NAME	AMOUNT	DOCNUM
!ENDTRNS
{{#each Receipts}}
TRNS	PAYMENT	{{PaymentType}}	{{ReceiptDate}}	{{UndepositedFundsAccountNumber}}	{{Customer}}	{{Amount}}	{{CheckNumber}}	{{ReceiptNote}}	N
SPL	PAYMENT	{{ReceiptDate}}	{{AccountsReceivableAccountNumber}}	{{Customer}}	{{AmountNegative}}	{{CheckNumber}}	F
ENDTRNS
{{/each}}
*/


namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "GI2FxKtrjja1")]
    public class InvoiceBatchExportController : AppExportController
    {
        public InvoiceBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InvoiceBatchExportLoader); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatchexport/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "gfekPjE6qLe0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<InvoiceBatchExportResponse>> Export([FromBody]InvoiceBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceBatchExportLoader data = new InvoiceBatchExportLoader();
                data.SetDependencies(this.AppConfig, this.UserSession);
                await data.DoLoad<InvoiceBatchExportLoader>(request);
                string[] exportSettings = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", new string[] { "dataexportformatid" }, new string[] { request.DataExportFormatId }, new string[] { "exportstring", "filename" });
                AppExportResponse response = Export<InvoiceBatchExportLoader>(data, exportSettings[0], exportSettings[1]);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
