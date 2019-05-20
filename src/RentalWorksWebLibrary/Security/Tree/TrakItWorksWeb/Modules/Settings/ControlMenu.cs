using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ControlMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ControlMenu() : base("{044829ED-579F-4AAD-B464-B4823FDB5A35}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{17122376-2468-4F72-A48B-34D579F91BE6}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{32DFBF86-206B-4B7D-8D13-F28618A2745F}", nodeBrowse.Id);
                    //var nodeBrowseSubMenu = tree.AddSubMenu("{66E4FD0E-D7F0-42A2-9DDD-C92C1E64C9E0}", nodeBrowseMenuBar.Id);
                    //    var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3C32F852-CF42-4F0B-BA27-8BFC6F13EC46}", nodeBrowseSubMenu.Id);
                    tree.AddViewMenuBarButton("{747EA5AB-A951-41FF-A44C-8D312888FCE3}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{D0E2888B-075B-4C93-8C6C-93000FB9EC88}", nodeBrowseMenuBar.Id);
            
            // Form
            var nodeForm = tree.AddForm("{7131B646-6344-442B-8059-5289D61C61C0}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{41AFBC03-C6D1-45C4-9173-198B0A0884FD}", nodeForm.Id);
                    //var nodeFormSubMenu = tree.AddSubMenu("{503BA83A-0DA8-4537-827A-177D54B5FFD2}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{0BEC4C82-6765-4101-BC23-E801ACD299EB}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
