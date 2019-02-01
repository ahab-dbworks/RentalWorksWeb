using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ExportSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExportSettingsMenu() : base("{70CEC5BB-2FD9-4C68-9BE2-F8A3C6A17BB7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0F76F01D-6522-427D-A9EA-DB3BBE7E6D5A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0110CD16-9C17-41C4-BEB8-7B8CE08CE622}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{188470F9-4C1C-48C1-B646-72967CD10DE4}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2849EC76-D6CF-4E3E-B7F1-A41C17B08DC2}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{64D6C1BF-1ACB-4C55-A6D6-F069E9D95940}", nodeBrowseMenuBar.Id);
            // Form
            var nodeForm = tree.AddForm("{50E8B65E-7735-439A-A905-6A7950324338}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E7E6FAC2-4C7A-4490-A202-F9E67E6EDA96}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{67658D32-FB47-43E5-BE83-63953E40DD93}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}