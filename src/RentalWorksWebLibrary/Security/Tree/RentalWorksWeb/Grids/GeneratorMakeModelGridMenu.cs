using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class GeneratorMakeModelGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorMakeModelGridMenu() : base("{12109673-165E-4620-8121-AF4259C7F367}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3A7A4491-FC6A-418D-84B5-BCAC29ECDE49}", MODULEID);
                tree.AddNewMenuBarButton("{3D60AE6D-EB51-4E4E-B26A-E01AE820CC44}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{8FED0349-5719-4E10-9449-340808729B8C}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{3C2E1ECE-97D3-403C-BE0B-ED52295650A7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}