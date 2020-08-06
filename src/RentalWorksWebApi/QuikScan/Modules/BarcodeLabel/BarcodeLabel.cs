using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace RentalWorksQuikScan.Modules
{
    public class BarcodeLabel
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public void BarcodedItemSearch(dynamic request, dynamic response, dynamic session)
        {
            FwSqlSelect select = new FwSqlSelect();
            dynamic userLocation = RwAppData.GetUserLocation(session);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("statusdate", false, FwJsonDataTableColumn.DataTypes.Date);
                qry.AddColumn("color", false, FwJsonDataTableColumn.DataTypes.OleToHtmlColor);
                select.PageNo   = request.pageno;
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
                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                select.AddParameter("@locationid", RwAppData.GetLocationId(session));
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public void ICodeSearch(dynamic request, dynamic response, dynamic session)
        {
            FwSqlSelect select = new FwSqlSelect();
            dynamic userLocation = RwAppData.GetUserLocation(session);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                select.PageNo   = request.pageno;
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
                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //---------------------------------------------------------------------------------------------
        public class Label
        {
            public string name { get; set; } = string.Empty;
            public string data { get; set; } = string.Empty;
        }

        [FwJsonServiceMethod]
        public void GetBarcodeLabels(dynamic request, dynamic response, dynamic session)
        {
            response.labels = new List<dynamic>();
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select barcodelabelid, category, description, barcodelabel, datestamp");
                qry.Add("from appbarcodelabel with (nolock)");
                qry.Add("where warehouseid = @warehouseid");
                qry.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                response.labels = qry.QueryToDynamicList2();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="searchvalue")]
        public void BarcodeLabelSearch(dynamic request, dynamic response, dynamic session)
        {
            FwSqlSelect select = new FwSqlSelect();
            dynamic userLocation = RwAppData.GetUserLocation(session);
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.UTCDateTime);
                select.PageNo   = request.pageno;
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
                select.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="mode,category,description,barcodelabel")]
        public static void SaveBarcodeLabel(dynamic request, dynamic response, dynamic session)
        {
            string mode = request.mode;
            string barcodelabel = request.barcodelabel;
            if (mode == "NEW")
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("insert into appbarcodelabel(barcodelabelid, warehouseid, category, description, barcodelabel, datestamp)");
                    qry.Add("values(@barcodelabelid, @warehouseid, @category, @description, @barcodelabel, @datestamp)");
                    qry.AddParameter("@barcodelabelid", FwSqlData.GetNextId(FwSqlConnection.RentalWorks));
                    qry.AddParameter("@warehouseid", RwAppData.GetWarehouseId(session));
                    qry.AddParameter("@category", request.category);
                    qry.AddParameter("@description", request.description);
                    qry.AddParameter("@barcodelabel", request.barcodelabel);
                    qry.AddParameter("@datestamp", DateTime.UtcNow);
                    response.rowsaffected = qry.ExecuteNonQuery();
                }
            }
            else if (mode == "EDIT")
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
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
                    response.rowsaffected = qry.ExecuteNonQuery();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="barcodelabelid")]
        public static void DeleteBarcodeLabel(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("delete appbarcodelabel");
                qry.Add("where barcodelabelid = @barcodelabelid");
                qry.AddParameter("@barcodelabelid", request.barcodelabelid);
                response.rowsaffected = qry.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
