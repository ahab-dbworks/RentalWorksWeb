using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class GeneratorRatingMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorRatingMenu() : base("{03C0E24C-BB02-4924-A7B2-8B1DDF171827}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{915BC77B-88C6-4DF7-9AC8-9B2CC0B2ECDB}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{D014CE24-1518-4022-9D66-E779DF284AB4}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{10B3F329-73CE-4359-8DF2-CF86DF76C9E4}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{53DD87EE-08DA-4FE5-A2CC-EE8C38DA40C8}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{7E849D41-933F-4715-9F77-4FDB6278A138}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{63D65189-639E-450E-964A-06566FA671DB}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}