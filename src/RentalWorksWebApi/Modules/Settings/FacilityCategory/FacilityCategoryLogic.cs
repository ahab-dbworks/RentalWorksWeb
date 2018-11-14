using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
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

        [FwLogicProperty(Id:"Z25aqO0uQegN")]
        public string FacilityTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"JVi56cSPVrC", IsReadOnly:true)]
        public string FacilityType { get; set; }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "SP";
        }
        //------------------------------------------------------------------------------------    
    }

}
