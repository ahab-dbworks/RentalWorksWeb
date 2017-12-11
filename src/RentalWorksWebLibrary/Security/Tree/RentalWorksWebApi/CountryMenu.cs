using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CountryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CountryMenu() : base("{251CEBD0-5EF5-4025-B3FF-84840F0D2525}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{4CAB8318-2AE7-424F-BB5C-BD6AC4CCA718}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{BE341297-9E45-498E-8081-335C486D1894}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{078A614A-2206-48BC-A40A-9A9B40F69724}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{37DE2307-C6A2-447E-AE77-05C9338A3E39}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{B0C2E80E-512C-449E-8177-CE6D9E39D067}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{056645FB-E33F-4784-B25B-FA57B76F1FC7}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}