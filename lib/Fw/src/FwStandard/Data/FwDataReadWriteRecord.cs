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
        public event EventHandler<BeforeDeleteEventArgs> BeforeDelete;
        public event EventHandler<AfterDeleteEventArgs> AfterDelete;
        public event EventHandler<EventArgs> AssignPrimaryKeys;

        public delegate void BeforeSaveEventHandler(BeforeSaveDataRecordEventArgs e);
        public delegate void AfterSaveEventHandler(AfterSaveDataRecordEventArgs e);
        public delegate void BeforeValidateEventHandler(BeforeValidateDataRecordEventArgs e);
        public delegate void BeforeDeleteEventHandler(BeforeDeleteEventArgs e);
        public delegate void AfterDeleteEventHandler(AfterDeleteEventArgs e);
        public delegate void AssignPrimaryKeysEventHandler(EventArgs e);

        protected virtual void OnBeforeSave(BeforeSaveDataRecordEventArgs e)
        {
            EventHandler<BeforeSaveDataRecordEventArgs> handler = BeforeSave;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnAfterSave(AfterSaveDataRecordEventArgs e)
        {
            EventHandler<AfterSaveDataRecordEventArgs> handler = AfterSave;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnBeforeValidate(BeforeValidateDataRecordEventArgs e)
        {
            EventHandler<BeforeValidateDataRecordEventArgs> handler = BeforeValidate;
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
        protected virtual void OnAssignPrimaryKeys(EventArgs e)
        {
            EventHandler<EventArgs> handler = AssignPrimaryKeys;
            if (handler != null)
            {
                handler(this, e);
            }
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
        public virtual async Task<int> SaveAsync(FwDataReadWriteRecord original)
        {
            int rowsAffected = 0;
            TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;

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

            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
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

                BeforeSaveDataRecordEventArgs beforeSaveArgs = new BeforeSaveDataRecordEventArgs();
                beforeSaveArgs.SaveMode = saveMode;
                beforeSaveArgs.Original = original;

                AfterSaveDataRecordEventArgs afterSaveArgs = new AfterSaveDataRecordEventArgs();
                afterSaveArgs.SaveMode = saveMode;
                afterSaveArgs.Original = original;

                if (BeforeSave != null)
                {
                    BeforeSave(this, beforeSaveArgs);
                }

                if (beforeSaveArgs.PerformSave)
                {
                    if ((saveMode.Equals(TDataRecordSaveMode.smInsert)) || ForceSave || IsModified(original))  // don't proceed with db activity if nothing changed
                    {
                        using (FwSqlCommand cmd = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
                        {
                            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                            {
                                rowsAffected = await cmd.InsertAsync(true, TableName, this, AppConfig.DatabaseSettings);
                            }
                            else
                            {
                                rowsAffected = await cmd.UpdateAsync(true, TableName, this);
                            }
                            bool savePerformed = (rowsAffected > 0);
                            if (savePerformed)
                            {
                                if (AfterSave != null)
                                {
                                    AfterSave(this, afterSaveArgs);
                                }
                            }
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
            BeforeDeleteEventArgs beforeDeleteArgs = new BeforeDeleteEventArgs();
            AfterDeleteEventArgs afterDeleteArgs = new AfterDeleteEventArgs();
            if (BeforeDelete != null)
            {
                BeforeDelete(this, beforeDeleteArgs);
            }
            if (beforeDeleteArgs.PerformDelete)
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout))
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
