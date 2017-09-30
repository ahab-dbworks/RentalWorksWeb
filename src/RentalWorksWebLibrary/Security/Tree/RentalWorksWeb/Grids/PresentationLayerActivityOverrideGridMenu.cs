using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PresentationLayerActivityOverrideGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerActivityOverrideGridMenu() : base("{ABA89B3D-AA83-4298-AAD4-AC5294BE7388}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{76950CDA-6059-4612-B5CE-3A53E2866257}", MODULEID);
            tree.AddEditMenuBarButton("{2A66E331-FFDB-4AF9-B2C3-1DEBFEE5F0C1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}