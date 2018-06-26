using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ContractDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractDetailGridMenu() : base("{30A4330D-516A-4B84-90FE-C8DDCC54DF02}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{4671F311-7F57-44DA-AC55-050052B888D5}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}