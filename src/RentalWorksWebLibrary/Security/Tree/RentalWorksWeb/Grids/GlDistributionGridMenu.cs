using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class GlDistributionGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GlDistributionGrid() : base("{A41DF75D-A3A3-40B8-84E0-7B8F8DACDC35}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3824BD3D-C561-4ACD-8867-7B60631770D7}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
