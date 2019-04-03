using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class AssetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AssetMenu() : base("{E1366299-0008-429C-93CC-B8ED8969B180}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9D8BEC9B-FD83-4664-8F2C-3C15E71AF34D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2FAC9C27-D05F-4883-A969-1BB4335766B8}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3FC66856-BEEC-4BB9-8A1F-257BC13A5F4C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5A30BFC9-2472-45E3-BDDF-254D6DB6666B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{866AF1AF-BF1D-4E88-990A-E79E325AE483}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{ED01C793-80E0-4E82-8CF5-8B415E861A6E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B4CD103D-9F21-4560-AE57-75B109AED15A}", nodeBrowseMenuBar.Id);
           
            // Form
            var nodeForm = tree.AddForm("{08437867-898E-4E34-9397-4A51CC07132C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5C76FBA4-594F-4ADC-B10F-157942D71577}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{C6B2433B-43CE-4EF4-AC7C-DCA045FE0B4E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{1D577BA2-D8E4-4B00-B069-A6506AB5E9E9}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}