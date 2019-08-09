namespace FwStandard.Data
{
    public class FwCustomTable
    {
        public string TableName;
        public string Alias;

        public FwCustomTable(string tableName, string alias)
        {
            this.TableName = tableName;
            this.Alias = alias;
        }
    }
}
