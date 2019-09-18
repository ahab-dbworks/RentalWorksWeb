using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FormDesignMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FormDesignMenu() : base("{4DFEC75D-C33A-4358-9EF1-4D1F5F9C5D73}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A5546F54-1BEA-45F7-AD51-DEA680A68F7C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6152817E-F31F-4CD6-9226-ED78B5288820}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5F1BCB44-F415-4E31-80F1-88D4E235401A}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3355D3B5-F5F3-4328-93C7-8D88422E553A}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5A32AA5B-3957-456C-A3E2-A358D67B3FB2}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{C334D676-ACC4-411D-8198-C230DBA6C77F}", nodeBrowseMenuBar.Id);
            //tree.AddViewMenuBarButton("{0C758608-C945-49A6-B542-A321B6E07DD0}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{76581E69-F252-4230-A68A-96B4C7834FE2}", nodeBrowseMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}
