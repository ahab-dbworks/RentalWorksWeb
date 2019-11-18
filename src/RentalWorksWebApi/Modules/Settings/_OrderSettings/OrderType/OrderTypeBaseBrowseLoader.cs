    using FwStandard.Data; 
    using FwStandard.Models; 
    using FwStandard.SqlServer; 
    using FwStandard.SqlServer.Attributes; 
    using WebApi.Data; 
    using System.Collections.Generic;
namespace WebApi.Modules.Settings.OrderType
{
    [FwSqlTable("ordertypeview")]
    public class OrderTypeBaseBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combineactivitytabs", modeltype: FwDataTypes.Boolean)]
        public bool? CombineActivityTabs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}