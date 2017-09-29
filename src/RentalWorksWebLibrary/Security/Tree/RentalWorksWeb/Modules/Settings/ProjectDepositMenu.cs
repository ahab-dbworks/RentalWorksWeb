using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectDepositMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectDepositMenu() : base("{24E6F284-7457-4E75-B77D-25B3A6BE6A4D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8769BD0E-8659-46FC-8580-AC027446CDF6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0A75DA4C-0B67-4F69-9047-5D47833C99BD}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B48F3C25-27A6-43A3-8C78-A7A3122EABEE}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D9D30FBB-4E4E-4FD6-85FD-7EAF2FD87732}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4C64A884-61B7-42C8-A7D8-1D8752CC3B13}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{52F2DCFF-FFC1-467D-8701-60C3756CA8EE}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{DBDF97C1-CA3B-4CF0-BB91-9F0125B1DA93}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C4B7D0F1-7770-4B35-9210-5627242A0BA7}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{31E37E73-ECFC-4780-989E-C6202B6B65B8}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4A490D41-BD1B-4C44-AA9C-E9A290B94C17}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DFC67EA8-3B0E-4073-A6A9-5C3FD2946B61}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{EA082537-B347-4589-9267-9F27901CE3BF}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8CAC51C9-5A8B-4F3A-8977-0272BD737CF8}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}