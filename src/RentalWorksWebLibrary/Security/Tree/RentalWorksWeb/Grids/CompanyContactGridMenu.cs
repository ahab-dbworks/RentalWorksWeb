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
            //tree.AddNewMenuBarButton("{D06EC9BE-2A02-4B5E-92D9-8530868A565A}", nodeGridMenuBar.Id);
            //tree.AddEditMenuBarButton("{1C79206D-7071-45B1-BC03-7D969A81022E}", nodeGridMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{BA816B39-A08B-4B21-B32B-69484701BD9E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}