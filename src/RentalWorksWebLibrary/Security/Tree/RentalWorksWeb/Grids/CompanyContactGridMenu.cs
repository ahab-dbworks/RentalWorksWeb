namespace FwStandard.Security.Tree.Grids
{
    public class CompanyContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyContactGridMenu() : base("{12C1C7E7-FC37-4ED2-807B-FFD5D6BF73C6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B54A8AD6-B81D-40CC-A2A4-4D93FA841313}", MODULEID);
            tree.AddNewMenuBarButton("{5CFE8084-B5CD-497E-8808-D776361B89DE}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{15D53BDF-BF12-42E8-A3D5-F3376723D23D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A36AF5AD-1EF2-47F8-88F3-A6CC802719F3}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}