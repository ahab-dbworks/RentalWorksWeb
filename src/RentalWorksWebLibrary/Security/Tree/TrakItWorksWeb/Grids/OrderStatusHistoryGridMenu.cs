using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderStatusHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusHistoryGridMenu() : base("{A3683C2F-A5B4-42FC-A944-DAA65ED71E87}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{15BE6326-C4AA-493B-AA7C-8CA4231DB8FE}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D3772ADE-E4D7-4C08-88D0-1B5C046137C6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9124D28C-3883-40EC-92FE-1BA46704BF6C}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5A0FC1FC-FE63-4B6E-9B9F-3D6AEBDD23D3}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}