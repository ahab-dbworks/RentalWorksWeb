using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusHistoryGridMenu() : base("{D5B97814-9FD7-4821-9553-28D276F67797}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8C067DE7-27CC-4B2B-9AE0-F772DEB5AF6D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CB7A74C8-4C8F-41A0-A3EF-24FADD503372}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E135E256-27B1-4C69-B110-5EF73D11753F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D859E507-A19E-4C45-AC32-3B38407E68F4}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}