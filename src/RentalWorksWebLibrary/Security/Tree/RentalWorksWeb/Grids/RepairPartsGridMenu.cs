using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairPartsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairPartsGridMenu() : base("{D3EB3232-9976-4607-A86F-7D64DF2AD4F8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CB941EF8-ED90-451C-9A65-55CA55E6222A}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}