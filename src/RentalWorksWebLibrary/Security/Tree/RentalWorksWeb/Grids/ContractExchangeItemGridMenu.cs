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
            var nodeGridSubMenu = tree.AddSubMenu("{59043756-90D1-4CCA-9320-26C8FCA18868}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{30DDB2B6-698F-454D-80A0-23C5F2D24FD8}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{08596E41-9811-4BC4-9CE8-9F556588D906}", nodeBrowseOptions.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}