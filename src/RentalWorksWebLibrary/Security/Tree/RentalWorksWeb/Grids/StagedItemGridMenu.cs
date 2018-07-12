using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StagedItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagedItemGridMenu() : base("{132DEBAB-45F6-4977-A1A8-BAE5AC152780}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{246CC445-AC0B-4260-AF45-4918A5F76EA2}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}