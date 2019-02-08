using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusRentalDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusRentalDetailGridMenu() : base("{5B497696-B956-453D-A2A0-755B84F8E83D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6A1C1AB-3205-40E4-ACF1-95D9BEC89B2B}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3706F08A-5292-48C4-8C96-3AC6E784322A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8D4A8D08-52A9-4114-958C-BE792B16C503}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6A697CD8-3783-4906-A0AA-F02C60A18CCC}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}