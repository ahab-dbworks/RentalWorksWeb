using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class PhotographyTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhotographyTypeMenu() : base("{B3FCE468-13A0-47F4-A1A4-E16D3264E544}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{F861973A-B87B-42D7-9F0F-627461B291E9}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{154E1E20-6A2F-435B-A762-4620D4504952}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{403666B2-2E10-427F-9EEF-C345250A7E80}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{342F9E27-4B52-4FD1-BB93-B2495CD62E86}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{C07592D3-34FE-4B26-A19B-7F91AD16910D}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{7C26739F-EC41-4B85-AB62-0DE69F495AB2}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}