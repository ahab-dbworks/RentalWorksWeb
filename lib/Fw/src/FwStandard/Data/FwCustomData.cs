using FwStandard.ConfigSection;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;


namespace FwStandard.DataLayer
{
    public class FwCustomField {
        public string moduleName;
        public string fieldName;
        public string customTableName;
        public string customFieldName;
    }

    public class FwCustomValue
    {
        public string FieldName;
        public string FieldValue;
        public FwCustomValue(string fieldName, string fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
        }
    }

    public class FwCustomData 
    {
        protected DatabaseConfig _dbConfig { get; set; }
        protected string _moduleName { get; set; }
        protected string[] _primaryKeyValues{ get; set; }
        public List<FwCustomValue> customValues = new List<FwCustomValue>();
        //------------------------------------------------------------------------------------
        public FwCustomData(string moduleName, string[] primaryKeyValues) 
        {
            this._moduleName = moduleName;
            this._primaryKeyValues = primaryKeyValues;
        }
        //------------------------------------------------------------------------------------
        public virtual void SetDbConfig(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public virtual bool Load()
        {
            bool loaded = false;
            if (_primaryKeyValues.Length > 0)
            {
                List<FwCustomField> customFields = null;

                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    qry.Add("select * from customfield where modulename = @modulename");
                    qry.AddParameter("@modulename", _moduleName);
                    customFields = (List<FwCustomField>)qry.Select<FwCustomField>(true, 1, 10);
                }

                if (customFields.Count > 0)
                {
                    using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                    {
                        FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                        qry.Add("select ");
                        int colNo = 0;
                        foreach (FwCustomField field in customFields)
                        {
                            if (colNo > 0)
                            {
                                qry.Add(",");
                            }
                            qry.Add("[" + field.fieldName + "] = " + field.customTableName + "." + field.customFieldName);
                            qry.AddColumn(field.fieldName);
                            colNo++;
                        }
                        qry.Add("from customvaluesstring ");// + field.customTableName);  (should not be hard-coded)

                        int k = 1;
                        for (k = 1; k<=3; k++)
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
                        foreach (string key in _primaryKeyValues)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString(), key);
                            k++;
                        }
                        while (k <= 3)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString(), "");
                            k++;
                        }

                        FwJsonDataTable table = qry.QueryToFwJsonTable(true);
                        colNo = 0; 
                        foreach (FwJsonDataTableColumn column in table.Columns) {
                            if (table.Rows.Count > 0)
                            {
                                customValues.Add(new FwCustomValue(column.DataField, table.Rows[0][colNo].ToString()));
                            }
                            else
                            {
                                customValues.Add(new FwCustomValue(column.DataField, ""));
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
        public virtual bool Save()
        {
            bool loaded = false;
            if (_primaryKeyValues.Length > 0)
            {
                List<FwCustomField> customFields = null;

                //jh EXTREMELY NON-OPTIMIZED.  need to change

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
                    foreach (string key in _primaryKeyValues)
                    {
                        qry.AddParameter("@keyvalue" + k.ToString(), key);
                        k++;
                    }
                    while (k <= 3)
                    {
                        qry.AddParameter("@keyvalue" + k.ToString(), "");
                        k++;
                    }
                    qry.ExecuteNonQuery();
                }

                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    qry.Add("select * from customfield where modulename = @modulename");
                    qry.AddParameter("@modulename", _moduleName);
                    customFields = (List<FwCustomField>)qry.Select<FwCustomField>(true, 1, 10);
                }

                if (customFields.Count > 0)
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
                        foreach (FwCustomField field in customFields)
                        {
                            qry.Add(field.customFieldName);
                            if (colNo < customFields.Count)
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
                        foreach (FwCustomField field in customFields)
                        {
                            qry.Add("@value" + colNo.ToString().PadLeft(2, '0'));
                            if (colNo < customFields.Count)
                            {
                                qry.Add(",");
                            }
                            colNo++;
                        }

                        qry.Add(") ");

                        k = 1;
                        foreach (string key in _primaryKeyValues)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString().PadLeft(2, '0'), key);
                            k++;
                        }
                        for (; k <= 3; k++)
                        {
                            qry.AddParameter("@keyvalue" + k.ToString().PadLeft(2, '0'), "");
                        }



                        colNo = 1;
                        foreach (FwCustomField field in customFields)
                        {
                            string value = "";
                            foreach (FwCustomValue customValue in customValues)
                            {
                                if (customValue.FieldName.Equals(field.fieldName))
                                {
                                    value = customValue.FieldValue;
                                }

                            }
                            qry.AddParameter("@value" + colNo.ToString().PadLeft(2, '0'), value);
                            colNo++;
                        }

                        qry.ExecuteNonQuery(true);
                    }
                }
            }
            else
            {
                throw new Exception("Primary Key values are missing on " + GetType().ToString() + ".Save");
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
    }
}
