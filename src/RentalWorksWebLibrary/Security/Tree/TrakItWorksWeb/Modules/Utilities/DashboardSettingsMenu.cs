using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class DashboardSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DashboardSettingsMenu() : base("{AD262A8E-A487-4786-895D-6E3DA1DB13BD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{5BC9A0A2-2FC9-4804-8FC5-BEC08785CFD8}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{1E8576C3-E36B-4527-B0AF-441A44129445}", nodeForm.Id);
            //        tree.AddSubMenu("{8DF58E6C-6F0B-4AD3-A44F-40C7CDEC43A4}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{4766A850-A8D8-4CE4-B6AA-A7BDEF2DA449}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
