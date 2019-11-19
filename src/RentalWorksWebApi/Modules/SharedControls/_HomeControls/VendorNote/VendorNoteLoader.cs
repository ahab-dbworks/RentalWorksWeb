using System.Threading.Tasks;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.HomeControls.VendorNote
{
    [FwSqlTable("vendnoteview")]
    public class VendorNoteLoader : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendnoteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notestbyid", modeltype: FwDataTypes.Text)]
        public string NotesById { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string NotesBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notify", modeltype: FwDataTypes.Boolean)]
        public bool? Notify { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string Datestamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("VendorId", "vendorid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}
