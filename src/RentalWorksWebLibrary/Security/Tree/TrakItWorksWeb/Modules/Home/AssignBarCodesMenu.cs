using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class AssignBarCodesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AssignBarCodesMenu() : base("{81B0D93C-9765-4340-8B40-63040E0343B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{9C9B425D-020E-4023-986A-99F14C016E9A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C699CFEF-C910-4F8C-B4F3-C6444C78664B}", nodeForm.Id);
                //var nodeFormSubMenu = tree.AddSubMenu("{941E5D44-C1C6-4847-90E7-F268F5A74BC1}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}