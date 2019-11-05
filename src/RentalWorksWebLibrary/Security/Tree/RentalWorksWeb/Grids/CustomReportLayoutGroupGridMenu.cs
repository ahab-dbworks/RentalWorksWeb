using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomReportLayoutGroupGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomReportLayoutGroupGridMenu() : base("{05CE64FC-9293-4013-A941-529B9CD21B87}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FC2E6A88-C9B0-4395-B02B-F2D5B1D8B490}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{64217B10-08D6-4245-9717-73E730C421A3}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{7C843129-0866-47A5-822C-DBAA21B70068}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F1D474A6-441A-4915-AA3C-287B766B74F4}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{1FDF1EC3-F674-4AAA-8902-73AD1331191E}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{64BA796A-7CBD-438E-A3A4-13B657DD6329}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{81F96479-7E4B-46E1-B879-3D90F43FD85A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}