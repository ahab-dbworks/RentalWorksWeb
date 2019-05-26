using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{
    public class FwDataReadWriteRecord : FwDataRecord
    {



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

        protected virtual void OnBeforeSave(BeforeSaveDataRecordEventArgs e)
        {
            BeforeSave?.Invoke(this, e);
        }
        protected virtual void OnAfterSave(AfterSaveDataRecordEventArgs e)
        {
            AfterSave?.Invoke(this, e);
        }
        protected virtual void OnBeforeValidate(BeforeValidateDataRecordEventArgs e)
        {
            BeforeValidate?.Invoke(this, e);
        }

        protected virtual void OnInsteadOfDelete(InsteadOfDataRecordDeleteEventArgs e)
        {
            InsteadOfDelete?.Invoke(this, e);
        }

        protected virtual void OnBeforeDelete(BeforeDeleteEventArgs e)
        {
            BeforeDelete?.Invoke(this, e);
        }
        protected virtual void OnAfterDelete(AfterDeleteEventArgs e)
        {
            AfterDelete?.Invoke(this, e);
        }
        protected virtual void OnAssignPrimaryKeys(EventArgs e)
        {
            AssignPrimaryKeys?.Invoke(this, e);
        }


        //------------------------------------------------------------------------------------
        public FwDataReadWriteRecord() : base() { }
        //------------------------------------------------------------------------------------
        protected virtual bool Validate(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, ref string validateMsg)
        {
            //override this method on a derived class to implement custom validation logic
            bool isValid = true;
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public virtual bool ValidateDataRecord(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, ref string validateMsg)
        {
            bool isValid = true;
            validateMsg = "";

            if (BeforeValidate != null)
            {
                BeforeValidateDataRecordEventArgs args = new BeforeValidateDataRecordEventArgs();
                args.SaveMode = saveMode;
                args.Original = original;
                BeforeValidate(this, args);
            }

            if (isValid)
            {
                isValid = AllFieldsValid(saveMode, ref validateMsg);
            }
            if (isValid)
            {
                isValid = Validate(saveMode, original, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Saves the record to the table.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public virtual async Task<int> SaveAsync(FwDataReadWriteRecord original, FwSqlConnection conn = null)
        {
            int rowsAffected = 0;

            BeforeSaveDataRecordEventArgs beforeSaveArgs = new BeforeSaveDataRecordEventArgs();
            beforeSaveArgs.SaveMode = TDataRecordSaveMode.smInsert;
            beforeSaveArgs.Original = original;
            beforeSaveArgs.SqlConnection = conn;

            if (NoPrimaryKeysHaveValues)
            {
                beforeSaveArgs.SaveMode = TDataRecordSaveMode.smInsert;
            }
            else if (AllPrimaryKeysHaveValues)
            {
                beforeSaveArgs.SaveMode = TDataRecordSaveMode.smUpdate;
            }
            else
            {
                throw new Exception("Values were not supplied for all primary keys.");
            }

            if (conn == null)
            {
                conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
            }

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

            BeforeSave?.Invoke(this, beforeSaveArgs);

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
                            AfterSave?.Invoke(this, afterSaveArgs);
                        }
                    }
                }
            }
            return rowsAffected;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> DeleteAsync()
        {
            bool success = false;
            InsteadOfDataRecordDeleteEventArgs insteadOfDeleteArgs = new InsteadOfDataRecordDeleteEventArgs();
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();
            BeforeDelete?.Invoke(this, beforeDeleteArgs);
            if (beforeDeleteArgs.PerformDelete)
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    if (InsteadOfDelete != null)
                    {
                        InsteadOfDelete(this, insteadOfDeleteArgs);
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
                    AfterDelete?.Invoke(this, afterDeleteArgs);
                }
            }
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}
