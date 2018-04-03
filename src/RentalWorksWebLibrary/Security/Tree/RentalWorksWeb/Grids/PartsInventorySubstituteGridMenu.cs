using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventorySubstituteGridMenu() : base("{F9B0308B-EBFC-4B37-B812-27E16897B115}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3D72C2A0-7F84-4313-8273-08259A254AB5}", MODULEID);
            tree.AddEditMenuBarButton("{B8B48179-06CA-4442-9046-733721727EE7}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{7860DA4A-DF22-42BE-80EA-D0B7E7310B9C}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6FCF0571-0821-43C1-A7EE-3F529529536F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}