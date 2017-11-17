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
            tree.AddNewMenuBarButton("{27C3387C-6402-4F04-99C3-206216E8C649}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A63B3B97-AF16-4D88-BC33-C0EBB7297493}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}