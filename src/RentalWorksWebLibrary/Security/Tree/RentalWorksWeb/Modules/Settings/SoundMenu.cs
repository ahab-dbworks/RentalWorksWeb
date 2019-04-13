using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SoundMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SoundMenu() : base("{29C327DD-7734-4039-9CE2-B25D7CD6F9CB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{2001CEAD-F84D-4603-A79A-E2C4EDFC8C3D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{05D1D3E0-FF23-4746-990E-C78F5D648766}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{61E7D749-B148-4F64-928E-BF32DFF9E779}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B4EE262A-8D5A-4370-ABF7-97BF0F5295E7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{82EF1808-9524-4634-9E25-D4C24D26E151}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{49A6227B-5D77-4EE3-AFE9-793A518A28BD}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{327CBDCB-73AB-412F-9FA6-CA0A0899EF7C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{38A4F379-27CD-4226-B433-6CD4444DA51F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{20DF596E-55E1-47DA-A4C4-1F31367246FF}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{E7CC3CE1-982D-4B7E-870A-F8BCF6274F9A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{04771B85-3AD7-48CD-B779-9054585A1E11}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{4867BC6D-DABC-441E-B7BF-18E0075F1791}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{BF057A3B-0FC8-4D37-A468-4924B559645A}", nodeFormMenuBar.Id);
        }


        //---------------------------------------------------------------------------------------------
    }
}