using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ContractSummaryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractSummaryGridMenu() : base("{D545110F-65B3-43B7-BAA8-334E35975881}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{27AACE1F-3CD1-4905-978A-906B34720D7D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0C64196B-AE30-4718-9565-6FBA8EEEA4AC}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F58D3A34-51E9-464E-B2E7-D8D2477337B5}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DA0EF9D8-8DB9-41C0-8FD8-AEDB0E766BC3}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}