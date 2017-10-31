using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic; 
namespace RentalWorksWebApi.Modules..PersonalEvent 
{ 
[FwSqlTable("contactpersonaleventviewweb")] 
public class PersonalEventLoader: RwDataLoadRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "personaleventid", modeltype: FwDataTypes.Text)] 
public string PersonaleventId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)] 
public string ContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contacteventid", modeltype: FwDataTypes.Text)] 
public string ContacteventId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactevent", modeltype: FwDataTypes.Text)] 
public string Contactevent { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "eventdate", modeltype: FwDataTypes.Date)] 
public string Eventdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer)] 
public int? Color { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Boolean)] 
public bool Textcolor { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "recurring", modeltype: FwDataTypes.Boolean)] 
public bool Recurring { get; set; } 
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
