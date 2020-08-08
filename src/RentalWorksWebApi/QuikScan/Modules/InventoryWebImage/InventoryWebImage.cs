using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class InventoryWebImage : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public InventoryWebImage(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task AddInventoryWebImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "AddInventoryWebImage";
            List<object> images;
            byte[] image;
            bool hasImages;
            string appimageid, uniqueid1, imagedescription = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                hasImages = FwValidate.IsPropertyDefined(request, "images");

                if (hasImages && (request.images.Count > 0))
                {
                    images = (List<object>)request.images;
                    for (int i = 0; i < images.Count; i++)
                    {
                        image = Convert.FromBase64String(images[i].ToString());
                        if (request.mode == "icode")
                        {
                            uniqueid1 = request.item.masterId;
                            if ((request.item.itemclass == "W") || (request.item.itemclass == "S"))
                            {
                                uniqueid1 = await this.AppData.GetLastSetImageIdAsync(conn, request.item.masterId);
                                imagedescription = "APPDOCUMENT_IMAGE";
                            }
                            appimageid = await FwSqlData.InsertAppImageAsync(conn: conn,
                                                                  dbConfig: this.ApplicationConfig.DatabaseSettings,
                                                                  uniqueid1: uniqueid1,
                                                                  uniqueid2: string.Empty,
                                                                  uniqueid3: string.Empty,
                                                                  description: imagedescription,
                                                                  rectype: string.Empty,
                                                                  extension: "JPG",
                                                                  image: image);
                        }
                        else if (request.mode == "barcode")
                        {
                            await FwSqlData.InsertAppImageAsync(conn: conn,
                                                     dbConfig: this.ApplicationConfig.DatabaseSettings,
                                                     uniqueid1: request.item.rentalitemid,
                                                     uniqueid2: string.Empty,
                                                     uniqueid3: string.Empty,
                                                     description: imagedescription,
                                                     rectype: string.Empty,
                                                     extension: "JPG",
                                                     image: Convert.FromBase64String(images[0].ToString()));
                        }
                    }
                }

                response.images = new ExpandoObject();
                response.images.icode = await LoadICodeImagesAsync(request.item);
                if (!request.item.isICode) { response.images.barcode = await LoadBarcodeImagesAsync(request.item); } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetInventoryItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.item = await this.AppData.WebGetItemStatusAsync(conn: conn,
                                                                   usersId: session.security.webUser.usersid,
                                                                   barcode: request.code);
                if (response.item.status == 401)
                {
                    response.item.status = 0;
                    response.item.genericError = string.Empty;
                    response.item.msg = string.Empty;
                }

                response.item.images = new ExpandoObject();
                response.item.images.icode = await LoadICodeImagesAsync(response.item);
                if (!response.item.isICode) { response.item.images.barcode = await LoadBarcodeImagesAsync(response.item); } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task DeleteAppImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "DeleteAppImage";
            FwSqlCommand qry;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "appimageid");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("delete appimage");
                qry.Add("where appimageid = @appimageid");
                qry.AddParameter("@appimageid", FwCryptography.AjaxDecrypt(request.appimageid));
                await qry.ExecuteNonQueryAsync();

                response.images = new ExpandoObject();
                response.images.icode = await LoadICodeImagesAsync(request.item);
                if (!request.item.isICode) { response.images.barcode = await LoadBarcodeImagesAsync(request.item); } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task MakePrimaryAppImage(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "MakePrimaryAppImage";
            FwSqlCommand sp;
            string uniqueid1;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "appimageid");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                uniqueid1 = (request.mode == "icode") ? request.item.masterId : request.item.rentalitemid;
                if ((request.mode == "icode") && ((request.item.itemclass == "W") || (request.item.itemclass == "S")))
                {
                    uniqueid1 = await this.AppData.GetLastSetImageIdAsync(conn, request.item.masterId);
                }

                sp = new FwSqlCommand(conn, "dbo.moveappimage", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@uniqueid1", uniqueid1);
                sp.AddParameter("@uniqueid2", "");
                sp.AddParameter("@uniqueid3", "");
                sp.AddParameter("@description", "");
                sp.AddParameter("@rectype", "");
                sp.AddParameter("@appimageid", FwCryptography.AjaxDecrypt(request.appimageid));
                sp.AddParameter("@toindex", "0");
                await sp.ExecuteNonQueryAsync();

                response.images = new ExpandoObject();
                response.images.icode = await LoadICodeImagesAsync(request.item);
                if (!request.item.isICode) { response.images.barcode = await LoadBarcodeImagesAsync(request.item); } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> LoadICodeImagesAsync(dynamic item)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string uniqueid1, imagedescription = string.Empty;
                dynamic response = new ExpandoObject();

                uniqueid1 = item.masterId;
                if ((item.itemclass == "W") || (item.itemclass == "S"))
                {
                    uniqueid1 = await this.AppData.GetLastSetImageIdAsync(conn, item.masterId);
                    imagedescription = "APPDOCUMENT_IMAGE";
                }

                response = await GetAppImageThumbnailsAsync(uniqueid1: uniqueid1,
                                                 uniqueid2: string.Empty,
                                                 uniqueid3: string.Empty,
                                                 description: imagedescription,
                                                 rectype: string.Empty);

                return response; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> LoadBarcodeImagesAsync(dynamic item)
        {
            dynamic response = new ExpandoObject();

            response = await GetAppImageThumbnailsAsync(uniqueid1:   item.rentalitemid,
                                             uniqueid2:   string.Empty,
                                             uniqueid3:   string.Empty,
                                             description: "",
                                             rectype:     string.Empty);

            return response;
        }
        //----------------------------------------------------------------------------------------------------
        private async Task<dynamic> GetAppImageThumbnailsAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand qry;
                FwJsonDataTable dt;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.AddColumn("thumbnail", false, FwDataTypes.JpgDataUrl);
                qry.Add("select appimageid, thumbnail");
                qry.Add("from appimage with (nolock)");
                qry.Add("where uniqueid1   = @uniqueid1");
                qry.Add("  and uniqueid2   = @uniqueid2");
                qry.Add("  and uniqueid3   = @uniqueid3");
                qry.Add("  and description = @description");
                qry.Add("  and rectype     = @rectype");
                qry.Add("order by orderby");
                qry.AddParameter("@uniqueid1", uniqueid1);
                qry.AddParameter("@uniqueid2", uniqueid2);
                qry.AddParameter("@uniqueid3", uniqueid3);
                qry.AddParameter("@description", description);
                qry.AddParameter("@rectype", rectype);
                dt = await qry.QueryToFwJsonTableAsync();
                result = new ExpandoObject[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result[i] = new ExpandoObject();
                    result[i].appimageid = FwCryptography.AjaxEncrypt(dt.GetValue(i, "appimageid").ToString().TrimEnd());
                    result[i].thumbnail = dt.GetValue(i, "thumbnail").ToString();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
