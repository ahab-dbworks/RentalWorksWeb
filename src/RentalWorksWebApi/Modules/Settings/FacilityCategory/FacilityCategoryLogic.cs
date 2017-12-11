using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.FacilityCategory
{
    public class FacilityCategoryLogic : CategoryLogic
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
