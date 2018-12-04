using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StageHoldingItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageHoldingItemGridMenu() : base("{48D4A52C-0B47-4C85-BAB7-8B0A20DF895F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5D194EB8-7436-4C54-95C6-0BAC10BB707F}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}