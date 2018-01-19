using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        public event EventHandler<SaveEventArgs> BeforeSaves;
        public event EventHandler<SaveEventArgs> AfterSaves;
        public event EventHandler<SaveEventArgs> BeforeValidate;

        public class SaveEventArgs : EventArgs
        {
            public TDataRecordSaveMode SaveMode    { get; set; }
            public bool                PerformSave { get; set; } = true;
        }
        public delegate void BeforeSavesEventHandler(SaveEventArgs e);
        public delegate void AfterSavesEventHandler(SaveEventArgs e);
        public delegate void BeforeValidateEventHandler(SaveEventArgs e);

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
        protected virtual void OnBeforeValidate(SaveEventArgs e)
        {
            EventHandler<SaveEventArgs> handler = BeforeValidate;
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

            if (BeforeValidate != null)
            {
                SaveEventArgs args = new SaveEventArgs();
                args.SaveMode = saveMode;
                BeforeValidate(this, args);
            }

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
                    SaveEventArgs saveArgs = new SaveEventArgs();
                    saveArgs.SaveMode = TDataRecordSaveMode.smInsert;
                    if (BeforeSaves != null)
                    {
                        BeforeSaves(this, saveArgs);
                    }
                    if (saveArgs.PerformSave)
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            rowsAffected = await cmd.InsertAsync(true, TableName, this, _dbConfig);
                            if ((AfterSaves != null) && (rowsAffected > 0))
                            {
                                AfterSaves(this, saveArgs);
                            }
                        }
                    }
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    SaveEventArgs args = new SaveEventArgs();
                    args.SaveMode = TDataRecordSaveMode.smUpdate;
                    if (BeforeSaves != null)
                    {
                        BeforeSaves(this, args);
                    }
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                        if ((AfterSaves != null) && (rowsAffected > 0))
                        {
                            AfterSaves(this, args);
                        }
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
