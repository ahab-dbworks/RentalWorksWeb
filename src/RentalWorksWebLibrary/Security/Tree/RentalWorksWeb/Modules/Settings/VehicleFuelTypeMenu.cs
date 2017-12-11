using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleFuelTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleFuelTypeMenu() : base("{D9140FB3-084D-4615-8E7A-95731670E682}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DBE35FA5-3DFD-4770-B6E1-4913E04942FD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{146D02FF-6A7C-4EF0-99C1-41BE3FE40B80}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{548CBB37-ED35-4C63-A0E9-C3631D67BE5B}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{347EBB98-CC8D-4A86-A336-31ACD0D697E3}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A2E49675-6178-4152-91C7-86024BD72008}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{6B5361B6-D590-4B17-B917-041F2EC89C71}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{792BF38A-E2A3-4C20-B381-D1E23AEF414B}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2AC9C676-E582-4DFC-A540-D6AB91CE3166}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{34D9DD77-3753-4E8A-9F18-118BD22DC475}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D941FE36-583B-4460-B132-A5A5D33EA411}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DA3029A7-5EA9-4AEB-9829-48CBAE1EADD5}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5D8B015D-DFAD-4D48-A2A6-C927CF847924}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{42CFACDA-BCDC-40A0-9239-7FC4946DB607}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}