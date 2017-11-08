using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class TermsAndConditionsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TermsAndConditionsGridMenu() : base("{CD65AB0D-A92D-4CA9-9EB3-1F789BC51717}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F758479A-434B-4CAA-9D8D-2194C9410078}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3EB9C167-28C7-477F-A582-D403C0F774A6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{486E0C1A-5C27-4A19-ACA3-6734154ED6F9}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D61330A6-8A38-4DEA-8A86-8C21CFDDF3AF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6D1D7152-0EA3-404D-9DB7-220050F1CE51}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{81D78B32-4D15-4566-8F05-0DA871BC558D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{01DE5CC0-38D5-4DD0-8C44-11B06D261490}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A21ED764-DBD4-48FF-B4BB-02DB7B1C5E08}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9BCB4F93-E99E-44A4-82A7-608ABE1C58CC}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F039F1C4-64EB-4E77-BCE8-15E8F8AFD9F5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{47AE8628-10D7-4727-B538-CBBA137A1A6E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9A051337-9594-43BD-AED7-D23CDAD2B0F8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{AC82D81C-DD09-4A39-89AB-2B83075FA903}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}