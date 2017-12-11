using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CurrencyMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CurrencyMenu() : base("{04D83434-3298-4187-8327-84942CF1CB03}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{2E2EC6C6-5116-4685-B483-375AA50EF9FA}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{6CF280DD-8125-4DB8-8085-02EBE879644D}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{74A2742C-D265-46D0-934E-6766B80B12F4}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{B823B543-A0CC-4E4A-9400-BFA80B748C97}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{68A00A55-24FF-4636-86E7-D63D324DFEE0}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{C6DE3877-AC21-4EFB-A493-8C86715C549E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}