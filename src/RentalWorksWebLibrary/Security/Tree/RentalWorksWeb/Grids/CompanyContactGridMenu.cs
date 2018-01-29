namespace FwStandard.Security.Tree.Grids
{
    public class CompanyContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyContactGridMenu() : base("{68E99935-E0AB-4552-BBFF-46ED2965E4F0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CCE48C43-8E43-4F7B-B0FF-06A055C398AB}", MODULEID);
            tree.AddNewMenuBarButton("{6D2C8640-BABA-46EC-A6C6-4440EB1219E8}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{60C5E33D-7243-4C8A-A46E-FCA633B49FDE}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{96A29C0A-3A75-43B4-8DE3-357F703C5C06}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}