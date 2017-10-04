﻿using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        public event EventHandler<SaveEventArgs> BeforeSaves;
        public event EventHandler<SaveEventArgs> AfterSaves;

        public class SaveEventArgs : EventArgs
        {
            public TDataRecordSaveMode SaveMode { get; set; }
        }
        public delegate void BeforeSavesEventHandler(SaveEventArgs e);
        public delegate void AfterSavesEventHandler(SaveEventArgs e);

        protected virtual void OnBeforeSaves(SaveEventArgs e)
        {
            EventHandler<SaveEventArgs> handler = BeforeSaves;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterSaves(SaveEventArgs e)
        {
            EventHandler<SaveEventArgs> handler = AfterSaves;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        //------------------------------------------------------------------------------------
        public FwDataReadWriteRecord() : base() { }
        //------------------------------------------------------------------------------------
        protected virtual bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            //override this method on a derived class to implement custom validation logic
            bool isValid = true;
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual bool ValidateDataRecord(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            validateMsg = "";
            if (isValid)
            {
                isValid = AllFieldsValid(saveMode, ref validateMsg);
            }
            if (isValid)
            {
                isValid = Validate(saveMode, ref validateMsg);
            }
            return isValid;
        }
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
                    if (BeforeSaves != null)
                    {
                        SaveEventArgs args = new SaveEventArgs();
                        args.SaveMode = TDataRecordSaveMode.smInsert;
                        BeforeSaves(this, args);
                    }
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        rowsAffected = await cmd.InsertAsync(true, TableName, this, _dbConfig);
                    }
                    if (AfterSaves != null)
                    {
                        SaveEventArgs args = new SaveEventArgs();
                        args.SaveMode = TDataRecordSaveMode.smInsert;
                        AfterSaves(this, args);
                    }
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    if (BeforeSaves != null)
                    {
                        SaveEventArgs args = new SaveEventArgs();
                        args.SaveMode = TDataRecordSaveMode.smUpdate;
                        BeforeSaves(this, args);
                    }
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                    }
                    if (AfterSaves != null)
                    {
                        SaveEventArgs args = new SaveEventArgs();
                        args.SaveMode = TDataRecordSaveMode.smUpdate;
                        AfterSaves(this, args);
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
