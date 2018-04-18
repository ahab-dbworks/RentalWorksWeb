using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.Deal
{
    [FwSqlTable("dealview")]
    public class DealBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtypeid", modeltype: FwDataTypes.Text)]
        public string DealTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealtype", modeltype: FwDataTypes.Text)]
        public string DealType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string DefaultRate { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "securitydepositamt", modeltype: FwDataTypes.Decimal)]
        //public decimal? SecurityDepositAmount { get; set; }
        ////------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("CustomerId", "customerid", select, request);
            addFilterToSelect("LocationId", "locationid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}