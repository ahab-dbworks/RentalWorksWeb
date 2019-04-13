using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobePatternMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobePatternMenu() : base("{2BE7072A-5588-4205-8DCD-0FFE6F0C48F7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8377A09B-2E39-47CA-9D03-D6F7A85D4663}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{44B1CC06-9CD8-41DB-9EB5-33D65BBABEDE}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2E4E1129-5FB9-4EA4-80FF-A09DAF6FACD3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F697E185-FA7C-4758-A389-F7300AB4EC8F}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CFCEFB8D-8632-4DAD-9A49-65BE59CAE03C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{9B3DDA5E-F13B-40ED-BB70-336861188898}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0ABCAAC2-B11D-447C-AC6B-FD8F5B31312A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5F679252-432B-4133-843A-F09DC80059B3}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1E18B03D-2466-4C98-8C3F-DA2C619F2EFC}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{48A2ABCB-9C06-4E60-95C3-3D4CF0882618}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D869F3C0-6BFF-4B13-AC85-D6FBFFCC41D5}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{B1A62E65-2974-4758-87A0-8BB63A48BE52}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{FFB9ECAB-E220-4FCC-AC52-A1D0DE113455}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}