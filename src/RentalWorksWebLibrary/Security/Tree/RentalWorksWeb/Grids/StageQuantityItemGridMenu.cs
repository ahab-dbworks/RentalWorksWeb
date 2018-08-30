using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StageQuantityItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageQuantityItemGridMenu() : base("{3CCB3EB0-983F-4974-9F7F-8B12A8C7DDE9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F367CE76-4D4A-4523-91A8-52731138DABC}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}