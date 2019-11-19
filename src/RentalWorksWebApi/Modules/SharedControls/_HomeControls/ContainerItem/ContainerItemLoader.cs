using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.Inventory.Item;

namespace WebApi.Modules.Home.ContainerItem
{
    public class ContainerItemLoader: ItemLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: false)]
        public override string ItemId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containeritemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public override string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}