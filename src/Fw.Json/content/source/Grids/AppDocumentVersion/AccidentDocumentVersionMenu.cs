using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace Fw.Json.Content.Source.Grids
{
    public class AppDocumentVersionMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AppDocumentVersionMenu() : base("{397FF02A-BF19-4C1F-8E5F-9DBE786D77EC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{534981A6-9522-409B-9B19-5634BD0D75C0}", MODULEID);
                tree.AddNewMenuBarButton("{58E8811A-3AC8-4A93-B565-015E85E21A23}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{BAD9CBCD-7F54-4E3A-B10E-0E67F91E2E46}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{76EC21DD-F378-4A82-88E3-F5D1DECA0337}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}