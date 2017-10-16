namespace FwStandard.Security.Tree.Grids
{
    public class ContactPersonalEventMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactPersonalEventMenu() : base("{C40394BA-E805-4A49-A4D0-938B2A84D9A7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B541F4C8-4232-40B1-9A44-030E3C35E8E1}", MODULEID);
                tree.AddNewMenuBarButton("{0AE5F6DE-26E0-4799-9273-CF1EF4A2E149}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{80B3714C-FBBF-44BD-A05B-C338CE22161B}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{B0601E5C-BF9E-41FC-9732-47EAB4ACDAE7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}