using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{

    public class FwCustomValues : List<FwCustomValue>
    {
        private FwApplicationConfig _appConfig = null;
        private FwUserSession _userSession = null;
        [JsonIgnore]
        public FwApplicationConfig AppConfig
        {
            get { return _appConfig; }
            set
            {
                _appConfig = value;
                CustomFields.AppConfig = value;
            }
        }

        public FwUserSession UserSession
        {
            get { return _userSession; }
            set
            {
                _userSession = value;
                CustomFields.UserSession = value;
            }
        }

        public FwCustomFields CustomFields = new FwCustomFields();

        //------------------------------------------------------------------------------------
        public FwCustomValues() { }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(SqlServerConfig dbConfig)
        {
            
        }
        //------------------------------------------------------------------------------------
        public virtual void AddCustomValue(string fieldName, string value, string fieldType)
        {
            Add(new FwCustomValue(fieldName, value, fieldType));
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> SaveAsync(object[] primaryKeyValues)
        {
            bool saved = false;
            if (primaryKeyValues.Length > 0)
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "savecustomvalues", AppConfig.DatabaseSettings.QueryTimeout);

                    string paramName = "";
                    int k = 1;
                    foreach (object key in primaryKeyValues)
                    {
                        paramName = "@uniqueid" + k.ToString().PadLeft(2, '0');
                        qry.AddParameter(paramName, SqlDbType.NVarChar, ParameterDirection.Input, key);
                        k++;
                    }
                    while (k <= 3)
                    {
                        paramName = "@uniqueid" + k.ToString().PadLeft(2, '0');
                        qry.AddParameter(paramName, SqlDbType.NVarChar, ParameterDirection.Input, "");
                        k++;
                    }

                    int p = 0;
                    foreach (FwCustomValue value in this)
                    {
                        int indexOfType = 0;
                        for (int f = p; f >= 0; f--) {
                            if (CustomFields[f].CustomTableName.Equals(CustomFields[p].CustomTableName)) {
                                indexOfType++;
                            }
                        }
                        paramName = "@custom" + CustomFields[p].CustomTableName.Replace("customvalues", "") + indexOfType.ToString().PadLeft(2, '0');
                        qry.AddParameter(paramName, SqlDbType.NVarChar, ParameterDirection.Input, value.FieldValue);
                        p++;
                    }
                    await qry.ExecuteNonQueryAsync(true);
                    saved = true;

                }
            }
            else
            {
                throw new Exception("Primary Key values are missing on " + GetType().ToString() + ".SaveAsync");
            }
            return saved;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
