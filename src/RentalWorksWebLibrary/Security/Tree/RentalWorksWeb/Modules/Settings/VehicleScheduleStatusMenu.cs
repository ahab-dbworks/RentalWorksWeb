using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleScheduleStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleScheduleStatusMenu() : base("{A001473B-1FB4-4E85-8093-37A92057CD93}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{11DFFF48-EED9-4646-A34D-5E0AE752E44C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E811CB76-520F-47A5-B67B-21DE27AFB878}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{ABEF228F-5B1D-47DB-9C1F-034FEDACFAF0}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{22FAB772-25F7-4068-BE95-CAE5AE5AC193}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0448F611-EF98-4FB5-972D-12A2A7155AAA}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{19870DA5-93E6-4676-8616-3C10C7FC7F21}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{071BF375-E6E8-4760-9479-70225B004399}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D0943895-6BE5-4266-88D5-F5DB7ED6FB78}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BA1925D1-E8C7-4FAA-A0C4-E6B89E00BF93}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{40219EC9-AECA-4A29-ABA9-9B94FC6C8A61}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4B480691-3EB4-4680-B248-EE16D5389D68}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{4EABE9A9-769E-425A-9CF8-CC64A93632F4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{837311CC-BCC0-42B3-91A7-A812316F9E19}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}