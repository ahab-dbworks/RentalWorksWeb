using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fw.Json.ValueTypes
{
    public abstract class FwApplicationTreeBranch
    {
        protected readonly string MODULEID;
        
        public FwApplicationTreeBranch(string moduleid)
        {
            MODULEID             = moduleid;
        }
        
        
        public abstract void BuildBranch(FwApplicationTree tree);
    }
}
