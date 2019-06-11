using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class RepairItemStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairItemStatusMenu() : base("{7D08F487-C6D7-4C54-ACA1-4C4A1AD27188}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3C7C03B2-9E33-4F3B-83BE-5BA3952835A6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5169D453-4B21-4B73-A1F6-0D50A175631F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2207945E-91F6-4AA7-8222-601BFFD54769}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B1CEDAD8-A139-409B-85E3-4D6814CB6250}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{474E1EED-8613-4333-9040-E1E1F679DB52}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{259FC138-6EA2-4D73-A86A-F364E08161C9}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{FC07129E-2B92-4418-8C4D-5A62E7E9E471}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FF7C7042-5876-4773-B440-8B643CD18945}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B8653AF0-5463-4ED3-9486-303953A611A1}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{C5CA39B3-C34F-4553-BC06-CB1C1EB420F7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6D191EA1-9DE6-44FC-8D58-770A14B244FA}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{931FA657-DDEE-4ECD-ACA5-AE3098AE40ED}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A67E19E0-D1BA-4EF9-B0F2-DE3B91E3D986}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
