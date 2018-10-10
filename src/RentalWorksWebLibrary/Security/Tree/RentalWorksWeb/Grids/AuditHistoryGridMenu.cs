using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AuditHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AuditHistoryGridMenu() : base("{FA958D9E-7863-4B03-94FE-A2D2B9599FAB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B3456F29-6CE4-4A62-9462-034A9D8835F7}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}