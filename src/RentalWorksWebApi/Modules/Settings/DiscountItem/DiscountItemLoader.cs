using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic; 
namespace RentalWorksWebApi.Modules.Settings.DiscountItem 
{ 
[FwSqlTable("discountitemviewview")] 
public class DiscountItemLoader: RwDataLoadRecord 
{ 
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
