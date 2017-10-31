using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic; 
namespace RentalWorksWebApi.Modules..ContactNote 
{ 
[FwSqlTable("contactnoteviewweb")] 
public class ContactNoteLoader: RwDataLoadRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactnoteid", modeltype: FwDataTypes.Text)] 
public string ContactNoteId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)] 
public string ContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text)] 
public string ComnpanyContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "notedate", modeltype: FwDataTypes.Date)] 
public string NoteDate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "notesbyid", modeltype: FwDataTypes.Text)] 
public string NotesById { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)] 
public string NameFML { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text)] 
public string NotesDescription { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)] 
public string CompanyId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)] 
public string Company { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)] 
public string Notes { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contacttype", modeltype: FwDataTypes.Text)] 
public string ContactType { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "color", modeltype: FwDataTypes.Text)] 
public string Color { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)] 
public bool Inactive { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------ 
protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null) 
{ 
base.SetBaseSelectQuery(select, qry, customFields, request); 
select.Parse(); 
//select.AddWhere("(xxxtype = 'ABCDEF')"); 
//addFilterToSelect("UniqueId", "uniqueid", select, request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
