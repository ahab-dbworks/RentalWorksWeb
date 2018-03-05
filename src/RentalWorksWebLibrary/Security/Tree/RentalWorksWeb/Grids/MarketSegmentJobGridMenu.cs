using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class MarketSegmentJobGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MarketSegmentJobGridMenu() : base("{6CB1FD8E-5E6E-45BC-B0E6-AC8E06A38990}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0A014A97-9FC2-4ADA-A225-8CD2ADCC6A84}", MODULEID);
            tree.AddEditMenuBarButton("{FD6BFA89-4627-4AB5-879B-8720ACDFDAB6}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{A5A508A7-66B6-45CB-9B69-E3C092695592}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B953B4A9-3717-4A66-A98C-75E6383FE218}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}