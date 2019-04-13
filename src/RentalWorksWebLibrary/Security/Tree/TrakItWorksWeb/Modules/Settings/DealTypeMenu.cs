using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class DealTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealTypeMenu() : base("{4FA964A3-4D02-432E-8E13-D7C003C67584}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D9058D30-D481-4EBA-A9BC-FA943658FD12}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{497380BE-38EC-41EC-A9D1-83B4789E4749}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{BFE89151-007C-4449-8997-9F547D862601}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{73B5B994-8A32-4D47-9187-C581DB5B8B7C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{56D6A950-9C35-4D5F-94B6-247076CB0156}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B9E5D972-3C1F-4D09-B881-96660F1B3621}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{EA0FA3FC-33E7-4704-9725-4C2603595CBF}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{40BF5E3B-CD81-4A76-ABC1-37F14A34312D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A5A08AAB-3320-4611-BC14-B714B01E2F30}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{60E3C763-82A1-498F-AB2D-ECB37A5F2052}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{28A936E4-1BF0-4FA5-BF32-02DDC361C3A3}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{A08059F4-A286-42F8-B166-8AFA70320177}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8B71C94D-80A1-4624-A5D5-224A610D4BDA}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    


