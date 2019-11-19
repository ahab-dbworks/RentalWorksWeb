using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.StageQuantityItem
{
    [FwLogic(Id:"PKyncKiHT9Han")]
    public class StageQuantityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        StageQuantityItemLoader stageQuantityItemLoader = new StageQuantityItemLoader();
        public StageQuantityItemLogic()
        {
            dataLoader = stageQuantityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"GXzrhRvdlLwo")]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"A7Io6i1ADTNY")]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"02z6uV17A0n6")]
        public string InventoryId { get; set; }

        [FwLogicProperty(Id:"h0fLr0UbuSte")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"EwwFJS9LDKjQ")]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"GnJ31khNVPgl")]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"b1mjoTKnTkbA")]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"QXXRGuMyljSd")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"mdspcflMrnK5")]
        public string TrackedBy { get; set; }

        [FwLogicProperty(Id:"Kxf4xIz56sGE")]
        public string Description { get; set; }

        [FwLogicProperty(Id:"x8YqSQ4kiNGs")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"d2CjypLN1FHT")]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"B9VrQDsJ1qWy")]
        public decimal? QuantityOut { get; set; }

        [FwLogicProperty(Id:"XLQJCqjoWwzg")]
        public decimal? QuantityStaged { get; set; }

        [FwLogicProperty(Id:"qgBQB9E2F2fA")]
        public string QuantityRemaining { get; set; }

        [FwLogicProperty(Id:"TPtLcwVVD7yP")]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"L02fOYYLdI6W")]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"ElhdeWStKf8N")]
        public string RecType { get; set; }

        [FwLogicProperty(Id:"lEkNoNUzzrcY")]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id:"EAlTqU2kpVrj")]
        public string RecTypeColor { get; set; }

        [FwLogicProperty(Id:"vdW6hmZj28CE")]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"Jm7DMApXOFpF")]
        public string NestedOrderItemId { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
