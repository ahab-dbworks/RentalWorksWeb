using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AttributeValueGridMenu() : base("{C11904A1-D612-469C-BFA6-E14534FC8E31}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1C69781C-3638-407E-9C0A-EBC4BBA6A4A4}", MODULEID);
            tree.AddEditMenuBarButton("{8D0BF6A9-AD7A-4B28-82A0-3A435B4BBD7C}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}