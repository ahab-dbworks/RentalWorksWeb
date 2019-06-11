using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderTypeCoverLetterGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeCoverLetterGridMenu() : base("{37A32E16-FE36-4B47-ADC5-11DD57BE895F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3EFFC8F6-321E-4EDA-AA9B-8E96651FFF17}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{FA39D418-4EDB-47F0-A537-637377F4747E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6B4F2C9D-FEA8-439C-831F-EFF51055B231}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9DA9D91B-B616-4356-9820-2180A65E8DC7}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{345FA769-CE50-4269-9791-5B4CA9FAECE1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}