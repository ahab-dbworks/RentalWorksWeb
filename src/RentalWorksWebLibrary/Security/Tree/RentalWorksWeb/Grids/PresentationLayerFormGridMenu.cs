using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PresentationLayerFormGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PresentationLayerFormGridMenu() : base("{88985C09-65AD-4480-830A-EFCE95C3940B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A6B953A3-D43E-407E-9F67-6C51A6D0BBA3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4B10BDA9-EC86-45FE-A739-29CFAEE1DD6A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1C8F7166-1766-4492-8090-1391449F1239}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0922198E-D58E-4EC7-A4EC-BB5CFE5E2CB8}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{456922E0-B3BB-4012-BB40-DFFC7CB6A508}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}