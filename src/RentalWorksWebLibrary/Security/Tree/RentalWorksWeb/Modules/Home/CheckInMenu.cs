using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CheckInMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInMenu() : base("{77317E53-25A2-4C12-8DAD-7541F9A09436}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{D4A58BEB-EC13-4959-90E5-847723B200A2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{0A06D8BC-6CE9-46AA-AD96-211FCEA44357}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}