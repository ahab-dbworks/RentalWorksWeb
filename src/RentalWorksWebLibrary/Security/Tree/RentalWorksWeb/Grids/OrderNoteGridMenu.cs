using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderNoteGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderNoteGrid() : base("{55248753-DF49-46E3-84AE-0532354F3550}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{761EF079-BC85-44A0-9FED-89B10742D909}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B65D188E-6311-41C4-8875-4ED06DF502B1}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F1141171-62BC-4B41-A250-F3801746F81F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7F11E8D7-AD9F-4EB4-AFBA-67CFBDC372EE}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{3F3978FA-75A0-4274-A0EC-E934E941C4C0}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{24F7DFC7-0104-4190-A3C7-AC549FD2A002}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5F1ECF9F-556B-404F-8CCF-804CFDB023A0}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
