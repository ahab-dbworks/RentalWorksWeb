using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class EmptyContainerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EmptyContainerMenu() : base("{60CAE944-DE89-459E-86AC-2F1B68211E07}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{419837F8-67E9-468F-8046-F7DDF4B184A8}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{84F63727-FD4F-46AF-BCF4-301B52856EA3}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}