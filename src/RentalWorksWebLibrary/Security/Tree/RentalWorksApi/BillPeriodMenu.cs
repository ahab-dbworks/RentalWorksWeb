using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class BillPeriodMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillPeriodMenu() : base("{39AC96F1-2834-491C-BCDE-92CD8E2B11AA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{154403C1-5422-4754-9BB9-8993BCDF7013}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{06C9D1C1-3DDF-4BAF-8D48-40286F870EBB}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{EC1DF66E-F686-4BF4-A61C-19CAF8FA3EE7}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{D1D60908-B2F3-46E3-9ED3-DD9312DA4323}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{DECE507D-7730-4759-959C-8BB54CDBAFC4}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{76A5F7AC-2489-4A50-9120-2376BAB3D75A}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}