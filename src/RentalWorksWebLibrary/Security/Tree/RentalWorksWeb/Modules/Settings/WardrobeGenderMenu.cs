using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeGenderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeGenderMenu() : base("{28574D17-D2FF-41A0-8117-5F252013E7B1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{41371F05-179D-4EC3-9504-E81BEAE56258}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{AF546AF2-DEAF-4763-B7FC-7D04C9E1CE1B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FE0713D5-37FF-4E27-B352-5B3C988837F8}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DDEC274C-C5A3-4B5A-A702-9FC7542D864E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E86943CE-2203-4C93-8AD8-7852871AE80D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{535B22F0-10E7-4DBD-808E-6ED6927B248E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{49FCF2EF-03D8-41E4-BD0A-3515B02FE51B}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FDD5E903-EC38-4B63-98B6-10FFE3A62998}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B73C2D12-D5A5-49FE-BD34-8D7338CD784F}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{5D613881-7583-4291-8AB3-13A60DEA735A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7594B15F-E611-443D-98F4-0A3FB5CC4C03}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{B42F3EFD-50C9-4737-83F6-FB2D97C3B439}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{14E39BEE-2FF1-4D0D-8ED9-404E428985C8}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

