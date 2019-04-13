using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class PaymentTermsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PaymentTermsMenu() : base("{8729D639-DF81-47C1-9F1C-61BDBAA8CB3C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{57A67276-4DA9-4837-A601-8F9B65E83A9C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{76B839EC-ACB9-4BD3-AE04-ED23B79AD006}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{99630608-7EF6-4F03-B129-4EF62DC6E466}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8B12797B-FBA3-4D58-A0CE-734631C70264}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{333C4338-A986-499E-9B0E-892D4478B3AC}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{86F6FDE3-9036-4E12-8049-A5F68A29307D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{26337925-A6DC-4F89-9DA8-03B47BA78C47}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{0B4224C7-D806-4652-862A-7C661C2D8F50}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C4B89C0B-6C38-48F7-AA16-8C426C68EA6C}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FBDE8779-FA46-46FA-BB1C-19DA75C1A838}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{504AE252-2F6C-4FBA-8344-3F584B12ED3D}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{C9F91C59-255E-400E-8F3A-7BDA8BDED89B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6B4AC894-23FE-466C-9780-C0FAACAF52D6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
