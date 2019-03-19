using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class CreatePickListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CreatePickListMenu() : base("{1407A536-B5C9-4363-8B54-A56DB8CE902D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{C796897C-65E9-420E-82BD-0D211A60B141}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AF28AFB8-E59D-44AA-9A04-25879FAE38FC}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{95EF4FEB-FC24-4373-9527-5F86A4F89D74}", nodeFormMenuBar.Id);


        }
        //---------------------------------------------------------------------------------------------
    }
}