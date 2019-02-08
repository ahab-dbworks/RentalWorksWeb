using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairPartGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairPartGridMenu() : base("{D3EB3232-9976-4607-A86F-7D64DF2AD4F8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CB941EF8-ED90-451C-9A65-55CA55E6222A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DE7132C7-6C21-4873-9D42-C63F271F2CAE}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{7845049C-3BE5-4B1D-891B-DE328252C2CF}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6020EC88-76FF-443A-BDF8-A843AAA1E538}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F0572D10-6C5F-4D23-AD87-D3342B6F7925}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{E1663AC2-57E0-4323-94D9-F164342143EA}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1113D116-93E2-4F8E-A1D1-BAECB833A7C1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}