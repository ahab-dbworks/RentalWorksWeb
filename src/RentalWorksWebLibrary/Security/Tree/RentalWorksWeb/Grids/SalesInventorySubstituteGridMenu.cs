using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SalesInventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventorySubstituteGridMenu() : base("{ED6DCEB4-2BB7-4B52-915A-10E1D94B083E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{20290E82-6C00-4BF5-9356-B4FD7FCF55FA}", MODULEID);
            tree.AddEditMenuBarButton("{A0B35712-9BC0-4C46-A770-F323AC0139ED}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{4165418E-0347-4190-8EA0-74013EC9B067}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{61FF638C-FE80-4F9C-9A0B-BC6D69BB3939}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}