using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class PersonnelTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PersonnelTypeMenu() : base("{874B18DE-2BA6-4427-9D20-54F64072C003}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{637BBFEB-0969-4213-AC47-57A430CB6738}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{1539F106-5CAA-4EA0-92C2-8A43C5E39318}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{2A3F6534-A2AD-400A-AC12-261CAFF601C6}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{3D2625E1-2B92-45AA-9A37-5F8AD4463998}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{8FF859E5-B844-412A-A148-854C1E7BC859}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{B39F2E96-EA2C-4D67-B947-1EEC48D403E4}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}