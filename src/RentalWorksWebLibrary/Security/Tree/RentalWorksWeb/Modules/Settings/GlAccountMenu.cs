using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GlAccountMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GlAccountMenu() : base("{F03CA227-99EE-42EF-B615-94540DCB21B3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{79E8B2FA-8082-4D50-A494-23AEB2E1FBA5}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{ADEA6C30-DBC8-47D8-8813-07E684B39D84}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6D3562E7-9C2E-4EE0-B59B-76FB2F915CD3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0D157ECB-1DFC-4EC4-A0FC-B3DEFCA718F8}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0ECB8D53-75CF-4326-B9C0-005A4451B07F}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{C9C226A9-2CE4-4DBB-BF14-0A1EE2D1E74D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F1C48BA1-03E1-41E7-9A75-5220297373D7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{ECC6F093-96E3-4962-B17A-B8C04DC107AB}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1F97143F-ED33-448A-97AA-70457C200F31}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{B50CB5AA-5B84-4A2C-ABD4-E3F2EC7EEE78}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{49240ACE-2023-4E8E-BB45-C37A30ADFF37}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{466FCBEB-9895-4F3A-BB0C-4D86BD2EFE3B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{54D53F6D-7EB0-420A-905C-263351A545CF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}