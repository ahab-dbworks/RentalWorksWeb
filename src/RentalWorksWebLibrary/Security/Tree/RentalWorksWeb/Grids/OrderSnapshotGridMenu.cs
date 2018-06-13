using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderSnapshotGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderSnapshotGridMenu() : base("{4259A144-C7C8-4382-BAB9-9FFA278AF294}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A20527CB-B0A1-4E38-9601-E1BF7422CA67}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}