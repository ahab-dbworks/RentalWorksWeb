using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RentalInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryWarehouseGridMenu() : base("{3AC00695-4130-4A34-B4B2-BC6E3E950FB1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{ED2DF78A-FFB2-472B-8848-3C8D9988A68E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}