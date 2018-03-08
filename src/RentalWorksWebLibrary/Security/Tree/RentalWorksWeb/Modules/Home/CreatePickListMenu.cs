using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CreatePickListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CreatePickListMenu() : base("{5013C4FF-FC42-4EFE-AE9D-AAF6857F17B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{4740F26E-384E-4F2F-A689-911053BAB610}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E768CD00-3EC2-429D-B26A-FE3B8F85C47D}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{C08AF0B0-13F0-4E70-87C7-7853D578F49C}", nodeFormMenuBar.Id);


        }
        //---------------------------------------------------------------------------------------------
    }
}