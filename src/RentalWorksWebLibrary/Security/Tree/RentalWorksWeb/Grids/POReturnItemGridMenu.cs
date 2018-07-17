using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReturnItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnItemGridMenu() : base("{10CF4A1A-3F85-4A8C-A4D7-ACEC1DB12CFC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{13EA730D-54E9-4DBD-8C8B-B5DEBAA788BD}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}