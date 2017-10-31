using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
namespace RentalWorksWebApi.Modules..AppDocument 
{ 
[FwSqlTable("appdocument")] 
public class AppDocumentRecord : RwDataReadWriteRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "appdocumentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)] 
public string AppDocumentId { get; set; } = ""; 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attachdate", modeltype: FwDataTypes.Date, sqltype: "datetime")] 
public string Attachdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)] 
public string Description { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string Uniqueid1 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string Uniqueid2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attachtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string Attachtime { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "documenttypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string DocumenttypeId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)] 
public string InputbyusersId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "uniqueid1int", modeltype: FwDataTypes.Integer, sqltype: "int")] 
public int? Uniqueid1int { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "episodes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)] 
public string Episodes { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool Internalchar { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer, sqltype: "int")] 
public int? OrdertranId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool Rectype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attachtoemail", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool Attachtoemail { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool Inactive { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------ 
} 
} 
