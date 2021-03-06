﻿using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class AssetSetLocation : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public AssetSetLocation(FwApplicationConfig applicationConfig) : base(applicationConfig) 
        {
            this.AppData = new RwAppData(applicationConfig);       
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ScanSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "ScanSearch";
                FwSqlCommand qry;

                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select distinct masterid, rentalitemid, purchasepoid, masteritemid, orderid, purchasefororderid, departmentid, dealid, setnoid, setno, statustype, masterno, master, setcharacter, department, departmentno, production, productionno, barcode, mfgid, manufacturer, buyerid, primaryvendor, transactionno, sourcecode, accountingcode, cost, thumbnail = cast(thumbnail as varbinary(max)), vendorid, vendor, dateacquired, location, refnobox, refnopallet, trackedby");
                qry.Add("  from dbo.funcassetsetlocation(@warehouseid, @searchvalue)");
                qry.AddParameter("@searchvalue", request.searchvalue);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);

                response.itemdetails = await qry.QueryToDynamicList2Async();

                for (int i = 0; i < response.itemdetails.Count; i++)
                {
                    response.itemdetails[i].setnoid = FwCryptography.AjaxEncrypt(response.itemdetails[i].setnoid);
                    response.itemdetails[i].mfgid = FwCryptography.AjaxEncrypt(response.itemdetails[i].mfgid);
                    response.itemdetails[i].buyerid = FwCryptography.AjaxEncrypt(response.itemdetails[i].buyerid);
                    response.itemdetails[i].vendorid = FwCryptography.AjaxEncrypt(response.itemdetails[i].vendorid);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ItemSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "ItemSearch";
                FwSqlCommand qry;
                FwSqlSelect select = new FwSqlSelect();

                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("thumbnail", false, FwDataTypes.JpgDataUrl);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select distinct masterid, rentalitemid, purchasepoid, purchasefororderid, departmentid, dealid, masterno, master, setcharacter, department, production, barcode, dateacquired, thumbnail = cast(thumbnail as varbinary(max))");
                select.Add("  from dbo.funcassetsetlocationsearch(@warehouseid)");
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "DESCRIPTION":
                            select.Add("where master like @master");
                            select.AddParameter("@master", request.searchvalue + "%");
                            break;
                        case "DEPARTMENT":
                            select.Add("where department like @department");
                            select.AddParameter("@department", request.searchvalue + "%");
                            break;
                        case "PRODUCTION":
                            select.Add("where production like @production");
                            select.AddParameter("@production", request.searchvalue + "%");
                            break;
                        case "SETCHARACTER":
                            select.Add("where setcharacter like @setcharacter");
                            select.AddParameter("@setcharacter", request.searchvalue + "%");
                            break;
                    }
                }
                select.Add("order by master");
                qry.AddParameter("@warehouseid", userLocation.warehouseId);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SaveItemInformation(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "SaveItemInformation";
                FwSqlCommand qry, qry2, qry3;

                qry = new FwSqlCommand(conn, "dbo.assetproductionupdateweb", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@rentalitemid", request.recorddata.rentalitemid);
                qry.AddParameter("@purchasefororderid", request.recorddata.purchasefororderid);
                qry.AddParameter("@purchasepoid", request.recorddata.purchasepoid);
                qry.AddParameter("@masterid", request.recorddata.masterid);
                qry.AddParameter("@masteritemid", request.recorddata.masteritemid);
                qry.AddParameter("@mfgid", FwCryptography.AjaxDecrypt(request.manufacturer));
                qry.AddParameter("@vendorid", FwCryptography.AjaxDecrypt(request.vendor));
                qry.AddParameter("@buyerid", FwCryptography.AjaxDecrypt(request.primaryvendor));
                qry.AddParameter("@refno", "");
                qry.AddParameter("@dateacquired", request.acquiredate);
                qry.AddParameter("@transactionno", request.transactionno);
                qry.AddParameter("@sourcecode", request.sourceno);
                qry.AddParameter("@accountingcode", request.accountingno);
                qry.AddParameter("@setnoid", FwCryptography.AjaxDecrypt(request.setno));
                qry.AddParameter("@location", request.location);
                qry.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await qry.ExecuteAsync();

                if (FwValidate.IsPropertyDefined(request, "assetsetlocations"))
                {
                    for (int i = 0; i < request.assetsetlocations.Length; i++)
                    {
                        if (FwValidate.IsPropertyDefined(request.assetsetlocations[i], "rowdata"))
                        {
                            qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                            qry2.Add("update assetlocation");
                            qry2.Add("   set location        = @location,");
                            qry2.Add("       qty             = @qty");
                            qry2.Add(" where internalchar    = @internalchar");
                            qry2.Add("   and assetlocationid = @assetlocationid");
                            qry2.AddParameter("@internalchar", request.assetsetlocations[i].rowdata.internalchar);
                            qry2.AddParameter("@assetlocationid", request.assetsetlocations[i].rowdata.assetlocationid);
                            qry2.AddParameter("@location", ((string)request.assetsetlocations[i].location).ToUpper());
                            qry2.AddParameter("@qty", request.assetsetlocations[i].qty);
                            await qry2.ExecuteAsync();
                        }
                        else
                        {
                            qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                            qry2.Add("insert into assetlocation (datestamp, internalchar, orderid, masterid, location, qty)");
                            qry2.Add("values (getdate(), dbo.funcgetinternalchar(), @orderid, @masterid, @location, @qty)");
                            qry2.AddParameter("@orderid", request.recorddata.orderid);
                            qry2.AddParameter("@masterid", request.recorddata.masterid);
                            qry2.AddParameter("@location", ((string)request.assetsetlocations[i].location).ToUpper());
                            qry2.AddParameter("@qty", request.assetsetlocations[i].qty);
                            await qry2.ExecuteAsync();
                        }
                    }
                }

                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                qry3 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                if (request.recorddata.trackedby == "BARCODE")
                {
                    qry3.Add("select distinct masterid, rentalitemid, purchasepoid, masteritemid, orderid, purchasefororderid, departmentid, dealid, setnoid, setno, statustype, masterno, master, setcharacter, department, departmentno, production, productionno, barcode, mfgid, manufacturer, buyerid, primaryvendor, transactionno, sourcecode, accountingcode, cost, thumbnail = cast(thumbnail as varbinary(max)), vendorid, vendor, dateacquired, location, refnobox, refnopallet, trackedby");
                    qry3.Add("  from dbo.funcassetsetlocation(@warehouseid, @barcode)");
                    qry3.AddParameter("@barcode", request.recorddata.barcode);
                    qry3.AddParameter("@warehouseid", userLocation.warehouseId);
                }
                else if (request.recorddata.trackedby == "QUANTITY")
                {
                    qry3.Add("select distinct masterid, rentalitemid, purchasepoid, masteritemid, orderid, purchasefororderid, departmentid, dealid, setnoid, setno, statustype, masterno, master, setcharacter, department, departmentno, production, productionno, barcode, mfgid, manufacturer, buyerid, primaryvendor, transactionno, sourcecode, accountingcode, cost, thumbnail = cast(thumbnail as varbinary(max)), vendorid, vendor, dateacquired, location, refnobox, refnopallet, trackedby");
                    qry3.Add("  from dbo.funcassetsetlocation(@warehouseid, @masterno)");
                    //qry3.Add(" where purchaseid         = @purchaseid");
                    qry3.Add(" where purchasepoid       = @purchasepoid");
                    qry3.Add("   and purchasefororderid = @purchasefororderid");
                    qry3.Add("   and statustype         = 'OUT'");
                    qry3.AddParameter("@masterno", request.recorddata.masterno);
                    //qry3.AddParameter("@purchaseid",         request.recorddata.purchaseid);
                    qry3.AddParameter("@purchasepoid", request.recorddata.purchasepoid);
                    qry3.AddParameter("@purchasefororderid", request.recorddata.purchasefororderid);
                    qry3.AddParameter("@warehouseid", userLocation.warehouseId);
                }

                response.itemdetails = await qry3.QueryToDynamicList2Async();

                for (int i = 0; i < response.itemdetails.Count; i++)
                {
                    response.itemdetails[i].setnoid = FwCryptography.AjaxEncrypt(response.itemdetails[i].setnoid);
                    response.itemdetails[i].mfgid = FwCryptography.AjaxEncrypt(response.itemdetails[i].mfgid);
                    response.itemdetails[i].buyerid = FwCryptography.AjaxEncrypt(response.itemdetails[i].buyerid);
                    response.itemdetails[i].vendorid = FwCryptography.AjaxEncrypt(response.itemdetails[i].vendorid);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadItemInformation(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "LoadItemInformation";
                FwSqlCommand qry;

                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select distinct masterid, rentalitemid, purchasepoid, masteritemid, orderid, purchasefororderid, departmentid, dealid, setnoid, setno, statustype, masterno, master, setcharacter, department, departmentno, production, productionno, barcode, mfgid, manufacturer, buyerid, primaryvendor, transactionno, sourcecode, accountingcode, cost, thumbnail = cast(thumbnail as varbinary(max)), vendorid, vendor, dateacquired, location, refnobox, refnopallet, trackedby");
                qry.Add("  from dbo.funcassetsetlocation(@warehouseid, @masterno)");
                qry.Add(" where rentalitemid       = @rentalitemid");
                qry.Add("   and purchasepoid       = @purchasepoid");
                qry.Add("   and purchasefororderid = @purchasefororderid");
                qry.Add("   and statustype         = 'OUT'");
                qry.AddParameter("@masterno", request.recorddata.masterno);
                qry.AddParameter("@rentalitemid", request.recorddata.rentalitemid);
                qry.AddParameter("@purchasepoid", request.recorddata.purchasepoid);
                qry.AddParameter("@purchasefororderid", request.recorddata.purchasefororderid);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);

                response.itemdetails = await qry.QueryToDynamicList2Async();

                for (int i = 0; i < response.itemdetails.Count; i++)
                {
                    response.itemdetails[i].setnoid = FwCryptography.AjaxEncrypt(response.itemdetails[i].setnoid);
                    response.itemdetails[i].mfgid = FwCryptography.AjaxEncrypt(response.itemdetails[i].mfgid);
                    response.itemdetails[i].buyerid = FwCryptography.AjaxEncrypt(response.itemdetails[i].buyerid);
                    response.itemdetails[i].vendorid = FwCryptography.AjaxEncrypt(response.itemdetails[i].vendorid);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadAssetLocations(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                //const string METHOD_NAME = "LoadAssetLocations";
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("  from assetlocation");
                qry.Add(" where orderid  = @orderid");
                qry.Add("   and masterid = @masterid");
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@masterid", request.masterid);

                response.assetlocations = await qry.QueryToDynamicList2Async(); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
