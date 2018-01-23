using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        public event EventHandler<BeforeSaveEventArgs> BeforeSaves;
        public event EventHandler<AfterSaveEventArgs> AfterSaves;
        public event EventHandler<BeforeValidateEventArgs> BeforeValidate;

        public class BeforeSaveEventArgs : EventArgs
        {
            public TDataRecordSaveMode SaveMode    { get; set; }
            public bool                PerformSave { get; set; } = true;
        }

        public class AfterSaveEventArgs : EventArgs
        {
            public TDataRecordSaveMode SaveMode { get; set; }
            public bool SavePerformed { get; set; } = true;
        }

        public class BeforeValidateEventArgs : EventArgs
        {
            public TDataRecordSaveMode SaveMode { get; set; }
        }
        public delegate void BeforeSavesEventHandler(BeforeSaveEventArgs e);
        public delegate void AfterSavesEventHandler(AfterSaveEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateEventArgs e);

        protected virtual void OnBeforeSaves(BeforeSaveEventArgs e)
        {
            EventHandler<BeforeSaveEventArgs> handler = BeforeSaves;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterSaves(AfterSaveEventArgs e)
        {
            EventHandler<AfterSaveEventArgs> handler = AfterSaves;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnBeforeValidate(BeforeValidateEventArgs e)
        {
            EventHandler<BeforeValidateEventArgs> handler = BeforeValidate;
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
                BeforeValidateEventArgs args = new BeforeValidateEventArgs();
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
                    BeforeSaveEventArgs beforeSaveArgs = new BeforeSaveEventArgs();
                    AfterSaveEventArgs afterSaveArgs = new AfterSaveEventArgs();
                    beforeSaveArgs.SaveMode = TDataRecordSaveMode.smInsert;
                    afterSaveArgs.SaveMode = TDataRecordSaveMode.smInsert;
                    if (BeforeSaves != null)
                    {
                        BeforeSaves(this, beforeSaveArgs);
                    }
                    if (beforeSaveArgs.PerformSave)
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            rowsAffected = await cmd.InsertAsync(true, TableName, this, _dbConfig);
                            afterSaveArgs.SavePerformed = (rowsAffected > 0);
                            if (AfterSaves != null)
                            {
                                AfterSaves(this, afterSaveArgs);
                            }
                        }
                    }
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    // update
                    BeforeSaveEventArgs beforeSaveArgs = new BeforeSaveEventArgs();
                    AfterSaveEventArgs afterSaveArgs = new AfterSaveEventArgs();
                    beforeSaveArgs.SaveMode = TDataRecordSaveMode.smUpdate;
                    afterSaveArgs.SaveMode = TDataRecordSaveMode.smUpdate;
                    if (BeforeSaves != null)
                    {
                        BeforeSaves(this, beforeSaveArgs);
                    }
                    if (beforeSaveArgs.PerformSave)
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                            afterSaveArgs.SavePerformed = (rowsAffected > 0);
                            if (AfterSaves != null) 
                            {
                                AfterSaves(this, afterSaveArgs);
                            }
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
