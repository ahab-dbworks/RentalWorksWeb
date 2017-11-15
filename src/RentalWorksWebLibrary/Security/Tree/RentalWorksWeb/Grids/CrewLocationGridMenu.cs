using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CrewLocationGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CrewLocationGridMenu() : base("{FFF47B06-017C-417B-A05B-AD8670126E06}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{58B2D26A-E373-497D-AF08-CD4E44417E22}", MODULEID);
            tree.AddEditMenuBarButton("{468E59BD-A83B-40A3-9639-CFA2C1D80DA8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}