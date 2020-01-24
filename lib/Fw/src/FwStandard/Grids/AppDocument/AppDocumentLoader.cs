using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace FwStandard.Grids.AppDocument
{
    [FwSqlTable("appdocumentview")]
    public class AppDocumentLoader : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appdocumentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string AppDocumentId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documenttypeid", modeltype: FwDataTypes.Text)]
        public string DocumenttypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string Uniqueid1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text)]
        public string Uniqueid2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1int", modeltype: FwDataTypes.Integer)]
        public int? Uniqueid1int { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text)]
        public string InputbyusersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extension", modeltype: FwDataTypes.Text)]
        public string Extension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachdate", modeltype: FwDataTypes.Date)]
        public string Attachdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtime", modeltype: FwDataTypes.Text)]
        public string Attachtime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documenttype", modeltype: FwDataTypes.Text)]
        public string Documenttype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputby", modeltype: FwDataTypes.Text)]
        public string Inputby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasimage", modeltype: FwDataTypes.Boolean)]
        public bool? Hasimage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasfile", modeltype: FwDataTypes.Boolean)]
        public bool? Hasfile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtoemail", modeltype: FwDataTypes.Boolean)]
        public bool? Attachtoemail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
