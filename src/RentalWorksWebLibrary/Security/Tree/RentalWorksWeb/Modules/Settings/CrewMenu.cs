using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CrewMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CrewMenu() : base("{FF4C0AF2-0984-48FD-A108-68D93CB8FFE6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B5A7740F-65DB-44B2-AF00-3DD3CF6F15CE}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D0A021BD-9CED-4195-A5FC-F3E3DDFA49C6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D8A273B7-0F47-4480-88C3-5426594ADA8B}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F7F8D8B2-7677-4B71-A4F2-8F09061DF733}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{225F315B-4314-4894-AC66-F96342748576}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1216DF55-C6F4-453D-8CB9-FF6CEAB8ADD1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{03301F9D-7E67-404D-923A-0635013DD4AD}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A9F7E161-6CDB-4C93-9C28-207EFC5740E1}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B9348821-41D2-4B7E-A7E6-CDE2524AD08D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{BBFFD767-B279-4B90-AA8D-4D4C3608D92E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BA2CCD67-EA65-4CB4-841C-6006ACD48059}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0E4C5572-2386-4838-8C19-0F1A9EAB8A69}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{262A90E3-65F1-461D-A27B-9F06306E30A1}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}


    

