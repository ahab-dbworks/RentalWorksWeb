using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderContractNoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderContractNoteMenu() : base("{2018FEB8-D15D-4F1C-B09D-9BCBD5491B52}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{370EFBDC-DC49-47A9-86A6-510FFEADA6D6}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{EDBBC93F-02ED-489A-9587-BA08194BE8FB}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{59108C36-7B06-4416-867E-F3E9238E0BF2}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{264464BB-2AFE-431B-8BFC-6E643BC83A6E}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{BD5A24E8-2D27-474D-B06B-D9E93D95A9FA}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{936CAFAB-FFEE-4DEF-882C-F677CD52352C}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{5B19EFEF-4689-471A-BAEB-D484B9855CB0}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}