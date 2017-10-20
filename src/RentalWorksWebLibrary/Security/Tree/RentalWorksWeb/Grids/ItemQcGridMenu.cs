using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ItemQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemQcGridMenu() : base("{496FEE6D-FC41-47D7-8576-7EF95CAE1B18}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F0863966-2736-46CD-B215-13D26B466FB2}", MODULEID);
         
        }
        //---------------------------------------------------------------------------------------------
    }
}