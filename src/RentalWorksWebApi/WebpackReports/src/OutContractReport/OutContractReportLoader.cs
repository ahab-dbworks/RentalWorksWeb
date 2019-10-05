using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using WebApi.Logic;

namespace WebApi.Modules.Reports.OutContractReport
{
    [FwSqlTable("dbo.funccontractoutrpt(@contractid, @applanguageid, @includebackorder)")]
    public class OutContractItemReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqtynumeric", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalout", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        //public string Notes { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string Rfids { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> LoadItems(OutContractReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddParameter("@contractid", request.ContractId);
                    select.AddParameter("@applanguageid", "");
                    select.AddParameter("@includebackorder", "F");
                    select.AddOrderBy("rectypeorder, itemorder, masterno, masteritemid");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (List<object> row in dt.Rows)
                {
                    row[dt.GetColumnNo("RecTypeDisplay")] = AppFunc.GetInventoryRecTypeDisplay(row[dt.GetColumnNo("RecType")].ToString());
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "QuantityOrdered", "QuantityOut", "TotalOut" };
                dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }


    [FwSqlTable("contractheadwebview")]
    public class OutContractReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text)]
        public string ContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date)]
        public string ContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttime", modeltype: FwDataTypes.Text)]
        public string ContractTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdatetime", modeltype: FwDataTypes.Date)]
        public string ContractDateAndTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttype", modeltype: FwDataTypes.Text)]
        public string ContractType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchange", modeltype: FwDataTypes.Boolean)]
        public bool? HasPendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldate", modeltype: FwDataTypes.Date)]
        public string BillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcompany", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd1", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locadd2", modeltype: FwDataTypes.Text)]
        public string OfficeLocationAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccitystatezip", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locphone", modeltype: FwDataTypes.Text)]
        public string OfficeLocationPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locfax", modeltype: FwDataTypes.Text)]
        public string OfficeLocationFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whadd1", modeltype: FwDataTypes.Text)]
        public string WarehouseAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whadd2", modeltype: FwDataTypes.Text)]
        public string WarehouseAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcitystatezip", modeltype: FwDataTypes.Text)]
        public string WarehouseCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcitystatezipcountry", modeltype: FwDataTypes.Text)]
        public string WarehouseCityStateZipCodeCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whphone", modeltype: FwDataTypes.Text)]
        public string WarehousePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whfax", modeltype: FwDataTypes.Text)]
        public string WarehouseFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealnodeal", modeltype: FwDataTypes.Text)]
        public string DealNumberAndDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string OrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderpono", modeltype: FwDataTypes.Text)]
        public string OrderPoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernodesc", modeltype: FwDataTypes.Text)]
        public string OrderNumberAndDescription { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "orderissuedtocompany", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtoatt1", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToAttentionTo1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtoatt2", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToAttentionTo2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtoadd1", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtoadd2", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtocity", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtostate", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtozip", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtocountry", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtophone", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderissuedtofax", modeltype: FwDataTypes.Text)]
        public string OrderIssuedToFax { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "containerbarcode", modeltype: FwDataTypes.Text)]
        public string ContainerBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usagedates", modeltype: FwDataTypes.Text)]
        public string UsageDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Boolean)]
        public bool? BillingDates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "terms", modeltype: FwDataTypes.Text)]
        public string PaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentemail", modeltype: FwDataTypes.Text)]
        public string AgentEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphoneext", modeltype: FwDataTypes.Text)]
        public string AgentPhoneAndExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentfax", modeltype: FwDataTypes.Text)]
        public string AgentFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoradd1", modeltype: FwDataTypes.Text)]
        public string VendorAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendoradd2", modeltype: FwDataTypes.Text)]
        public string VendorAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorcity", modeltype: FwDataTypes.Text)]
        public string VendorCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorstate", modeltype: FwDataTypes.Text)]
        public string VendorState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorzip", modeltype: FwDataTypes.Text)]
        public string VendorZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorphone", modeltype: FwDataTypes.Text)]
        public string VendorPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorfax", modeltype: FwDataTypes.Text)]
        public string VendorFax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorcontact", modeltype: FwDataTypes.Text)]
        public string VendorContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvcontact", modeltype: FwDataTypes.Text)]
        public string DeliveryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvlocation", modeltype: FwDataTypes.Text)]
        public string DeliveryLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvadd1", modeltype: FwDataTypes.Text)]
        public string DeliveryAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvadd2", modeltype: FwDataTypes.Text)]
        public string DeliveryAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvcitystatezip", modeltype: FwDataTypes.Text)]
        public string DeliveryCityStateZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvcountry", modeltype: FwDataTypes.Text)]
        public string DeliveryCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dvcontactphone", modeltype: FwDataTypes.Text)]
        public string DeliveryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text)]
        public string TermsAndConditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsfilename", modeltype: FwDataTypes.Text)]
        public string TermsAndConditionsFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsnewpage", modeltype: FwDataTypes.Boolean)]
        public bool? TermsAndConditionsNewPage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsiblepersonid", modeltype: FwDataTypes.Text)]
        public string ResponsiblePersonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsibleperson", modeltype: FwDataTypes.Text)]
        public string ResponsiblePerson { get; set; }
        //------------------------------------------------------------------------------------ 
        public FwJsonDataTable Items { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<OutContractReportLoader> RunReportAsync(OutContractReportRequest request)
        {

            Task<OutContractReportLoader> taskOutContract;
            Task<FwJsonDataTable> taskOutContractItems;

            OutContractReportLoader OutContract = null;
            OutContractItemReportLoader OutContractItems = null;

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    //SetBaseSelectQuery(select, qry);
                    //select.Parse();
                    //select.AddWhereIn("contractid", request.ContractId);
                    //taskOutContract = qry.QueryToTypedObjectAsync<OutContractReportLoader>();

                    qry.Add("select *                                   ");
                    qry.Add(" from  " +  TableName + " c     ");
                    qry.Add(" where c.contractid = @contractid          ");
                    qry.AddParameter("@contractid", request.ContractId);
                    AddPropertiesAsQueryColumns(qry);
                    taskOutContract = qry.QueryToTypedObjectAsync<OutContractReportLoader>();


                    OutContractItems = new OutContractItemReportLoader();
                    OutContractItems.SetDependencies(AppConfig, UserSession);
                    taskOutContractItems = OutContractItems.LoadItems(request);

                    await Task.WhenAll(new Task[] { taskOutContract, taskOutContractItems });

                    OutContract = taskOutContract.Result;

                    if (OutContract != null)
                    {
                        OutContract.Items = taskOutContractItems.Result;
                    }

                }
            }
            //if (request.IncludeSubHeadingsAndSubTotals)
            //{
            //    string[] totalFields = new string[] { "RentalTotal", "SalesTotal" };
            //    dt.InsertSubTotalRows("GroupField1", "RowType", totalFields);
            //    dt.InsertSubTotalRows("GroupField2", "RowType", totalFields);
            //    dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            //}
            //return dt;
            return OutContract;
        }
        //------------------------------------------------------------------------------------ 
    }
}
