using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.Data
{
    public class FwDataReadWriteRecord : FwDataRecord
    {
        // It's better to use for example: BeforeSaveAsync vs BeforeSave event handler because event handlers are not async.
        // The time to use the event handlers is when you are working with an instance of the logic, where as the async method should be overriden in the logic class itself, since it allows you to preserve the async flow
        public event EventHandler<BeforeSaveDataRecordEventArgs> BeforeSave;
        public event EventHandler<AfterSaveDataRecordEventArgs> AfterSave;
        public event EventHandler<BeforeValidateDataRecordEventArgs> BeforeValidate;
        public event EventHandler<InsteadOfDataRecordDeleteEventArgs> InsteadOfDelete;
        public event EventHandler<BeforeDeleteEventArgs> BeforeDelete;
        public event EventHandler<AfterDeleteEventArgs> AfterDelete;
        public event EventHandler<EventArgs> AssignPrimaryKeys;

        public delegate void BeforeSaveEventHandler(BeforeSaveDataRecordEventArgs e);
        public delegate void AfterSaveEventHandler(AfterSaveDataRecordEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateDataRecordEventArgs e);
        public delegate void InsteadOfDeleteEventHandler(InsteadOfDataRecordDeleteEventArgs e);
        public delegate void BeforeDeleteEventHandler(BeforeDeleteEventArgs e);
        public delegate void AfterDeleteEventHandler(AfterDeleteEventArgs e);
        public delegate void AssignPrimaryKeysEventHandler(EventArgs e);

        protected virtual async Task BeforeSaveAsync(BeforeSaveDataRecordEventArgs e)
        {
            BeforeSave?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AfterSaveAsync(AfterSaveDataRecordEventArgs e)
        {
            AfterSave?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task BeforeValidateAsync(BeforeValidateDataRecordEventArgs e)
        {
            BeforeValidate?.Invoke(this, e);
            await Task.CompletedTask;
        }

        protected virtual async Task InsteadOfDeleteAsync(InsteadOfDataRecordDeleteEventArgs e)
        {
            InsteadOfDelete?.Invoke(this, e);
            await Task.CompletedTask;
        }

        protected virtual async Task BeforeDeleteAsync(BeforeDeleteEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AfterDeleteAsync(AfterDeleteEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
            await Task.CompletedTask;
        }
        protected virtual async Task AssignPrimaryKeysAsync(EventArgs e)
        {
            AssignPrimaryKeys?.Invoke(this, e);
            await Task.CompletedTask;
        }


        //------------------------------------------------------------------------------------
        public FwDataReadWriteRecord() : base() { }
        //------------------------------------------------------------------------------------
        protected virtual async Task<FwValidateResult> ValidateAsync(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, FwValidateResult result)
        {
            await Task.CompletedTask;
            ////override this method on a derived class to implement custom validation logic
            //bool isValid = true;
            //return isValid;
            return result;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<FwValidateResult> ValidateDataRecordAsync(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, FwValidateResult result)
        {
            BeforeValidateDataRecordEventArgs args = new BeforeValidateDataRecordEventArgs();
            args.SaveMode = saveMode;
            args.Original = original;
            //BeforeValidate?.Invoke(this, args);
            await BeforeValidateAsync(args);

            if (result.IsValid)
            {
                result = await AllFieldsValidAsync(saveMode, result);
            }
            if (result.IsValid)
            {
                result = await ValidateAsync(saveMode, original, result);
            }
            return result;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Saves the record to the table.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public virtual async Task<int> SaveAsync(FwDataReadWriteRecord original, FwSqlConnection conn = null, TDataRecordSaveMode saveMode = TDataRecordSaveMode.Auto)
        {
            int rowsAffected = 0;

            BeforeSaveDataRecordEventArgs beforeSaveArgs = new BeforeSaveDataRecordEventArgs();
            beforeSaveArgs.SaveMode = TDataRecordSaveMode.smInsert;
            beforeSaveArgs.Original = original;
            if (saveMode == TDataRecordSaveMode.Auto)
            {
                if (NoPrimaryKeysHaveValues)
                {
                    saveMode = TDataRecordSaveMode.smInsert;
                }
                else if (AllPrimaryKeysHaveValues)
                {
                    saveMode = TDataRecordSaveMode.smUpdate;
                }
                else
                {
                    throw new Exception("Values were not supplied for all primary keys.");
                }
            }
            else if (saveMode == TDataRecordSaveMode.smInsert)
            {
                if (!NoPrimaryKeysHaveValues)
                {
                    throw new Exception("Primary key values must be blank.");
                }
            }
            else if (saveMode == TDataRecordSaveMode.smUpdate)
            {
                if (!AllPrimaryKeysHaveValues)
                {
                    throw new Exception("Values were not supplied for all primary keys.");
                }
            }
            beforeSaveArgs.SaveMode = saveMode;

            if (conn == null)
            {
                conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
            }

            beforeSaveArgs.SqlConnection = conn;

            if (beforeSaveArgs.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (AssignPrimaryKeys == null)
                {
                    await SetPrimaryKeyIdsForInsertAsync(conn);
                }
                else
                {
                    EventArgs e = new EventArgs();
                    AssignPrimaryKeys(this, e);
                }
            }

            //BeforeSave?.Invoke(this, beforeSaveArgs);
            await BeforeSaveAsync(beforeSaveArgs);

            if (beforeSaveArgs.PerformSave)
            {
                if ((beforeSaveArgs.SaveMode.Equals(TDataRecordSaveMode.smInsert)) || ForceSave || IsModified(original))  // don't proceed with db activity if nothing changed
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        if (beforeSaveArgs.SaveMode.Equals(TDataRecordSaveMode.smInsert))
                        {
                            rowsAffected = await cmd.InsertAsync(TableName, this, AppConfig.DatabaseSettings);
                        }
                        else
                        {
                            rowsAffected = await cmd.UpdateAsync(TableName, this);
                        }
                        bool savePerformed = (rowsAffected > 0);
                        if (savePerformed)
                        {
                            AfterSaveDataRecordEventArgs afterSaveArgs = new AfterSaveDataRecordEventArgs();
                            afterSaveArgs.SaveMode = beforeSaveArgs.SaveMode;
                            afterSaveArgs.Original = original;
                            afterSaveArgs.SqlConnection = conn;
                            //AfterSave?.Invoke(this, afterSaveArgs);
                            await AfterSaveAsync(afterSaveArgs);
                        }
                    }
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes the record from the table.
        /// </summary>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public virtual async Task<bool> DeleteAsync(FwSqlConnection conn = null)
        {
            bool success = false;
            InsteadOfDataRecordDeleteEventArgs insteadOfDeleteArgs = new InsteadOfDataRecordDeleteEventArgs();
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();
            //BeforeDelete?.Invoke(this, beforeDeleteArgs);
            await BeforeDeleteAsync(beforeDeleteArgs);  // may need to send the "conn" here someday
            if (beforeDeleteArgs.PerformDelete)
            {
                if (conn == null)
                {
                    conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                }

                if (InsteadOfDelete != null)
                {
                    InsteadOfDelete(this, insteadOfDeleteArgs);  // may need to send the "conn" here someday
                    success = insteadOfDeleteArgs.Success;
                }
                else
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                    {
                        int rowcount = await cmd.DeleteAsync(TableName, this);
                        success = (rowcount > 0);
                    }
                }
                //AfterDelete?.Invoke(this, afterDeleteArgs);
                await AfterDeleteAsync(afterDeleteArgs);   // may need to send the "conn" here someday
            }
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}
