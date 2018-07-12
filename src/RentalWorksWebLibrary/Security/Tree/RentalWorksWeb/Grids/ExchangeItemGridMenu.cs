using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ExchangeItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeItemGridMenu() : base("{B58D8E40-D6C1-45D4-97B8-18A1270822B9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B166CDFB-13E3-4559-8B4F-2C56F061FDFB}", MODULEID);

        }
        //---------------------------------------------------------------------------------------------
    }
}