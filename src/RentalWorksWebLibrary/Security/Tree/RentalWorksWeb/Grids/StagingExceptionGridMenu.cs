using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StagingExceptionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagingExceptionGridMenu() : base("{28DA22B8-D429-4751-B97D-8210D78C9402}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5BFDD131-A05F-4106-9153-8B993DFC43D4}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}