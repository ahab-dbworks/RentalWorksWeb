using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderSettings.OrderStatus
{
    [FwSqlTable("orderstatusvalidationview")]
    public class OrderStatusRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string OrderStatus { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}