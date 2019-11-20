using FwStandard.AppManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwStandard.Security
{
    public abstract class FwSecurityTreeBranch
    {
        protected readonly string MODULEID;
        
        public FwSecurityTreeBranch(string moduleid)
        {
            MODULEID = moduleid;
        }
        
        
        public abstract void BuildBranch(FwAppManager tree);
    }
}
