using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class PickListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PickListMenu() : base("{744B371E-5478-42F9-9852-E143A1EC5DDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1B672778-B4C0-4848-BFC8-AB0840F820EE}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{563FC9FA-A6E5-4C4F-A83E-18F61F0C29AB}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2815BE35-E178-4E50-9F6A-4E8B0542EE3E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{958FD43D-2818-4E75-8D52-16DA4C1DA2D8}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FC425DAE-32F0-4635-81E7-7C81364DDC25}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{948EF714-A16C-431C-9E8F-E0DC01D55790}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C93715E6-AEE0-4135-ACB1-3EEE2C5A2749}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F9C6A55E-B39A-40CA-BD42-1797864ED9AA}", nodeBrowseMenuBar.Id);
            tree.AddSubMenuItem("Print Pick List", "{E6EA3633-4CB8-4F5F-8EB4-C29D41C1B394}", nodeBrowseExport.Id);
    

            // Form
            var nodeForm = tree.AddForm("{B79BAA0C-4B5C-4D88-BA3F-AECAE41CD5F5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{EF348DA8-1E95-4A50-B1C9-3E547FC13658}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{24FBECBE-C08D-4C6C-B111-23F68AA7B64D}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{FA280434-67C3-4F60-A368-70F37B23630C}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Print Pick List", "{E4C83683-8B4A-46F4-8A47-E11416AB10E7}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Cancel Pick List", "{DDC5BB9F-D214-458C-9559-67F2900DD011}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{501E6B97-884F-4509-9286-5CBD33DECC9E}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}