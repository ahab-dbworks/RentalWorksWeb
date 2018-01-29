using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        public event EventHandler<BeforeSaveEventArgs> BeforeSave;
        public event EventHandler<AfterSaveEventArgs> AfterSave;
        public event EventHandler<BeforeValidateEventArgs> BeforeValidate;
        public event EventHandler<BeforeDeleteEventArgs> BeforeDelete;
        public event EventHandler<AfterDeleteEventArgs> AfterDelete;

        public delegate void BeforeSaveEventHandler(BeforeSaveEventArgs e);
        public delegate void AfterSaveEventHandler(AfterSaveEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateEventArgs e);
        public delegate void BeforeDeleteEventHandler(BeforeDeleteEventArgs e);
        public delegate void AfterDeleteEventHandler(AfterDeleteEventArgs e);

        protected virtual void OnBeforeSave(BeforeSaveEventArgs e)
        {
            EventHandler<BeforeSaveEventArgs> handler = BeforeSave;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterSave(AfterSaveEventArgs e)
        {
            EventHandler<AfterSaveEventArgs> handler = AfterSave;
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
        protected virtual void OnBeforeDelete(BeforeDeleteEventArgs e)
        {
            EventHandler<BeforeDeleteEventArgs> handler = BeforeDelete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterDelete(AfterDeleteEventArgs e)
        {
            EventHandler<AfterDeleteEventArgs> handler = AfterDelete;
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
                    if (BeforeSave != null)
                    {
                        BeforeSave(this, beforeSaveArgs);
                    }
                    if (beforeSaveArgs.PerformSave)
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            rowsAffected = await cmd.InsertAsync(true, TableName, this, _dbConfig);
                            afterSaveArgs.SavePerformed = (rowsAffected > 0);
                            if (AfterSave != null)
                            {
                                AfterSave(this, afterSaveArgs);
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
                    if (BeforeSave != null)
                    {
                        BeforeSave(this, beforeSaveArgs);
                    }
                    if (beforeSaveArgs.PerformSave)
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                        {
                            rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                            afterSaveArgs.SavePerformed = (rowsAffected > 0);
                            if (AfterSave != null) 
                            {
                                AfterSave(this, afterSaveArgs);
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
            bool success = false;
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();
            if (BeforeDelete != null)
            {
                BeforeDelete(this, beforeDeleteArgs);
            }
            if (beforeDeleteArgs.PerformDelete)
            {
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout))
                    {
                        int rowcount = await cmd.DeleteAsync(true, TableName, this);
                        success = (rowcount > 0);

                        if (AfterDelete != null)
                        {
                            AfterDelete(this, afterDeleteArgs);
                        }
                    }
                }
            }
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}
