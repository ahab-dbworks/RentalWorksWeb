using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class FiscalMonthGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FiscalMonthGridMenu() : base("{EB2DCCD4-0747-4055-87A4-0C60D811AFB5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C789E139-9745-400A-AFCD-FF7EB1C47E59}", MODULEID);
                tree.AddEditMenuBarButton("{E748B724-7CAA-45A8-B5D0-876E602D3295}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}