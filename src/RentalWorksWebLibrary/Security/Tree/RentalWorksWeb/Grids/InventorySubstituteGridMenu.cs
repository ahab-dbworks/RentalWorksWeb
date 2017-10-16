using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventorySubstituteGridMenu() : base("{B715DFB4-5700-48DE-878A-F8D93F99ECA3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6FC3B66-D99E-4E38-AEB6-2B07AAFC7F42}", MODULEID);
            tree.AddEditMenuBarButton("{15CCA7B4-6E06-40FF-AB57-14FD12AE31F5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}