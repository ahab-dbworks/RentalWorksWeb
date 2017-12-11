using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class EventCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EventCategoryMenu() : base("{7D659770-FEC6-4580-A93B-29C766B9DA4B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{C9D7D24A-2CD3-4979-B6BC-B9DDF4070AAF}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{08DE89EE-4C55-4170-B1D8-33EC04F2745C}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{ED9F7706-C1C0-41B7-8A05-BD2BCB224DF4}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{FADA1CF8-C517-4A23-966B-97E7A784B16B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{7EFD1410-B290-4C6F-8AF4-61D42667E7D7}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{138C6D9F-5B58-4619-A4E5-AE7C11EA51CB}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}