using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContractSummaryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractSummaryGridMenu() : base("{9CE13261-0A5D-4B21-BC4A-3E6A18E80492}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3FC2A280-92C0-460B-95CB-C171D4AF2340}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{41C36F19-E3D7-4A25-8D4D-2BE987A95C24}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B95284D7-A986-4E89-BC7D-DDDF0E8A7F8A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{69E03DD9-AF01-4722-8760-DA1E1354129D}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
