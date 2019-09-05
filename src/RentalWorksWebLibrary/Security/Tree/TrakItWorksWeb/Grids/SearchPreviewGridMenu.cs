using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class SearchPreviewGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public SearchPreviewGrid() : base("{EFA46DCE-338B-49C3-9016-1BAD13E6A561}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9F0DB46E-0763-413D-8E4E-9F32524C1A58}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{962BAAA8-7959-47BE-A790-E0283F37C2EB}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A655A0D5-E732-47EE-BFDF-D9F2F59C2C73}", nodeGridSubMenu.Id);
                        tree.AddDownloadExcelSubMenuItem("{D48E052B-9EE2-4C9F-BB99-296EC9C37633}", nodeBrowseOptions.Id);
                        //tree.AddSubMenuItem("Refresh Availability", "{C4168B67-EDD2-4749-9841-95A113251248}", nodeBrowseOptions.Id);
                tree.AddEditMenuBarButton("{9C448001-036B-402C-9398-FE50F4E26C80}", nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{9156ED70-01B3-4C35-81A9-BD41024224B9}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}