using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReturnBarCodeGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnBarCodeGridMenu() : base("{C25168A5-1741-4E77-83C9-CA52FBC2C794}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{081FCA2E-645E-4137-ACF9-BE686E74735E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}