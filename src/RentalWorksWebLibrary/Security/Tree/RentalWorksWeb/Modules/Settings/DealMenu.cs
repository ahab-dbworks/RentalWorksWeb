using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DealMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealMenu() : base("{C67AD425-5273-4F80-A452-146B2008B41C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9BC0D46A-B033-4AEF-9F1B-0E29F46108F6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5A1AFB8D-B969-47D7-910E-07E8DD45E102}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{589AEAD5-45BC-4218-AB36-49FD6D65DBE5}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{21AADB1B-0BAE-41BC-92FF-1E46DC1080F5}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D6E0A77C-09C7-4075-8367-2F7E210FE73A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{C3EB4925-F2AA-4A88-A300-D6082F1B99EB}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{26740F93-809B-4562-AD45-D2109E790D72}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A4E3C68D-CC28-4368-ABA9-5EF65B4C3D71}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{684FE748-8AA5-43EB-9A46-0D59F6F264A9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{883EB18A-7DC3-4035-BDA6-55A3815A7A97}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E497FE0A-E025-420D-ADD3-DEC02F955FB2}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{74A5AA0A-94B6-46CD-A34A-C72FCA1D147F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}