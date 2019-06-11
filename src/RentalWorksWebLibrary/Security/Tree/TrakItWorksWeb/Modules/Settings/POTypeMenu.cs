using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class POTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POTypeMenu() : base("{0FF30649-FD58-4FC6-9F5E-C8AC2FB87D8B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{341924BF-DFDD-483A-AA00-97D25A4DB122}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{6BC83004-02C2-4877-91A4-2DD8C36E7E36}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{FB505EC6-F281-4416-837A-77DC9CD3B1FA}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{07E5BBA1-E93F-4D31-8615-0C6EF04A156B}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{7EDC8B57-C799-41B2-8B8B-E7F1C069798B}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{212C2D1F-4D44-47AB-BF21-A99B1B69D5E5}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{A75C3410-CC93-485E-B897-165AF9C1D244}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{778D5AA8-C5A8-41A4-B80A-EDC60FB7DE4C}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{10D67190-43D2-416E-8CAF-46C3426B28AD}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{AD7202F3-35CB-4C6B-BECC-9D464FA1BA34}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{9ED8DD62-5398-4707-893B-F95B9E25B4BF}", nodeForm.Id);
                    //var nodeFormSubMenu = tree.AddSubMenu("{F90DB1EF-1425-4799-A8B2-0AC9B21C48ED}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{57088510-967F-4489-B5F4-B17BDC730E9C}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}
