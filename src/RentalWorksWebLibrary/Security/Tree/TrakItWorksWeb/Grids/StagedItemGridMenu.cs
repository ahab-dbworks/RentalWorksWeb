using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class StagedItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagedItemGridMenu() : base("{2A719A9F-F237-4C3A-92DF-FBC515204A38}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{91FF8909-CE37-4B6D-AD7C-BF656826E1D5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4829997A-3E5C-433D-8268-8CFB135BD13F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{EBFD6EDC-3293-4462-9D78-B32E36647B13}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AF9F591C-A9FC-41DA-B708-2C085BBBAF37}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
