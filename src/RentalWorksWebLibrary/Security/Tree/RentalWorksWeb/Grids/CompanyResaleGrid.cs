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
                tree.AddNewMenuBarButton("{EABFCF5B-FB49-460F-B64D-88C2A213DB65}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{4399FB51-13F4-4AA3-A5D5-785E0CC7C638}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{B93E4AF6-EF21-4CA7-8A33-B574D67E56C9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}