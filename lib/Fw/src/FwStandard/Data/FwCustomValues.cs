using FwStandard.ConfigSection;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FwStandard.DataLayer
{

    public class FwCustomValues : List<FwCustomValue>
    {
        protected DatabaseConfig _dbConfig { get; set; }
        public FwCustomFields CustomFields = new FwCustomFields();

        //------------------------------------------------------------------------------------
        public FwCustomValues() { }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
            CustomFields.SetDbConfig(dbConfig);
        }
        //------------------------------------------------------------------------------------
        public virtual async Task LoadCustomFieldsAsync(string moduleName)
        {
            await CustomFields.LoadAsync(moduleName);
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> LoadAsync(string[] primaryKeyValues)
        {
            bool loaded = false;
            Clear();
            if (primaryKeyValues.Length > 0)
            {
                if (CustomFields.Count > 0)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                    {
                        int customTableIndex = 1;
                        int k = 1;
                        string firstTableAlias = "";
                        List<FwCustomTable> customTables = new List<FwCustomTable>();

                        FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                        qry.Add("select ");
                        int colNo = 0;
                        foreach (FwCustomField field in CustomFields)
                        {
                            string customTableAlias = "";
                            bool customTableExists = false;
                            foreach (FwCustomTable customTable in customTables)
                            {
                                if (field.CustomTableName.Equals(customTable.TableName))
                                {
                                    customTableExists = true;
                                    customTableAlias = customTable.Alias;
                                    break;
                                }
                            }
                            if (!customTableExists)
                            {
                                customTableAlias = "customtable" + customTableIndex.ToString().PadLeft(2, '0');
                                customTables.Add(new FwCustomTable(field.CustomTableName, customTableAlias));
                                customTableIndex++;
                            }


                            if (colNo > 0)
                            {
                                qry.Add(",");
                            }
                            qry.Add("[" + field.FieldName + "] = " + customTableAlias + "." + field.CustomFieldName);
                            qry.AddColumn(field.FieldName);
                            colNo++;
                        }
                        customTableIndex = 1;
                        foreach (FwCustomTable customTable in customTables)
                        {
                            if (customTableIndex == 1)
                            {
                                qry.Add(" from ");
                                firstTableAlias = customTable.Alias;
                            }
                            else {
                                qry.Add(" join ");
                            }
                            qry.Add(customTable.TableName + " " + customTable.Alias);
                            if (customTableIndex > 1) {
                                qry.Add(" on ( ");

                                for (k = 1; k <= 3; k++)
                                {
                                    if (k > 1)
                                    {
                                        qry.Add(" and ");
                                    }
                                    qry.Add(firstTableAlias + ".uniqueid" + k.ToString().PadLeft(2, '0'));
                                    qry.Add(" = ");
                                    qry.Add(customTable.Alias + ".uniqueid" + k.ToString().PadLeft(2, '0'));
                                }
                                qry.Add(" )");

                            }
                            customTableIndex++;
                        }

                        for (k = 1; k <= 3; k++)
                        {
                            if (k == 1)
                            {
                                qry.Add("where ");
                            }
                            else
                            {
                                qry.Add("and ");
                            }
                            qry.Add(firstTableAlias + ".uniqueid" + k.ToString().PadLeft(2, '0'));
                            qry.Add(" = @keyvalue" + k.ToString());
                        }

                        k = 1;
                        foreach (string key in primaryKeyValues)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString(), key);
                            k++;
                        }
                        while (k <= 3)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString(), "");
                            k++;
                        }

                        FwJsonDataTable table = await qry.QueryToFwJsonTableAsync(true);
                        colNo = 0;
                        foreach (FwJsonDataTableColumn column in table.Columns)
                        {
                            if (table.Rows.Count > 0)
                            {
                                Add(new FwCustomValue(column.DataField, table.Rows[0][colNo].ToString()));
                            }
                            else
                            {
                                Add(new FwCustomValue(column.DataField, ""));
                            }
                            colNo++;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Primary Key values are missing on " + GetType().ToString() + ".LoadAsync");
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> SaveAsync(string[] primaryKeyValues)
        {
            bool saved = false;
            if (primaryKeyValues.Length > 0)
            {
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "savecustomvalues", _dbConfig.QueryTimeout);

                    string paramName = "";
                    int k = 1;
                    foreach (string key in primaryKeyValues)
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
