using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class RemoveFromContainerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RemoveFromContainerMenu() : base("{FB9876B5-165E-486C-9E06-DFB3ACB3CBF0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A28F5389-BC53-420E-A313-FC9F47CBE1ED}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3D828E5A-AD1E-4D30-9A88-C6AF3642C5A0}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}