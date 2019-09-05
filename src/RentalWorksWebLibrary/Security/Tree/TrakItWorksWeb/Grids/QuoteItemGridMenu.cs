using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class QuoteItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public QuoteItemGrid() : base("{DFE986B8-557B-4A37-83D9-500FC16626F9}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6585F3E1-B77B-401C-B5C5-E422DAD1A936}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{B71E6942-F90B-45C1-A16F-DDCCAD53C337}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{14552ADC-F79C-40F3-83B3-EF4055598A11}", nodeGridSubMenu.Id);
                        tree.AddDownloadExcelSubMenuItem("{2E5C5348-7058-4106-A138-E2A52C669230}", nodeBrowseOptions.Id);
                        tree.AddSubMenuItem("Summary View", "{224054C8-1D06-4DC5-BBED-D6A1112CCB3C}", nodeBrowseOptions.Id);
                        //tree.AddSubMenuItem("Search Inventory", "{A87867E1-6756-49C4-AE71-C31648A5F029}", nodeBrowseOptions.Id, true);
                        tree.AddSubMenuItem("Bold / Unbold Selected", "{373CEE50-A632-463D-9F6F-0387557509CB}", nodeBrowseOptions.Id);
                        //tree.AddSubMenuItem("Sub Worksheet", "{E6C6D5E2-68A0-4FF8-9146-7217C282DAAB}", nodeBrowseOptions.Id);
                        //tree.AddSubMenuItem("Refresh Availability", "{74E160D8-54C7-4293-9944-CAB226AD4565}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{823E1CC8-5FC1-4B3C-B0D4-1A39FEB3B516}", nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{C7AE7FB2-5C68-468B-89CD-D8269EC693AC}", nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{F292EFCF-C415-4A0D-8B67-F12EE73A8296}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}