using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class ProductionTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProductionTypeMenu() : base("{309300EC-4CCB-421A-BAF6-64005D2C9DE9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{0DCA156B-BDCC-4931-958D-A6D41C2CC671}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{2B071A36-E8E3-426E-970C-56CA25DC9D3B}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{B462957B-7FDC-4DAD-B654-77452C9E9A1B}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{71E7E97E-4970-44FA-BB12-07A4220E1ED0}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{0F3A11A1-653E-4A9D-BB0D-CFCC9316B2B0}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{DCE4B61F-3A9B-4A77-85AD-6DE367904F69}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}