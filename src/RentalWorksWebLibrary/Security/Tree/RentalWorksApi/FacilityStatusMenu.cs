using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class FacilityStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityStatusMenu() : base("{61C5A81A-AAB3-43C7-9239-4E08CE8F3F34}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{4CBD88A1-AF2C-4DF5-A3CE-B2BB6C40092D}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{6C73F07A-50C9-471D-84B6-3115B8495662}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{6206FA53-91F1-4B6E-BAC9-D02490571609}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{521B52BB-9C28-425D-9B06-BFD0333FDCBF}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{92046B12-F2B5-4371-9448-01113F0FD496}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{149F4190-E046-4FB3-94CD-3F4C727C511D}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}