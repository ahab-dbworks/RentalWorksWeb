using FwStandard.Models;
using FwStandard.Utilities;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.BusinessLogic
{
    public class FwAppImageLogic
    {
        FwApplicationConfig _appConfig;
        //------------------------------------------------------------------------------------
        public FwAppImageLogic(FwApplicationConfig appConfig)
        {
            this._appConfig = appConfig;
        }
        //------------------------------------------------------------------------------------
        public async Task<FwAppImageModel> GetOneAsync(string appimageid, string thumbnail = "false", string uniqueid1 = "", string uniqueid2 = "", string uniqueid3 = "", string orderby = "")
        {
            byte[] image = null;
            using (FwSqlConnection conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 description, extension, orderby");
                    if ((thumbnail != null) && (thumbnail == "true"))
                    {
                        qry.Add(",thumbnail");
                    }
                    else
                    {
                        qry.Add(",image");
                    }
                    qry.Add("from appimage");
                    if (appimageid != null)
                    {
                        qry.Add("where appimageid = @appimageid");
                        qry.AddParameter("@appimageid", appimageid);
                    }
                    else
                    {
                        qry.Add("where uniqueid1 = @uniqueid1");
                        qry.Add("  and uniqueid2 = @uniqueid2");
                        qry.Add("  and uniqueid3 = @uniqueid3");
                        qry.Add("  and orderby   = @orderby");
                        qry.AddParameter("@uniqueid1", uniqueid1);
                        qry.AddParameter("@uniqueid2", uniqueid2);
                        qry.AddParameter("@uniqueid3", uniqueid3);
                        qry.AddParameter("@orderby", orderby);
                    }
                    await qry.ExecuteAsync();
                    FwAppImageModel appimage = null;
                    if (qry.RowCount > 0)
                    {
                        appimage = new FwAppImageModel();
                        if ((thumbnail != null) && (thumbnail == "true"))
                        {
                            image = qry.GetField("thumbnail").ToByteArray();
                        }
                        else
                        {
                            image = qry.GetField("image").ToByteArray();
                        }
                        appimage.Image = image;
                        appimage.Description = FwConvert.StripNonAlphaNumericCharacters(qry.GetField("description").ToString().Trim().ToLower());
                        appimage.Extension = FwConvert.StripNonAlphaNumericCharacters(qry.GetField("extension").ToString().Trim().ToLower());
                        appimage.MimeType = FwMimeTypeTranslator.GetMimeTypeFromExtension(appimage.Extension);
                        appimage.OrderBy = qry.GetField("orderby").ToInt32();
                    }


                    return appimage;
                    //string filename = description + orderby + "." + extension;
                    //if (image != null)
                    //{
                    //    context.Response.Clear();
                    //    context.Response.ContentType = mimetype;
                    //    context.Response.AddHeader("Pragma", "public");
                    //    context.Response.AddHeader("Cache-Control", "max-age=0");
                    //    context.Response.AddHeader("Content-Length", image.Length.ToString());
                    //    context.Response.AddHeader("Content-Disposition", "filename=\"" + filename + "\";");
                    //    context.Response.BinaryWrite(image);
                    //    context.Response.StatusCode = 200;
                    //    context.Response.Flush();
                    //    context.Response.SuppressContent = true;
                    //}
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<List<FwAppImageModel>> GetManyAsync(string uniqueid1, string uniqueid2 = "", string uniqueid3 = "", string description = "", string rectype = "")
        {
            using (FwSqlConnection conn = new FwSqlConnection(_appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, _appConfig.DatabaseSettings.QueryTimeout))
                {
                    uniqueid2 = (uniqueid2 == null) ? "" : uniqueid2;
                    uniqueid3 = (uniqueid3 == null) ? "" : uniqueid3;
                    description = (description == null) ? "" : description;
                    rectype = (rectype == null) ? "" : rectype;
                    qry.Add("select appimageid, datestamp, description, extension, width, height, rectype, orderby");
                    qry.Add("from appimage with (nolock)");
                    qry.Add("where uniqueid1   = @uniqueid1");
                    qry.Add("  and uniqueid2   = @uniqueid2");
                    qry.Add("  and uniqueid3   = @uniqueid3");
                    if (!string.IsNullOrEmpty("description"))
                    {
                        qry.Add("  and description = @description");
                        qry.AddParameter("@description", description);
                    }
                    if (!string.IsNullOrEmpty("rectype"))
                    {
                        qry.Add("  and rectype     = @rectype");
                        qry.AddParameter("@rectype", rectype);
                    }
                    qry.Add("order by orderby, datestamp desc");
                    qry.AddParameter("@uniqueid1", uniqueid1);
                    qry.AddParameter("@uniqueid2", uniqueid2);
                    qry.AddParameter("@uniqueid3", uniqueid3);
                    var dt = await qry.QueryToFwJsonTableAsync();
                    var images = new List<FwAppImageModel>();
                    for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                    {
                        var image = new FwAppImageModel();
                        image.AppImageId = dt.GetValue(rowno, "appimageid").ToString();
                        image.DateStamp = dt.GetValue(rowno, "datestamp").ToShortDateTimeString();
                        image.Description = dt.GetValue(rowno, "description").ToString();
                        image.Extension = dt.GetValue(rowno, "extension").ToString();
                        image.MimeType = FwMimeTypeTranslator.GetMimeTypeFromExtension(image.Extension);
                        image.Width = dt.GetValue(rowno, "width").ToInt32();
                        image.Height = dt.GetValue(rowno, "height").ToInt32();
                        image.RecType = dt.GetValue(rowno, "rectype").ToString();
                        image.OrderBy = dt.GetValue(rowno, "orderby").ToInt32();
                        images.Add(image);
                    }
                    return images;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task AddAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string extension, string rectype, string dataurl, FwSqlConnection conn = null)
        {
            int height = 0;
            int width = 0;

            var dataUrlParts = dataurl.Split(new char[] { ',' });
            if (dataUrlParts.Length != 2) throw new ArgumentException("Missing dataurl prefix.", "imagedataurl");
            var dataUrlPrefix = dataUrlParts[0];
            var base64Data = dataUrlParts[1];
            byte[] image = Convert.FromBase64String(base64Data);
            byte[] thumbnail = new byte[0];
            if (dataUrlPrefix.StartsWith("data:image/"))
            {
                using (MemoryStream stream = new MemoryStream(image))
                {
                    Image sizingImage = Bitmap.FromStream(stream);
                    width = sizingImage.Width;
                    height = sizingImage.Height;
                }
                thumbnail = FwGraphics.GetJpgThumbnail(image);
            }
            FwDateTime datestamp = DateTime.UtcNow;
            if (conn == null)
            {
                conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString);
            }
            // generate appimageid
            var appimageid = await FwSqlData.GetNextIdAsync(conn, this._appConfig.DatabaseSettings);

            // insert appimage
            using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
            {
                cmd.Add("insert into appimage(appimageid, uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, width, height, thumbnail, image, datestamp)");
                cmd.Add("values(@appimageid, @uniqueid1, @uniqueid2, @uniqueid3, @description, @extension, @rectype, @width, @height, @thumbnail, @image, @datestamp)");
                cmd.AddParameter("@appimageid", appimageid);
                cmd.AddParameter("@uniqueid1", uniqueid1);
                cmd.AddParameter("@uniqueid2", uniqueid2);
                cmd.AddParameter("@uniqueid3", uniqueid3);
                cmd.AddParameter("@description", description);
                cmd.AddParameter("@extension", extension);
                cmd.AddParameter("@rectype", rectype);
                cmd.AddParameter("@height", height);
                cmd.AddParameter("@width", width);
                cmd.AddParameter("@thumbnail", thumbnail);
                cmd.AddParameter("@image", image);
                cmd.AddParameter("@datestamp", datestamp.GetSqlValue());
                await cmd.ExecuteNonQueryAsync();
            }
        }
        //------------------------------------------------------------------------------------        
        public async Task DeleteAsync(string appimageid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("delete appimage");
                    cmd.Add("where appimageid = @appimageid");
                    cmd.AddParameter("@appimageid", appimageid);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        //------------------------------------------------------------------------------------        
        public async Task DeleteAsync(string uniqueid1, string uniqueid2, string uniqueid3, string rectype)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("delete appimage");
                    cmd.Add("where uniqueid1 = @uniqueid1");
                    cmd.Add("  and uniqueid2 = @uniqueid2");
                    cmd.Add("  and uniqueid3 = @uniqueid3");
                    cmd.Add("  and rectype = @rectype");
                    cmd.AddParameter("@uniqueid1", uniqueid1);
                    cmd.AddParameter("@uniqueid2", uniqueid2);
                    cmd.AddParameter("@uniqueid3", uniqueid3);
                    cmd.AddParameter("@rectype", rectype);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task RepositionAsync(string appimageid, int orderby)
        {
            //jh wip 08/12/2019 #868
            using (FwSqlConnection conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString))
            {
                string uniqueid1 = "";
                string uniqueid2 = "";
                string uniqueid3 = "";
                using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("select ai.uniqueid1, ai.uniqueid2, ai.uniqueid3");
                    cmd.Add(" from  appimage ai with (nolock)");
                    cmd.Add(" where appimageid = @appimageid");
                    cmd.AddParameter("@appimageid", appimageid);
                    var dt = await cmd.QueryToFwJsonTableAsync();
                    if (dt.Rows.Count > 0)
                    {
                        uniqueid1 = dt.GetValue(0, "uniqueid1").ToString();
                        uniqueid2 = dt.GetValue(0, "uniqueid2").ToString();
                        uniqueid3 = dt.GetValue(0, "uniqueid3").ToString();
                    }
                }

                using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("update appimage");
                    cmd.Add(" set   orderby = @orderby");
                    cmd.Add(" where appimageid = @appimageid");
                    cmd.AddParameter("@appimageid", appimageid);
                    cmd.AddParameter("@orderby", orderby);
                    await cmd.ExecuteNonQueryAsync();
                }

                using (FwSqlCommand cmd = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmd.Add("update ai");
                    cmd.Add(" set   orderby = (select count(*)                      ");
                    cmd.Add("                   from  appimage ai2                  ");
                    cmd.Add("                   where ai2.uniqueid1 =  ai.uniqueid1 ");
                    cmd.Add("                   and   ai2.uniqueid2 =  ai.uniqueid2 ");
                    cmd.Add("                   and   ai2.uniqueid3 =  ai.uniqueid3 ");
                    cmd.Add("                   and   ai2.orderby   <= ai.orderby)  ");
                    cmd.Add(" from  appimage ai");
                    cmd.Add(" where ai.uniqueid1  =  @uniqueid1");
                    cmd.Add(" and   ai.uniqueid2  =  @uniqueid2");
                    cmd.Add(" and   ai.uniqueid3  =  @uniqueid3");
                    cmd.Add(" and   ai.appimageid <> @appimageid");
                    cmd.AddParameter("@uniqueid1", uniqueid1);
                    cmd.AddParameter("@uniqueid2", uniqueid2);
                    cmd.AddParameter("@uniqueid3", uniqueid3);
                    cmd.AddParameter("@appimageid", appimageid);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
