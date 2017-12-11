using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ItemLocationTaxGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemLocationTaxGridMenu() : base("{7DDD2E10-5A1E-4FE9-BBA5-FDBE99DF04F6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F3EEBCAC-EC1E-4B22-A6B8-625EA44839BC}", MODULEID);
            tree.AddEditMenuBarButton("{B29C1E3E-B843-424B-BEDC-984DFBBED7D8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}