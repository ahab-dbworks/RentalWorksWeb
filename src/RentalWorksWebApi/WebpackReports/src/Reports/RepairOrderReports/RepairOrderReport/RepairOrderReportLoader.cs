using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace WebApi.Modules.Reports.RepairReports.RepairOrderReport
{

    [FwSqlTable("repairdetailwebview")]
    public class RepairOrderItemReportLoader : AppReportLoader
    {
        protected string recType = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemtype", modeltype: FwDataTypes.Text)]
        public string ItemType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Tax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2", modeltype: FwDataTypes.DecimalString8Digits)]
        public string Tax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string TaxTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedwithtax", modeltype: FwDataTypes.DecimalString8Digits)]
        public string ExtendedWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "note", modeltype: FwDataTypes.Text)]
        public string Note { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<List<T>> LoadItems<T>(RepairOrderReportRequest request)
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
                    select.AddWhereIn("repairid", request.RepairId);
                    select.AddWhere("rectype = @rectype");
                    select.AddParameter("@rectype", recType);
                    select.AddOrderBy("rectypeorder, itemorder");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
            string[] totalFields = new string[] { "Extended", "Tax1", "Tax2", "ExtendedWithTax" };
            dt.InsertSubTotalRows("ItemType", "RowType", totalFields, nameHeaderColumns: new string[] { "TaxRate1", "TaxRate2" }, includeGroupColumnValueInFooter: true, totalFor: "");
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

            List<T> items = new List<T>();
            foreach (List<object> row in dt.Rows)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = item.GetType().GetProperties();
                foreach (var property in properties)
                {
                    string fieldName = property.Name;
                    int columnIndex = dt.GetColumnNo(fieldName);
                    if (!columnIndex.Equals(-1))
                    {
                        object value = row[dt.GetColumnNo(fieldName)];
                        FwDataTypes propType = dt.Columns[columnIndex].DataType;
                        bool isDecimal = false;
                        NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;

                        // we need the 8-digit precision for summing above. But now that we have our sums, we need to go back down to 2-digit display
                        if (propType.Equals(FwDataTypes.DecimalString8Digits))
                        {
                            propType = FwDataTypes.DecimalString2Digits;
                        }

                        FwSqlCommand.FwDataTypeIsDecimal(propType, value, ref isDecimal, ref numberFormat);
                        if (isDecimal)
                        {
                            decimal d = FwConvert.ToDecimal((value ?? "0").ToString());
                            property.SetValue(item, d.ToString("N", numberFormat));
                        }
                        else if (propType.Equals(FwDataTypes.Boolean))
                        {
                            property.SetValue(item, FwConvert.ToBoolean((value ?? "").ToString()));
                        }
                        else
                        {
                            property.SetValue(item, (value ?? "").ToString());
                        }


                    }
                }
                items.Add(item);
            }

            return items;
        }
        //------------------------------------------------------------------------------------ 
    }
    //------------------------------------------------------------------------------------ 
    public class PartsRepairOrderItemReportLoader : RepairOrderItemReportLoader
    {
        public PartsRepairOrderItemReportLoader()
        {
            recType = RwConstants.RECTYPE_PARTS;
        }
    }
    //------------------------------------------------------------------------------------ 
    public class MiscRepairOrderItemReportLoader : RepairOrderItemReportLoader
    {
        public MiscRepairOrderItemReportLoader()
        {
            recType = RwConstants.RECTYPE_MISCELLANEOUS;
        }
    }
    //------------------------------------------------------------------------------------ 
    public class LaborRepairOrderItemReportLoader : RepairOrderItemReportLoader
    {
        public LaborRepairOrderItemReportLoader()
        {
            recType = RwConstants.RECTYPE_LABOR;
        }
    }
    //------------------------------------------------------------------------------------ 


    [FwSqlTable("repairheaderwebview")]
    public class RepairOrderReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdate", modeltype: FwDataTypes.Date)]
        public string RepairDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.DecimalStringNoTrailingZeros)]
        public string Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damage", modeltype: FwDataTypes.Text)]
        public string Damage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "correction", modeltype: FwDataTypes.Text)]
        public string Correction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean)]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billablemaintenance", modeltype: FwDataTypes.Text)]
        public string BillableMaintenance { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text)]
        public string InputBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text)]
        public string ContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date)]
        public string ContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractnodate", modeltype: FwDataTypes.Text)]
        public string ContractNumberAndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccompany", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "loccountry", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCountry { get; set; }
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
        [FwSqlDataField(column: "locemail", modeltype: FwDataTypes.Text)]
        public string OfficeLocationEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locwebaddress", modeltype: FwDataTypes.Text)]
        public string OfficeLocationWebAddress { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1referencename", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1referenceno", modeltype: FwDataTypes.Text)]
        public string Tax1ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2referencename", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2referenceno", modeltype: FwDataTypes.Text)]
        public string Tax2ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1name", modeltype: FwDataTypes.Text)]
        public string Tax1Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2name", modeltype: FwDataTypes.Text)]
        public string Tax2Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRentalRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxRentalRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxSalesRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxSalesRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxLaborRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.DecimalString3Digits)]
        public string TaxLaborRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedto", modeltype: FwDataTypes.Text)]
        public string IssuedTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtoadd1", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtoadd2", modeltype: FwDataTypes.Text)]
        public string IssuedToAddress2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocity", modeltype: FwDataTypes.Text)]
        public string IssuedToCity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtostate", modeltype: FwDataTypes.Text)]
        public string IssuedToState { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtozip", modeltype: FwDataTypes.Text)]
        public string IssuedToZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtocountry", modeltype: FwDataTypes.Text)]
        public string IssuedToCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtophone", modeltype: FwDataTypes.Text)]
        public string IssuedToPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issuedtofax", modeltype: FwDataTypes.Text)]
        public string IssuedToFax { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax1", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Tax1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax2", modeltype: FwDataTypes.DecimalString2Digits)]
        public string Tax2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.DecimalString2Digits)]
        public string TaxTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extendedwithtax", modeltype: FwDataTypes.DecimalString2Digits)]
        public string ExtendedWithTax { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        public List<PartsRepairOrderItemReportLoader> PartsItems { get; set; } = new List<PartsRepairOrderItemReportLoader>(new PartsRepairOrderItemReportLoader[] { new PartsRepairOrderItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<MiscRepairOrderItemReportLoader> MiscItems { get; set; } = new List<MiscRepairOrderItemReportLoader>(new MiscRepairOrderItemReportLoader[] { new MiscRepairOrderItemReportLoader() });
        //------------------------------------------------------------------------------------ 
        public List<LaborRepairOrderItemReportLoader> LaborItems { get; set; } = new List<LaborRepairOrderItemReportLoader>(new LaborRepairOrderItemReportLoader[] { new LaborRepairOrderItemReportLoader() });
        //------------------------------------------------------------------------------------ 


        public async Task<RepairOrderReportLoader> RunReportAsync(RepairOrderReportRequest request)
        {
            RepairOrderReportLoader RepairOrder = null;
            useWithNoLock = false;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhereIn("repairid", request.RepairId);
                    select.SetQuery(qry);
                    Task<RepairOrderReportLoader> taskRepairOrder = qry.QueryToTypedObjectAsync<RepairOrderReportLoader>();

                    //parts items
                    Task<List<PartsRepairOrderItemReportLoader>> taskPartsRepairOrderItems;
                    PartsRepairOrderItemReportLoader PartsItems = new PartsRepairOrderItemReportLoader();
                    PartsItems.SetDependencies(AppConfig, UserSession);
                    taskPartsRepairOrderItems = PartsItems.LoadItems<PartsRepairOrderItemReportLoader>(request);

                    //misc items
                    Task<List<MiscRepairOrderItemReportLoader>> taskMiscRepairOrderItems;
                    MiscRepairOrderItemReportLoader MiscItems = new MiscRepairOrderItemReportLoader();
                    MiscItems.SetDependencies(AppConfig, UserSession);
                    taskMiscRepairOrderItems = MiscItems.LoadItems<MiscRepairOrderItemReportLoader>(request);

                    //labor items
                    Task<List<LaborRepairOrderItemReportLoader>> taskLaborRepairOrderItems;
                    LaborRepairOrderItemReportLoader LaborItems = new LaborRepairOrderItemReportLoader();
                    LaborItems.SetDependencies(AppConfig, UserSession);
                    taskLaborRepairOrderItems = LaborItems.LoadItems<LaborRepairOrderItemReportLoader>(request);

                    await Task.WhenAll(new Task[] { taskRepairOrder, taskPartsRepairOrderItems, taskMiscRepairOrderItems, taskLaborRepairOrderItems, });

                    RepairOrder = taskRepairOrder.Result;

                    if (RepairOrder != null)
                    {
                        RepairOrder.PartsItems = taskPartsRepairOrderItems.Result;
                        RepairOrder.MiscItems = taskMiscRepairOrderItems.Result;
                        RepairOrder.LaborItems = taskLaborRepairOrderItems.Result;
                    }
                }
            }
            return RepairOrder;
        }
        //------------------------------------------------------------------------------------ 
    }
}
