using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WardrobeInventoryColorGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeInventoryColorGrid() : base("{ED2BCE54-1255-4B65-976B-B24A6573F176}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CDC777C8-96B4-4B77-98D3-E0830D06708E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D1469BF0-4915-41AA-B650-33A75DAF993A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D345A698-FDDA-42F0-9422-C06795AD31F9}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{83F09913-5CC5-462C-8468-5DFC185BD46A}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{8D52FDCA-58E8-4E71-9B08-7C15820B6099}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{BCB9B466-8CBC-48FC-BC4F-808E91D2C651}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5504A647-216D-45A9-93F9-24627B3A752F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
