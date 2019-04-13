using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class LaborTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LaborTypeMenu() : base("{8DCFF480-0FFB-44A3-86C2-2C26B2CF56B4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{5BB193BA-E791-46B7-B475-8696D36535A7}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{FB4ECDAE-B506-4C1E-A35E-677B2E52A924}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F58319B1-BE6D-41AD-B3CC-C2D27CB18F3F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{06DB298F-54C7-4D8C-8726-562949B3118A}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{830D8839-DE60-410C-B18C-19AD20B366F6}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CC55DE0A-62A5-4937-A047-32DE79D5409F}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BE082295-49EE-43AC-ABBF-69E7680996B3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{946300A6-372A-4C35-9EFD-9F077686F509}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{97CE9552-C8EC-423C-874B-1E8A23053E8D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{64D97920-6168-4427-8F00-15ADAA96F51A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D8997AFC-50DA-4883-BC52-E8224BE4B801}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{83A3EFFC-7C69-419B-A462-CB88B14F0522}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{50C0BADA-0EB5-47DB-8BF8-C8F949D92FC2}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
