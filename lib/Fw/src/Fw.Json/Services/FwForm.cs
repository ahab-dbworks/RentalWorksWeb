using System.Collections.Generic;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Dynamic;
using System.Reflection;
using Fw.Json.Services.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace Fw.Json.Services
{
    public class FwForm
    {
        public class EventArgsOnLoadCalculatedField : EventArgs
        {
            public Dictionary<string, FwJsonFormTable> JsonTables {get;set;}
        }
        //---------------------------------------------------------------------------------------------
        public FwDatabases Database {get;set;}
        public FwSqlConnection DatabaseConnection {get{return new FwSqlConnection(this.Database);}}
        public Dictionary<string, FwSqlTable> Tables {get;set;}
        public FwApplicationSchema.Form FormSchema {get;set;}
        public event EventHandler<EventArgsOnLoadCalculatedField> OnLoadCalculatedFields;
        private Assembly webServiceAssembly;
        private string applicationServicesNamespace;
        private string applicationServicesTypePrefix;
        public Modes Mode;
        public enum Modes {NEW, EDIT};
        //---------------------------------------------------------------------------------------------
        public FwForm(string applicationServicesNamespace, string applicationServicesTypePrefix, Assembly webServiceAssembly, FwApplicationSchema.Form formSchema)
        {
            this.applicationServicesNamespace  = applicationServicesNamespace;
            this.applicationServicesTypePrefix = applicationServicesTypePrefix;
            this.webServiceAssembly = webServiceAssembly;
            this.FormSchema         = formSchema;
            this.Database           = (FwDatabases)Enum.Parse(typeof(FwDatabases), formSchema.DatabaseConnection);
            this.Tables             = new Dictionary<string,FwSqlTable>();
            this.LoadSchemaTables();
        }
        //---------------------------------------------------------------------------------------------
        public void GatherData(dynamic uniqueids, dynamic fields)
        {
            this.PopulateUniqueIds(uniqueids);
            if (fields != null)
            {
                this.PopulateFields(fields);
            }
        }
        //---------------------------------------------------------------------------------------------
        public void LoadSchemaTables()
        {
            FwSqlTable newTable;

            foreach (var table in this.FormSchema.Tables)
            {
                newTable = new FwSqlTable(table.Key, table.Value);

                foreach (var uniqueid in table.Value.UniqueIds)
                {
                    newTable.GetUniqueId(uniqueid.Key).UniqueIdentifier = true;
                }
                this.Tables[newTable.Table] = newTable;
            }
        }
        //---------------------------------------------------------------------------------------------
        public void LoadSchemaTableColumns()
        {
            foreach (var table in this.FormSchema.Tables)
            {
                foreach(var column in table.Value.UniqueIds)
                {
                    this.Tables[table.Key].GetUniqueId(column.Key);
                }
                foreach(var column in table.Value.Columns)
                {
                    this.Tables[table.Key].GetField(column.Key);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void PopulateUniqueIds(IDictionary<string, object>  uniqueIds)
        {
            foreach(var uniqueidItem in uniqueIds)
            {
                string[] datafieldFragments;
                string table, column;
                dynamic uniqueidfield;

                uniqueidfield      = uniqueidItem.Value;
                datafieldFragments = uniqueidfield.datafield.Split('.');
                table              = datafieldFragments[0];
                column             = datafieldFragments[1];
                if (this.FormSchema.Tables.ContainsKey(table) && this.FormSchema.Tables[table].UniqueIds.ContainsKey(column))
                {
                    if ((this.FormSchema.Tables[table].UniqueIds[column].DataType == "key") ||
                        (this.FormSchema.Tables[table].UniqueIds[column].DataType == "validation"))
                    {
                        this.Tables[table].GetUniqueId(column).Value = FwCryptography.AjaxDecrypt(uniqueidfield.value);
                    }
                    else
                    {
                        this.Tables[table].GetUniqueId(column).Value = uniqueidfield.value;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void PopulateFields(IDictionary<string, object> fields)
        {
            foreach(var fieldItem in fields)
            {
                string[] datafieldFragments;
                string table, column;
                dynamic field;

                field              = fieldItem.Value;
                datafieldFragments = field.datafield.Split('.');
                table              = datafieldFragments[0];
                column             = datafieldFragments[1];
                if (this.FormSchema.Tables.ContainsKey(table) && this.FormSchema.Tables[table].UniqueIds.ContainsKey(column))
                {
                    if ((this.FormSchema.Tables[table].UniqueIds[column].DataType == "validation") && (this.FormSchema.Tables[table].UniqueIds[column].DataType == "key"))
                    {
                        this.Tables[table].GetField(column).Value = FwCryptography.AjaxDecrypt(field.value);
                    }
                    else
                    {
                        this.Tables[table].GetField(column).Value = field.value;
                    }
                }
                if (this.FormSchema.Tables.ContainsKey(table) && this.FormSchema.Tables[table].Columns.ContainsKey(column))
                {
                    if ((this.FormSchema.Tables[table].Columns[column].DataType == "validation") && (this.FormSchema.Tables[table].Columns[column].DataType == "key"))
                    {
                        this.Tables[table].GetField(column).Value = FwCryptography.AjaxDecrypt(field.value);
                    }
                    else
                    {
                        this.Tables[table].GetField(column).Value = field.value;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public Dictionary<string, FwJsonFormTable> Load()
        {
            Dictionary<string, FwJsonFormTable> jsonTables;
            FwSqlConnection conn;

            jsonTables = new Dictionary<string,FwJsonFormTable>();
            foreach (var tableitem in this.Tables)
            {
                FwSqlTable sqlTable;
                FwJsonFormTable jsonTable;

                sqlTable = tableitem.Value;
                conn = new FwSqlConnection(this.Database);
                sqlTable.SelectRow(conn);
                jsonTable = new FwJsonFormTable();
                foreach (var columnItem in sqlTable.Fields)
                {
                    string validationName, columnName, columnValue, columnText, dataType;
                    FwSqlColumn column;

                    column         = columnItem.Value;
                    columnName     = column.Column;
                    columnValue    = column.Value;
                    validationName = string.Empty;
                    if (this.FormSchema.Tables[sqlTable.Table].UniqueIds.ContainsKey(column.Column))
                    {
                        validationName = this.FormSchema.Tables[sqlTable.Table].UniqueIds[column.Column].ValidationName;
                    }
                    else if (this.FormSchema.Tables[sqlTable.Table].Columns.ContainsKey(column.Column))
                    {
                        dataType = this.FormSchema.Tables[sqlTable.Table].Columns[column.Column].DataType;
                        switch(dataType)
                        {
                            case "key":
                                columnValue = FwCryptography.AjaxEncrypt(new FwDatabaseField(column.Value).ToString().TrimEnd());
                                break;
                            case "date":
                                columnValue = new FwDatabaseField(column.Value).ToShortDateString();
                                break;
                            case "datetime":
                                columnValue = new FwDatabaseField(column.Value).ToShortDateTimeString();
                                break;
                            case "color":
                                columnValue = FwConvert.OleToHex(FwConvert.ToInt32(column.Value));
                                break;
                            case "number":
                                if (this.FormSchema.Tables[sqlTable.Table].Columns[column.Column].SqlDataType == "int")
                                {
                                    string[] numberParts = columnValue.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                                    if (numberParts.Length > 1)
                                    {
                                        columnValue = numberParts[0];
                                    }
                                }
                                break;
                            case "password":
                                Random random = new Random();
                                if (columnValue != "")
                                {
                                    //Do not load passwords. Added a random number to randomize the encryption string.
                                    columnValue = FwCryptography.AjaxEncrypt("DoNotLoad" + random.Next().ToString());
                                }
                                break;
                            case "ssn":
                                columnValue = FwCryptography.DbwDecrypt(conn, column.Value);
                                break;
                            case "encrypt":
                                columnValue = FwCryptography.DbwDecrypt(conn, column.Value);
                                break;
                        }
                        validationName = this.FormSchema.Tables[sqlTable.Table].Columns[column.Column].ValidationName;
                    }
                    columnText = string.Empty;
                    if (!string.IsNullOrEmpty(validationName))
                    {
                        columnValue = FwCryptography.AjaxEncrypt(column.Value);
                        columnText  = this.GetValidationDisplayField(validationName, column);
                    }
                    
                    jsonTable.AddField(columnName, columnValue, columnText);
                }
                if (Mode == Modes.EDIT)
                {
                    foreach (var uniqueidItem in sqlTable.UniqueIds)
                    {
                        string uniqueidName, uniqueidValue, uniqueidText;
                        FwSqlColumn uniqueid;

                        uniqueid      = uniqueidItem.Value;
                        uniqueidName  = uniqueid.Column;
                        if (this.FormSchema.Tables[sqlTable.Table].UniqueIds[uniqueidName].DataType == "key")
                        {
                            uniqueidValue = FwCryptography.AjaxEncrypt(uniqueid.Value);
                        }
                        else
                        {
                            uniqueidValue = uniqueid.Value;
                        }
                        uniqueidText  = string.Empty;

                        jsonTable.AddField(uniqueidName, uniqueidValue, uniqueidText);
                    }
                }
                jsonTables[sqlTable.TableName] = jsonTable;
            }
            if (OnLoadCalculatedFields != null)
            {
                EventArgsOnLoadCalculatedField args;
                args = new EventArgsOnLoadCalculatedField();
                args.JsonTables = jsonTables;
                OnLoadCalculatedFields(this, args);
            }

            return jsonTables;
        }
        //---------------------------------------------------------------------------------------------
        protected string GetValidationDisplayField(string validationName, FwSqlColumn column)
        {
            string typeName, result, uniqueidColumName, validationdisplayfield = string.Empty;
            Type type;
            FwApplicationSchema.Browse browseSchema;
            FwSqlSelect selectQry;
            FwBrowse validationBrowse;
            FwValidation validation;
            int pageNo, pageSize, colno;
            List<string> searchfields, searchfieldoperators, searchfieldvalues;
            string orderby;
            FwJsonDataTable jsonTable;

            result = string.Empty;
            if (validationName != FwConvert.StripNonAlphaNumericCharacters(validationName))
            {
                throw new Exception(validationName + "' contains illegal characters. [FwForm.cs:GetValidationDisplayField]");
            }
            if (!FwApplicationSchema.Current.Validations.ContainsKey(validationName))
            {
                throw new Exception(validationName + "' is not a Validation in the schema. [FwForm.cs:GetValidationDisplayField]");
            }
            else
            {
                typeName = applicationServicesNamespace + ".Validations." + applicationServicesTypePrefix + validationName;
                type = webServiceAssembly.GetType(typeName, false);
                if ( (type == null) || (!type.IsSubclassOf(typeof(FwValidation))) )
                {
                    throw new Exception("There is no web service setup for validation: " + validationName + ". [FwForm.cs:GetValidationDisplayField]");
                }
                validation = (FwValidation)Activator.CreateInstance(type);
            }
            browseSchema = FwApplicationSchema.Current.Validations[validationName].Browse;
            colno = 0;
            foreach(var columnItem in browseSchema.Columns)
            {
                if (columnItem.Value.ValidationDisplayField == "true")
                {
                    validationdisplayfield = columnItem.Value.ColumnName;
                    break;
                } 
                else if (colno == 0)
                {
                    // default the first column in case none are specified as the validationdisplayfield
                    validationdisplayfield = columnItem.Value.ColumnName;
                }
                colno++;
            }
            if (string.IsNullOrWhiteSpace(validationdisplayfield))
            {
                throw new Exception("Validation DisplayField is required.");
            }
            foreach(var uniqueidItem in browseSchema.UniqueIds)
            {
                uniqueidColumName = uniqueidItem.Value.ColumnName;
                pageNo   = 0;
                pageSize = 0;
                searchfields         = new List<string>(new string[]{uniqueidColumName});
                searchfieldoperators = new List<string>(new string[]{"="});
                searchfieldvalues    = new List<string>(new string[]{column.Value});
                orderby              = uniqueidColumName;
                validationBrowse     = new FwBrowse(browseSchema);
                selectQry            = validationBrowse.GetBrowseQry(pageNo, pageSize, searchfields, searchfieldoperators, searchfieldvalues, orderby);
                selectQry.SqlCommand.AddColumn(validationName, false);
                validation.GetDisplayFieldQuery(selectQry);

                selectQry.EnablePaging = false;

                jsonTable            = selectQry.SqlCommand.QueryToFwJsonTable(selectQry, browseSchema);
                if (jsonTable.Rows.Count > 0)
                {
                    result = jsonTable.GetValue(0, validationdisplayfield).ToString();
                }
                
                break;
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public void Insert()
        {
            if (this.Tables.Count > 0)
            {
                using(FwSqlConnection conn = new FwSqlConnection(this.Database))
                {
                    conn.Open();
                    using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                    {
                        foreach (KeyValuePair<string,FwSqlTable> tableItem in this.Tables)
                        {
                            if (!tableItem.Key.StartsWith("#"))
                            {
                                using (FwSqlCommand cmd = tableItem.Value.GetInsertCommand(conn))
                                {
                                    cmd.Transaction = transaction;
                                    cmd.ExecuteNonQuery(false);
                                }
                                foreach(var fieldItem in tableItem.Value.Fields)
                                {
                                    if (fieldItem.Value.UniqueIdentifier == true)
                                    {
                                        tableItem.Value.UniqueIds[fieldItem.Key] = fieldItem.Value;
                                    }
                                }
                                foreach(var uniqueidItem in tableItem.Value.UniqueIds)
                                {
                                    tableItem.Value.Fields.Remove(uniqueidItem.Key);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void Update()
        {
            if (this.Tables.Count > 0)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.Database))
                {
                    conn.Open();
                    using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                    {
                        foreach (KeyValuePair<string,FwSqlTable> tableItem in this.Tables)
                        {
                            if (!tableItem.Key.StartsWith("#"))
                            {
                                using (FwSqlCommand cmd = tableItem.Value.GetUpdateCommand(conn))
                                {
                                    cmd.Transaction = transaction;
                                    cmd.ExecuteNonQuery(false);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void Delete()
        {
            LoadSchemaTableColumns();
            if (this.Tables.Count > 0)
            {
                using (FwSqlConnection conn = new FwSqlConnection(this.Database))
                {
                    conn.Open();

                    using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                    {
                        foreach (KeyValuePair<string,FwSqlTable> tableItem in this.Tables)
                        {
                            using (FwSqlCommand cmd = tableItem.Value.GetDeleteCommand(conn))
                            {
                                cmd.Transaction = transaction;
                                cmd.ExecuteNonQuery(false);
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public bool ValidateRequiredFields()
        {
            bool isValid = true;

            foreach (var tableItem in this.Tables)
            {
                FwSqlTable sqlTable = tableItem.Value;
                // MV 2014-05-27 - I need to add Captions to the Columns in FwApplicationSchema, so I can show error messages appropriate for users rather than developers
                if (!this.FormSchema.Tables.ContainsKey(sqlTable.Table))
                {
                    isValid = false;
                    throw new Exception("Invalid table '" + sqlTable.Table + "'. [FwForm.cs:Validate]");
                }
                foreach (var columnItem in sqlTable.Fields)
                {
                    FwSqlColumn column = columnItem.Value;
                    if (column.EnableValidation)
                    {
                        
                        if ( ((this.FormSchema.Tables[sqlTable.Table].UniqueIds.ContainsKey(column.Column)) && (this.FormSchema.Tables[sqlTable.Table].UniqueIds[column.Column].Required) && (string.IsNullOrEmpty(column.Value))) ||
                             ((this.FormSchema.Tables[sqlTable.Table].Columns.ContainsKey(column.Column))   && (this.FormSchema.Tables[sqlTable.Table].Columns[column.Column].Required) && (string.IsNullOrEmpty(column.Value)))
                           )
                        {
                            isValid = false;
                            throw new Exception(column.ColumnSchema.Caption + " is required.");
                        }
                    }
                }
            }

            return isValid;
        }
        //---------------------------------------------------------------------------------------------
        public bool CheckDuplicate(string datafield, string value)
        {
            bool duplicate = false;
            FwSqlCommand qry;
            string table, field;

            table = datafield.Split('.')[0];
            field = datafield.Split('.')[1];

            if (this.FormSchema.Tables[table].Columns.ContainsKey(field))
            {
                qry = new FwSqlCommand(DatabaseConnection);
                qry.Add("select top 1 totalcount = count(*)");
                qry.Add("from " + table);
                qry.Add("where " + field + " = @value");
                //qry.AddParameter("@table", table);
                //qry.AddParameter("@field", field);
                qry.AddParameter("@value", value);
                qry.Execute();
                if (qry.GetField("totalcount").ToInt32() > 0)
                {
                    duplicate = true;
                }
            }

            return duplicate;
        }
        //---------------------------------------------------------------------------------------------
        public bool ValidateDuplicate(string table, Dictionary<string, string> datafields)
        {
            bool duplicate = false, validateSchema = true, haswhere = false;
            FwSqlCommand qry;
            string field, value;
            int counter = 0;

            if (!this.FormSchema.Tables.ContainsKey(table)) { validateSchema = false; }

            foreach (var datafield in datafields)
            {
                field = datafield.Key;
                value = datafield.Value;

                if ((!this.FormSchema.Tables[table].Columns.ContainsKey(field)) && (!this.FormSchema.Tables[table].UniqueIds.ContainsKey(field)))
                {
                    validateSchema = false; break;
                }
                //if (string.IsNullOrEmpty(value))
                //{
                //    validateSchema = false; break;
                //}
            }

            if (validateSchema)
            {
                qry = new FwSqlCommand(DatabaseConnection);
                qry.Add("select totalcount = count(*)");
                qry.Add("from " + table);
                foreach (var datafield in datafields)
                {
                    counter++;
                    if (!haswhere)
                    {
                        qry.Add("where " + datafield.Key + " = @nodupe" + counter.ToString());
                        qry.AddParameter("@nodupe" + counter.ToString(), datafield.Value);
                        haswhere = true;
                    }
                    else
                    {
                        qry.Add("and " + datafield.Key + " = @nodupe" + counter.ToString());
                        qry.AddParameter("@nodupe" + counter.ToString(), datafield.Value);
                        
                    }
                }
                qry.Execute();
                if (qry.GetField("totalcount").ToInt32() > 0)
                {
                    duplicate = true;
                }
            }

            return duplicate;
        }
        //---------------------------------------------------------------------------------------------
    }
}
