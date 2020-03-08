using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 

namespace WebApi.Modules.Settings.OrderSettings.OrderType
{
    [FwSqlTable("ordertypeview")]
    public class OrderTypeBaseBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "combineactivitytabs", modeltype: FwDataTypes.Boolean)]
        public bool? CombineActivityTabs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultmanualsort", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultManualSort { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}