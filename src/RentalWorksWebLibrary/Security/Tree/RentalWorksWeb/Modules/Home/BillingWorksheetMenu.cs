using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class BillingWorksheetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingWorksheetMenu() : base("{BF8E2838-A31D-46B2-ABE1-5B09FC3E2A9E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{541BD266-6E05-46A6-A142-4CB7E3271AAE}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{15B517CA-3FF0-423B-AD8C-1C747A256D4B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{558D8A91-5C57-4810-BE3A-E621FAFF5C2E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{AE1B0AE4-30D0-4468-B98A-477EF480791B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{72CD3A95-6C9B-4EDA-A780-00BBDAEA740D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CF6AAE9C-6CDB-45FC-A5DC-FC3913A826E2}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{294D64ED-19FD-4609-996E-47B5BC5457BE}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{AB63CFD3-6357-4E26-83A7-31A33379251C}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A0FF4DB6-D10C-4AC4-B501-96BC131D02D1}", nodeBrowseMenuBar.Id);
            tree.AddSubMenuItem("Approve", "{20CA8800-41C5-4387-8EF0-558330B96AAA}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Unapprove", "{DE1069AB-2A4D-4556-AD00-9D89FAA22B54}", nodeBrowseExport.Id);

            // Form
            var nodeForm = tree.AddForm("{3D883DA9-6853-498E-8380-5D5AC1E56DB1}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{B7B0D45D-DFC6-4C53-A357-BB2ABFC0E16C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{705E25BA-98B1-4302-9D53-810FFC3AAED0}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{2539A9C0-555F-436D-B8D5-6E56DCD36D4F}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{3636A324-1E1A-4611-92C5-76DDC36A66F6}", nodeFormMenuBar.Id);
            tree.AddSubMenuItem("Approve", "{16932E29-821B-45B1-A7B7-82D203258E70}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Unapprove", "{09B18F85-BD05-462F-994D-DF7989D37E44}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}