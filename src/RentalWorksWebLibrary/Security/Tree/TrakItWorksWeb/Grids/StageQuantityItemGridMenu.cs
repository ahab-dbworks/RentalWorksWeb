using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class StageQuantityItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageQuantityItemGridMenu() : base("{162DCF5B-759A-42E9-82E9-88B628B6901D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0425EA23-2AE9-4DF7-8B77-8FCA4D8114D0}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{AD7429DE-118D-47D3-A541-4E7B1E18F33D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{44E58614-DD80-476A-BF9C-C4D67BDCD42C}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5651416C-89AE-4EEA-85BA-440D509114E2}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
