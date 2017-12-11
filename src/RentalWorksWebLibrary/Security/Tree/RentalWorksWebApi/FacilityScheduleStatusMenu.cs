using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class FacilityScheduleStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityScheduleStatusMenu() : base("{3249CECE-FCDC-4B51-96B6-13609CB9F642}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{EE95A8B5-B9E7-4C19-8085-286D7E85F7F9}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{196E7BFC-8E31-42E3-A776-4E8B2B66AD47}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{17E60B4C-49C0-45DB-B9A5-1E7805A05AC2}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{260ECC71-B40A-4626-9B3C-F6FBA00F4EBF}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{2AC9A850-6CCF-4997-ADC7-B125A9887CAC}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{E38E7214-0410-48B9-B7C8-F47089102F1F}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}