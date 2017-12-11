using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeCoverLetterGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeCoverLetterGridMenu() : base("{7521D3CC-FF1C-44F5-8F93-9272B6CADC64}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A4C750FF-0761-4ACC-BC23-0C732FAD4BE8}", MODULEID);
            tree.AddEditMenuBarButton("{41BCFA02-C8AC-43EA-94D5-94300A0DD007}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}