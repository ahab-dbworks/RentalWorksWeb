using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WarehouseCatalogMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseCatalogMenu() : base("{9045B118-A790-44FB-9867-3E8035EFEE69}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{E5C503C4-98EE-42E0-B03F-5645436B3E26}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A12D5AD9-2C6E-47F4-A056-77363E8C74E6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{01FD8919-4AD3-4EBD-BFAC-55EEC3DE1C0F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{45A2B988-DAE1-472B-BFE9-F419DBBB045C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BCDA37B3-2AEA-426C-B1FA-BA82730BEAA1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{426D9175-A108-4B9A-BCCF-583D72A0724D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B34B64E1-B497-417E-A6A6-C0867A01CBCF}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{64863581-B773-4C69-847C-9FD3D0D06A12}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6250C5D4-DDD5-4FEE-AB86-11CF80902317}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7AA82685-05EF-4769-92AF-B6747AD44B60}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6FF796F0-4F15-4FD0-92A4-FA4F526EA9C2}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9406D516-11E1-4A75-B125-6B2090F7B6CA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8194A603-5591-42E8-B885-285321ECBE9A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}