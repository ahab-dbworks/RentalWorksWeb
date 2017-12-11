using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RateLocationTaxGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RateLocationTaxGridMenu() : base("{F1A613A6-FD31-4082-88CC-4F0252BF56AC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E791CF2B-67BC-4F9C-827E-EEA916760E03}", MODULEID);
            tree.AddEditMenuBarButton("{2AC29944-7390-49A6-A440-44B37944F837}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}