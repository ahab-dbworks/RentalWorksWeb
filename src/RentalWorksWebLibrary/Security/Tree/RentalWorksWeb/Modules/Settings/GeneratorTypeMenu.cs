using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GeneratorTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorTypeMenu() : base("{95D9D422-DCEB-4150-BCC2-79573B87AC4D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1CDD2EE1-A98A-4391-89CC-51493A68CB64}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{68F4D290-1247-44B1-A1B5-AA0F73FFA9D9}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{50E359A5-A862-41E3-B587-1630E54455EF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5315CC54-C434-4B92-8A2F-2F03F1DE58EE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0AB651EC-2317-47DB-AC56-BB02231D0246}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A886178A-EB49-455F-BE6B-EE56958434A1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{12FE44A4-59AD-4AE9-A530-1A8C900E34C3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{6734037F-CF80-43D6-A84A-B834936A66AD}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AB4B1484-2F8B-4965-8B41-80F7783E5A9B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{9D658428-8507-41DC-94B3-AFC91344E4EA}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{32DDB1FB-9E7D-4048-9623-4D8C97ED5D73}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{746AB48C-AA5D-4D7F-8C90-AFA51748042B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}