using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectAsBuildMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectAsBuildMenu() : base("{A3BFF1F7-0951-4F3A-A6DE-1A62BEDF45E6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1D67D718-A290-4B6C-81FA-56FA089420DA}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2F281964-8288-47F6-84AD-408B3336CF0A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2DE53CFD-D98C-4893-84BC-8653EB2A61F4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{52DE87BC-D1C1-4F9F-A9D6-3A3C53D0AEAE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{59638176-B69B-4712-B6A6-255113119DF2}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{8AE4C7FD-13A8-4CAB-ABA6-6362253BFA20}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{CDF1F7A0-3CA7-4011-BD96-5126C6AD5FE4}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{254251BE-97F1-4226-A17B-ACDAE1688044}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E47BA10B-CAEC-463A-87D4-0F5AF1553FDC}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FCBEF8F3-7989-4DBE-99DC-347A0EC6EE6C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{B4122E19-375A-49C7-BBA4-BBAE52E526ED}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{436B2431-1E67-4F14-8D50-A9C05E97565E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}