using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderActivityDatesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderActivityDatesMenu() : base("{E00980E5-7A1C-4438-AB06-E8B7072A7595}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A7A73C1F-1B91-4A44-BA8C-FDB5EB6CB9DA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{25AFC344-D7FE-4C99-B665-9C227DD5910A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{00FABB1C-DDB8-4069-876C-C922CCB3EE31}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7D1A4F4B-73FA-47D8-914C-D5DCD8A1CC4C}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{EAE3E658-3409-4F09-ADD8-357660AAD131}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F160E989-CA6B-465A-B018-8B71CAEFC6E8}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{FFEB05F6-B0F7-4963-A374-C84A6D8FFFF6}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{46F7165B-47F2-4637-865F-717FD3DA9760}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}