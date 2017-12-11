using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class EventTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EventTypeMenu() : base("{FE501F99-95D4-444C-A7B6-EA20ACE88879}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C1A40AB9-B518-4002-856D-9253213343EF}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7FE35517-B830-40CD-9179-40F3195AFA9E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{791F9F77-D3E6-4A67-8916-986DEDCE9ED2}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{90CDA1D9-069F-4BD2-B2A3-0B356305DA5E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{197F6ECF-3046-4981-BAE0-14351D4D4071}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{7262BAC2-889E-46E6-8FD4-06A5CC8AF593}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{3268802A-32D9-4C0F-B3BC-E866DD64CA41}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3BA1B0E3-7E54-4529-B6DB-9CE2D81EA4C4}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A4C5FC8F-99B1-4FC0-9C92-3D5041883538}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{90A8C7AF-4283-4EC9-947A-6B85A075180A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4F756B71-E583-40BB-8C2D-FB30A344B2A3}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{AB0085C3-756D-4941-86AA-8B272858833F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{29CEFAA7-4ED9-4E1B-90EF-55D6C12550E6}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}