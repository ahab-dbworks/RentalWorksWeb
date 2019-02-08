using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckedInItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedInItemGrid() : base("{5845B960-827B-4A89-9FC4-E41108C27C21}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{846F3D1F-0E58-4122-BE33-801064A01B85}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{114BEC3B-E966-4D5A-B6F1-BB913E8226E3}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{863B0D32-6E09-4F96-93F2-C83616B6456E}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B4D0CBFA-506B-4F21-B127-8A6777AAF2A6}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
