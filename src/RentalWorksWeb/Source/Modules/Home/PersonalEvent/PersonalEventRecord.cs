using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
namespace RentalWorksWebApi.Modules..PersonalEvent 
{ 
[FwSqlTable("personalevent")] 
public class PersonalEventRecord : RwDataReadWriteRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "personaleventid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)] 
public string PersonalEventId { get; set; } = ""; 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contacteventid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ContacteventId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "eventdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")] 
public string Eventdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string ContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------ 
} 
} 
