using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class POApproverRoleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POApproverRoleMenu() : base("{992314B6-A24F-468C-A8B6-5EAC8F14BE16}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8DC9B201-8942-4767-880D-636DDF7C3CE7}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E5B4EDED-CE87-40ED-84A4-827D4EC44132}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FEFB9B15-7CA9-44DA-A67E-9C23F45A5DEA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{727B13C3-77F0-48F7-8DF2-9DC057482CAF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6E120650-CCE4-4427-BD93-BA89F3FBDD1A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{BCE89027-BB70-4948-AEBD-C571B0F2F372}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BC9932C8-74C8-46F5-BB30-AA0510CCFCDC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{15AE16FA-0BAB-4FF9-B8E7-9AB2DA889C66}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{3637B76B-6384-4185-8153-FD6535D43C43}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{861E5964-49F9-4009-BA0C-E683948A1D20}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1C35DCC5-98B4-418F-9260-B1260D36DB49}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{7289CF19-8F92-4CE3-80F0-FF2A9D54633B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{3C32A620-0BE7-4191-933B-7B4DD2383E8C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}