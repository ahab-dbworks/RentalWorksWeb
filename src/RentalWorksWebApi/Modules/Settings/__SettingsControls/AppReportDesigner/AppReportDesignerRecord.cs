using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
namespace WebApi.Modules.Settings.AppReportDesigner 
{ 
[FwSqlTable("appreportdesigner")] 
public class AppReportDesignerRecord : AppDataReadWriteRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "appreportdesignerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)] 
public string AppReportDesignerId { get; set; } = ""; 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "category", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)] 
public string Category { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40)] 
public string Description { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "reportxml", modeltype: FwDataTypes.Text, sqltype: "text")] 
public string Reportxml { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")] 
public bool? Inactive { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------ 
} 
} 
