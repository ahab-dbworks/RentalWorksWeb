using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class FwSqlTableAttribute : Attribute
    {
        public readonly string Table;

        public FwSqlTableAttribute(string table)
        {
            Table = table;
        }
    }
}
