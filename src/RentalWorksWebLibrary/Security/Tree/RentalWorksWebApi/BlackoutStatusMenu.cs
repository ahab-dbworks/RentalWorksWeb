using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class BlackoutStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BlackoutStatusMenu() : base("{DCA313A2-92A3-41CA-B203-38BE725743A8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{3EC1D66D-A977-4A7F-8D24-5930A002E63E}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{C30C427F-F9DF-41D8-8569-14AD17680624}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{5B6C7CAF-E5E5-45CC-88DF-0AA132F61CE0}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{9B53148A-781B-4CAE-B4F3-AFAAA749A65A}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{1BA52854-4796-41B5-973D-9A9731BC4AFE}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{278A4455-2772-4E7A-A207-F0CD4283FDA2}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}