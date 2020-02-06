using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace FwStandard.Grids.AppDocument
{
    [FwLogic(Id: "zuFWMVYo0LqlY")]
    public class AppDocumentLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected AppDocumentRecord appDocument = new AppDocumentRecord();
        //protected AppDocumentLoader appDocumentLoader = new AppDocumentLoader();
        public AppDocumentLogic() : base()
        {
            this.ForceSave = true;
            dataRecords.Add(appDocument);
            //dataLoader = appDocumentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "D4onPEMcc5rU", IsPrimaryKey: true)]
        public string DocumentId { get { return appDocument.AppDocumentId; } set { appDocument.AppDocumentId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mO9IHWbp0gHx")]
        public string DocumentTypeId { get { return appDocument.DocumentTypeId; } set { appDocument.DocumentTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "6p04SUJjZ0SR8")]
        //public string UniqueId1 { get { return appDocument.UniqueId1; } set { appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "HQ5X1e02ZOflD")]
        //public string UniqueId2 { get { return appDocument.UniqueId2; } set { appDocument.UniqueId2 = value; } }
        //------------------------------------------------------------------------------------ 
        //[FwLogicProperty(Id: "fIGSqv9uXCVv")]
        //public int? UniqueId1Int { get { return appDocument.UniqueId1Int; } set { appDocument.UniqueId1Int = value; } }
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
                    // delete any existing images
                    await appImageLogic.DeleteAsync(this.appDocument.UniqueId1, this.appDocument.UniqueId2, "");
                }

                // add the new image
                await appImageLogic.AddAsync(appDocument.UniqueId1, appDocument.UniqueId2, string.Empty, string.Empty, this.Extension, "F", this.FileDataUrl);
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
            bool hasImage = false;
            bool hasFile = false;
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
                        hasImage = true;
                        imageDescription = "APPDOCUMENT_IMAGE";
                        break;
                    default:
                        hasFile = true;
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
                            cmd.AddParameter("@hasimage", hasImage);
                            cmd.AddParameter("@hasfile", hasFile);
                            cmd.AddParameter("@fileappimageid", appimageid);
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
                            Image sizingImage = Bitmap.FromStream(stream);
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
    }
}
