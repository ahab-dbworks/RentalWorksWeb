using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Utilities.ProgressMeter
{
    [FwSqlTable("tmpspmeter")]
    public class ProgressMeterRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SessionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "caption", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Caption { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentstep", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? CurrentStep { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalsteps", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? TotalSteps { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}