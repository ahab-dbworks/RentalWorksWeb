using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class DealTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealTypeMenu() : base("{44BFDDB3-D93B-4A99-A38F-A0CE8162413F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{9B31A9CF-F852-45F4-9944-4AE386C826C7}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{9862D27F-0B5C-4399-A238-DD306EC7C39C}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{7B6498F1-EC58-4627-9E71-67C689FB37A8}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{044DC0AB-B54A-4A29-A784-648E341BDC06}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{B9BFE8F0-BB31-40A3-9DAC-A38C4CA65F30}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{43FCAEE3-57AF-440B-9BF2-BE36E13D9356}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}