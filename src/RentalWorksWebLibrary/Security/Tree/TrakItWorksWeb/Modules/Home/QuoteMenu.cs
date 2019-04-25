using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class QuoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuoteMenu() : base("{9213AF53-6829-4276-9DF9-9DAA704C2CCF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6FDE705B-79A5-423F-8B9F-BF14A0BECEC7}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{A36FAAE7-1B9D-408C-9019-B6DF1938C408}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{7859A4B8-198C-4201-A01E-DFA3EDF48D9E}", nodeBrowseMenuBar.Id);
                        var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A7D5F6A1-7DAA-4C1A-B05E-C08FE7826C8B}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Cancel / Uncancel", "{86412C14-9351-4C03-8D13-5338AB0EAEC8}", nodeBrowseOptions.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{56AF9F9D-E5F4-4C6A-98C2-F5970129C61E}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{83999B80-57CD-46B4-A46B-967C00FCCE84}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{973A991F-2496-425A-9019-DC8078CF65ED}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{BC37582E-5D65-41AE-84F8-8B7E7C225898}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{E49177E4-BAC4-4FEA-96BC-CFC5CA02CD8D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4372CC3E-59AA-434F-826E-0933A501A738}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{854A361E-8355-40BD-A2A8-AFAD8B79E5A6}", nodeForm.Id);
                    var nodeFormSubMenu = tree.AddSubMenu("{0DEAF798-3522-4079-B3EF-5729C2E7201D}", nodeFormMenuBar.Id);
                        var nodeFormOptions = tree.AddSubMenuGroup("Options", "{A69F6246-4273-40D4-86F6-849733642E68}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Copy Request", "{85B7FD07-74CD-4320-AFB0-9718EE5C8CDD}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Search", "{66EDC5EA-DC03-4A5B-82F2-A41D2B8A34E4}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Print Request", "{CC3F3DB4-21A4-4E70-8992-30886B2D1515}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Create Order", "{F9B8EAC3-07BD-4286-B4F8-CCBE53710B1F}", nodeFormOptions.Id);
                            //tree.AddSubMenuItem("New Version", "{18ECF5BB-18E0-45F5-AB9A-98A377E38D1F}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Cancel / Uncancel", "{8F3E3263-B5AE-4CB4-8CBB-E9F680AA8C11}", nodeFormOptions.Id);
                    tree.AddSaveMenuBarButton("{98D68BD4-D01B-4C9B-B60A-D6C09A87285F}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}