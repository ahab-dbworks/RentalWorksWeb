using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Modules.Inventory.Asset;

namespace WebApi.Modules.HomeControls.ContainerItem
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