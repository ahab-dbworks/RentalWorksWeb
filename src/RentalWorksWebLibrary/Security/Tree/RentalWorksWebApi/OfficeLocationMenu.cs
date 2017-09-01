using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class OfficeLocationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OfficeLocationMenu() : base("{C78B7368-C42E-4546-83CC-B7B6A1006FF0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{79FB176C-8272-4858-9DEF-215A65EE3BE8}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{5D726B57-ED66-4700-A9B9-434DBE4457A5}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{5551F358-E319-4A01-9DA0-2C88C687A2E9}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{7E867130-C7D5-480D-BB0E-F9AEA31AD3FE}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{E4E6899F-1A17-4B3E-B807-4EE53956DCB4}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{C597BF5E-8F5E-44AC-AAFA-0140DDE1A5F7}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}