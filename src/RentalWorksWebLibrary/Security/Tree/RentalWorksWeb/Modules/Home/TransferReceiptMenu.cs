using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class TransferReceiptMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TransferReceiptMenu() : base("{2B60012B-ED6A-430B-B2CB-C1287FD4CE8B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6950AA16-27EA-41C4-A392-0D0912859147}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F1BD3B43-3A2D-4C10-8F78-B9339709332F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C2962435-9B7F-402E-9057-B2EF463A2B82}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{744CBF01-5D02-41BF-AEB9-78B67730E409}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{72403608-43E0-487E-AAAA-DE78CD1F3B8D}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{E90734C1-9D9E-4AF2-A8E3-4E9791D32400}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F0A6A106-1D2D-41FB-8B8B-5383AE610018}", nodeBrowseMenuBar.Id);
          
            // Form
            var nodeForm = tree.AddForm("{9A4F168C-49DD-498C-BB6C-93CF64AFF1E1}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{462ED074-EB3A-46B5-973E-2C026A8EEEF8}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{1C12110F-CB9B-4014-BA22-A0BB9A621994}", nodeFormMenuBar.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{33561741-5828-4FD0-B6C2-2147020E9035}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{3EEB8165-5C47-4890-90DB-34149930A841}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Print", "{5C35E285-F8DA-4D27-AA64-379156213B7F}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}