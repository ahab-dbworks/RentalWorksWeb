using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class VendorClassMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorClassMenu() : base("{61ABD143-5A80-424F-998A-A264F5806709}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{6D41DBCD-7E68-45B5-A91E-9D43E46D7C48}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{39E35BD8-9B0B-4E67-A859-447D86FCD72E}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{95615F52-47DD-407A-8371-4046240B7921}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{39FF6036-A4FB-4861-B555-14B771DCDB66}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{555BFB44-2988-4368-81AA-E411F61B5F6A}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{F2E8AE29-64B5-4806-B7EB-6594D823DA5E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}