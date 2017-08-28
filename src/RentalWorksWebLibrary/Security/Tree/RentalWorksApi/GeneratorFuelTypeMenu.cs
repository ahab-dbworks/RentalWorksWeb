using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class GeneratorFuelTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorFuelTypeMenu() : base("{FC2DA3CA-B5CC-4490-8D20-C121A1D3F7D6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{A159EF71-9F4D-40F6-8027-0AB1FC7A7CE0}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{9B922EC1-D8C9-492E-91F4-E234E6AFAC64}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{BC15A1F5-FAB4-4A2B-AD04-F9B7E3462560}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{90835994-22C3-4742-8634-44FEDC574D8B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{9AFAD4EB-3629-4524-9688-7C7E46974AA8}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{A3BBBC9C-0447-440C-A19D-927CA3559DD3}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}