using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PresentationLayerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerMenu() : base("{BBEF0AFD-B46A-46B0-8046-113834736060}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6B51AC2A-4F56-4FAB-97AA-D7F61DF5CD84}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1C4B4A9C-36F9-4810-945E-6C5346BA3E23}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D85DD83D-9B9B-4A0E-98AF-FB0C783D520C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{2D2AFF13-0E60-4659-AF81-CEE56C0C942C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F3C19BD8-F633-4ADD-9C8D-102D4900B2B6}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CC9B8099-CD20-43AC-A8B4-D2D2131F5191}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2F0D98C7-5F02-4D6C-83F6-2BE206593205}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2040163E-A450-4B86-A168-DB55B34F2FA9}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AD169EF8-25CF-4F92-909A-2E2FE1B01376}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{1F7AA789-19B5-41EB-8F66-24BAA5EC273E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{B169A8F5-91AA-4DF8-BA03-A9C5DB4238BA}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{7F1B305B-7207-451B-8D6A-5FE9DC747998}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{E3664A5D-F342-49DC-BB80-A4D95568825C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}