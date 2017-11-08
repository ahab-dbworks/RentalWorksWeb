using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CoverLetterGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CoverLetterGridMenu() : base("{7521D3CC-FF1C-44F5-8F93-9272B6CADC64}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A87545DD-E712-468E-9929-5461AB3CE617}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{67384A69-B06F-42C0-A807-FC6E6AA0264E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5B3753A6-FF01-46A4-B28B-FB3E4C927880}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{6EAA5FEF-4DAF-4A57-BA98-4BAF482C70A4}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6A87AC96-3513-4656-89E8-E31DB4B7C0DA}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A379B690-07D4-4A9F-AF51-9E5E3E116356}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{06C5A513-0562-4093-89C9-FC3D64F3F73F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C1EA9A52-C923-4B0C-B8D2-7DF35828029B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C3E80AAE-4B69-4693-90B6-6B4DE8BF7ADE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0DC7ADC9-6827-45D6-B1D3-29226FA69299}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C49985DD-45B6-4B90-9432-DA59E9474760}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D4E59658-0D9E-4027-82FC-1E6E3728320B}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{B46D2E38-A8BF-49F4-B3F1-3511C0188B50}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}