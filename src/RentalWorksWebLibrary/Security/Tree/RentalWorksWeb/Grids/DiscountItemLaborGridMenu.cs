using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemLaborGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemLaborGridMenu() : base("{B65A5839-0226-4BAD-99F9-64FA9D1C1E33}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{03743206-2007-422F-B147-71C7F3C0D88B}", MODULEID);
            tree.AddEditMenuBarButton("{53E7EADF-D52F-497D-8DAA-E81BEF777A21}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{6853FD75-03E4-4B97-A9B0-2A91DDC62EF8}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{739A37AA-B256-4B41-90D0-858D8C035E2E}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}