using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderStatusSummaryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusSummaryGridMenu() : base("{959E3D3C-B83D-4ACC-997D-A5508DE0A542}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{90DD5FC2-D29D-4340-BBCB-2355DB420844}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6467D643-F704-4F77-A922-0A6EDA0C9E78}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{68ABA86F-4FD6-480A-A281-BE7B5943FBDE}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{423B5DDD-A237-4E18-B656-0517E8803AD3}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}