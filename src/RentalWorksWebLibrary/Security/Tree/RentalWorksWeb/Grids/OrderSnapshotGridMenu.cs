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
            var nodeGridSubMenu = tree.AddSubMenu("{74654A03-44AF-41DE-B6CC-E8324B6B6CD1}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A9DFBD0C-6C5E-4B39-AB33-A99A25F42CC5}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("View Snapshot", "{C6633D9A-3800-41F2-8747-BC780663E22F}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}