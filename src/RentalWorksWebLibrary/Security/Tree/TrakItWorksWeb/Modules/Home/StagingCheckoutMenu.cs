using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class StagingCheckoutMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagingCheckoutMenu() : base("{AD92E203-C893-4EB9-8CA7-F240DA855827}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{36B06F73-177A-4918-AFB6-4FF19EC94473}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{ADB7B24A-F59D-4314-93FB-459CD532DC4A}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}