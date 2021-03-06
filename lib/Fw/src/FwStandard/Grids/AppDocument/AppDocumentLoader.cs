using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using FwStandard.Utilities;

namespace FwStandard.Grids.AppDocument
{
    [FwSqlTable("appdocumentview")]
    public class AppDocumentLoader : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appdocumentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DocumentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documenttypeid", modeltype: FwDataTypes.Text)]
        public string DocumentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1int", modeltype: FwDataTypes.Integer)]
        public int? UniqueId1Int { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extension", modeltype: FwDataTypes.Text)]
        public string FileExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extension", modeltype: FwDataTypes.Text)]
        public string Extension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachdate", modeltype: FwDataTypes.Date)]
        public string AttachDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtime", modeltype: FwDataTypes.Time)]
        public string AttachTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documenttype", modeltype: FwDataTypes.Text)]
        public string DocumentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "docimage", modeltype: FwDataTypes.JpgDataUrl)]
        public string DocImage { get; set; }
        //------------------------------------------------------------------------------------ 
        public string FileDataUrl
        {
            get
            {
                string fileDataUrl = "";
                if (!string.IsNullOrEmpty(DocImage))
                {
                    fileDataUrl = "data:" + FwMimeTypeTranslator.GetMimeTypeFromExtension(Extension) + "," + DocImage.Replace("data:image/jpg;base64,", "");   // replace the assumed image/jpg with the actual mime type
                }
                return fileDataUrl;
            }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text)]
        public string InputBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasimage", modeltype: FwDataTypes.Boolean)]
        public bool? HasImages { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasfile", modeltype: FwDataTypes.Boolean)]
        public bool? HasFile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtoemail", modeltype: FwDataTypes.Boolean)]
        public bool? AttachToEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("UniqueId1", "uniqueid1", select, request);
            this.addFilterToSelect("UniqueId2", "uniqueid2", select, request);
            this.addFilterToSelect("UniqueId3", "uniqueid3", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
