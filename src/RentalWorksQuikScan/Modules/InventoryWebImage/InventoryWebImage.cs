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
            string appimageid, uniqueid1, imagedescription = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            hasImages = FwValidate.IsPropertyDefined(request, "images");

            if (hasImages && (request.images.Length > 0))
            {
                images = (string[])request.images;
                for (int i = 0; i < images.Length; i++)
                {
                    image = Convert.FromBase64String(images[i]);
                    if (request.mode == "icode")
                    {
                        uniqueid1 = request.item.masterId;
                        if ((request.item.itemclass == "W") || (request.item.itemclass == "S"))
                        {
                            uniqueid1 = RwAppData.GetLastSetImageId(FwSqlConnection.RentalWorks, request.item.masterId);
                            imagedescription = "APPDOCUMENT_IMAGE";
                        }
                        appimageid = FwSqlData.InsertAppImage(conn:        FwSqlConnection.RentalWorks,
                                                              uniqueid1:   uniqueid1,
                                                              uniqueid2:   string.Empty,
                                                              uniqueid3:   string.Empty,
                                                              description: imagedescription,
                                                              rectype:     string.Empty,
                                                              extension:   "JPG",
                                                              image:       image);
                    }
                    else if (request.mode == "barcode")
                    {
                        FwSqlData.InsertAppImage(conn:        FwSqlConnection.RentalWorks,
                                                 uniqueid1:   request.item.rentalitemid,
                                                 uniqueid2:   string.Empty,
                                                 uniqueid3:   string.Empty,
                                                 description: imagedescription,
                                                 rectype:     string.Empty,
                                                 extension:   "JPG",
                                                 image:       Convert.FromBase64String(images[0]));
                    }
                }
            }

            response.images       = new ExpandoObject();
            response.images.icode = LoadICodeImages(request.item);
            if (!request.item.isICode) { response.images.barcode = LoadBarcodeImages(request.item); }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetInventoryItem(dynamic request, dynamic response, dynamic session)
        {
            response.item = RwAppData.WebGetItemStatus(conn:    FwSqlConnection.RentalWorks,
                                                       usersId: session.security.webUser.usersid,
                                                       barcode: request.code);
            if (response.item.status == 401)
            {
                response.item.status       = 0;
                response.item.genericError = string.Empty;
                response.item.msg          = string.Empty;
            }

            response.item.images       = new ExpandoObject();
            response.item.images.icode = LoadICodeImages(response.item);
            if (!response.item.isICode) { response.item.images.barcode = LoadBarcodeImages(response.item); }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void DeleteAppImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "DeleteAppImage";
            FwSqlCommand qry;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "appimageid");

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("delete appimage");
            qry.Add("where appimageid = @appimageid");
            qry.AddParameter("@appimageid", FwCryptography.AjaxDecrypt(request.appimageid));
            qry.Execute();

            response.images       = new ExpandoObject();
            response.images.icode = LoadICodeImages(request.item);
            if (!request.item.isICode) { response.images.barcode = LoadBarcodeImages(request.item); }
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

            uniqueid1 = (request.mode == "icode") ? request.item.masterId : request.item.rentalitemid;
            if ((request.mode == "icode") && ((request.item.itemclass == "W") || (request.item.itemclass == "S")))
            {
                uniqueid1 = RwAppData.GetLastSetImageId(FwSqlConnection.RentalWorks, request.item.masterId);
            }

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.moveappimage");
            sp.AddParameter("@uniqueid1",   uniqueid1);
            sp.AddParameter("@uniqueid2",   "");
            sp.AddParameter("@uniqueid3",   "");
            sp.AddParameter("@description", "");
            sp.AddParameter("@rectype",     "");
            sp.AddParameter("@appimageid",  FwCryptography.AjaxDecrypt(request.appimageid));
            sp.AddParameter("@toindex",     "0");
            sp.Execute();

            response.images       = new ExpandoObject();
            response.images.icode = LoadICodeImages(request.item);
            if (!request.item.isICode) { response.images.barcode = LoadBarcodeImages(request.item); }
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic LoadICodeImages(dynamic item)
        {
            string uniqueid1, imagedescription = string.Empty;
            dynamic response = new ExpandoObject();

            uniqueid1 = item.masterId;
            if ((item.itemclass == "W") || (item.itemclass == "S"))
            {
                uniqueid1 = RwAppData.GetLastSetImageId(FwSqlConnection.RentalWorks, item.masterId);
                imagedescription = "APPDOCUMENT_IMAGE";
            }

            response = GetAppImageThumbnails(uniqueid1:   uniqueid1,
                                             uniqueid2:   string.Empty,
                                             uniqueid3:   string.Empty,
                                             description: imagedescription,
                                             rectype:     string.Empty);

            return response;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic LoadBarcodeImages(dynamic item)
        {
            dynamic response = new ExpandoObject();

            response = GetAppImageThumbnails(uniqueid1:   item.rentalitemid,
                                             uniqueid2:   string.Empty,
                                             uniqueid3:   string.Empty,
                                             description: "",
                                             rectype:     string.Empty);

            return response;
        }
        //----------------------------------------------------------------------------------------------------
        private static dynamic GetAppImageThumbnails(string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            dynamic result;
            FwSqlCommand qry;
            DataTable dt;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select appimageid, thumbnail");
            qry.Add("from appimage with (nolock)");
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
                result[i].appimageid = FwCryptography.AjaxEncrypt(new FwDatabaseField(dt.Rows[i]["appimageid"]).ToString().TrimEnd());
                result[i].thumbnail  = new FwDatabaseField(dt.Rows[i]["thumbnail"]).ToBase64String();
            }
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
