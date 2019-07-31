using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class TransferOrderItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public TransferOrderItemGrid() : base("{521D83C6-DEA4-4723-A7F3-25C00F940B75}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8CE4A769-C9DD-4C6C-9B37-1DC79C707241}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B75C2E71-63EB-4C9F-94BC-2C22E10CB8E5}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A64DA80E-6B26-4DCB-8145-04C27FFDCC67}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{221C64AA-5514-429A-BFB6-BFB8682DDD35}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("QuikSearch", "{16CD0101-28D7-49E2-A3ED-43C03152FEE6}", nodeBrowseOptions.Id, true); 
            tree.AddSubMenuItem("Copy Template", "{5E73772F-F5E2-4382-9F50-3272F4E79A25}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Refresh Availability", "{1065995B-3EF3-4B50-B513-F966F88570F1}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{4724DA38-BB5A-4E0A-85A5-7591C3023AE9}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{CE5252AC-A6AD-4A4B-BD8A-C46D8AB5C62A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{63F891D4-682E-4904-AC57-287F90542C9C}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}