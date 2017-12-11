using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class DepartmentMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DepartmentMenu() : base("{0DF9DBC3-8755-460C-B830-FF169CC3D859}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{EE8AEB08-C76C-4C7A-BC44-701306F7EDE2}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{FA2F2119-F187-4F94-A6BA-62EF1DE5892B}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{562375F5-18F6-4A11-8B91-F9185988358B}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{032B5C1D-B986-47F2-BE91-FC3FBF63319A}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{A5FB724E-DCC6-49C8-B15F-625C902303ED}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{4F942871-81C6-45E1-95D2-D4844D91F9AD}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}