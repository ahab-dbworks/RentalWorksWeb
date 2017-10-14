using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Home.ItemCompatible
{
    public class ItemCompatibleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ItemCompatibleRecord itemCompatible = new ItemCompatibleRecord();
        ItemCompatibleLoader itemCompatibleLoader = new ItemCompatibleLoader();
        public ItemCompatibleLogic()
        {
            dataRecords.Add(itemCompatible);
            dataLoader = itemCompatibleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ItemCompatibleId { get { return itemCompatible.ItemCompatibleId; } set { itemCompatible.ItemCompatibleId = value; } }
        public string ItemId { get { return itemCompatible.ItemId; } set { itemCompatible.ItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        public string CompatibleWithItemId { get { return itemCompatible.CompatibleWithItemId; } set { itemCompatible.CompatibleWithItemId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompatibleWithClassification { get; set; }
        public string DateStamp { get { return itemCompatible.DateStamp; } set { itemCompatible.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}