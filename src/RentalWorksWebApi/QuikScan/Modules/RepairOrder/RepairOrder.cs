using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class RepairOrder : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public RepairOrder(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetRepairOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetRepairOrder";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.webSelectRepairOrder = await this.AppData.WebRepairItemAsync(conn: conn
                                                                              , code: request.code
                                                                              , repairMode: RwAppData.RepairMode.Select
                                                                              , usersId: session.security.webUser.usersid
                                                                              , qty: 0);
                if (!string.IsNullOrEmpty(response.webSelectRepairOrder.repairId))
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select top 1 damage");
                        qry.Add("from repair with (nolock)");
                        qry.Add("where repairid = @repairid");
                        qry.AddParameter("@repairid", response.webSelectRepairOrder.repairId);
                        response.repair = await qry.QueryToDynamicObject2Async();
                    }
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select top 10 ad.appdocumentid, ad.description, ad.attachdate, ad.attachtime, ai.appimageid, ai.imagedesc, ai.imageno, ai.thumbnail"); //, ai.image
                        qry.Add("from appdocument ad with (nolock) join appimage     ai with (nolock) on (ad.appdocumentid  = ai.uniqueid1)");
                        qry.Add("                                  join documenttype dt with (nolock) on (ad.documenttypeid = dt.documenttypeid)");
                        qry.Add("where ad.inactive <> 'T'");
                        qry.Add("  and ad.uniqueid1 = @repairid");
                        qry.Add("  and dt.documenttype = 'IMAGE'");
                        qry.Add("  and ai.description = 'APPDOCUMENT_IMAGE'");
                        qry.Add("order by appdocumentid");
                        qry.AddParameter("@repairid", response.webSelectRepairOrder.repairId);
                        //response.images = qry.QueryToDynamicList();
                        response.appdocuments = new List<ExpandoObject>();
                        List<dynamic> appdocumentimages = await qry.QueryToDynamicList2Async();
                        string appdocumentid = string.Empty;
                        dynamic appdocument = null;
                        for (int i = 0; i < appdocumentimages.Count; i++)
                        {
                            if (appdocumentid != appdocumentimages[i].appdocumentid)
                            {
                                appdocumentid = appdocumentimages[i].appdocumentid;
                                appdocument = new ExpandoObject();
                                appdocument.appdocumentid = appdocumentimages[i].appdocumentid;
                                appdocument.description = appdocumentimages[i].description;
                                appdocument.attachdate = appdocumentimages[i].attachdate;
                                appdocument.attachtime = appdocumentimages[i].attachtime;
                                appdocument.images = new List<ExpandoObject>();
                                response.appdocuments.Add(appdocument);
                            }
                            if (appdocument != null)
                            {
                                dynamic image = new ExpandoObject();
                                image.appimageid = appdocumentimages[i].appimageid;
                                image.imagedesc = appdocumentimages[i].imagedesc;
                                image.imageno = appdocumentimages[i].imageno;
                                image.thumbnail = appdocumentimages[i].thumbnail;
                                appdocument.images.Add(image);
                            }
                        }
                    }
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task UpdateRepairOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "UpdateRepairOrder";
            string documenttypeid, documentdescription;
            List<object> images, imagedescriptions, deleteimages;
            //Fw.Json.SqlServer.FwSqlData.WebInsertAppDocumentResponse webInsertAppDocumentResponse = null;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "repairId");
            if ((request.images != null) && (request.images.Count > 0) && (request.imagedescriptions != null) && (request.imagedescriptions.Count > 0))
            {
                images              = (List<object>)request.images;
                documentdescription = request.documentdescription;
                imagedescriptions   = (List<object>)request.imagedescriptions;
                using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
                {
                    documenttypeid = await FwSqlData.GetDocumentTypeIdAsync(conn, this.ApplicationConfig.DatabaseSettings, "IMAGE");
                    if (string.IsNullOrEmpty(documenttypeid))
                    {
                        documenttypeid = await FwSqlData.GetNextIdAsync(conn, this.ApplicationConfig.DatabaseSettings);
                        await FwSqlData.InsertDocumentTypeAsync(conn: conn
                                                   , dbConfig: this.ApplicationConfig.DatabaseSettings
                                                   , documenttypeid: documenttypeid
                                                   , documenttype: "IMAGE"
                                                   , rowtype: ""
                                                   , floorplan: false
                                                   , videos: false
                                                   , panoramic: false
                                                   , autoattachtoemail: false);
                        documenttypeid = await FwSqlData.GetDocumentTypeIdAsync(conn, this.ApplicationConfig.DatabaseSettings, "IMAGE");
                        if (string.IsNullOrEmpty(documenttypeid))
                        {
                            throw new Exception("Unable to create documentype: IMAGE");
                        }
                    } 
                }
                using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
                {
                    await conn.OpenAsync();
                    conn.BeginTransaction();
                    string appdocumentid = await FwSqlData.GetNextIdAsync(conn, this.ApplicationConfig.DatabaseSettings);
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Add("insert into appdocument(appdocumentid  ,");
                        cmd.Add("                        documenttypeid ,");
                        cmd.Add("                        uniqueid1      ,");
                        cmd.Add("                        inputbyusersid ,");
                        cmd.Add("                        description    ,");
                        cmd.Add("                        attachdate     ,");
                        cmd.Add("                        attachtime     ,");
                        cmd.Add("                        datestamp      )");
                        cmd.Add("                 values(@appdocumentid  ,");
                        cmd.Add("                        @documenttypeid ,");
                        cmd.Add("                        @uniqueid1      ,");
                        cmd.Add("                        @inputbyusersid ,");
                        cmd.Add("                        @description    ,");
                        cmd.Add("                        dbo.converttodate(getdate()),");
                        cmd.Add("                        dbo.gettime(getdate()),");
                        cmd.Add("                        getdate())");
                        cmd.AddParameter("@appdocumentid", appdocumentid);
                        cmd.AddParameter("@documenttypeid", documenttypeid);
                        cmd.AddParameter("@uniqueid1", request.repairId);
                        cmd.AddParameter("@inputbyusersid", session.security.webUser.usersid);
                        cmd.AddParameter("@description", documentdescription);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    for (int i = 0; i < images.Count; i++)
                    {
                        string appimageid = await FwSqlData.GetNextIdAsync(conn, this.ApplicationConfig.DatabaseSettings);
                        int width = 0, height = 0;
                        byte[] image = Convert.FromBase64String(images[i].ToString());
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                        {
                            cmd.Add("insert into appimage(appimageid,");
                            cmd.Add("                     uniqueid1,");
                            cmd.Add("                     uniqueid2,");
                            cmd.Add("                     uniqueid3,");
                            cmd.Add("                     description,");
                            cmd.Add("                     image,");
                            cmd.Add("                     thumbnail,");
                            cmd.Add("                     height,");
                            cmd.Add("                     width,");
                            cmd.Add("                     rectype,");
                            cmd.Add("                     extension,");
                            cmd.Add("                     imagedesc,");
                            cmd.Add("                     imageno,");
                            cmd.Add("                     datestamp)");
                            cmd.Add("              values(@appimageid,");
                            cmd.Add("                     @uniqueid1,");
                            cmd.Add("                     @uniqueid2,");
                            cmd.Add("                     @uniqueid3,");
                            cmd.Add("                     @description,");
                            cmd.Add("                     @image,");
                            cmd.Add("                     @thumbnail,");
                            cmd.Add("                     @height,");
                            cmd.Add("                     @width,");
                            cmd.Add("                     @rectype,");
                            cmd.Add("                     @extension,");
                            cmd.Add("                     @imagedesc,");
                            cmd.Add("                     @imageno,");
                            cmd.Add("                     getdate())");
                            cmd.AddParameter("@appimageid", appimageid);
                            cmd.AddParameter("@uniqueid1", appdocumentid);
                            cmd.AddParameter("@uniqueid2", "");
                            cmd.AddParameter("@uniqueid3", "");
                            cmd.AddParameter("@description", "APPDOCUMENT_IMAGE");
                            cmd.AddParameter("@image", FwGraphics.ResizeAndConvertToJpg(image, ref width, ref height));
                            cmd.AddParameter("@thumbnail", FwGraphics.GetJpgThumbnail(image));
                            cmd.AddParameter("@height", height);
                            cmd.AddParameter("@width", width);
                            cmd.AddParameter("@rectype", "");
                            cmd.AddParameter("@extension", "JPG");
                            cmd.AddParameter("@imagedesc", imagedescriptions[i]);
                            cmd.AddParameter("@imageno", "");
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                    conn.CommitTransaction();
                }
            }
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("update repair");
                    qry.Add("set damage = @damage");
                    qry.Add("where repairid = @repairid");
                    qry.AddParameter("@damage", request.damage);
                    qry.AddParameter("@repairid", request.repairId);
                    await qry.ExecuteNonQueryAsync();
                } 
            }
            if (request.deleteimages != null && request.deleteimages.Count > 0) {
                deleteimages = (List<object>)request.deleteimages;
                for (int i = 0; i < deleteimages.Count; i++)
                {
                    string appdocumentid = deleteimages[i].ToString();
                    using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
                    {
                        await conn.OpenAsync();
                        conn.BeginTransaction();
                        using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                        {
                            qry.Add("delete appdocument");
                            qry.Add("where appdocumentid = @appdocumentid");
                            qry.AddParameter("@appdocumentid", appdocumentid);
                            await qry.ExecuteNonQueryAsync();
                        }
                        using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.deleteappimage", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                        {
                            qry.AddParameter("@uniqueid1", appdocumentid);
                            qry.AddParameter("@uniqueid2", "");
                            qry.AddParameter("@uniqueid3", "");
                            qry.AddParameter("@description", "APPDOCUMENT_IMAGE");
                            qry.AddParameter("@appimageid", "");
                            qry.AddParameter("@rectype", "");
                            await qry.ExecuteNonQueryAsync();
                        }
                        conn.CommitTransaction();
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetRepairOrders(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RepairOrder.GetRepairOrders";
            string masterno = string.Empty;
            string orderno = string.Empty;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pageno");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pagesize");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                if (FwValidate.IsPropertyDefined(request, "masterno"))
                {
                    masterno = request.masterno;
                }
                if (FwValidate.IsPropertyDefined(request, "orderno"))
                {
                    orderno = request.orderno;
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("repairno", false, FwDataTypes.Text);
                    qry.AddColumn("masterno", false, FwDataTypes.Text);
                    qry.AddColumn("master", false, FwDataTypes.Text);
                    qry.AddColumn("barcode", false, FwDataTypes.Text);
                    qry.AddColumn("damagedeal", false, FwDataTypes.Text);
                    qry.AddColumn("status", false, FwDataTypes.Text);
                    qry.AddColumn("statusdate", false, FwDataTypes.Date);
                    qry.Add("select top 100 repairno,repairdate,barcode,mfgserial,rfid,qty,masterno,master,damagedeal,pono,status,statusdate,completedby,priority,department,billable,notbilled,outsiderepair,outsiderepairpono,duedate,released,releasedqty,repairitemstatus,transferid,chargeinvoiceid,repairid");
                    qry.Add("from repairview rv with (nolock)");
                    qry.Add("where rv.status not in ('COMPLETE','VOID')  and (rv.warehouseid = @warehouseid or rv.transferredfromwarehouseid = @warehouseid)");
                    qry.Add("order by repairdate desc");
                    qry.AddParameter("@warehouseid", session.userLocation.warehouseId);
                    response.repairorders = await qry.QueryToFwJsonTableAsync(true, request.pageno, request.pagesize);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
