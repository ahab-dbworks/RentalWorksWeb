using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairCostsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairCostsGridMenu() : base("{38219D4D-C8F6-4C8C-B86B-D86D5F645251}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{08D9E43C-033D-4656-B94A-BDFC550E5D67}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}