using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
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
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------

        public string FacilityTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "SP";
        }
        //------------------------------------------------------------------------------------    
    }

}
