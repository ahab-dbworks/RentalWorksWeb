using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class OrganizationTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrganizationTypeMenu() : base("{850B2B9F-A3A8-4162-809D-DFBD52262B76}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{FA04143C-1650-4427-8C06-55747A748B8F}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{CD82B877-4D20-4405-BC19-9B826582C4FD}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{BC3B68C7-232D-482D-AB40-FF62A01C8EFB}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{E53368D7-54BD-4D64-B967-747FD8CA56C4}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{DC0C7C5E-9284-4102-8CD3-8320E69EDF52}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{1B602F22-2EC1-4378-A887-4021A0385358}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}