using FwStandard.SqlServer;
using System;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        //------------------------------------------------------------------------------------
        public FwDataReadWriteRecord() : base() { }
        //------------------------------------------------------------------------------------
        public virtual int Save()
        {
            int rowsAffected = 0;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                if (NoPrimaryKeysHaveValues)
                {
                    //insert
                    SetPrimaryKeyIdsForInsert(conn);
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    rowsAffected = cmd.Insert(true, TableName, this, _dbConfig);
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    rowsAffected = cmd.Update(true, TableName, this);

                }
                else
                {
                    throw new Exception("Primary key values were not supplied for all primary keys.");
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Delete()
        {
            bool success = false;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                int rowcount = cmd.Delete(true, TableName, this);
                success = (rowcount > 0);
            }
            return success;
        }
        //------------------------------------------------------------------------------------    }
    }
}
