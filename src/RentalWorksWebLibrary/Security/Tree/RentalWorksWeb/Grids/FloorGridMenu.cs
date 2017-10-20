using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class FloorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FloorGridMenu() : base("{472B5E4A-57BB-4DFB-AD6A-D0F71915124B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0567D5ED-2A54-41B2-B82B-0C2CC9170392}", MODULEID);
            tree.AddEditMenuBarButton("{984186F5-0C1A-4D88-8916-021F93E04636}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{0FCBBD5A-2184-4731-BB3F-3E1F25A03506}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1C16B572-B469-48E1-BC8D-F011E8B00027}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}