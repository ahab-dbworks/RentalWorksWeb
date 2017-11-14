using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class DuplicateRulesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DuplicateRulesMenu() : base("{2E0EA479-AC02-43B1-87FA-CCE2ABA6E934}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{BFF70BDA-1580-4620-B34C-524F3E567179}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{9B592E8D-5D15-4471-833B-6D980A3762DA}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F2355285-EE0D-46A4-812D-60C96AC3C587}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D02BFFDD-C674-450C-9A8E-8ACCC9FD0C30}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2C1B6781-D34A-4B9C-93FD-1B785F6DF7A0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{24570892-CF2A-49EE-904A-6697F0A1BF53}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0F12D0D3-CB34-4079-9C8C-A9E9D0E01B12}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{6D95DA3E-3757-4EA9-A5A7-D93B3DC2346C}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E082CEB3-B93E-487A-83F8-1569393BC74B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{88D58AF2-E19F-420A-ACC1-4794E78BE35B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2B3EDCD4-094D-4538-877C-3477B0CCAFB4}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{CBEFB868-BD54-4E87-A089-3C4D9DFC0D43}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6E0732AA-9DB5-4B03-BEB1-95AC82CE43C9}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}