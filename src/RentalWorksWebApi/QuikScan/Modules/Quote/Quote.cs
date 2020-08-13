using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class Quote : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public Quote(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadModuleProperties(dynamic request, dynamic response, dynamic session)
        {
            response.syscontrol = await LoadSysControlValuesAsync();
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                decimal grandtotal = 0.0M;

                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                qry.AddColumn("qtyordered", false, FwDataTypes.Integer);
                qry.AddColumn("price", false, FwDataTypes.Decimal);
                qry.AddColumn("periodextended", false, FwDataTypes.Decimal);
                select.Add("select *");
                select.Add("  from dbo.qsmasteritem(@orderid, @masteritemid)");
                select.Add("order by orderby");
                select.AddParameter("@orderid", request.orderid);
                select.AddParameter("@masteritemid", string.Empty);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);

                for (int i = 0; i < response.searchresults.Rows.Count; i++)
                {
                    grandtotal += response.searchresults.Rows[i][response.searchresults.ColumnIndex["periodextended"]];
                }
                response.grandtotal = grandtotal; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ScanItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.iteminfo = await this.AppData.WebGetItemStatusAsync(conn, session.security.webUser.usersid, request.enteredvalue);

                if (response.iteminfo.trackedby != "QUANTITY")
                {
                    request.qty = 1;
                    request.masterno = request.enteredvalue;
                    await AddItem(request, response, session);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task AddItem(dynamic request, dynamic response, dynamic session)
        {
            response.insert = await QSInsertMasterItemAsync(orderid:          request.orderid,
                                                 barcode:          request.masterno,
                                                 qtyordered:       request.qty,
                                                 webusersid:       session.security.webUser.webusersid,
                                                 masteritemid:     "",
                                                 spaceid:          (request.locationdata != null) ? request.locationdata.spaceid : "",
                                                 spacetypeid:      (request.locationdata != null) ? request.locationdata.spacetypeid : "",
                                                 facilitiestypeid: (request.locationdata != null) ? request.locationdata.facilitiestypeid : "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task UpdateItem(dynamic request, dynamic response, dynamic session)
        {
            response.update = await QSInsertMasterItemAsync(orderid:          request.orderid,
                                                 barcode:          request.masterno,
                                                 qtyordered:       request.qty,
                                                 webusersid:       session.security.webUser.webusersid,
                                                 masteritemid:     request.masteritemid,
                                                 spaceid:          (request.locationdata != null) ? request.locationdata.spaceid : "",
                                                 spacetypeid:      (request.locationdata != null) ? request.locationdata.spacetypeid : "",
                                                 facilitiestypeid: (request.locationdata != null) ? request.locationdata.facilitiestypeid : "");
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task DeleteItem(dynamic request, dynamic response, dynamic session)
        {
            response.deleteitem = await QSDeleteMasterItemAsync(orderid:      request.orderid,
                                                     masteritemid: request.masteritemid,
                                                     rentalitemid: request.rentalitemid,
                                                     qtyremoved:   request.qtyremoved,
                                                     webusersid:   session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SubmitQuote(dynamic request, dynamic response, dynamic session)
        {
            response.submit = await QSSubmitQuoteAsync(orderid:        request.orderid,
                                            webusersid:     session.security.webUser.webusersid,
                                            createcontract: request.createcontract);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CancelQuote(dynamic request, dynamic response, dynamic session)
        {
            response.cancel = await QSCancelQuoteAsync(orderid:    request.orderid,
                                            webusersid: session.security.webUser.webusersid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SearchItems(dynamic request, dynamic response, dynamic session)
        {
            response.items = await SearchQuantityItemsAsync(request.searchvalue, request.warehouseid);
        }
        //---------------------------------------------------------------------------------------------
        public async Task<List<dynamic>> QSMasterItemAsync(string orderid, string masteritemid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                List<dynamic> result;

                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("  from dbo.qsmasteritem(@orderid, @masteritemid)");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@masteritemid", masteritemid);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> QSInsertMasterItemAsync(string orderid, string barcode, int qtyordered, string masteritemid, string webusersid, string spaceid, string spacetypeid, string facilitiestypeid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result = new ExpandoObject();
                FwSqlCommand sp = new FwSqlCommand(conn, "dbo.qsinsertmasteritem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@barcode", barcode);
                sp.AddParameter("@qtyordered", qtyordered);
                sp.AddParameter("@webusersid", webusersid);
                sp.AddParameter("@spaceid", spaceid);
                sp.AddParameter("@spacetypeid", spacetypeid);
                sp.AddParameter("@facilitiestypeid", facilitiestypeid);
                sp.AddParameter("@masteritemid", SqlDbType.Char, ParameterDirection.InputOutput, masteritemid);
                sp.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                try
                {
                    await sp.ExecuteAsync();
                    result.masteritemid = sp.GetParameter("@masteritemid").ToString();
                    result.errno = sp.GetParameter("@errno").ToString();
                    result.errmsg = sp.GetParameter("@errmsg").ToString();
                }
                catch (Exception ex)
                {
                    int indexofBEGINTRANSACTION = ex.Message.IndexOf("BEGIN TRANSACTION");
                    string error = ex.Message.Remove(0, indexofBEGINTRANSACTION + 17);
                    result.errno = "1";
                    result.errmsg = error;
                }
                result.masteritemid = sp.GetParameter("@masteritemid").ToString();
                result.errno = sp.GetParameter("@errno").ToString();
                result.errmsg = sp.GetParameter("@errmsg").ToString();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> QSDeleteMasterItemAsync(string orderid, string masteritemid, string rentalitemid, int qtyremoved, string webusersid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand sp = new FwSqlCommand(conn, "dbo.qsdeletemasteritem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@masteritemid", masteritemid);
                sp.AddParameter("@rentalitemid", rentalitemid);
                sp.AddParameter("@webusersid", webusersid);
                sp.AddParameter("@qty", qtyremoved);
                sp.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();

                dynamic result = new ExpandoObject();
                result.errno = sp.GetParameter("@errno").ToString();
                result.errmsg = sp.GetParameter("@errmsg").ToString();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> QSSubmitQuoteAsync(string orderid, string webusersid, string createcontract)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand sp = new FwSqlCommand(conn, "dbo.qssubmitquote", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@webusersid", webusersid);
                sp.AddParameter("@createcontract", createcontract);
                sp.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();

                dynamic result = new ExpandoObject();
                result.errno = sp.GetParameter("@errno").ToString();
                result.errmsg = sp.GetParameter("@errmsg").ToString();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> QSCancelQuoteAsync(string orderid, string webusersid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand sp = new FwSqlCommand(conn, "dbo.qscancelquote", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@webusersid", webusersid);
                sp.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();

                dynamic result = new ExpandoObject();
                result.errno = sp.GetParameter("@errno").ToString();
                result.errmsg = sp.GetParameter("@errmsg").ToString();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task<List<dynamic>> SearchQuantityItemsAsync(string searchvalue, string warehouseid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                List<dynamic> result;
                string[] searchvalues;

                searchvalues = searchvalue.Split(' ');

                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("  from masterview with (nolock)");
                qry.Add(" where warehouseid = @warehouseid");
                qry.Add("   and availfor in ('R')");
                qry.Add("   and availfrom in ('W')");
                qry.Add("   and inactive <> 'T'");
                qry.Add("   and hasqty = 'T'");
                qry.Add("   and trackedby = 'QUANTITY'");
                qry.Add("   and class = 'I'");

                if (searchvalues.Length == 1)
                {
                    qry.Add("   and master like '%' + @searchvalue + '%'");
                    qry.AddParameter("@searchvalue", searchvalues[0]);
                }
                else
                {
                    qry.Add("   and (");
                    for (int i = 0; i < searchvalues.Length; i++)
                    {
                        if (i > 0)
                        {
                            qry.Add(" and ");
                        }
                        qry.Add("(master like '%' + @searchvalue" + i + " + '%')");
                        qry.AddParameter("@searchvalue" + i, searchvalues[i]);
                    }
                    qry.Add(")");
                }

                qry.Add("order by master");
                qry.AddParameter("@warehouseid", warehouseid);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SearchLocations(dynamic request, dynamic response, dynamic session)
        {
            response.locations = await SearchOrderLocationsAsync(orderid:     request.orderid,
                                                      searchvalue: request.searchvalue);
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> SearchOrderLocationsAsync(string orderid, string searchvalue)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result = new ExpandoObject();
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                string includefacilitiestype = "F";
                dynamic applicationoptions = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
                string[] searchvalues = searchvalue.Split(' ');

                if (applicationoptions.facilities.enabled)
                {
                    dynamic controlresult = await LoadSysControlValuesAsync();

                    if ((controlresult.itemsinrooms == "T") && (controlresult.facilitytypeincurrentlocation == "T"))
                    {
                        includefacilitiestype = "T";
                    }
                }

                qry.Add("select *");
                qry.Add("  from dbo.funcorderspacelocation(@orderid, @includefacilitiestype)");
                qry.Add(" where orderid = orderid");

                if (searchvalues.Length == 1)
                {
                    qry.Add("   and location like '%' + @searchvalue + '%'");
                    qry.AddParameter("@searchvalue", searchvalues[0]);
                }
                else
                {
                    qry.Add("   and (");
                    for (int i = 0; i < searchvalues.Length; i++)
                    {
                        if (i > 0)
                        {
                            qry.Add(" and ");
                        }
                        qry.Add("(location like '%' + @searchvalue" + i + " + '%')");
                        qry.AddParameter("@searchvalue" + i, searchvalues[i]);
                    }
                    qry.Add(")");
                }

                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@includefacilitiestype", includefacilitiestype);
                result = await qry.QueryToDynamicList2Async();

                return result; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> LoadSysControlValuesAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 itemsinrooms, facilitytypeincurrentlocation");
                qry.Add("  from syscontrol with (nolock)");
                qry.Add(" where controlid = '1'");
                dynamic result = await qry.QueryToDynamicObject2Async();

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}