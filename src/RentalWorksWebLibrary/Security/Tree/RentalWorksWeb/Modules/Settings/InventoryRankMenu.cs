using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryRankMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryRankMenu() : base("{963F5133-29CA-4675-9BE6-E5C47D38789A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{2A5C627A-92A0-411D-865B-1BAAD5B37978}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1EE9AE41-122F-4235-8140-7CFC9FED52E4}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{560293E8-6BC1-44B2-9391-39B1E8D05763}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{6E4A91C4-C958-4D26-A2B8-717A18D83653}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BD8599B1-79B7-4E12-84F8-FBC4E9A318BA}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D5C9EFD5-3CF0-411A-81EE-C65002791585}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C58701E0-A002-4131-AAFA-40E9A18C2B06}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{36EDA248-DBFE-4E38-8E78-0E729C071FF0}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FF4B1DF4-277C-4A06-8082-06B1BE51FDDB}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7912FF4B-4FA7-4610-A26B-88B19CA5CA22}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7474C040-EC06-4726-8ADF-F42AB1433864}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{A4B17B87-06B4-4D98-B6E0-A20338A269C2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{31570E01-8887-4653-A89A-A070F29CDBC1}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
