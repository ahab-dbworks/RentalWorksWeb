using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class RepairReleaseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairReleaseGridMenu() : base("{33DACEA3-20F0-4D7F-A0E3-C2D21BF60BAC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B8CCCA65-08BE-4CD2-B8F3-24E51D9CD609}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EF94EC39-65BF-4E8C-8E91-341D9EF6EBDF}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{78AB45EF-EF71-43E2-86C7-B6D91C5D0DAB}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3AD9061C-AD5D-43B7-9067-C67007FAD3F1}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}