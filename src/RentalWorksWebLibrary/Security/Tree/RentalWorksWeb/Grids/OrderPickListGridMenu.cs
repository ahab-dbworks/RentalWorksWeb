using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderPickListGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderPickListGridMenu() : base("{ABE29218-C144-4CA7-825F-3FDA7DC860A5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{74D6E288-0B70-4CC5-A6EF-0D572C27A5B4s}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C541821F-E4AE-4688-9208-273D8B617BB4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{926C0DA3-E440-4DEB-9B03-8153DB5B1FF7}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("Print Pick List", "{04B03223-8240-4F1A-9905-ECD2823F96B0E}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Cancel Pick List", "{C6CC3D94-24CE-41C1-9B4F-B4F94A50CB48}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}