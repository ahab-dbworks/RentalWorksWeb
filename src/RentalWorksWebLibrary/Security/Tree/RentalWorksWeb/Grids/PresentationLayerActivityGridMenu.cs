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
            tree.AddNewMenuBarButton("{D044BABB-C19F-4A91-8D36-8CFF4287B8F3}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{0E68A9E1-1206-48DC-B100-E179C67DCBF6}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5705928F-79C3-4687-A9F2-60A65AADF73F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}