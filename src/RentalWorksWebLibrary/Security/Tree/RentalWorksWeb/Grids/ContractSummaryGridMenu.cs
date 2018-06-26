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
        }
        //---------------------------------------------------------------------------------------------
    }
}