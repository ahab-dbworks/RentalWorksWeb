using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class DuplicateRulesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DuplicateRulesMenu() : base("{8A1EA4A2-6019-4B9B-8324-6143BD7916A1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{88CCAB7A-247A-473F-BE2D-688C6A374427}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E30929DB-E451-4E27-860A-91559042C446}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{431907C9-A832-4551-AF2F-851E91389E3C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{4A8D671C-FD8A-4A99-8634-3EB6B7773D72}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D146FB59-F837-4A7D-9094-5E1F241D4CDE}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{796DA06A-8207-4495-9B6A-9538E5D752C3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4F81F37A-0892-47A4-9504-C2869945CFD3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C05785B7-4F18-47EB-A912-9C5A7C700030}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{198EDAC4-1467-4578-87D0-632CAE68C6C8}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{64F8BA82-64A8-4F46-B0F1-01EC00EF3108}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{9CA8D28D-CCE4-49D5-BD57-4536470E9C72}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9FF51BDF-00C1-4307-BA9F-30943751192D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{121092EE-00D0-444F-B0FD-A5491B7F5F33}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}
