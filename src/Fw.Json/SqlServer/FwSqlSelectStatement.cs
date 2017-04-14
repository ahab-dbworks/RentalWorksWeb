using System.Collections.Generic;

namespace Fw.Json.SqlServer
{
    public class FwSqlSelectStatement
    {
        //---------------------------------------------------------------------------------------------
        public string       Union   { get; set; }
        public List<string> Select  { get; set; }
        public List<string> From    { get; set; }
        public List<string> Where   { get; set; }
        public List<string> GroupBy { get; set; }
        public List<string> Having  { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwSqlSelectStatement()
        {
            Union   = string.Empty;
            Select  = new List<string>();
            From    = new List<string>();
            Where   = new List<string>();
            GroupBy = new List<string>();
            Having  = new List<string>();
        }
        //---------------------------------------------------------------------------------------------
    }
}
