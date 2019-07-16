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
            var nodeFormSubMenu = tree.AddSubMenu("{5156A93B-7B11-4DC4-BCBB-9DCB60FE7CAB}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{5642F55D-349A-41A0-805B-DF099F59A55F}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Cancel Check-In", "{52BEF7F5-C9F7-44DE-AD84-8E5AC68A9D7B}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}