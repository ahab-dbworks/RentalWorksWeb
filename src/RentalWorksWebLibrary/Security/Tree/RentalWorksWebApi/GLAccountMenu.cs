using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class GLAccountMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GLAccountMenu() : base("{04730651-F4CD-47BC-ABAD-3B0B0F929AFD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{18BE67E6-DD35-467C-8A1E-4CE8E037E4EE}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{1F72077F-3984-4129-8F0C-5F3654887D33}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{7791AADC-7DA8-4421-A799-7849947C46ED}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{E9BAC740-C851-4C6C-A4AB-ABE0B9C48BD1}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{5CC4EF58-E0B2-42E5-AC0F-CD1B302F354A}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{77B67FE4-C822-4F15-BF59-EA5E606E88E5}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}