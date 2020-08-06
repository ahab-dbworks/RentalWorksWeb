using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class BarcodeLabel : QuikScanModule
    {
        //----------------------------------------------------------------------------------------------------
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public BarcodeLabel(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public async Task BarcodedItemSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                    qry.AddColumn("color", false, FwDataTypes.OleToHtmlColor);
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select masterno, master, barcode, statustype, statusdate, color, icode=masterno, description=master, mfgserial");
                    select.Add("from dbo.funcrentalitem(@locationid,'')");
                    select.Add("where trackedby in ('BARCODE')");
                    select.Add("  and statustype <> 'RETIRED'");
                    select.Add("  and barcode > ''");
                    select.Add("  and warehouseid = @warehouseid");
                    switch ((string)request.searchmode)
                    {
                        case "ICODE":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and masterno like @masterno");
                                select.AddParameter("@masterno", request.searchvalue + "%");
                            }
                            select.Add("order by masterno");
                            break;
                        case "DESCRIPTION":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and master like @master");
                                select.AddParameter("@master", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by master");
                            break;
                    }
                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                    select.AddParameter("@locationid", await this.AppData.GetLocationIdAsync(session));
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public async Task ICodeSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                dynamic userLocation = await this.AppData.GetUserLocationAsync(session);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select masterid, masterno, master, inventorydepartment, department=inventorydepartment, category, subcategory=isnull(subcategory, ''), trackedby, qty, icode=masterno, description=master");
                    select.Add("from masterview m with (nolock)");
                    select.Add("where warehouseid  = @warehouseid");
                    select.Add("  and m.availfor in ('R')");
                    select.Add("  and m.availfrom in ('W')");
                    switch ((string)request.searchmode)
                    {
                        case "ICODE":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and masterno like @searchvalue");
                                select.AddParameter("@searchvalue", request.searchvalue + "%");
                            }
                            select.Add("order by masterno");
                            break;
                        case "DESCRIPTION":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and master like @searchvalue");
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by master");
                            break;
                        case "DEPARTMENT":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and inventorydepartment like @searchvalue");
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by master");
                            break;
                        case "CATEGORY":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and category like @searchvalue");
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by master");
                            break;
                        case "SUBCATEGORY":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and subcategory like @searchvalue");
                                select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by master");
                            break;
                    }
                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        public class Label
        {
            public string name { get; set; } = string.Empty;
            public string data { get; set; } = string.Empty;
        }

        [FwJsonServiceMethod]
        public async Task GetBarcodeLabels(dynamic request, dynamic response, dynamic session)
        {
            response.labels = new List<dynamic>();
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select barcodelabelid, category, description, barcodelabel, datestamp");
                    qry.Add("from appbarcodelabel with (nolock)");
                    qry.Add("where warehouseid = @warehouseid");
                    qry.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync(true);
                    response.labels = await qry.QueryToDynamicList2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public async Task BarcodeLabelSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("datestamp", false, FwDataTypes.UTCDateTime);
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select barcodelabelid, warehouseid, category, description, barcodelabel, datestamp");
                    select.Add("from appbarcodelabel with (nolock)");
                    select.Add("where warehouseid = @warehouseid");
                    switch ((string)request.searchmode)
                    {
                        case "DESCRIPTION":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and description like @description");
                                select.AddParameter("@description", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by category, description");
                            break;
                        case "CATEGORY":
                            if (!string.IsNullOrEmpty(request.searchvalue))
                            {
                                select.Add("and category like @category");
                                select.AddParameter("@category", "%" + request.searchvalue + "%");
                            }
                            select.Add("order by category, description");
                            break;
                    }
                    select.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="mode,category,description,barcodelabel")]
        public async Task SaveBarcodeLabel(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string mode = request.mode;
                string barcodelabel = request.barcodelabel;
                if (mode == "NEW")
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("insert into appbarcodelabel(barcodelabelid, warehouseid, category, description, barcodelabel, datestamp)");
                        qry.Add("values(@barcodelabelid, @warehouseid, @category, @description, @barcodelabel, @datestamp)");
                        qry.AddParameter("@barcodelabelid", await FwSqlData.GetNextIdAsync(conn, this.ApplicationConfig.DatabaseSettings));
                        qry.AddParameter("@warehouseid", await this.AppData.GetWarehouseIdAsync(session));
                        qry.AddParameter("@category", request.category);
                        qry.AddParameter("@description", request.description);
                        qry.AddParameter("@barcodelabel", request.barcodelabel);
                        qry.AddParameter("@datestamp", DateTime.UtcNow);
                        response.rowsaffected = await qry.ExecuteNonQueryAsync();
                    }
                }
                else if (mode == "EDIT")
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("update appbarcodelabel");
                        qry.Add("set category = @category");
                        qry.Add("  , description = @description");
                        if (barcodelabel.Length > 0)
                        {
                            qry.Add("  , barcodelabel = @barcodelabel");
                            qry.AddParameter("@barcodelabel", request.barcodelabel);
                        }

                        qry.Add("where barcodelabelid = @barcodelabelid");
                        qry.AddParameter("@barcodelabelid", request.barcodelabelid);
                        qry.AddParameter("@category", request.category);
                        qry.AddParameter("@description", request.description);
                        qry.AddParameter("@datestamp", DateTime.UtcNow);
                        response.rowsaffected = await qry.ExecuteNonQueryAsync();
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="barcodelabelid")]
        public async Task DeleteBarcodeLabel(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("delete appbarcodelabel");
                    qry.Add("where barcodelabelid = @barcodelabelid");
                    qry.AddParameter("@barcodelabelid", request.barcodelabelid);
                    response.rowsaffected = await qry.ExecuteNonQueryAsync();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
