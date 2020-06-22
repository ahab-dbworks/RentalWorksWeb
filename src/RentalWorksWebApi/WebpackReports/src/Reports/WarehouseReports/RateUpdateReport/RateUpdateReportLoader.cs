using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using WebApi.Modules.Inventory.Inventory;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Globalization;

namespace WebApi.Modules.Reports.RateUpdateReport
{

    [FwSqlTable("rateupdatebatchitemview")]
    public class RateUpdateItemReportLoader : AppReportLoader
    {
        protected string recType = "";
        //------------------------------------------------------------------------------------ 
        public RateUpdateItemReportLoader() { }
        //------------------------------------------------------------------------------------ 
        public RateUpdateItemReportLoader(string recType)
        {
            this.recType = recType;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", isPrimaryKey: true, modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfordisplay", modeltype: FwDataTypes.Text)]
        public string AvailableForDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string PartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddefaultcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdefaultcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewDefaultCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldprice", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newprice", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldretail", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldRetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newretail", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewRetail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewHourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldhourlycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newhourlycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewHourlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewDailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "olddailycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newdailycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewDailyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek2rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek3rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek4rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweek5rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldweeklycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklyrate", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewWeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek2rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewWeek2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek3rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewWeek3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek4rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewWeek4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweek5rate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewWeek5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newweeklycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewWeeklyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmonthlycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmaxdiscount", modeltype: FwDataTypes.DecimalString2Digits)]
        public string OldMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlyrate", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewMonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmonthlycost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewMonthlyCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmaxdiscount", modeltype: FwDataTypes.DecimalString2Digits)]
        public string NewMaxDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmanifestvalue", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmanifestvalue", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewUnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldreplacementcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newreplacementcost", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "oldmindw", modeltype: FwDataTypes.DecimalString3Digits)]
        public string OldMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "newmindw", modeltype: FwDataTypes.DecimalString3Digits)]
        public string NewMinDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<List<T>> LoadItems<T>(RateUpdateReportRequest request)
        {
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

                    select.AddWhere("rateupdatebatchid = @rateupdatebatchid");
                    if (request.PendingModificationsOnly.GetValueOrDefault(false))
                    {
                        select.AddParameter("@rateupdatebatchid", "0");
                        select.AddWhere(InventoryFunc.GetRateUpdatePendingModificationsWhere());
                    }
                    else
                    {
                        select.AddParameter("@rateupdatebatchid", request.RateUpdateBatchId);
                    }
                    select.AddWhere("availfor = @availfor");
                    select.AddParameter("@availfor", recType);

                    select.AddOrderBy("availfor, warehouse, masterno");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);


                    //AddPropertiesAsQueryColumns(qry);
                    //dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            //dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;

            string[] totalFields = new string[] { "OldMinDaysPerWeek" };
            dt.InsertSubTotalRows("AvailableForDisplay", "RowType", totalFields);
            dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
            dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
            dt.InsertSubTotalRows("Category", "RowType", totalFields);
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


    //[FwSqlTable("rateupdatebatchitemview")]
    public class RateUpdateReportLoader : AppReportLoader
    {
        public List<RateUpdateItemReportLoader> RentalItems { get; set; } = new List<RateUpdateItemReportLoader>(new RateUpdateItemReportLoader[] { new RateUpdateItemReportLoader(RwConstants.RECTYPE_RENTAL) });
        public List<RateUpdateItemReportLoader> SalesItems { get; set; } = new List<RateUpdateItemReportLoader>(new RateUpdateItemReportLoader[] { new RateUpdateItemReportLoader(RwConstants.RECTYPE_SALE) });
        public List<RateUpdateItemReportLoader> PartsItems { get; set; } = new List<RateUpdateItemReportLoader>(new RateUpdateItemReportLoader[] { new RateUpdateItemReportLoader(RwConstants.RECTYPE_PARTS) });
        public List<RateUpdateItemReportLoader> LaborItems { get; set; } = new List<RateUpdateItemReportLoader>(new RateUpdateItemReportLoader[] { new RateUpdateItemReportLoader(RwConstants.RECTYPE_LABOR) });
        public List<RateUpdateItemReportLoader> MiscellaneousItems { get; set; } = new List<RateUpdateItemReportLoader>(new RateUpdateItemReportLoader[] { new RateUpdateItemReportLoader(RwConstants.RECTYPE_MISCELLANEOUS) });
        //------------------------------------------------------------------------------------ 
        public async Task<RateUpdateReportLoader> RunReportAsync(RateUpdateReportRequest request)
        {
            RateUpdateReportLoader Report = new RateUpdateReportLoader();


            //rental items
            Task<List<RateUpdateItemReportLoader>> taskRentalItems;
            RateUpdateItemReportLoader RentalItems = new RateUpdateItemReportLoader(RwConstants.RECTYPE_RENTAL);
            RentalItems.SetDependencies(AppConfig, UserSession);
            taskRentalItems = RentalItems.LoadItems<RateUpdateItemReportLoader>(request);

            //sales items
            Task<List<RateUpdateItemReportLoader>> taskSalesItems;
            RateUpdateItemReportLoader SalesItems = new RateUpdateItemReportLoader(RwConstants.RECTYPE_SALE);
            SalesItems.SetDependencies(AppConfig, UserSession);
            taskSalesItems = SalesItems.LoadItems<RateUpdateItemReportLoader>(request);

            //parts items
            Task<List<RateUpdateItemReportLoader>> taskPartsItems;
            RateUpdateItemReportLoader PartsItems = new RateUpdateItemReportLoader(RwConstants.RECTYPE_PARTS);
            PartsItems.SetDependencies(AppConfig, UserSession);
            taskPartsItems = PartsItems.LoadItems<RateUpdateItemReportLoader>(request);

            //misc items
            Task<List<RateUpdateItemReportLoader>> taskMiscItems;
            RateUpdateItemReportLoader MiscItems = new RateUpdateItemReportLoader(RwConstants.RECTYPE_MISCELLANEOUS);
            MiscItems.SetDependencies(AppConfig, UserSession);
            taskMiscItems = MiscItems.LoadItems<RateUpdateItemReportLoader>(request);

            //labor items
            Task<List<RateUpdateItemReportLoader>> taskLaborItems;
            RateUpdateItemReportLoader LaborItems = new RateUpdateItemReportLoader(RwConstants.RECTYPE_LABOR);
            LaborItems.SetDependencies(AppConfig, UserSession);
            taskLaborItems = LaborItems.LoadItems<RateUpdateItemReportLoader>(request);

            await Task.WhenAll(new Task[] { taskRentalItems, taskSalesItems, taskPartsItems, taskMiscItems, taskLaborItems });

            Report.RentalItems = taskRentalItems.Result;
            Report.SalesItems = taskSalesItems.Result;
            Report.MiscellaneousItems = taskMiscItems.Result;
            Report.LaborItems = taskLaborItems.Result;
            Report.PartsItems = taskPartsItems.Result;

            return Report;

        }
        //------------------------------------------------------------------------------------ 
    }
}
