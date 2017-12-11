using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SalesInventoryCompatibilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventoryCompatibilityGridMenu() : base("{69790C03-D7CC-4422-9122-674E2BCCA040}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{707D66E1-1F11-4DF9-96D0-1B6A1BC4CD92}", MODULEID);
            tree.AddEditMenuBarButton("{B532596A-1F47-4B56-80B0-7D2592E6EEC2}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{10625B3C-AF4D-46B3-B8C3-1D94EB88CDEA}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AD8A7049-8A5A-4CA8-9C83-23652C8AE108}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}