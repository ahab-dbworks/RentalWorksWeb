using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic; 
namespace RentalWorksWebApi.Modules.Settings.AppReportDesigner 
{ 
[FwSqlTable("appreportdesignerview")] 
public class AppReportDesignerLoader: RwDataLoadRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "appreportdesignerid", modeltype: FwDataTypes.Text, isPrimaryKey: true)] 
public string AppReportDesignerId { get; set; } = ""; 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)] 
public string Category { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)] 
public string Description { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)] 
public bool? Inactive { get; set; } 
//------------------------------------------------------------------------------------ 
protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null) 
{ 
base.SetBaseSelectQuery(select, qry, customFields, request); 
select.Parse(); 
//select.AddWhere("(xxxtype = 'ABCDEF')"); 
//addFilterToSelect("UniqueId", "uniqueid", select, request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
