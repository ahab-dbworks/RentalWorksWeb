using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ActivityDatesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ActivityDatesMenu() : base("{0C7E7F68-50C8-45A0-B6CA-BE11223D7806}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{51BD56FA-3217-4776-A51E-7083BC2286AD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{50186F2C-7C04-47AA-8976-2EF2A5DE345D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{409C3916-C877-4533-94CB-BC34D7F7E5C4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{82E59987-2429-43BE-A066-2EFA8C0E20AA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5FAD3AEE-7BE5-4757-85BA-A4D9F1E2015B}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5123A151-AE35-49CB-ADE1-76DB5F551CF3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C4B3813D-981B-40B6-8D2B-4A99B14D37CD}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F582A6ED-6424-48C0-A3E2-38ADDDABF634}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9AFC20DA-B89A-46DB-99B9-D3816C788760}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{E132FBF6-384F-4DA9-A346-BB68D0FB9ABA}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A1591FCA-F0F1-4A01-AE13-7B63298A3F78}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0B92FEA7-5748-4DC7-A0F3-EE6F2201F098}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{27B9F802-4D3C-442A-BEC8-4A593C53466A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}