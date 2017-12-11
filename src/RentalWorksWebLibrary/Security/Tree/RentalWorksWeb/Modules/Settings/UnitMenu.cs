using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class UnitMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UnitMenu() : base("{EE9F1081-BD9F-4004-A0CA-3813E2360642}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D602D414-9F94-4F4A-8617-078480F4389B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{66DDC08D-30EA-4606-9D20-217D83F4DA0D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5A996507-E29D-4BB0-8FC1-2A6598B9C5CF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{32799E3F-8F70-4110-86B0-7D5E44F55A5D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5917E231-8526-4E44-A54B-A763095A9321}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{59842C96-04F7-44C0-9D73-AF1E3F37A07F}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{7E28E1FD-2E55-4F4E-BFE7-D757C4DC3E7A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E496FD2C-C594-49B5-86E0-A4A805142A9A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5F57CBBD-5247-4652-ACF9-AB925BF5E2D6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{768F991D-7CBB-44A5-A1D7-2BF46A365DB3}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F043460E-016F-44CB-BB13-E8AA66D2F9F3}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{477D7701-7A5D-4ED9-A6F9-C923D1830462}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{75489533-EAA7-48D7-82E8-8D41296ED917}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}