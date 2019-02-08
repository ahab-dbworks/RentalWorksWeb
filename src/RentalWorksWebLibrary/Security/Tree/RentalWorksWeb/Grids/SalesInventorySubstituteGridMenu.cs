using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SalesInventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventorySubstituteGridMenu() : base("{ED6DCEB4-2BB7-4B52-915A-10E1D94B083E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{20290E82-6C00-4BF5-9356-B4FD7FCF55FA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B71E241D-9DE7-4D7A-80BC-8DD5F7A25CDB}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{5E115143-C30D-4717-BD08-00D5CC7C4DCE}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{84D7CB0E-33EA-444A-A677-07A307BB599E}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{A0B35712-9BC0-4C46-A770-F323AC0139ED}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{4165418E-0347-4190-8EA0-74013EC9B067}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{61FF638C-FE80-4F9C-9A0B-BC6D69BB3939}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}