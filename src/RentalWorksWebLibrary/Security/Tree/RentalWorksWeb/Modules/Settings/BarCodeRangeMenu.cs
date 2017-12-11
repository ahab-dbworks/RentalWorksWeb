using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BarCodeRangeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BarCodeRangeMenu() : base("{9A52C5B8-98AB-49A0-A392-69DB0873F943}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D666DB32-FBC0-427D-9313-0C406ABE1CB9}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6039FD5C-0DE0-4319-A6FA-23BE9FC4FBCF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{BC00FAC3-7681-4139-B23E-1015547228C3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{265BBFC1-AB61-4F98-9FB3-63E52C3EDC5D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BDE8FB89-6CFA-4D9C-A3F4-78B279D8E5AE}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{DAA8A220-88C0-4C21-8672-A2EB547DA560}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{EBA8F5FD-2110-45B0-8911-2ACCE0119EBB}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3B090377-C04F-4D87-9616-A5D38CB9D027}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B8FA7F8D-CA69-4B47-B5BE-8E2C310D27DE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F16F140C-E925-4283-B2C1-3099DBB44ECD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{69899B70-F3D3-4DF2-B89F-061D7347B2C5}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{177EC524-4014-4205-B731-34812C1F05DC}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}