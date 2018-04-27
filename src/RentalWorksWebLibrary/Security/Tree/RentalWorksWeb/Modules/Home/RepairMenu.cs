using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class RepairMenu : FwSecurityTreeBranch
    {
      //---------------------------------------------------------------------------------------------
        public RepairMenu() : base("{2BD0DC82-270E-4B86-A9AA-DD0461A0186A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C61B72A7-2D98-4B33-900C-9DD5EDC20E9D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{4314544E-1BE3-402C-9924-0FB23C6FE9EB}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{91EC2D90-AF3F-467C-B068-4D54283845B8}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7E69D87E-7AC1-4486-9797-82D55147E918}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{638B8DBB-3E8E-4154-98DD-675B5DBFBEEC}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Void", "{AFA36551-F49E-4FB9-84DD-A54A423CCFF3}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B680CE05-50AF-48D6-928D-5640E73197C1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{E45865D3-E667-45C9-9EB0-B1764B6F8051}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{774614E8-86CC-41F4-9951-3E71E0163B9E}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{76C8EDE7-014C-42A2-BD11-DB370A8FB8FE}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{337489B7-6E18-4C84-BBCB-FEC010F4727D}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6A2C8FDE-A1DF-41FB-BE25-45F5B02DD635}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{0D0BF2AD-ECA2-4054-95D4-BAD3426925D3}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Estimate", "{AEDCEB81-2A5A-4779-8A88-25FD48E88E6A}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Complete", "{6EE5D9E2-8075-43A6-8E81-E2BCA99B4308}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Void", "{9F58C03B-89CD-484A-8332-CDBF9961A258}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Release Items", "{EE709549-C91C-473E-96CC-2DB121082FB5}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{1D0F8CBF-548A-47A4-8719-3A46AD0BF46B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}