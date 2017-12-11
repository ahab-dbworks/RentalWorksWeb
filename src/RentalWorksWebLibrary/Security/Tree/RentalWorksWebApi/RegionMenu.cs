using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class RegionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RegionMenu() : base("{105E6E96-475E-4747-92C9-164F2E8F2DD7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{DD8AFDBC-6A1A-4E42-8BD4-00DF9084FC56}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{7D97CFC7-D058-4248-8E1F-023C2AAEF7A0}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{41C7D68A-8E11-4CC3-9F1A-43946EF849E2}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{A9D28634-E33E-41BF-A43A-A2CD7766173B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{685CA143-7F6F-4DA1-BC13-379C63041417}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{86D9A573-4647-4379-870D-39CFBE49E48E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}