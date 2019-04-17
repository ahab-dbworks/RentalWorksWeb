using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Modules.Home.Item;

namespace WebApi.Modules.Home.ContainerItem
{
    public class ContainerItemLoader: ItemLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: false)]
        public override string ItemId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containeritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}