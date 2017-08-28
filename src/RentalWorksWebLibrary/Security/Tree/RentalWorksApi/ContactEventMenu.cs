using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class ContactEventMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactEventMenu() : base("{CC37FF90-094F-4A5D-B4D4-3552B5B8D65A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{5973FA5B-5519-45DC-9ABF-EF6AF65471C1}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{1415227E-1A40-4492-B519-462EC788CDE1}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{15A4DD14-CE3C-454E-B475-62B6BE30081F}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{35C46276-BBB7-43ED-BBE6-98FFB12655DC}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{A2EC754D-B1AB-4355-8803-30DF1D42B49D}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{7B9C2F8D-D527-47DF-8F79-8C554104EA2C}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}