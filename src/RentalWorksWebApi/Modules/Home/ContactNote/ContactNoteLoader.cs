using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.ContactNote
{
    [FwSqlTable("contactnoteview")]
    public class ContactNoteLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactnoteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ContactNoteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text)]
        public string CompanyContactId { get; set; }
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
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ContactId", "contactid", select, request); 
            addFilterToSelect("CompanyId", "companyid", select, request); 
            addFilterToSelect("CompanyContactId", "compcontactid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}