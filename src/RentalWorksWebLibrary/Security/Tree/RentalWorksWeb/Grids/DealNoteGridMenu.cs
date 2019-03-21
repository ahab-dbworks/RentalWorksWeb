using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class DealNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealNoteGridMenu() : base("{562D88B4-7CFB-4239-B445-C30BE8F8BAC9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{140E8B7E-5EAA-40CE-9C69-99E68A670BD9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{49986685-D309-470C-9469-0A41721AA5ED}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{63E6A3DC-378F-4AB5-B4A2-D92687FD6536}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2716CAB2-2C0D-4C30-AB35-34AD1B310D45}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{D2552082-3109-47D8-A02C-AF9FFCD921B1}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{84BE9BF4-0642-4A42-83B0-5940721BD5F2}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9F0A19FC-DE50-4EAD-BB1B-805BEE91A570}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
