using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomFormUserGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFormUserGridMenu() : base("{FAAAE8F2-F68F-4B26-97E3-D143A80D1C18}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3708D594-A9E7-4A55-BC67-24CECC542F23}", MODULEID);
                tree.AddNewMenuBarButton("{17848CF2-B867-4866-B9C6-584FFF7AB044}", nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{236B49A9-65C5-46E6-956A-03B69CC636A3}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{33A1A95E-D2FE-461B-A6A1-1B8A6BED5EB0}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}