using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class TaxOptionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TaxOptionMenu() : base("{494F00BE-84B8-460C-9E1C-9799815A2B73}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8317625F-FFE7-4318-91A8-C4D4A14674F3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{4C302BC4-E55B-4E15-9AD9-84458363BEE9}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F5B13AD1-4A45-466D-899A-47586EB345BA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5D23AAA0-BB49-4DFF-9ED6-BD8C28A02BB7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4034C6C6-4FD4-4648-98BA-3152C35599C5}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2D3CD5A2-3092-4120-A854-237FE10048C0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{1C16702F-F278-434F-AD6F-6D7738DEC931}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D58D8DD9-2F5E-4F6C-A9FD-AB0AF4F3AC5B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AF1B3B62-BCB9-43A5-B27F-5F5CF98E1866}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{852E26F3-B5D0-497A-9ACC-EAF523B3F655}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6277BA81-E60F-4A77-B9D4-B469BF20A3B5}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{3760D1C9-9E76-494D-86E1-C21D5D304D9A}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{79210E51-D01C-4A00-8E6B-D85A0283829C}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Force Tax Rates", "{2D6A0588-EF38-4F08-B812-920121F8818F}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{B18606CD-6B15-4315-B27C-6D2689392EDB}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
