using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class ContactEventMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactEventMenu() : base("{25ad258e-db9d-4e94-a500-0382e7a2024a}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{09c4561e-384b-412b-aa93-b30ec4354bd1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6b6fe694-6052-465e-89b6-01e8ef8f164a}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{ee72a5db-53d4-42aa-96b4-c2ff7af735b0}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{48c4f9fb-c1e1-4a57-bdc9-3edccc11b2b5}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4ef62915-eb01-46ac-bfc5-91bf0bdf56fa}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{71f36fa8-f239-419b-8234-8a63762663e4}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{a724b2e8-f996-4cec-a892-b31e0cc97ded}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{f437b821-19e7-4253-b9bd-8224e6111ae3}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1cefa6e6-37b9-48e9-9ef7-ccdee1749ba6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{c7179de4-751f-48af-80eb-a898f832f873}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8ffa70ad-0316-4e54-9cd3-710a139eee01}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{d6f8965b-1e6a-4566-9586-042f504d2522}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{d23887ff-ca64-4f6a-b8fd-5cbf11cc02c0}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}