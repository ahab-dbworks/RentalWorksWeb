using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwCore.ValueTypes
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
