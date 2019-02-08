using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class QuikEntryCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikEntryCategoryMenu() : base("{01604AEC-2127-4756-BF92-3A340EF000E1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{AC6FFABF-9D23-4E65-B051-C75DCD030CE5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D07DB207-F909-4CA4-A9B9-BCE6D76A50D9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A83B884C-D908-4A07-8517-E1E043E2930E}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7F871F4B-EBAF-41F1-8200-E009BF56487A}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{04B0EF85-ACC0-4AB0-B59C-F92E95253C01}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{6094BDBF-25F2-4ED8-A87C-3BA667F504CF}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{8D60D906-320F-4F1C-8CBC-632FB80D320D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6D21F254-16F1-405F-A7EE-59C5438FA98E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}