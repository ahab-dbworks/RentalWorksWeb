using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.DataHealth
{
    [FwSqlTable("datahealthview")]
    public class DataHealthLoader : DataHealthBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
