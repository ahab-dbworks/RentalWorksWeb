using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Utilities
{
    public class MigrateOrdersMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MigrateOrdersMenu() : base("{6FAA0140-ACA2-40CA-9FDD-507EAC437F2A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{EE03F1F1-E3FF-4232-8F68-BD17699D3829}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{118B4A68-FE2B-4B10-AE14-EC67258F9C9E}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}