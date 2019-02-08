using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CompanyResaleGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyResaleGridMenu() : base("{571F090C-D7EC-4D95-BA7B-84D09B609F39}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0542BBB7-85D5-4308-BF10-D078B84C8617}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{20572077-F1C7-405C-A19D-6D159A343C63}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E24C60ED-F834-4FFE-B9DF-A64E29321AC4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{41E7D2A7-A13E-4683-9E67-844B82FF4823}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{EABFCF5B-FB49-460F-B64D-88C2A213DB65}",    nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4399FB51-13F4-4AA3-A5D5-785E0CC7C638}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B93E4AF6-EF21-4CA7-8A33-B574D67E56C9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}