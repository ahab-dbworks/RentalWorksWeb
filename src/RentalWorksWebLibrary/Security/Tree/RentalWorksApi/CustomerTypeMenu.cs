using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CustomerTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerTypeMenu() : base("{8B861232-B0CC-4CDA-AD86-4B72AA721B73}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{A681A1CC-5F38-4C6A-A96A-9B72EC884EB4}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{38EC0AB9-BA14-429F-ACEE-F5F7A9130A7D}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{B8EB8539-A5EE-4E9F-90A8-599BFB548988}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{4A6544A8-12A3-4E13-92CB-19207AF1187E}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{AEAA52B7-CD39-4EFC-A9AB-54BB373F3A66}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{F88A7D84-D56C-4AD7-AE9C-4B55EA53CD17}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}