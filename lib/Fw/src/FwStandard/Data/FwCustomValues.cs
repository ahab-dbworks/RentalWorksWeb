using FwStandard.ConfigSection;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
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
                        FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                        qry.Add("select ");
                        int colNo = 0;
                        foreach (FwCustomField field in CustomFields)
                        {
                            if (colNo > 0)
                            {
                                qry.Add(",");
                            }
                            qry.Add("[" + field.FieldName + "] = " + field.CustomTableName + "." + field.CustomFieldName);
                            qry.AddColumn(field.FieldName);
                            colNo++;
                        }
                        qry.Add("from customvaluesstring ");// + field.customTableName);  (should not be hard-coded)

                        int k = 1;
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
                            qry.Add("uniqueid" + k.ToString().PadLeft(2, '0'));
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
                throw new Exception("Primary Key values are missing on " + GetType().ToString() + ".Load");
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
        public virtual async Task<bool> SaveAsync(string[] primaryKeyValues)
        {

            bool saved = false;
            if (primaryKeyValues.Length > 0)
            {
                //jh EXTREMELY NON-OPTIMIZED.  need to change to a single stored procedure where all custom vaules are passed back

                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    qry.Add("delete ");
                    qry.Add(" from customvaluesstring ");// + field.customTableName);  (should not be hard-coded)

                    int k = 1;
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
                        qry.Add("uniqueid" + k.ToString().PadLeft(2, '0'));
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
                    await qry.ExecuteNonQueryAsync();
                }

                if (CustomFields.Count > 0)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                        qry.Add("insert into  customvaluesstring ("); // + field.customTableName);  (should not be hard-coded)

                        int k = 1;
                        for (k = 1; k <= 3; k++)
                        {
                            qry.Add("uniqueid" + k.ToString().PadLeft(2, '0') + ",");
                        }

                        int colNo = 1;
                        foreach (FwCustomField field in CustomFields)
                        {
                            qry.Add(field.CustomFieldName);
                            if (colNo < CustomFields.Count)
                            {
                                qry.Add(",");
                            }
                            colNo++;
                        }

                        qry.Add(") values (");

                        k = 1;
                        for (k = 1; k <= 3; k++)
                        {
                            qry.Add("@keyvalue" + k.ToString().PadLeft(2, '0') + ",");
                        }

                        colNo = 1;
                        foreach (FwCustomField field in CustomFields)
                        {
                            qry.Add("@value" + colNo.ToString().PadLeft(2, '0'));
                            if (colNo < CustomFields.Count)
                            {
                                qry.Add(",");
                            }
                            colNo++;
                        }

                        qry.Add(") ");

                        k = 1;
                        foreach (string key in primaryKeyValues)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString().PadLeft(2, '0'), key);
                            k++;
                        }
                        for (; k <= 3; k++)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString().PadLeft(2, '0'), "");
                        }



                        colNo = 1;
                        foreach (FwCustomField field in CustomFields)
                        {
                            string value = "";
                            foreach (FwCustomValue customValue in this)
                            {
                                if (customValue.FieldName.Equals(field.FieldName))
                                {
                                    value = customValue.FieldValue;
                                }

                            }
                            qry.AddParameter("@value" + colNo.ToString().PadLeft(2, '0'), value);
                            colNo++;
                        }

                        await qry.ExecuteNonQueryAsync(true);
                    }
                }
            }
            else
            {
                throw new Exception("Primary Key values are missing on " + GetType().ToString() + ".Save");
            }
            return saved;
        }
        //------------------------------------------------------------------------------------
    }
}
