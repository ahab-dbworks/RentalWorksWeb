using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CompleteQcMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompleteQcMenu() : base("{3F20813A-CC21-49D8-A5F8-9930B7F05404}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{7D52A4B5-17AE-4709-9885-6792606EDD9E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A333EF95-9E8A-42E9-870A-595C56DD3926}", nodeForm.Id);
  
          
        }
        //---------------------------------------------------------------------------------------------
    }
}