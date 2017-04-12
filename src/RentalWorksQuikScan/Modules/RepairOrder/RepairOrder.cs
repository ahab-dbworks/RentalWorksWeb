using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;

namespace RentalWorksQuikScan.Modules
{
    class RepairOrder
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetRepairOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetRepairOrder";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "code");
            response.webSelectRepairOrder = RwAppData.WebRepairItem(conn:       FwSqlConnection.RentalWorks
                                                                  , code:       request.code
                                                                  , repairMode: RwAppData.RepairMode.Select
                                                                  , usersId:    session.security.webUser.usersid
                                                                  , qty:        0);
            if (!string.IsNullOrEmpty(response.webSelectRepairOrder.repairId))
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("select top 1 damage");
                    qry.Add("from repair with (nolock)");
                    qry.Add("where repairid = @repairid");
                    qry.AddParameter("@repairid", response.webSelectRepairOrder.repairId);
                    response.repair = qry.QueryToDynamicObject2();
                }
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
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
                    List<dynamic> appdocumentimages = qry.QueryToDynamicList2();
                    string appdocumentid = string.Empty;
                    dynamic appdocument = null;
                    for (int i = 0; i < appdocumentimages.Count; i++)
                    {
                        if (appdocumentid != appdocumentimages[i].appdocumentid)
                        {
                            appdocumentid = appdocumentimages[i].appdocumentid;
                            appdocument = new ExpandoObject();
                            appdocument.appdocumentid = appdocumentimages[i].appdocumentid;
                            appdocument.description   = appdocumentimages[i].description;
                            appdocument.attachdate    = appdocumentimages[i].attachdate;
                            appdocument.attachtime    = appdocumentimages[i].attachtime;
                            appdocument.images = new List<ExpandoObject>();
                            response.appdocuments.Add(appdocument);
                        }
                        if (appdocument != null)
                        {
                            dynamic image = new ExpandoObject();
                            image.appimageid = appdocumentimages[i].appimageid;
                            image.imagedesc  = appdocumentimages[i].imagedesc;
                            image.imageno    = appdocumentimages[i].imageno;
                            image.thumbnail  = appdocumentimages[i].thumbnail;
                            appdocument.images.Add(image);
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UpdateRepairOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "UpdateRepairOrder";
            string documenttypeid, documentdescription;
            string[] images, imagedescriptions, deleteimages;
            //Fw.Json.SqlServer.FwSqlData.WebInsertAppDocumentResponse webInsertAppDocumentResponse = null;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "repairId");
            if ((request.images.Length > 0) && (request.imagedescriptions.Length > 0))
            {
                images              = (string[])request.images;
                documentdescription = request.documentdescription;
                imagedescriptions   = (string[])request.imagedescriptions;
                documenttypeid = FwSqlData.GetDocumentTypeId(FwSqlConnection.RentalWorks, "IMAGE");
                if (string.IsNullOrEmpty(documenttypeid))
                {
                    documenttypeid = FwSqlData.GetNextId(FwSqlConnection.RentalWorks);
                    FwSqlData.InsertDocumentType(conn:              FwSqlConnection.RentalWorks
                                               , documenttypeid:    documenttypeid
                                               , documenttype:      "IMAGE"
                                               , rowtype:           ""
                                               , floorplan:         false
                                               , videos:            false
                                               , panoramic:         false
                                               , autoattachtoemail: false);
                    documenttypeid = FwSqlData.GetDocumentTypeId(FwSqlConnection.RentalWorks, "IMAGE");
                    if (string.IsNullOrEmpty(documenttypeid))
                    {
                        throw new Exception("Unable to create documentype: IMAGE");
                    }
                }
                using(FwSqlConnection conn = new FwSqlConnection(FwSqlConnection.AppDatabase))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                    {
                        string appdocumentid = FwSqlData.GetNextId(conn, transaction);
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
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
                            cmd.ExecuteNonQuery(false);
                        }
                        for (int i = 0; i < images.Length; i++)
                        {
                            string appimageid = FwSqlData.GetNextId(conn, transaction);
                            int width=0, height=0;
                            byte[] image = Convert.FromBase64String(images[i]);
                            using (FwSqlCommand cmd = new FwSqlCommand(conn))
                            {
                                cmd.Transaction = transaction;
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
                                cmd.AddParameter("@appimageid",  appimageid);
                                cmd.AddParameter("@uniqueid1",   appdocumentid);
                                cmd.AddParameter("@uniqueid2",   "");
                                cmd.AddParameter("@uniqueid3",   "");
                                cmd.AddParameter("@description", "APPDOCUMENT_IMAGE");
                                cmd.AddParameter("@image",       FwGraphics.ConvertToJpg(image, ref width, ref height));
                                cmd.AddParameter("@thumbnail",   FwGraphics.GetJpgThumbnail(image));
                                cmd.AddParameter("@height",      height);
                                cmd.AddParameter("@width",       width);
                                cmd.AddParameter("@rectype",     "");
                                cmd.AddParameter("@extension",   "JPG");
                                cmd.AddParameter("@imagedesc",   imagedescriptions[i]);
                                cmd.AddParameter("@imageno",     "");
                                cmd.ExecuteNonQuery(false);
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("update repair");
                qry.Add("set damage = @damage");
                qry.Add("where repairid = @repairid");
                qry.AddParameter("@damage", request.damage);
                qry.AddParameter("@repairid", request.repairId);
                qry.ExecuteNonQuery();
            }
            if (request.deleteimages.Length > 0) {
                deleteimages = (string[])request.deleteimages;
                for (int i = 0; i < deleteimages.Length; i++)
                {
                    string appdocumentid = deleteimages[i];
                    using (FwSqlConnection conn = new FwSqlConnection(FwDatabases.RentalWorks))
                    {
                        conn.Open();
                        using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                        {
                            using (FwSqlCommand qry = new FwSqlCommand(conn))
                            {
                                qry.Transaction = transaction;
                                qry.Add("delete appdocument");
                                qry.Add("where appdocumentid = @appdocumentid");
                                qry.AddParameter("@appdocumentid", appdocumentid);
                                qry.ExecuteNonQuery(false);
                            }
                            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.deleteappimage"))
                            {
                                qry.Transaction = transaction;
                                qry.AddParameter("@uniqueid1", appdocumentid);
                                qry.AddParameter("@uniqueid2", "");
                                qry.AddParameter("@uniqueid3", "");
                                qry.AddParameter("@description", "APPDOCUMENT_IMAGE");
                                qry.AddParameter("@appimageid", "");
                                qry.AddParameter("@rectype", "");
                                qry.ExecuteNonQuery(false);
                            }
                            transaction.Commit();
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetRepairOrders(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RepairOrder.GetRepairOrders";
            string masterno = string.Empty;
            string orderno = string.Empty;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pageno");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pagesize");
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            session.userLocation   = RwAppData.GetUserLocation(conn, session.security.webUser.usersid);
            if (FwValidate.IsPropertyDefined(request, "masterno"))
            {
                masterno = request.masterno;
            }
            if (FwValidate.IsPropertyDefined(request, "orderno"))
            {
                orderno = request.orderno;
            }
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("repairno", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masterno", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("master", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("barcode", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("damagedeal", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("status", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("statusdate", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
                qry.Add("select top 100 repairno,repairdate,barcode,mfgserial,rfid,qty,masterno,master,damagedeal,pono,status,statusdate,completedby,priority,department,billable,notbilled,outsiderepair,outsiderepairpono,duedate,released,releasedqty,repairitemstatus,transferid,chargeinvoiceid,repairid");
                qry.Add("from repairview rv with (nolock)");
                qry.Add("where rv.status not in ('COMPLETE','VOID')  and (rv.warehouseid = @warehouseid or rv.transferredfromwarehouseid = @warehouseid)");
                qry.Add("order by repairdate desc");
                qry.AddParameter("@warehouseid", session.userLocation.warehouseId);
                response.repairorders = qry.QueryToFwJsonTable(request.pageno, request.pagesize);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
