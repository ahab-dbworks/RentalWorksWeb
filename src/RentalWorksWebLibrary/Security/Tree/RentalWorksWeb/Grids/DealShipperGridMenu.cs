using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DealShipperGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealShipperGridMenu() : base("{032CBF05-9924-4244-AB5A-B5298E6F7498}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1D30009E-EB84-471D-9DB4-09AF884EFE35}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{AF0EA3C5-D80F-4C74-B5A0-380C1E9378B9}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{140875D6-8798-4C0F-8047-D6D305F2A763}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{F9CEDD25-65CE-4C59-8BA7-1D6AA7D584A7}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{65297441-1B20-4E15-B0C4-AA7BB8C9DDF9}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{28E876FF-B36A-4B1B-8729-5988FA8D84E6}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{07853CBD-5DD6-40C6-87FF-0232B74C929D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}