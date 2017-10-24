using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class TemplateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TemplateMenu() : base("{BDDB1439-F128-4AB7-9657-B1CDFFA12721}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{CFF14D41-FA7C-47C5-9E0D-72CB4BD0AABD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6E95EACE-6FF0-4AA0-980B-AD422FCE94C5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{74C4D7B8-07C7-43C9-8F0C-C6DF99D957BC}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{FD060461-FDB8-4454-ACF2-F4B5B5DE2FCB}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{31520E87-3F9F-4E26-A0EE-982615D7D310}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{ADC3B43C-B79F-43CD-918D-24D1126CDCB0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{432B2D72-0EFF-4F6D-AA9F-5BBAEDC702D4}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E725B087-88A2-4073-9338-F5B5B0BB5F87}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{182E2F3D-8654-4B9E-ADE0-9719B8735AE3}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{63E9EF44-D1E8-41FA-9DA0-4C4DA7F0E84C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5B9A7A99-E0F4-4B0F-8965-1ADAD11DA700}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{8BF45DD9-5BB3-47A8-B1E7-802D9E2B83AB}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6DC6121F-A1A7-40AC-90EA-FC49FCDF7DF5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}