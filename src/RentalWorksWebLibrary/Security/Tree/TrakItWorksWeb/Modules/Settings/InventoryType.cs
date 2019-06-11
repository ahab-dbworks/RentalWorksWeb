using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class InventoryTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryTypeMenu() : base("{8D8CBE05-2111-40B9-AE5E-560AF549AE03}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F391CB46-774F-439B-9EAA-89EED0216050}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{27B76E27-A665-437D-AEC8-9D3F3794F64F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B4F7ACBE-EBDF-4E88-8D77-83014E1E21E7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DB2C9F60-713F-4C14-899E-56C880254CD2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{ACD0380F-D538-4AF9-A6D1-335F3CD4D67E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0D43A6E3-B3BC-4BCC-BB54-AAD8477EFE71}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{CF2C5D0B-7935-4146-A218-3C1DBC70E8ED}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{DFA5ADE2-3C8E-435A-9945-9326D1D33040}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CEDF660B-C3AB-45AD-BBF3-A43287DF31AC}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CE79A7FC-40E0-4B55-AC25-71E633E5D5FB}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{282BD5FF-FD47-40F2-98E0-0FD470A9C6D0}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{FAF93D3A-183B-4753-BBAC-472C776049A1}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{03250858-592A-4B02-AB02-F6DDE4F3D7EA}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
