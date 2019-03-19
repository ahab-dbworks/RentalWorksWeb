using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ManifestMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ManifestMenu() : base("{1643B4CE-D368-4D64-8C05-6EF7C7D80336}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{92368D01-D954-4E17-B0A3-E7E7AE36E79A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{C7EA06C4-653C-4812-B6C7-30136FF3B13B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{88336ED1-38F4-49A7-9778-97A256F82A46}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9ECA4003-0071-46A6-89E9-1366FAF53828}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E9862D84-73CB-4D93-9E99-20C2C7BB70A9}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{B12F73E6-A687-473C-AB0A-EC047E4B772D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{1A050BA9-FE34-411F-B8BA-8081A24DD7EE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AA98D69A-2100-48DA-8596-BB317A021F94}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FBD38893-7B9D-4099-9C89-32A5F8A0D044}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{25AE3643-5FE3-4535-9556-929F2509D28D}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{71C7AC4E-99F9-4F16-ADF0-41D1F83F208D}", nodeFormMenuBar.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C263473D-DC08-4E5A-BB33-3D4428C290AB}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{7E3EB887-4E46-4BD3-BB6A-780D11B47334}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Print", "{8FC8A0F2-C016-476F-971B-64CF2ED95E41}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}