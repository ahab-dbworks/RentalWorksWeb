using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class TermsConditionsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TermsConditionsMenu() : base("{5C09A4C3-4272-458A-80DA-A5DF6B098D02}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{20F5C8C4-203D-4EB8-B0D2-381DDE1E1C06}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{65861B82-19D7-46D0-BF6D-E0B807B97A9F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2D5E9B7A-76DD-408A-BCE6-6EB375E5E9CF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0DB52B6F-4532-4F8E-BC45-D9A90124A2E9}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{45BDFFBC-7D04-47DD-84DF-40DE72670370}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F8B61918-2ACF-4DD8-A3F2-BD9B615E086A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B1EEBA77-A01A-40BA-A7CA-8EAF2EB0CDD2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{6F3308F7-4A69-4ACE-928A-D13A02C9E605}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0EDA89A3-7B02-4CEF-95B4-E28C8BF4A218}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{15246686-DC6E-4699-AC4F-B7C65BD31527}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{ACA6BFD5-AC48-4F5E-B2CF-33D07EB2337A}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{7A3C6DCE-E1BF-498D-B6F6-FF79BEDF6C77}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8B0F2EBC-FDC5-4920-88EC-1E9A1854EDC5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

