using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class IntegrationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public IntegrationMenu() : base("{A68213D3-809F-4591-9EE9-F4E06FC6984E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{04385F32-A091-40FD-9285-EB61FE1D82D9}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{664D0EEC-D42B-4FF0-9B9A-E161570D65CF}", nodeForm.Id);
                    //tree.AddSubMenu("{D13FE10F-986B-4B1B-99DE-0E0EBE0CACDE}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{5F9D5FA3-A771-4E85-8793-148BA557ADD5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
