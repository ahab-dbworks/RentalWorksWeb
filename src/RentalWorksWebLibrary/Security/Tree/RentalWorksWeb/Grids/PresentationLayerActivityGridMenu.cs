using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PresentationLayerActivityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerActivityGridMenu() : base("{AA12FF6E-DE89-4C9A-8DB6-E42542BB1689}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FF2F59D2-24E1-4DEC-90CB-F17FDECC4968}", MODULEID);
            tree.AddEditMenuBarButton("{FC01173B-1BF7-49E4-82B7-6333E841FA67}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}