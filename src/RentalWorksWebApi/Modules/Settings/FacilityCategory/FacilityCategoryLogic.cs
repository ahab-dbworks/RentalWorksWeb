using RentalWorksWebApi.Modules.Settings.InventoryCategory;

namespace RentalWorksWebApi.Modules.Settings.FacilityCategory
{
    public class FacilityCategoryLogic : InventoryCategoryLogic
    {
        //------------------------------------------------------------------------------------
        FacilityCategoryLoader inventoryCategoryLoader = new FacilityCategoryLoader();
        public FacilityCategoryLogic()
        {
            dataLoader = inventoryCategoryLoader;
        }
        //------------------------------------------------------------------------------------


        public override void BeforeSave()
        {
            RecType = "SP";
        }
        //------------------------------------------------------------------------------------    
    }

}
