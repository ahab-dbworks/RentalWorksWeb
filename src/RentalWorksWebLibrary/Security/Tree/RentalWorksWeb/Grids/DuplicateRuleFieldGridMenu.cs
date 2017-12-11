using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DuplicateRuleFieldGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DuplicateRuleFieldGridMenu() : base("{0B65E7C7-E661-466A-BBFA-D2A32FB03FF7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FB3DDC34-996C-42F5-93C8-1F837EEC6950}", MODULEID);
            tree.AddEditMenuBarButton("{97248D6C-78FE-49A5-B662-599F6FDE34C7}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{06735481-45AC-4601-9FA4-CE251A484AA4}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1E2E3C5A-B395-4379-8699-0EA6E249B665}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}