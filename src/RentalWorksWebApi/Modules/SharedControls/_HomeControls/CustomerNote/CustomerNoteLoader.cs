using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.HomeControls.CustomerNote
{
    [FwSqlTable("customernoteview")]
    public class CustomerNoteLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custnoteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerNoteId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)]
        public string NoteDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesbyid", modeltype: FwDataTypes.Text)]
        public string NotesById { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string NotesBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notify", modeltype: FwDataTypes.Boolean)]
        public bool? Notify { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------

        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("CustomerId", "customerid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}