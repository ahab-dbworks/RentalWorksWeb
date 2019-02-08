using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceNoteGridMenu() : base("{09E91168-0C59-4EC7-9DCD-2B65F0EB2A6C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{10B82690-F22F-467F-A8F9-401044ED8F85}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9B51B708-BF28-4B8A-B9A8-DB7E86CBC4FD}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{EB616087-1AAC-49FA-AB53-D37AF9703E7B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FEA701CF-3B0C-429E-8F7D-4F190CE30819}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{C920C157-83E4-4BA4-9956-7967E5DF2F58}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{013E7A2E-4555-44D0-A1E2-788EDFFA3F57}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{EEE0CBB6-A795-404A-B64F-9BF3C62686B6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
