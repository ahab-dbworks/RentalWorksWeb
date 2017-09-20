using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomerNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerNoteGridMenu() : base("{50EB024E-6D9A-440A-8161-458A2E89EFB8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{4C2DE149-AAE0-449E-B044-D8A570DC974F}", MODULEID);
                tree.AddEditMenuBarButton("{10EC35E9-BC23-452B-89DD-1617B7F2B8D4}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{C62F8041-1398-4CA1-87BB-8833F5FED485}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}