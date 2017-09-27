using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ItemsOrderedMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemsOrderedMenu() : base("{25507FAD-E140-4A19-8FED-2C381DA653D9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8C4E2C96-4764-47C4-86E2-048F3050307D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{450E83BF-0674-4578-A3F1-6C1BC5DAB0B0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{56CE4A98-C7A4-4C61-9586-B0BB30CD0E0C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{4FD0B1FC-76FD-41C4-93C4-A8F9011AD57C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{21CFC334-4AAE-45F1-B66F-E0752F5FA209}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{84EDFAC6-3BA1-41EE-8AFB-6AC4C6A48BBB}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C9A67E22-6554-4028-AE13-0F3DA4FF127C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{7A845B04-D00F-435E-AB3F-04E8423663CC}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E169C93E-E364-4153-9BB6-0A5E964E293E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{65DFBDEC-B411-45C5-BF2F-DB5762D8A750}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6305AD48-5633-4CDA-9519-B784ABD325B7}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6AC81577-72D8-438B-B9A8-B83CD02B76E8}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}