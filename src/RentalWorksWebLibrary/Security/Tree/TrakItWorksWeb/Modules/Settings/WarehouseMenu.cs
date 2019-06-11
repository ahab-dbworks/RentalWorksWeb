using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class WarehouseMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseMenu() : base("{8DD21206-86D4-4C69-9094-A8CF0A5C93FF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{862DDAD5-6A51-4CA0-BAD3-4BEF8975FE24}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{244F128B-BF76-4473-B27E-FC52D1574781}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{D9697EFF-8A36-41C7-A6BA-9EE2F270FEDF}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{78074861-E890-43DB-BF9D-BE9359EE178F}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{AF0DA892-BE93-4A3F-B89E-A6532379427C}", nodeBrowseExport.Id);
                    //tree.AddNewMenuBarButton("{5E00D3D3-0502-4EA6-B252-F05346D74D50}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{C4C4B7CF-4606-4490-BF42-1C509A70225B}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{5CA2D63A-0DCC-4967-9526-237AFA66949A}", nodeBrowseMenuBar.Id);
                    //tree.AddDeleteMenuBarButton("{4B354B4B-88EE-4FBD-9216-D8945D170B25}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{43B5EEF9-09EE-43BB-9303-6FA633072CAE}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{65A5123F-03CF-4F58-A5CF-FBC8994ED83B}", nodeForm.Id);
                    // var nodeFormSubMenu = tree.AddSubMenu("{8419E444-38A8-4DD0-8E00-E4F3EAFBA26E}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{CBAE4E02-8BFD-4DE5-B83D-BC24F523DEAD}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
