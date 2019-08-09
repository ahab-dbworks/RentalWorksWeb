using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FwStandard.Data
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
        /// <summary>
        /// Saves the custom field data using a stored procedure
        /// </summary>
        /// <param name="primaryKeyValues"></param>
        /// <param name="conn">Specify an existing SqlConnection if desired.  Can be used for multi-statement transactions. If null, then a new Connection will be established.</param>
        public virtual async Task<bool> SaveAsync(object[] primaryKeyValues, FwSqlConnection conn = null)
        {
            bool saved = false;
            if (primaryKeyValues.Length > 0)
            {
                if (this.Count > 0) // only return saved=true if custom values are provided
                {
                    if (conn == null)
                    {
                        conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                    }
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

                    int indexOfBoolean = 0;
                    int indexOfString = 0;
                    int indexOfInteger = 0;
                    int indexOfFloat = 0;
                    int indexOfDateTime = 0;

                    foreach (FwCustomField f in CustomFields)
                    {

                        if (f.FieldType.Equals("True/False"))
                        {
                            indexOfBoolean++;
                            paramName = "@customboolean" + indexOfBoolean.ToString().PadLeft(2, '0');
                        }
                        else if (f.FieldType.Equals("Text"))
                        {
                            indexOfString++;
                            paramName = "@customstring" + indexOfString.ToString().PadLeft(2, '0');
                        }
                        else if (f.FieldType.Equals("Integer"))
                        {
                            indexOfInteger++;
                            paramName = "@customint" + indexOfInteger.ToString().PadLeft(2, '0');
                        }
                        else if (f.FieldType.Equals("Float"))
                        {
                            indexOfFloat++;
                            paramName = "@customnumeric" + indexOfFloat.ToString().PadLeft(2, '0');
                        }
                        else if (f.FieldType.Equals("Date"))
                        {
                            indexOfDateTime++;
                            paramName = "@customdatetime" + indexOfDateTime.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            throw new Exception("Invalid Custom Field Type: " + f.FieldType + " in " + GetType().ToString() + ".SaveAsync");
                        }

                        foreach (FwCustomValue value in this)
                        {
                            if (value.FieldName.Equals(f.FieldName))
                            {
                                if (f.FieldType.Equals("True/False"))
                                {
                                    qry.AddParameter(paramName, SqlDbType.NVarChar, ParameterDirection.Input, FwConvert.LogicalToCharacter(FwConvert.ToBoolean(value.FieldValue)));
                                }
                                else if (f.FieldType.Equals("Text"))
                                {
                                    qry.AddParameter(paramName, SqlDbType.NVarChar, ParameterDirection.Input, value.FieldValue);
                                }
                                else if (f.FieldType.Equals("Integer"))
                                {
                                    Int32? i = 0;
                                    if (!string.IsNullOrEmpty(value.FieldValue))
                                    {
                                        i = FwConvert.ToInt32(value.FieldValue);
                                    }
                                    qry.AddParameter(paramName, SqlDbType.Int, ParameterDirection.Input, i);
                                }
                                else if (f.FieldType.Equals("Float"))
                                {
                                    Decimal? d = 0;
                                    if (!string.IsNullOrEmpty(value.FieldValue))
                                    {
                                        d = FwConvert.ToDecimal(value.FieldValue);
                                    }
                                    qry.AddParameter(paramName, SqlDbType.Decimal, ParameterDirection.Input, d);
                                }
                                else if (f.FieldType.Equals("Date"))
                                {
                                    DateTime? dt = null;
                                    if (!string.IsNullOrEmpty(value.FieldValue))
                                    {
                                        dt = FwConvert.ToDateTime(value.FieldValue);
                                    }
                                    qry.AddParameter(paramName, SqlDbType.DateTime, ParameterDirection.Input, dt);
                                }
                            }
                        }

                    }

                    await qry.ExecuteNonQueryAsync();
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
