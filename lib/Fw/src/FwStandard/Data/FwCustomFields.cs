using FwStandard.Models;
using System.Collections.Generic;

namespace FwStandard.DataLayer
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
