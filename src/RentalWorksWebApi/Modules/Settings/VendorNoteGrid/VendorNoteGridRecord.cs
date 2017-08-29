using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.VendorNoteGrid
{
    [FwSqlTable("vendnote")]
    public class VendorNoteGridRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0, isPrimaryKey: true)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendnoteid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string VendorNoteId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notestbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string NotestById { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string NotesDesc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 255)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notify", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1)]
        public string Notify { get; set; }
        //------------------------------------------------------------------------------------
        
    }
}