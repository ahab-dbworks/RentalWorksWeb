using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        //------------------------------------------------------------------------------------
        public FwDataReadWriteRecord() : base() { }
        //------------------------------------------------------------------------------------
        public virtual async Task<int> SaveAsync()
        {
            int rowsAffected = 0;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                if (NoPrimaryKeysHaveValues)
                {
                    //insert
                    await SetPrimaryKeyIdsForInsertAsync(conn);
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        rowsAffected = await cmd.InsertAsync(true, TableName, this, _dbConfig);
                    }
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                    }
                }
                else
                {
                    throw new Exception("Primary key values were not supplied for all primary keys.");
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> DeleteAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                {
                    int rowcount = await cmd.DeleteAsync(true, TableName, this);
                    bool success = (rowcount > 0);
                    return success;
                }
            }
        }
        //------------------------------------------------------------------------------------    }
    }
}
