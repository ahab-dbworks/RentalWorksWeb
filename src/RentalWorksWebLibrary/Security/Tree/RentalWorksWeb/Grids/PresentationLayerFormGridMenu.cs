using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PresentationLayerFormGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerFormGridMenu() : base("{88985C09-65AD-4480-830A-EFCE95C3940B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6B953A3-D43E-407E-9F67-6C51A6D0BBA3}", MODULEID);
            tree.AddEditMenuBarButton("{456922E0-B3BB-4012-BB40-DFFC7CB6A508}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}