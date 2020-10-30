using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Billing.ProcessCreditCard
{
    //[FwSqlTable("loadercte")]
    [FwSqlTable("processcreditcardloadview")]
    public class ProcessCreditCardLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ProcessCreditCardLoader()
        {
            /*
            this.Cte.AppendLine("loadercte as (");
            this.Cte.AppendLine("  select");
            this.Cte.AppendLine("    title = 'Process Credit Card: ' + orderno,");
            this.Cte.AppendLine("    pccv.*");
            this.Cte.AppendLine("  from processcreditcardloadview pccv with (nolock)");
            this.Cte.AppendLine(")");
            */
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            
            // Get One
            if (request == null) {
                // filter by orderid
                select.AddWhere("orderid = @orderid");
                select.AddParameter("@orderid", this.OrderId);  
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
