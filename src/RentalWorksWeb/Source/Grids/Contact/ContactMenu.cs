using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksWeb.Source.Grids
{
    public class ContactMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactMenu() : base("{B6A0CAFC-35E8-4490-AEED-29F4E3426758}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9C718765-D324-4588-B84F-66CC07693D61}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{713BCDDB-E28A-4950-87A7-233E076C8E2A}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{848CACB1-B332-41F8-BB71-15BF8E3C236B}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{707DF35F-9A14-49B1-A74D-CC703C12BDB4}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{5A58C4DB-AB75-4E63-9EC1-9ED634979F6A}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{BD10F6E0-DD0D-4ED4-AE50-1C54EA9B5E6A}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{604EDB22-2DCD-46CD-897F-C950F88852B2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}