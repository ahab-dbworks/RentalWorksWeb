using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Depreciation
{
    [FwSqlTable("depreciationview")]
    public class DepreciationLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? DepreciationId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationdate", modeltype: FwDataTypes.Date)]
        public string DepreciationDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasedate", modeltype: FwDataTypes.Date)]
        public string PurchaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "receivedate", modeltype: FwDataTypes.Date)]
        public string ReceiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasewarehouseid", modeltype: FwDataTypes.Text)]
        public string PurchaseWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasewhcode", modeltype: FwDataTypes.Text)]
        public string PurchaseWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchasewarehouse", modeltype: FwDataTypes.Text)]
        public string PurchaseWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglaccountid", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglno", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "debitglacctdesc", modeltype: FwDataTypes.Text)]
        public string DebitGlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglaccountid", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglno", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditglacctdesc", modeltype: FwDataTypes.Text)]
        public string CreditGlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationqty", modeltype: FwDataTypes.Integer)]
        public int? DepreciationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitdepreciationamount", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitDepreciationAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationextended", modeltype: FwDataTypes.Decimal)]
        public decimal? DepreciationExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Boolean)]
        public bool? Adjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("PurchaseId", "purchaseid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
