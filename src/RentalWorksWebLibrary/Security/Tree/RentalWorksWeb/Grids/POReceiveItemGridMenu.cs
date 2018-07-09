using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReceiveItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveItemGridMenu() : base("{EF042B8D-23B8-4253-A6E8-11603E800629}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{462FA992-2286-4496-8537-4A5DD79F2520}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}