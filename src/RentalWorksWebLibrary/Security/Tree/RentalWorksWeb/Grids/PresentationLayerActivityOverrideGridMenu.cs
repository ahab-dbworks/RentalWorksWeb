using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PresentationLayerActivityOverrideGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerActivityOverrideGridMenu() : base("{ABA89B3D-AA83-4298-AAD4-AC5294BE7388}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{76950CDA-6059-4612-B5CE-3A53E2866257}", MODULEID);
            tree.AddNewMenuBarButton("{8108E283-9F4F-444A-AEE9-ED70395EA503}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{2A66E331-FFDB-4AF9-B2C3-1DEBFEE5F0C1}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{79DB168F-CE12-4659-8B76-B4B14DC46F93}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}