using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ContractExchangeItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractExchangeItemGridMenu() : base("{E91A6E7B-9F19-4368-ACD1-19693B273161}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9543C871-D027-4937-B775-906D80BDC863}", MODULEID);

        }
        //---------------------------------------------------------------------------------------------
    }
}