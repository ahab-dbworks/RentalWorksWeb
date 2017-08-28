using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class MailListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MailListMenu() : base("{1ED190A8-051D-46C9-823E-825021CCCDBE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{88D2E629-F2E9-4E66-BD37-50DBD274FC95}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{1656CC13-F8CB-4B58-846A-E3797F18B923}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{74D79F00-7892-4611-A3C6-8722C092D204}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{37185FE5-0704-44C4-B805-8DA91A47EB5F}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{7E546D25-33A2-4534-888E-6E22EAA50CDE}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{E7EA2659-081B-45F1-84F0-A2E8DE9B4EA4}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}