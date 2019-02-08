using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class FiscalMonthGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FiscalMonthGridMenu() : base("{EB2DCCD4-0747-4055-87A4-0C60D811AFB5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C789E139-9745-400A-AFCD-FF7EB1C47E59}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{96494BB5-9EB2-4CF0-81BD-21AEFC1CAF1E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E26E3274-5F20-443F-B42F-D61565A37FFC}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C6BF73B0-AA9E-484D-8D96-41E2FD68894F}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E748B724-7CAA-45A8-B5D0-876E602D3295}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}