using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class OrderTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeMenu() : base("{E593AA0F-28AE-4B4A-BFFA-EEDB5FBC4E24}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1F1BCA77-BC2B-48C1-9945-354FCB50E12D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{96B2B529-4BB2-4E95-8A26-EE81482DD795}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C78718B5-0382-4AE3-BE8F-DA117895F557}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D7CBEA59-E9C3-4AFB-ABE7-1D31857E8977}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4338F5E0-00FC-4523-AEC5-09AD3EB810F3}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{AD9D94CF-31DE-4519-BCAD-09582D276497}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{E0CE6098-2FB8-4F68-946B-4014BE7C79B7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{70CA2747-982D-4F0E-9CA9-4B63522588EC}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{917A9DED-BB8F-46C1-8436-D95DE3833ECF}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{62641851-40C5-4595-AE24-EA7042F5F0A5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1A159F5E-4B1D-40AB-9BD6-F9EAF8069791}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{461EB23F-B767-4A0A-98B3-2938D3E0C738}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A4F44517-06EF-4E63-ABF8-E77E2434B119}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
