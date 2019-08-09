using FwStandard.Models;
using System.Collections.Generic;

namespace FwStandard.Data
{
    public class FwCustomFields : List<FwCustomField>
    {
        public FwApplicationConfig AppConfig { get; set; }
        public FwUserSession UserSession { get; set; }


        //------------------------------------------------------------------------------------
        public FwCustomFields() { }
        //------------------------------------------------------------------------------------
    }

}
