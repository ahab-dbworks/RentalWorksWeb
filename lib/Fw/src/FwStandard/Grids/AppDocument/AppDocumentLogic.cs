using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace FwStandard.Grids.AppDocument
{
    [FwLogic(Id: "zuFWMVYo0LqlY")]
    public class AppDocumentLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected AppDocumentRecord appDocument = new AppDocumentRecord();
        protected AppDocumentLoader appDocumentLoader = new AppDocumentLoader();
        public AppDocumentLogic() : base()
        {
            this.ForceSave = true;
            dataRecords.Add(appDocument);
            dataLoader = appDocumentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D4onPEMcc5rU", IsPrimaryKey: true)]
        public string DocumentId { get { return appDocument.AppDocumentId; } set { appDocument.AppDocumentId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mO9IHWbp0gHx")]
        public string DocumentTypeId { get { return appDocument.DocumentTypeId; } set { appDocument.DocumentTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "6p04SUJjZ0SR8")]
        public string UniqueId1 { get { return appDocument.UniqueId1; } set { appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "HQ5X1e02ZOflD")]
        public string UniqueId2 { get { return appDocument.UniqueId2; } set { appDocument.UniqueId2 = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "fIGSqv9uXCVv")]
        public int? UniqueId1Int { get { return appDocument.UniqueId1Int; } set { appDocument.UniqueId1Int = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "3BuqoMDOQ6zMd", IsRecordTitle: true)]
        public string Description { get { return appDocument.Description; } set { appDocument.Description = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bDYUtAjBDeww")]
        public string InputByUsersId { get { return appDocument.InputByUsersId; } set { appDocument.InputByUsersId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Mnc6EuVdsHPRY")]
        public string AttachDate { get { return appDocument.AttachDate; } set { appDocument.AttachDate = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "J2ZtLCbH5uZHF")]
        public string AttachTime { get { return appDocument.AttachTime; } set { appDocument.AttachTime = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "TFcFiL5IVWUU6")]
        public bool? AttachToEmail { get { return appDocument.AttachToEmail; } set { appDocument.AttachToEmail = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Z9H0vG2lTAL2V")]
        public bool? Inactive { get { return appDocument.Inactive; } set { appDocument.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "frbbeuJWSctH")]
        public string DateStamp { get { return appDocument.DateStamp; } set { appDocument.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 

        // view fields
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D4SRviqFO2oKP", IsReadOnly: true)]
        public string Extension { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "i4bsUte3RNJAn", IsReadOnly: true)]
        public string DocumentType { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "yclBOzCBUoK75", IsReadOnly: true)]
        public string InputBy { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "hZy1MLzHv1g3v", IsReadOnly: true)]
        public bool? HasImage { get; set; } = false;
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ppq4min6dQ5u", IsReadOnly: true)]
        public bool? HasFile { get; set; } = false;


        // Begin AppImageRecord Fields
        [FwLogicProperty(Id: "nPeSSfR12e0X")]
        public bool? FileIsModified { get; set; } = false;

        [FwLogicProperty(Id: "5fuQOWprVsoR")]
        public string FileDataUrl { get; set; } = string.Empty;

        [FwLogicProperty(Id: "IrU0IPX3bPgy")]
        public string FilePath { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        protected override async Task BeforeSaveAsync(BeforeSaveEventArgs e)
        {
            await base.BeforeSaveAsync(e);
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                this.InputByUsersId = this.UserSession.UsersId;
                var date = DateTime.Now;
                this.AttachDate = date.ToString("yyyy-MM-dd");
                this.AttachTime = date.ToString("hh:mm:ss");
            }
        }
        //------------------------------------------------------------------------------------ 
        protected override async Task AfterSaveAsync(AfterSaveEventArgs e)
        {
            await base.AfterSaveAsync(e);
            
            // if the user uploaded a file, then save an appimage
            if (this.FileDataUrl != null && this.FileDataUrl.Length > 0)
            {
                FwAppImageLogic appImageLogic = new FwAppImageLogic(this.AppConfig);
                if (e.SaveMode == TDataRecordSaveMode.smUpdate)
                {
                    // delete any files, since business rules only allow 1 file
                    await appImageLogic.DeleteAsync(this.appDocument.AppDocumentId, string.Empty, string.Empty, "F");
                }

                // add the new image
                await appImageLogic.AddAsync(this.appDocument.AppDocumentId, string.Empty, string.Empty, string.Empty, this.Extension, "F", this.FileDataUrl, e.SqlConnection);
            }
        }
        //------------------------------------------------------------------------------------ 
        public void SetUniqueId1(string value)
        {
            this.appDocument.UniqueId1 = value;
        }
        //------------------------------------------------------------------------------------ 
        public void SetUniqueId2(string value)
        {
            this.appDocument.UniqueId2 = value;
        }
        //------------------------------------------------------------------------------------ 
        public string GetUniqueId1()
        {
            return this.appDocument.UniqueId1;
        }
        //------------------------------------------------------------------------------------ 
        public string GetUniqueId2()
        {
            return this.appDocument.UniqueId2;
        }
        //------------------------------------------------------------------------------------ 
        public enum SaveModes { Insert, Update }
        public async Task SaveAppDocumentImageAsync(SaveModes saveMode)
        {
            FwDateTime datestamp;
            string appimageid = string.Empty;
            string imageDescription = string.Empty;
            string imageRectype = string.Empty;
            bool hasFileOrImage = (this.FileDataUrl != null) && this.FileDataUrl.Length > 0;
            string[] dataUrlParts = null;
            string dataUrlPrefix = null;
            string base64Data = null;
            string imageExtension = null;
            //bool hasImage = false;
            //bool hasFile = false;
            if (hasFileOrImage)
            {
                dataUrlParts = this.FileDataUrl.Split(new char[] { ',' });
                dataUrlPrefix = dataUrlParts[0];
                base64Data = dataUrlParts[1];
                imageExtension = Path.GetExtension(this.FilePath).ToLower().Replace(".", string.Empty);
                switch (imageExtension)
                {
                    case "jpg":
                    case "jpeg":
                    case "gif":
                    case "tif":
                    case "tiff":
                    case "bmp":
                    case "png":
                        //hasImage = true;
                        imageDescription = "APPDOCUMENT_IMAGE";
                        break;
                    default:
                        //hasFile = true;
                        imageRectype = "F";
                        break;
                }
            }
            if (hasFileOrImage && dataUrlParts.Length != 2)
            {
                throw new ArgumentException("Invalid file.  File must be submitted as a dataurl.", "Model.FileDataUrl");
            }
            datestamp = DateTime.UtcNow;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    if (hasFileOrImage)
                    {
                        appimageid = await FwSqlData.GetNextIdAsync(conn, this.AppConfig.DatabaseSettings);
                    }
                    if (saveMode == SaveModes.Insert)
                    {
                        DocumentId = await FwSqlData.GetNextIdAsync(conn, this.AppConfig.DatabaseSettings);
                        // update the attachdate/time on the appdocument
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                        {
                            cmd.Transaction = transaction;
                            cmd.AddParameter("@appdocumentid", this.DocumentId);
                            cmd.AddParameter("@documenttypeid", this.DocumentTypeId);
                            cmd.AddParameter("@uniqueid1", this.GetUniqueId1());
                            cmd.AddParameter("@uniqueid2", this.GetUniqueId2());
                            cmd.AddParameter("@attachdate", this.AttachDate);
                            cmd.AddParameter("@attachtime", this.AttachTime);
                            await cmd.ExecuteInsertQueryAsync("appdocument");
                        }
                    }
                    else if (saveMode == SaveModes.Update)
                    {
                        // update the attachdate/time on the appdocument
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                        {
                            cmd.Transaction = transaction;
                            cmd.AddParameter("@attachdate", this.AttachDate);
                            cmd.AddParameter("@attachtime", this.AttachTime);
                            await cmd.ExecuteUpdateQueryAsync("appdocument", "appdocumentid", this.DocumentId);

                        }
                    }

                    // delete any existing appimages
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Transaction = transaction;
                        cmd.Add("delete appimage");
                        cmd.Add("where uniqueid1 = @uniqueid1");
                        cmd.AddParameter("@uniqueid1", this.DocumentId);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // insert appimage
                    int height = 0;
                    int width = 0;
                    byte[] image = Convert.FromBase64String(base64Data);
                    byte[] thumbnail = new byte[0];
                    if (dataUrlPrefix.StartsWith("data:image/"))
                    {
                        using (MemoryStream stream = new MemoryStream(image))
                        {
                            System.Drawing.Image sizingImage = Bitmap.FromStream(stream);
                            width = sizingImage.Width;
                            height = sizingImage.Height;
                        }
                        thumbnail = FwGraphics.GetJpgThumbnail(image);
                    }
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Transaction = transaction;
                        cmd.AddParameter("@appimageid", appimageid);
                        cmd.AddParameter("@uniqueid1", this.DocumentId);
                        cmd.AddParameter("@uniqueid2", string.Empty);
                        cmd.AddParameter("@uniqueid3", string.Empty);
                        cmd.AddParameter("@description", imageDescription);
                        cmd.AddParameter("@extension", imageExtension.ToUpper());
                        cmd.AddParameter("@rectype", imageRectype);
                        cmd.AddParameter("@height", height);
                        cmd.AddParameter("@width", width);
                        cmd.AddParameter("@thumbnail", thumbnail);
                        cmd.AddParameter("@image", image);
                        cmd.AddParameter("@datestamp", datestamp.GetSqlValue());
                        await cmd.ExecuteInsertQueryAsync("appimage");
                    }
                    transaction.Commit();
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public async Task<GetDocumentThumbnailsResponse> GetThumbnailsAsync(string validateUniqueid1Query, int pageNo, int pageSize)
        {
            int rowNoStart = 1;
            if (pageNo > 1)
            {
                rowNoStart = (pageNo * pageSize) + 1;
            }
            int rowNoEnd = rowNoStart + pageSize;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("DataUrl", false, FwDataTypes.JpgDataUrl);
                    qry.Add(";with");
                    qry.Add("  main_cte as (");
                    qry.Add("    select RowNumber = row_number() over(order by i.datestamp desc),");
                    qry.Add("       ImageId = i.appimageid,");
                    qry.Add("       Description = i.imagedesc,");
                    qry.Add("       ImageNumber = i.imageno,");
                    qry.Add("       DataUrl = i.thumbnail,");
                    qry.Add("       DateStamp = i.datestamp");
                    qry.Add("    from appimage i with(nolock) join appdocument d with (nolock) on (i.uniqueid1 = d.appdocumentid)");
                    qry.Add("    where i.uniqueid1 = @appdocumentid ");
                    qry.Add("      and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                    qry.Add("      and i.description = 'APPDOCUMENT_IMAGE'");
                    qry.Add("  ),");
                    qry.Add("  count_cte as (");
                    qry.Add("    select TotalRows = count(*)");
                    qry.Add("    from main_cte with(nolock)");
                    qry.Add("  ),");
                    qry.Add("  paging_cte as (");
                    qry.Add("    select top(@rownoend) row_number() over(order by DateStamp desc) as PagingCteRowNumber, *");
                    qry.Add("    from main_cte with(nolock), count_cte with(nolock)");
                    qry.Add("  )");
                    qry.Add("select *");
                    qry.Add("from paging_cte with(nolock)");
                    qry.Add("where RowNumber between @rownostart and @rownoend");
                    qry.AddParameter("@appdocumentid", this.DocumentId);
                    qry.AddParameter("@rownostart", rowNoStart);
                    qry.AddParameter("@rownoend", rowNoEnd);
                    var response = new GetDocumentThumbnailsResponse();
                    response.Thumbnails = await qry.QueryToTypedListAsync<DocumentImage>();
                    return response;

                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public async Task<GetDocumentImageResponse> GetImageAsync(string validateUniqueid1Query, string appImageId)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1");
                    qry.Add("  i.appimageid,");
                    qry.Add("  i.imagedesc,");
                    qry.Add("  i.imageno,");
                    qry.Add("  i.image");
                    qry.Add("from appimage i with (nolock) join appdocument d with (nolock) on (i.uniqueid1 = d.appdocumentid)");
                    qry.Add("where i.appimageid = @appimageid");
                    qry.Add("  and i.uniqueid1 = @appdocumentid");
                    qry.Add("  and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                    qry.Add("  and i.description = 'APPDOCUMENT_IMAGE'");
                    qry.Add("order by i.datestamp");
                    qry.AddParameter("@appimageid", appImageId);
                    qry.AddParameter("@appdocumentid", this.DocumentId);
                    GetDocumentImageResponse response = null;
                    using (SqlDataReader reader = await qry.ExecuteReaderAsync())
                    {
                        int colIndexAppImageId = reader.GetOrdinal("appimageid");
                        int colIndexImageDesc = reader.GetOrdinal("imagedesc");
                        int colIndexImageNo = reader.GetOrdinal("imageno");
                        int colIndexImage = reader.GetOrdinal("image");
                        if (await reader.ReadAsync())
                        {
                            response = new GetDocumentImageResponse();
                            response.Image = new DocumentImage();
                            if (!reader.IsDBNull(colIndexAppImageId))
                            {
                                response.Image.ImageId = reader.GetString(colIndexAppImageId);
                            }
                            if (!reader.IsDBNull(colIndexImageDesc))
                            {
                                response.Image.Description = reader.GetString(colIndexImageDesc);
                            }
                            if (!reader.IsDBNull(colIndexImageNo))
                            {
                                response.Image.ImageNumber = reader.GetString(colIndexImageNo);
                            }
                            if (!reader.IsDBNull(colIndexImage))
                            {
                                byte[] buffer = reader.GetSqlBytes(colIndexImage).Value;
                                using (Stream stream = reader.GetSqlBytes(colIndexImage).Stream)
                                {
                                    bool isnull = (buffer.Length == 0) || ((buffer.Length == 1) && (buffer[0] == 255));
                                    if (!isnull)
                                    {
                                        // currently the appimage table doesn't provide this needed info for efficiently serving images on the web
                                        var bitmap = new System.Drawing.Bitmap(stream);
                                        string dataUrl = string.Empty;
                                        string base64data = Convert.ToBase64String(buffer);
                                        if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                                        {
                                            dataUrl = "data:image/bmp;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                                        {
                                            dataUrl = "data:image/jpeg;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                                        {
                                            dataUrl = "data:image/png;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Tiff.Guid)
                                        {
                                            dataUrl = "data:image/tiff;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Exif.Guid)
                                        {
                                            dataUrl = "data:image/jpeg;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                                        {
                                            dataUrl = "data:image/gif;base64," + base64data;
                                        }
                                        else if (bitmap.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Emf.Guid)
                                        {
                                            dataUrl = "data:image/emf;base64," + base64data;
                                        }
                                        response.Image.DataUrl = dataUrl;

                                    }
                                }
                            }
                        }
                    }
                    return response;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> AttachImageAsync(string validateUniqueid1Query, string dataUrl)
        {
            bool result = false;
            string[] dataUrlParts = dataUrl.Split(new char[] { ',' });
            string dataUrlPrefix = dataUrlParts[0];
            string base64Data = dataUrlParts[1];
            byte[] image = Convert.FromBase64String(base64Data);
            result = await this.AttachImageAsync(validateUniqueid1Query, image);
            return result;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> AttachImageAsync(string validateUniqueid1Query, byte[] image)
        {
            bool result = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    var date = DateTime.Now;
                    this.AttachDate = date.ToString("yyyy-MM-dd");
                    this.AttachTime = date.ToString("hh:mm:ss");
                    this.DateStamp = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                    await this.SaveAsync(saveMode: TDataRecordSaveMode.smUpdate);

                    string appImageId = await FwSqlData.GetNextIdAsync(conn, this.AppConfig.DatabaseSettings);

                    // insert appimage
                    int height = 0;
                    int width = 0;
                    byte[] thumbnail = new byte[0];
                    thumbnail = FwGraphics.GetJpgThumbnail(image);
                    image = FwGraphics.ResizeAndConvertToJpg(image);
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Transaction = transaction;
                        cmd.AddParameter("@appimageid", appImageId);
                        cmd.AddParameter("@uniqueid1", this.DocumentId);
                        cmd.AddParameter("@uniqueid2", string.Empty);
                        cmd.AddParameter("@uniqueid3", string.Empty);
                        cmd.AddParameter("@description", "APPDOCUMENT_IMAGE");
                        cmd.AddParameter("@extension", "");
                        cmd.AddParameter("@rectype", "");
                        cmd.AddParameter("@height", height);
                        cmd.AddParameter("@width", width);
                        cmd.AddParameter("@thumbnail", thumbnail);
                        cmd.AddParameter("@image", image);
                        cmd.AddParameter("@datestamp", date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
                        await cmd.ExecuteInsertQueryAsync("appimage");
                        result = cmd.RowCount > 0;
                    }
                    transaction.Commit();
                }
            }
            return result;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> DeleteImageAsync(string validateUniqueid1Query, string appImageId)
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    // delete the appimage
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("delete i");
                        qry.Add("from appimage i join appdocument d on (i.uniqueid1 = d.appdocumentid)");
                        qry.Add("where i.appimageid = @appimageid");
                        qry.Add("  and i.uniqueid1 = @appdocumentid");
                        qry.Add("  and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                        qry.AddParameter("@appimageid", appImageId);
                        qry.AddParameter("@appdocumentid", this.DocumentId);
                        await qry.ExecuteNonQueryAsync();
                        success = qry.RowCount > 0;
                    }

                    // Update the datestamp on the appdocument
                    if (success)
                    { 
                        var date = DateTime.Now;
                        this.AttachDate = date.ToString("yyyy-MM-dd");
                        this.AttachTime = date.ToString("hh:mm:ss");
                        this.DateStamp = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                        using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                        {
                            qry.Add("update appdocument");
                            qry.Add("set attachdate = @attachdate,");
                            qry.Add("    attachtime = @attachtime,");
                            qry.Add("    datestamp = @datestamp");
                            qry.Add("where appdocumentid = @appdocumentid");
                            qry.Add("  and uniqueid1 in (" + validateUniqueid1Query + ")");
                            qry.AddParameter("@appdocumentid", this.DocumentId);
                            qry.AddParameter("@attachdate", this.AttachDate);
                            qry.AddParameter("@attachtime", this.AttachTime);
                            qry.AddParameter("@datestamp", this.DateStamp);
                            await qry.ExecuteNonQueryAsync();
                            success = qry.RowCount > 0;
                        }
                    }
                    if (success)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            return success;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<GetDocumentFileResponse> GetFileAsync(string validateUniqueid1Query)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1");
                    qry.Add("  i.appimageid,");
                    qry.Add("  i.extension,");
                    qry.Add("  i.image");
                    qry.Add("from appimage i with (nolock) join appdocument d with (nolock) on (i.uniqueid1 = d.appdocumentid)");
                    qry.Add("where i.uniqueid1 = @appdocumentid");
                    qry.Add("  and i.rectype = 'F'");
                    qry.Add("  and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                    qry.Add("order by i.datestamp");
                    qry.AddParameter("@appdocumentid", this.DocumentId);
                    GetDocumentFileResponse response = null;
                    using (SqlDataReader reader = await qry.ExecuteReaderAsync())
                    {
                        int colIndexAppImageId = reader.GetOrdinal("appimageid");
                        int colIndexExtension = reader.GetOrdinal("extension");
                        int colIndexImage = reader.GetOrdinal("image");
                        if (await reader.ReadAsync())
                        {
                            response = new GetDocumentFileResponse();
                            response.File = new DocumentFile();
                            if (!reader.IsDBNull(colIndexAppImageId))
                            {
                                response.File.ImageId = reader.GetString(colIndexAppImageId);
                            }
                            if (!reader.IsDBNull(colIndexExtension))
                            {
                                var extension = reader.GetString(colIndexExtension);
                                response.File.Extension = extension.ToUpper();
                                response.File.ContentType = FwMimeTypeTranslator.GetMimeTypeFromExtension(extension);
                            }
                            if (!reader.IsDBNull(colIndexImage))
                            {
                                byte[] buffer = reader.GetSqlBytes(colIndexImage).Value;
                                using (Stream stream = reader.GetSqlBytes(colIndexImage).Stream)
                                {
                                    bool isnull = (buffer.Length == 0) || ((buffer.Length == 1) && (buffer[0] == 255));
                                    if (!isnull)
                                    {
                                        response.File.Data = buffer;
                                    }
                                }
                            }
                        }
                    }
                    return response;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> AttachFileAsync(string validateUniqueid1Query, string dataUrl, string fileExtension)
        {
            bool result = false;
            string[] dataUrlParts = dataUrl.Split(new char[] { ',' });
            string dataUrlPrefix = dataUrlParts[0];
            string base64Data = dataUrlParts[1];
            byte[] image = Convert.FromBase64String(base64Data);
            result = await this.AttachFileAsync(validateUniqueid1Query, image, fileExtension);
            return result;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> AttachFileAsync (string validateUniqueid1Query, byte[] fileData, string fileExtension)
        {
            bool result = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    var date = DateTime.Now;
                    this.AttachDate = date.ToString("yyyy-MM-dd");
                    this.AttachTime = date.ToString("hh:mm:ss");
                    this.DateStamp = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                    await this.SaveAsync(saveMode: TDataRecordSaveMode.smUpdate);

                    // delete any existing file
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("delete i");
                        qry.Add("from appimage i join appdocument d on (i.uniqueid1 = d.appdocumentid)");
                        qry.Add("where i.uniqueid1 = @appdocumentid");
                        qry.Add("  and i.rectype = 'F'");
                        qry.Add("  and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                        qry.AddParameter("@appdocumentid", this.DocumentId);
                        await qry.ExecuteNonQueryAsync();
                    }

                    string appImageId = await FwSqlData.GetNextIdAsync(conn, this.AppConfig.DatabaseSettings);

                    // insert file into appimage
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Transaction = transaction;
                        cmd.AddParameter("@appimageid", appImageId);
                        cmd.AddParameter("@uniqueid1", this.DocumentId);
                        cmd.AddParameter("@uniqueid2", string.Empty);
                        cmd.AddParameter("@uniqueid3", string.Empty);
                        cmd.AddParameter("@description", string.Empty);
                        cmd.AddParameter("@extension", fileExtension.ToUpper());
                        cmd.AddParameter("@rectype", "F");
                        cmd.AddParameter("@height", 0);
                        cmd.AddParameter("@width", 0);
                        cmd.AddParameter("@thumbnail", new byte[0]);
                        cmd.AddParameter("@image", fileData);
                        cmd.AddParameter("@datestamp", date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
                        await cmd.ExecuteInsertQueryAsync("appimage");
                        result = cmd.RowCount > 0;
                    }
                    transaction.Commit();
                }
            }
            return result;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<bool> DeleteFileAsync(string validateUniqueid1Query)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    // delete any existing files or images
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("delete i");
                        qry.Add("from appimage i join appdocument d on (i.uniqueid1 = d.appdocumentid)");
                        qry.Add("where i.uniqueid1 = @appdocumentid");
                        qry.Add("  and i.rectype = 'F'");
                        qry.Add("  and d.uniqueid1 in (" + validateUniqueid1Query + ")");
                        qry.AddParameter("@appdocumentid", this.DocumentId);
                        await qry.ExecuteNonQueryAsync();
                    }

                    // update the appdocument dates
                    using (FwSqlCommand qry = new FwSqlCommand(conn, this.AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        var date = DateTime.Now;
                        qry.Add("update appdocument");
                        qry.Add("set");
                        qry.Add("  attachdate = @attachdate,");
                        qry.Add("  attachtime = @attachtime,");
                        qry.Add("  datestamp = @datestamp");
                        qry.Add("where appdocumentid = @appdocumentid");
                        qry.AddParameter("@appdocumentid", this.DocumentId);
                        qry.AddParameter("@attachdate", date.ToString("yyyy-MM-dd"));
                        qry.AddParameter("@attachtime", date.ToString("hh:mm:ss"));
                        qry.AddParameter("@datestamp", date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
                        await qry.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
            }
            return true;
        }
        //------------------------------------------------------------------------------------ 
    }
}
