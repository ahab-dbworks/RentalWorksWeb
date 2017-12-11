using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleRatingMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleRatingMenu() : base("{09913CDB-68FB-4F18-BBAA-DCA8A8F926E5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{26278D58-67C4-4672-90F0-A903B29AEB3D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F8350788-4B69-4D05-A471-462E610FC3C9}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{53E679AF-D624-4205-AF91-25AFCDE33CE1}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DB2845FD-C09E-4485-B53F-228E884A8872}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AE2D7309-67A9-4E92-BD50-4537AC71F759}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1D40ADBD-7AA0-47D4-8E96-E98DCB3AA0E2}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F153E20E-D682-4C23-954E-B46B536911E0}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FFF3360C-5C58-4317-B6E7-860D4757B682}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{DC016C32-BC75-40FD-96FC-C7EEBF29E7BA}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{48B9CD73-F947-407A-8400-18016AFE45E7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E2D435FB-C810-4625-B91E-148ACB60642E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{DB6E064A-B296-4C33-94BC-0C2B69319549}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{879458D9-262E-4459-8C37-0B195CED9A60}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}