using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderStatusSalesDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusSalesDetailGridMenu() : base("{5B13583B-503D-4F1F-AA67-BDBE3B0AB59C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{4E90F41F-7412-41C8-B5F6-069DBD77C921}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3A11D092-ED9E-4471-90F0-666C979BDF93}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{DE64C41E-C6CD-46BB-8168-8B55725AC64B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{66BA945D-2EA0-4C0F-ADCB-F8BC4457F81D}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
