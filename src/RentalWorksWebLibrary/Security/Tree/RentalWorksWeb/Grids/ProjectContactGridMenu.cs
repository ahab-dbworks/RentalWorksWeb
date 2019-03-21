using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ProjectContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectContactGridMenu() : base("{F0D3B8C2-1CEC-41B6-81E4-D7B9C821684B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{837998F0-9752-4E45-BE91-43142AD1E011}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8969A7A7-A5E0-428D-95B2-7317AD9666C6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A981CA0F-2BB1-4ED0-9B73-CBE473177D16}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D9BC57ED-8BE1-4C59-9CA5-0B5BA2959E21}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{3A5EF6D7-550C-4B35-95C3-1A27AA7EFE0B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{46280398-D801-4B6B-9FDB-FAC6CF94E908}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{912EDEE5-F50E-4E72-9000-95C2AAA07FF9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}