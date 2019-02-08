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
            var nodeGridSubMenu = tree.AddSubMenu("{266337B3-580C-4120-977D-8D69E121521A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{0DBB4B1A-F03F-4A59-B793-BC6E90D49CEF}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CEA6296A-4A96-4E15-AC30-B5FD11A68316}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{2AC29944-7390-49A6-A440-44B37944F837}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}