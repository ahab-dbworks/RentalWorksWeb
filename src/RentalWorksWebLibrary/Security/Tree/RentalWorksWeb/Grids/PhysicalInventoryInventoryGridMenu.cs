using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PhysicalInventoryInventoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhysicalInventoryInventoryGridMenu() : base("{B4256DE8-60A8-4C7F-B35E-FB6971263793}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CA53ED73-4CF2-456F-AEF8-CBC0B68100EC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{930C65C7-82EC-4097-984B-B4838626A641}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{700321C2-783C-433C-9C7A-85931DBFAA29}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B6D018D9-7DCE-4E21-B96E-2BB0AEE70AB4}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{83BF2014-D8CC-4EA7-BEBF-93692D9E85C4}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{586CF89C-B64E-4622-82EE-4C9F92156C4A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{971DF574-B265-4AC8-901F-D4AAF2664AE7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}