using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    class InventoryWebImage
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void AddInventoryWebImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "AddInventoryWebImage";
            string[] images;
            byte[] image;
            bool hasImages;
            string appimageid, uniqueid1;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            hasImages = FwValidate.IsPropertyDefined(request, "images");

            uniqueid1 = (!request.item.isICode) ? request.item.rentalitemid : request.item.masterId;

            if (hasImages && (request.images.Length > 0))
            {
                images = (string[])request.images;
                for (int i = 0; i < images.Length; i++)
                {
                    image = Convert.FromBase64String(images[i]);
                    appimageid = FwSqlData.InsertAppImage(conn:        FwSqlConnection.RentalWorks,
                                                          uniqueid1:   uniqueid1,
                                                          uniqueid2:   string.Empty,
                                                          uniqueid3:   string.Empty,
                                                          description: string.Empty,
                                                          rectype:     string.Empty,
                                                          extension:   "JPG",
                                                          image:       image);
                }
            }

            response.appImages = GetAppImageThumbnails(conn:        FwSqlConnection.RentalWorks,
                                                       uniqueid1:   uniqueid1,
                                                       uniqueid2:   string.Empty,
                                                       uniqueid3:   string.Empty,
                                                       description: string.Empty,
                                                       rectype:     string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetInventoryItem(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetInventoryItem";
            string uniqueid1;

            response.webGetItemStatus = RwAppData.WebGetItemStatus(conn:    FwSqlConnection.RentalWorks,
                                                                   usersId: session.security.webUser.usersid,
                                                                   barcode: request.barcode);
            if (response.webGetItemStatus.status == 401)
            {
                response.webGetItemStatus.status       = 0;
                response.webGetItemStatus.genericError = string.Empty;
                response.webGetItemStatus.msg          = string.Empty;
            }

            uniqueid1 = (!response.webGetItemStatus.isICode) ? response.webGetItemStatus.rentalitemid : response.webGetItemStatus.masterId;

            response.appImages = GetAppImageThumbnails(conn:        FwSqlConnection.RentalWorks,
                                                       uniqueid1:   uniqueid1,
                                                       uniqueid2:   string.Empty,
                                                       uniqueid3:   string.Empty,
                                                       description: string.Empty,
                                                       rectype:     string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void DeleteAppImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "DeleteAppImage";
            FwSqlCommand qry;
            string uniqueid1;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "appimageid");

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("delete appimage");
            qry.Add("where appimageid = @appimageid");
            qry.AddParameter("@appimageid", request.appimageid);
            qry.Execute();

            uniqueid1 = (!request.item.isICode) ? request.item.rentalitemid : request.item.masterId;

            response.appImages = GetAppImageThumbnails(conn:        FwSqlConnection.RentalWorks,
                                                       uniqueid1:   uniqueid1,
                                                       uniqueid2:   string.Empty,
                                                       uniqueid3:   string.Empty,
                                                       description: string.Empty,
                                                       rectype:     string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void MakePrimaryAppImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "MakePrimaryAppImage";
            FwSqlCommand sp;
            string uniqueid1;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "appimageid");

            uniqueid1 = (!request.item.isICode) ? request.item.rentalitemid : request.item.masterId;

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.moveappimage");
            sp.AddParameter("@uniqueid1",   uniqueid1);
            sp.AddParameter("@uniqueid2",   "");
            sp.AddParameter("@uniqueid3",   "");
            sp.AddParameter("@description", "");
            sp.AddParameter("@rectype",     "");
            sp.AddParameter("@appimageid",  request.appimageid);
            sp.AddParameter("@toindex",     "0");
            sp.Execute();

            response.appImages = GetAppImageThumbnails(conn:        FwSqlConnection.RentalWorks,
                                                       uniqueid1:   uniqueid1,
                                                       uniqueid2:   string.Empty,
                                                       uniqueid3:   string.Empty,
                                                       description: string.Empty,
                                                       rectype:     string.Empty);
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic GetAppImageThumbnails(FwSqlConnection conn, string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            dynamic result;
            FwSqlCommand qry;
            DataTable dt;

            qry = new FwSqlCommand(conn);
            qry.Add("select appimageid, thumbnail");
            qry.Add("from appimage");
            qry.Add("where uniqueid1   = @uniqueid1");
            qry.Add("  and uniqueid2   = @uniqueid2");
            qry.Add("  and uniqueid3   = @uniqueid3");
            qry.Add("  and description = @description");
            qry.Add("  and rectype     = @rectype");
            qry.Add("order by orderby");
            qry.AddParameter("@uniqueid1",   uniqueid1);
            qry.AddParameter("@uniqueid2",   uniqueid2);
            qry.AddParameter("@uniqueid3",   uniqueid3);
            qry.AddParameter("@description", description);
            qry.AddParameter("@rectype",     rectype);
            dt = qry.QueryToTable();
            result = new ExpandoObject[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i] = new ExpandoObject();
                result[i].appimageid = new FwDatabaseField(dt.Rows[i]["appimageid"]).ToString().TrimEnd();
                result[i].thumbnail  = new FwDatabaseField(dt.Rows[i]["thumbnail"]).ToBase64String();
            }
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
