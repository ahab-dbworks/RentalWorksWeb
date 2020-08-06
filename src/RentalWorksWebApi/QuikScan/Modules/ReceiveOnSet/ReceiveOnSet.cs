using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class ReceiveOnSet : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public ReceiveOnSet(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task POSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "POSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                                 usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                qry.AddColumn("acquire", false, FwDataTypes.Date);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("from   receiveonsetpoview pov with (nolock)");
                //select.Add("where exists(select * from masteritem mi with (nolock) where mi.orderid = pov.orderid and mi.warehouseid = @warehouseid)");
                select.Add("where pov.warehouseid = @warehouseid");
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "PONO":
                            select.Add("and pov.orderno like @orderno");
                            select.AddParameter("@orderno", request.searchvalue + "%");
                            break;
                        case "DEPARTMENT":
                            select.Add("and pov.department like @department");
                            select.AddParameter("@department", request.searchvalue + "%");
                            break;
                        case "VENDOR":
                            select.Add("and pov.vendor like @vendor");
                            select.AddParameter("@vendor", request.searchvalue + "%");
                            break;
                        case "DESCRIPTION":
                            select.Add("and pov.orderdesc like @orderdesc");
                            select.AddParameter("@orderdesc", "%" + request.searchvalue + "%");
                            break;
                    }
                }
                select.Add("order by orderdate desc, orderno desc");
                select.AddParameter("@warehouseid", userLocation.warehouseId);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetPOReceiveContractID(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select dbo.getporeceivecontractid(@poid) as outreceivecontractid");
                qry.AddParameter("@poid", request.orderid);
                await qry.ExecuteAsync();

                response.outreceivecontractid = qry.GetField("outreceivecontractid").ToString().TrimEnd(); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadProductions(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("deal", false);
                qry.AddColumn("dealid", false);
                qry.Add("select text=deal, value=dealid");
                qry.Add("from dealview");
                qry.Add("where dealstatus = 'ACTIVE'");
                qry.Add("order by deal");

                response.productions = await qry.QueryToDynamicList2Async(); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadSets(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("deal", false);
                qry.AddColumn("dealid", false);
                qry.Add("select text=setno, value=setnoid");
                qry.Add("from setnoview");
                qry.Add("where inactive <> 'T'");
                //qry.Add("  and dealid = @dealid");
                qry.Add("order by setno");
                //qry.AddParameter("@dealid", request.production);

                response.sets = await qry.QueryToDynamicList2Async(); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "OrderSearch";
            dynamic userLocation;
            FwSqlCommand qry;
            FwSqlSelect select = new FwSqlSelect();

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                                 usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                qry.AddColumn("estrentto", false, FwDataTypes.Date);
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("  from receiveonsetorderview with (nolock)");
                select.Add(" where warehouseid  = @warehouseid");
                select.Add("   and departmentid = @departmentid");
                if (!request.showall)
                {
                    select.Add("   and dealid      = @dealid");
                }
                if (!string.IsNullOrEmpty(request.searchvalue))
                {
                    switch ((string)request.searchmode)
                    {
                        case "SETNO":
                            select.Add("and setno like @setno");
                            select.AddParameter("@setno", request.searchvalue + "%");
                            break;
                        case "SETCHARACTER":
                            select.Add("and orderdesc like @orderdesc");
                            select.AddParameter("@orderdesc", request.searchvalue + "%");
                            break;
                        case "STATUS":
                            select.Add("and status like @status");
                            select.AddParameter("@status", request.searchvalue + "%");
                            break;
                    }
                }
                select.Add("order by orderdate desc, orderno desc");
                select.AddParameter("@warehouseid", userLocation.warehouseId);
                select.AddParameter("@departmentid", request.departmentid);
                select.AddParameter("@dealid", request.dealid);

                response.searchresults = await qry.QueryToFwJsonTableAsync(select, true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task NewOrder(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;
                FwSqlCommand qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                response.record = new ExpandoObject();

                userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);


                using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.receiveonsetneworder", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@poid", request.poid);
                    qry.AddParameter("@dealid", request.production);
                    qry.AddParameter("@orderdesc", ((request.description != "") ? request.description : "N/A"));
                    qry.AddParameter("@estrentfrom", request.eststartdate);
                    qry.AddParameter("@estrentto", request.estenddate);
                    qry.AddParameter("@webusersid", session.security.webUser.webusersid);
                    qry.AddParameter("@location", userLocation.locationId);
                    qry.AddParameter("@setnoid", request.setno);
                    qry.AddParameter("@orderid", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                    await qry.ExecuteAsync();
                    response.record.orderid = qry.GetParameter("@orderid").ToString();
                    response.record.errno = qry.GetParameter("@errno").ToString();
                    response.record.errmsg = qry.GetParameter("@errmsg").ToString();
                }

                if (response.record.errno == "0")
                {
                    qry2.Add("select *");
                    qry2.Add("from   receiveonsetorderview");
                    qry2.Add("where orderid = @orderid");
                    qry2.AddParameter("@orderid", response.record.orderid);
                    response.record = await qry2.QueryToDynamicList2Async();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadItems(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation;
                FwSqlCommand qry;

                userLocation = this.AppData.GetUserLocationAsync(conn: conn,
                                                         usersId: session.security.webUser.usersid);

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("from   dbo.funcreceiveonsetpoitem(@orderid, @outreceivecontractid)");
                qry.Add("where  warehouseid = @warehouseid");
                //if (request.mode == "REMAINING")
                //{
                //    qry.Add("and qtyremaining > 0");
                //}
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@outreceivecontractid", request.receivecontractid);
                qry.AddParameter("@warehouseid", userLocation.warehouseId);

                response.items = await qry.QueryToDynamicList2Async(); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task POReceive(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry, qry2;
            response.receive = new ExpandoObject();
            string barcode  = string.Empty,
                   location = string.Empty;

            if (FwValidate.IsPropertyDefined(request, "barcode"))  barcode  = request.barcode;
            if (FwValidate.IsPropertyDefined(request, "location")) location = request.location;

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                qry = new FwSqlCommand(conn, "dbo.receiveonsetporeceive", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", request.orderid);
                qry.AddParameter("@masteritemid", request.recorddata.masteritemid);
                qry.AddParameter("@barcode", barcode);
                qry.AddParameter("@qty", request.qty);
                qry.AddParameter("@usersid", session.security.webUser.usersid);
                qry.AddParameter("@location", location.ToUpper());
                qry.AddParameter("@receivecontractid", SqlDbType.Char, ParameterDirection.InputOutput, request.receivecontractid);
                qry.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await qry.ExecuteAsync();
                response.receive.receivecontractid = qry.GetParameter("@receivecontractid").ToString();
                response.receive.errno = qry.GetParameter("@errno").ToString();
                response.receive.errmsg = qry.GetParameter("@errmsg").ToString();

                if (FwValidate.IsPropertyDefined(request, "assetsetlocations"))
                {
                    for (int i = 0; i < request.assetsetlocations.Length; i++)
                    {
                        qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                        qry2.Add("insert into assetlocation (datestamp, internalchar, orderid, masterid, location, qty)");
                        qry2.Add("values (getdate(), dbo.funcgetinternalchar(), @orderid, @masterid, @location, @qty)");
                        qry2.AddParameter("@orderid", request.selectedorderid);
                        qry2.AddParameter("@masterid", request.recorddata.masterid);
                        qry2.AddParameter("@location", ((string)request.assetsetlocations[i].location).ToUpper());
                        qry2.AddParameter("@qty", request.assetsetlocations[i].qty);
                        await qry2.ExecuteAsync();
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task POReceiveImage(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string[] images;
                byte[] image;
                bool hasImages;

                hasImages = FwValidate.IsPropertyDefined(request, "images");
                if (hasImages && (request.images.Length > 0))
                {
                    images = (string[])request.images;
                    for (int i = 0; i < images.Length; i++)
                    {
                        image = Convert.FromBase64String(images[i]);
                        await ReceiveOnSetPOReceiveImageAsync(conn: conn,
                                                   poid: request.poid,
                                                   masteritemid: request.masteritemid,
                                                   barcode: request.barcode,
                                                   image: image);
                    }
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        private async Task ReceiveOnSetPOReceiveImageAsync(FwSqlConnection conn, string poid, string masteritemid, string barcode, byte[] image)
        {
            int width  = 0,
                height = 0;

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.receiveonsetporeceiveimage", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@poid", poid);
                qry.AddParameter("@masteritemid", masteritemid);
                qry.AddParameter("@barcode", barcode);
                qry.AddParameter("@image", FwGraphics.ResizeAndConvertToJpg(image, ref width, ref height));
                qry.AddParameter("@thumbnail", FwGraphics.GetJpgThumbnail(image));
                qry.AddParameter("@width", width);
                qry.AddParameter("@height", height);
                await qry.ExecuteAsync();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CreateContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry, qry2, qry3;
                response.contract = new ExpandoObject();
                byte[] image;
                int width = 0,
                    height = 0;

                image = Convert.FromBase64String(request.signatureimage);

                qry = new FwSqlCommand(conn, "dbo.receiveonsetcreatecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@poid", request.poid);
                qry.AddParameter("@receivecontractid", request.receivecontractid);
                qry.AddParameter("@orderid", request.orderid);
                qry.AddParameter("@signatureimage", FwGraphics.ResizeAndConvertToJpg(image, ref width, ref height));
                qry.AddParameter("@signaturethumbnail", FwGraphics.GetJpgThumbnail(image));
                qry.AddParameter("@signatureheight", height);
                qry.AddParameter("@signaturewidth", width);
                qry.AddParameter("@usersid", session.security.webUser.usersid);
                qry.AddParameter("@outcontractid", SqlDbType.Char, ParameterDirection.Output);
                qry.AddParameter("@errno", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
                await qry.ExecuteAsync();
                response.contract.outcontractid = qry.GetParameter("@outcontractid").ToString();
                response.contract.errno = qry.GetParameter("@errno").ToString();
                response.contract.errmsg = qry.GetParameter("@errmsg").ToString();

                qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry2.Add("select contractno");
                qry2.Add("  from contract with (nolock)");
                qry2.Add(" where contractid = @outcontractid");
                qry2.AddParameter("@outcontractid", response.contract.outcontractid);
                await qry2.ExecuteAsync();
                response.contract.outcontractno = qry2.GetField("contractno").ToString();

                qry3 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry3.Add("select contractno");
                qry3.Add("  from contract with (nolock)");
                qry3.Add(" where contractid = @receivecontractid");
                qry3.AddParameter("@receivecontractid", request.receivecontractid);
                await qry3.ExecuteAsync();
                response.contract.receivecontractno = qry3.GetField("contractno").ToString(); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
