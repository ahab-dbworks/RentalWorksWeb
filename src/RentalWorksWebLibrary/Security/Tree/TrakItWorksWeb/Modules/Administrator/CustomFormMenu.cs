using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class CustomFormMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFormMenu() : base("{2F07BFC4-A120-4C97-9D96-F16906CD1B88}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9930E77B-84E7-4318-A75F-D96A441ABF1D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{EDBDFB94-002F-4802-9F7F-DABFDF8FE7B8}", nodeBrowse.Id);
            //var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{76F308CE-42F3-4E40-BBB8-EAF18E33E536}", nodeBrowseSubMenu.Id);
            //tree.AddDownloadExcelSubMenuItem("{22D57CC7-1B17-4E20-95D2-F0FAC1C5DF38}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{40032C3D-5894-40A2-8605-1559F42B3599}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{15329811-AF80-4C5C-9B9A-239263CBC895}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{73AB03F1-E67E-46FB-B67A-AF81CA0C3CCE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{3F621046-19EA-4983-88C2-A8AC33FDB3E5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F33353D8-57A9-4058-BD52-5143BF8845D0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2B819EF3-C577-4A26-89D1-C02F9606A62B}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{7477797A-2773-4E01-81C4-D00A58AF29CD}", nodeFormMenuBar.Id);
            //var nodeFormOptions = tree.AddSubMenuGroup("Options", "{ACCAE2DB-077B-4696-9D12-ED66AE1A9791}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{EB966F57-A6C4-452A-BC97-0ACD1636ED10}", nodeFormMenuBar.Id);
            
        }
        //---------------------------------------------------------------------------------------------
    }
}
