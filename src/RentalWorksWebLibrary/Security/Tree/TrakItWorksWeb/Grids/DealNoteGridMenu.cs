using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class DealNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealNoteGridMenu() : base("{CA1FA1E1-2BE8-473D-ABC3-24D741D2AD8E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C1EF2F5C-B600-467F-B08C-D45629248B64}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4A6872CD-662B-41F4-9747-FCA0DD905F6A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{87A85044-A877-452D-9817-BB22FCA33B27}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{785F70F4-ECF3-4797-B863-41DAD08936D1}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{534DE0D5-8BE3-4875-8F4F-E2C6B775FF12}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{9D4C9A39-14D5-4218-8374-79CADD3B8974}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8FF5D1CD-F11F-4BE0-9ADE-EB71C602C70D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
