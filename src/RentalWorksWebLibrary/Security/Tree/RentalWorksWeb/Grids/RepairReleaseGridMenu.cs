using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairReleaseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairReleaseGridMenu() : base("{06BFFEEF-632D-4DBE-9DFC-E64309784D44}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{349CBF69-2196-4FB2-950C-7E95DD3FAC67}", MODULEID);
        }
          //---------------------------------------------------------------------------------------------
    }
}