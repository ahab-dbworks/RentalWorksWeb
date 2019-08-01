using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Utilities
{
    public class RefreshGLHistoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RefreshGLHistoryMenu() : base("{8F036E39-78D3-4FB9-A98E-BD33A5DB7FDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{E80A01EB-F885-48DC-8865-012DCF386C00}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{48527C0C-8325-4EA6-A875-18ABE849BAA5}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{3F69FF7A-9E65-46A0-906F-D3E3D75A3A0E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}