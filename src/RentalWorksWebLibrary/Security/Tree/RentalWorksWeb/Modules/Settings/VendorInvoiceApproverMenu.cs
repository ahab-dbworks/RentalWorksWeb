using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VendorInvoiceApproverMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoiceApproverMenu() : base("{4E34DB8F-84C0-4810-B49E-AE6640DD8E4B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A5CE4D86-9BEF-47CA-8FB3-04F9D5AE9FD3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1D9EFEE4-F3EF-464B-8D8A-9EA0CEBC6277}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{76A5CFF0-612D-4639-86F1-3E9EF2380CC5}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{A4B8253C-A2FF-4D9F-8438-412747B1F33C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{116EF3AE-4B7A-466B-945B-1B80120C66D8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{7FB9969B-460C-4DB4-8AC7-2C32A5BD3BF3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{90B5BD73-B9B2-4325-932D-85942A5FF28F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{68A5E7FF-EEAB-432D-9E71-1A61D1FF5BCF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{72252B59-9F69-4B11-9728-D4D3D3C0323E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{EFE44602-2F2D-4CE8-BF99-441BE36F9C82}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6C1EFD3A-837B-4FBC-B06E-01D9C2084CBF}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{27092722-B5F3-4436-BE52-957E259EB389}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{2CACBD46-1E54-409D-B783-41C1964E5FF5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}