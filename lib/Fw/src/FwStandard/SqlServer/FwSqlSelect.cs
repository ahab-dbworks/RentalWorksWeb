using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace FwStandard.SqlServer
{
    public class FwSqlSelect
    {
        //---------------------------------------------------------------------------------------------
        private enum Clauses { None, Unknown, Union, Select, From, Where, GroupBy, Having, OrderBy }

        public StringBuilder Cte { get; set; } = new StringBuilder();
        public StringBuilder OriginalSQL {get;set;} = new StringBuilder();
        public List<FwSqlSelectStatement> SelectStatements {get;set;} = new List<FwSqlSelectStatement>();
        public List<string> OrderBy {get;set;} = new List<string>();
        public Dictionary<string, SqlParameter> Parameters {get;set;} = new Dictionary<string, SqlParameter>();
        public int Top {get;set;} = 0;
        public bool EnablePaging {get;set;} = false;
        public int PageNo {get;set;} = 0;
        public int PageSize {get;set;} = 10;
        public FwSqlConnection SqlConnection;
        public FwSqlCommand SqlCommand;
        public enum PagingCompatibilities { AutoDetect, PreSql2012, Sql2012 } // AutoDetect logic is handled in FwController, since the Database Connection info is dependency injected there
        public static PagingCompatibilities PagingCompatibility { get; set; } = PagingCompatibilities.AutoDetect;
        public List<string> TotalFields { get; set; } = new List<string>();
        public static string ROW_NUMBER_FIELD = "Row__Number";
        public static string TOTAL_ROWS_FIELD = "Total__Query__Rows";
        public static string TOTAL_FIELD_PREFIX = "Total__";
        public bool UseOptionRecompile = false;


        public bool Parsed { get; private set; } = false;
        //---------------------------------------------------------------------------------------------
        public FwSqlSelect()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public void Parse()
        {
            List<string> lines;
            Clauses lastClause;
            Clauses currentClause;

            if (Parsed)
            {
                throw new Exception("Query was already parsed.");
            }
            lines         = new List<string>(OriginalSQL.ToString().Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
            lastClause    = Clauses.None;
            currentClause = Clauses.None;
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i] = lines[i];
                currentClause = GetCurrentClause(lines[i]);
                if (currentClause == Clauses.Unknown)
                {
                    currentClause = lastClause;
                }
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    SetClause(lastClause, currentClause, lines[i]);
                }
                lastClause = currentClause;
            }
            Parsed = true;
        }
        //---------------------------------------------------------------------------------------------
        private Clauses GetCurrentClause(string line)
        {
            Clauses currentClause;

            currentClause = Clauses.Unknown;
            if (line.StartsWith("union", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.Union;
            }
            else if (line.StartsWith("select", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.Select;
            }
            else if (line.StartsWith("from", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.From;
            }
            else if (line.StartsWith("where", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.Where;
            }
            else if (line.StartsWith("group by", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.GroupBy;
            }
            else if (line.StartsWith("having", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.Having;
            }
            else if (line.StartsWith("order by", StringComparison.OrdinalIgnoreCase))
            {
                currentClause = Clauses.OrderBy;
            }
            return currentClause;
        }
        //---------------------------------------------------------------------------------------------
        private void SetClause(Clauses lastClause, Clauses currentClause, string line)
        {
            FwSqlSelectStatement selectStatement;

            selectStatement = null;
            if (this.SelectStatements.Count > 0)
            {
                selectStatement = this.SelectStatements[this.SelectStatements.Count - 1];
            }
            switch (currentClause)
            {
                case Clauses.Union:
                    selectStatement = new FwSqlSelectStatement();
                    SelectStatements.Add(selectStatement);
                    selectStatement.Union = line;
                    break;
                case Clauses.Select:                    
                    switch(lastClause)
                    {
                        case Clauses.Union:
                        case Clauses.Select:
                            break;
                        default:
                            selectStatement = new FwSqlSelectStatement();
                            SelectStatements.Add(selectStatement);
                            break;
                    }                    
                    selectStatement.Select.Add(line);
                    break;
                case Clauses.From:
                    selectStatement.From.Add(line);
                    break;
                case Clauses.Where:
                    selectStatement.Where.Add(line);
                    break;
                case Clauses.GroupBy:
                    selectStatement.GroupBy.Add(line);
                    break;
                case Clauses.Having:
                    selectStatement.Having.Add(line);
                    break;
                case Clauses.OrderBy:
                    OrderBy.Add(line);
                    break;               
            }
        }
        //---------------------------------------------------------------------------------------------
        public void AddCte(string line)
        {
            this.Cte.AppendLine(line);
        }
        //---------------------------------------------------------------------------------------------
        public void Add(string line)
        {
            OriginalSQL.AppendLine(line);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhere(string line)
        {
            this.AddWhere("and", line);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhere(string conjunction, string line)
        {
            if (!Parsed)
            {
                throw new Exception("Need to parse select statement before calling AddWhere.");
            }
            foreach (FwSqlSelectStatement statement in SelectStatements)
            {
                if (statement.Where.Count == 0)
                {
                    statement.Where.Add("where " + line);
                }
                else
                {
                    statement.Where.Add(conjunction + " " + line);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void AddOrderBy(string line)
        {
            if (OrderBy.Count == 0)
            {
                OrderBy.Add("order by " + line);
            }
            else
            {
                OrderBy.Add(", " + line);
            }
        }
        //---------------------------------------------------------------------------------------------
        //public void AddTag(string key, string value)
        //{
        //    tags.Add(key, value);
        //}
        //---------------------------------------------------------------------------------------------
        public void AddParameter(string paramName, SqlDbType paramType, ParameterDirection paramDirection, int size, Object paramValue)
        {
            SqlParameter param;

            param = new SqlParameter(paramName, paramType, size);
            param.Direction = paramDirection;
            param.Value = paramValue;
            Parameters[paramName] = param;
        }
        //---------------------------------------------------------------------------------------------
        public void AddParameter(string paramName, Object paramValue)
        {
            //SqlParameter param = new SqlParameter();
            SqlParameter param = new SqlParameter(paramName, SqlDbType.VarChar);  //justin 04/17/2018 using VarChar here instead of NVarChar to avoid implicit conversion in the query optimizer
            param.ParameterName = paramName;

            if (paramValue is bool)
            {
                paramValue = (bool)paramValue ? "T" : "F";
            }
            if (paramValue is JValue)
            {
                JValue val = ((JValue)paramValue);
                switch (val.Type)
                {
                    case JTokenType.Null:
                        paramValue = null;
                        break;
                    case JTokenType.Undefined:
                        paramValue = null;
                        break;
                    case JTokenType.Date:
                        paramValue = val.Value<DateTime>();
                        break;
                    case JTokenType.Raw:
                        paramValue = null;
                        break;
                    case JTokenType.Bytes:
                        paramValue = val.Value<byte[]>();
                        break;
                    case JTokenType.Guid:
                        paramValue = val.Value<Guid>();
                        break;
                    case JTokenType.Uri:
                        paramValue = val.Value<string>();
                        break;
                    case JTokenType.TimeSpan:
                        paramValue = val.Value<string>();
                        break;
                    case JTokenType.Object:
                        paramValue = val.Value<object>();
                        break;
                    case JTokenType.Array:
                        paramValue = val.Value<DateTime>();
                        break;
                    case JTokenType.Constructor:
                        paramValue = null;
                        break;
                    case JTokenType.Property:
                        paramValue = null;
                        break;
                    case JTokenType.Comment:
                        paramValue = null;
                        break;
                    case JTokenType.Integer:
                        paramValue = val.Value<Int32>();
                        break;
                    case JTokenType.Float:
                        paramValue = val.Value<Decimal>();
                        break;
                    case JTokenType.String:
                        paramValue = val.Value<String>();
                        break;
                    case JTokenType.Boolean:
                        paramValue = val.Value<Boolean>();
                        break;
                }
            }

            param.Value = paramValue;

            if (!(paramValue is string))  //justin 04/17/2018  have the driver set the actual data type here when not a string data type (ie. integers, decimals, etc)
            {
                param.ResetSqlDbType();
            }
            Parameters[paramName] = param;
        }
        //---------------------------------------------------------------------------------------------
        public void AddParameter(string paramName, SqlDbType paramType, ParameterDirection paramDirection, int size)
        {
            SqlParameter param;

            param                 = new SqlParameter(paramName, paramType, size);
            param.Direction       = paramDirection;
            Parameters[paramName] = param;
        }
        //---------------------------------------------------------------------------------------------
        public void SetQuery(FwSqlCommand cmd)
        {
            string query;
            StringBuilder sb;
            int rowNoStart, rowNoEnd;

            if (!Parsed)
            {
                this.Parse();
            }
            sb = new StringBuilder();
            if (EnablePaging && PagingCompatibility == PagingCompatibilities.PreSql2012)
            {
                rowNoStart = ((PageNo - 1) * PageSize) + 1;
                rowNoEnd = ((PageNo - 1) * PageSize) + PageSize;
                AddParameter("@fwrownostart", rowNoStart);
                AddParameter("@fwrownoend", rowNoEnd);
                sb.AppendLine(";with");
                if (Cte.Length > 0)
                {
                    sb.Append(Cte.ToString() + ",");
                }
                sb.AppendLine("main_cte as(");
                cmd.PageNo = PageNo;
                cmd.PageSize = PageSize;
            }
            else if (EnablePaging && PagingCompatibility == PagingCompatibilities.Sql2012)
            {
                AddParameter("@fwpageno", PageNo);
                AddParameter("@fwpagesize", PageSize);
                sb.AppendLine(";with");
                if (Cte.Length > 0)
                {
                    sb.Append(Cte.ToString() + ",");
                }
                sb.AppendLine("main_cte as(");
                cmd.PageNo = PageNo;
                cmd.PageSize = PageSize;
            }
            if (!EnablePaging && Cte.Length > 0)
            {
                sb.AppendLine(";with");
                sb.Append(Cte.ToString());
            }
            foreach (FwSqlSelectStatement selectStatement in SelectStatements)
            {
                if (selectStatement.Union != string.Empty)
                {
                    sb.AppendLine(selectStatement.Union);
                }
                for (int i = 0; i <  selectStatement.Select.Count; i++)
                {
                    string line = selectStatement.Select[i];
                    if ((i == 0) && (!EnablePaging) && (Top > 0))
                    {
                        line = line.Insert("select ".Length, "top " + Top.ToString() + " ");
                    }
                    sb.AppendLine(line);
                }
                foreach (string line in selectStatement.From)
                {
                    sb.AppendLine(line);
                }
                foreach (string line in selectStatement.Where)
                {
                    sb.AppendLine(line);
                }
                foreach (string line in selectStatement.GroupBy)
                {
                    sb.AppendLine(line);
                }
                foreach (string line in selectStatement.Having)
                {
                    sb.AppendLine(line);
                }
            }
            if (EnablePaging && PagingCompatibility == PagingCompatibilities.PreSql2012)
            {
                sb.AppendLine(")");
                sb.AppendLine(", count_cte as (");
                sb.AppendLine("    select count(*) as [" + TOTAL_ROWS_FIELD + "]");
                foreach (string totalField in TotalFields)
                {
                    sb.AppendLine("             , sum([" + totalField + "]) as [" + TOTAL_FIELD_PREFIX + totalField + "]");
                }
                sb.AppendLine("    from main_cte with (nolock)");
                sb.AppendLine(")");
                sb.AppendLine(", paging_cte as (");
                sb.Append("    select top(@fwrownoend) row_number() over (");
                if (OrderBy.Count == 0)
                {
                    throw new Exception("A sort expression is required for paged queries.");
                }
                foreach (string line in OrderBy)
                {
                    sb.Append(line);
                }
                sb.Append(") as ");
                sb.Append(ROW_NUMBER_FIELD); 
                sb.AppendLine(" , *"); 
                sb.AppendLine("    from main_cte with (nolock), count_cte with (nolock)");
                sb.AppendLine(")");
                sb.AppendLine("select *");
                sb.AppendLine("from paging_cte with (nolock)");
                sb.Append("where ");
                sb.Append(ROW_NUMBER_FIELD);
                sb.AppendLine(" between @fwrownostart and @fwrownoend");
            }
            if (!EnablePaging)
            {
                foreach (string line in OrderBy)
                {
                    sb.Append(line);
                }
            }
            if (EnablePaging && PagingCompatibility == PagingCompatibilities.Sql2012)
            {
                sb.AppendLine(")");
                sb.AppendLine(", count_cte as (");
                sb.AppendLine("  select count(*) as [" + TOTAL_ROWS_FIELD + "]");
                foreach (string totalField in TotalFields)
                {
                    sb.AppendLine("             , sum([" + totalField + "]) as [" + TOTAL_FIELD_PREFIX + totalField + "]");
                }
                sb.AppendLine("  from main_cte with (nolock)");
                sb.AppendLine(")");
                sb.AppendLine("select *");
                sb.AppendLine("from main_cte with (nolock), count_cte with (nolock)");
                if (OrderBy.Count == 0)
                {
                    throw new Exception("A sort expression is required for paged queries.");
                }
                foreach (string line in OrderBy)
                {
                    sb.Append(line);
                }
                sb.AppendLine();
                sb.AppendLine("offset (@fwpageno - 1) * @fwpagesize rows");
                sb.AppendLine("fetch next @fwpagesize rows only");
            }

            if (UseOptionRecompile)
            {
                sb.AppendLine();
                sb.AppendLine("option (recompile)"); // to achieve constant folding and eliminate parameter sniffing.  "on" by default on reports
            }


            query = sb.ToString();
            //cmd.Clear();
            //cmd.Parameters.Clear();
            if (!query.Equals(string.Empty))
            {
                cmd.Clear();
                cmd.Add(query);
            }
            foreach (KeyValuePair<string, SqlParameter> item in Parameters)
            {
                cmd.Parameters.Add(item.Value);
            }
        }
        //---------------------------------------------------------------------------------------------
        public string GetInClause(List<string> items, string prefix)
        {
            StringBuilder sb;
            string paramName;

            sb = new StringBuilder();
            paramName = string.Empty;
            sb.Append("(");
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                paramName = "@fw" + prefix + i;
                sb.Append(paramName);
                AddParameter(paramName, items[i]);
            }
            sb.Append(")");

            return sb.ToString();
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string column, SelectedCheckBoxListItems items)
        {
            AddWhereIn("and", column, "", items.ToString(), true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string column, string parameterNamePrefix, SelectedCheckBoxListItems items)
        {
            AddWhereIn("and", column, parameterNamePrefix, items.ToString(), true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string column, string parameterList)
        {
            AddWhereIn("and", column, "", parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereNotIn(string column, string parameterList)
        {
            AddWhereNotIn("and", column, "", parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereNotIn(string column, string parameterPrefix, string parameterList)
        {
            AddWhereNotIn("and", column, parameterPrefix, parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterList)
        {
            AddWhereIn(conjunction, column, "", parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string column, string parameterList, bool selectAllIfEmpty)
        {
            AddWhereIn("and", column, "", parameterList, selectAllIfEmpty);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterList, bool selectAllIfEmpty)
        {
            AddWhereIn(conjunction, column, "", parameterList, selectAllIfEmpty);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterNamePrefix, string parameterList, bool selectAllIfEmpty)
        {
            string[] fields;
            StringBuilder sb;
            string result, parameterName, parameterValue;

            sb = new StringBuilder();
            if (((selectAllIfEmpty) && !string.IsNullOrWhiteSpace(parameterList)) || (!selectAllIfEmpty))
            {
                fields = parameterList.Split(new char[]{','}, StringSplitOptions.None);
                sb.Append(column);
                sb.Append(" in (");
                for (int i = 0; i < fields.Length; i++)
                {
                    parameterName  = "@" + parameterNamePrefix + column.Replace('.', '_') + i.ToString();
                    //parameterValue = fields[i];
                    parameterValue = fields[i].Trim();
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(parameterName);
                    this.AddParameter(parameterName, parameterValue);
                }
                sb.Append(")");
                result = sb.ToString();
                this.AddWhere(conjunction, result);
            }
        }
        //---------------------------------------------------------------------------------------------

        public void AddWhereNotIn(string conjunction, string column, string parameterNamePrefix, string parameterList, bool selectAllIfEmpty)
        {
            string[] fields;
            StringBuilder sb;
            string result, parameterName, parameterValue;

            sb = new StringBuilder();
            if (((selectAllIfEmpty) && !string.IsNullOrWhiteSpace(parameterList)) || (!selectAllIfEmpty))
            {
                fields = parameterList.Split(new char[] { ',' }, StringSplitOptions.None);
                sb.Append(column);
                sb.Append(" not in (");
                for (int i = 0; i < fields.Length; i++)
                {
                    parameterName = "@" + parameterNamePrefix + column.Replace('.', '_') + i.ToString();
                    //parameterValue = fields[i];
                    parameterValue = fields[i].Trim();
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(parameterName);
                    this.AddParameter(parameterName, parameterValue);
                }
                sb.Append(")");
                result = sb.ToString();
                this.AddWhere(conjunction, result);
            }
        }
        //---------------------------------------------------------------------------------------------



        //01/09/2020 justin hoffman - commenting the following two methods to avoid confusion with the above method. Are the two below used anywhere anymore?

        //public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after)
        //{
        //    AddWhereIn(conjunction, paramternameprefix, before, parameterList, after, true);
        //}
        //---------------------------------------------------------------------------------------------
        //public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after, bool selectAllIfEmpty)
        //{
        //    string[] fields;
        //    StringBuilder sb;
        //    string result, parameterName, parameterValue;

        //    sb = new StringBuilder();
        //    if ((selectAllIfEmpty && !string.IsNullOrWhiteSpace(parameterList)) || (!selectAllIfEmpty))
        //    {
        //        fields = parameterList.Split(new char[]{','}, StringSplitOptions.None);
        //        sb.Append(before);
        //        sb.Append(" in (");
        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            parameterName  = "@" + paramternameprefix + i.ToString();
        //            parameterValue = fields[i];
        //            if (i > 0)
        //            {
        //                sb.Append(",");
        //            }
        //            sb.Append(parameterName);
        //            this.AddParameter(parameterName, parameterValue);
        //        }
        //        sb.Append(")");
        //        sb.Append(after);
        //        result = sb.ToString();
        //        this.AddWhere(conjunction, result);
        //    }
        //}






        //---------------------------------------------------------------------------------------------
        public void AddWhereInFromCheckboxList(string conjunction, string column, dynamic parameterList)
        {
            StringBuilder sb;
            string paramString;

            sb = new StringBuilder();
            foreach(dynamic parameter in parameterList)
            {
                sb.Append(parameter.value);
            }
            paramString = sb.ToString();
            AddWhereIn(conjunction, column, paramString, true);
        }
        //---------------------------------------------------------------------------------------------
        //public void AddWhereInFromCheckboxList(string conjunction, string column, dynamic parameterList, List<FwReportStatusItem> allowedList, bool decrypt)
        //{
        //    StringBuilder sb;
        //    string paramString;

        //    sb = new StringBuilder();
        //    foreach(dynamic parameter in parameterList)
        //    {
        //        for (int i = 0; i < allowedList.Count; i++)
        //        {
        //            if (parameter.value == allowedList[i].value)
        //            {
        //                if (sb.Length > 0)
        //                {
        //                    sb.Append(",");
        //                }
        //                sb.Append(parameter.value);
        //                break;
        //            }
        //        }
        //    }
        //    paramString = sb.ToString();
        //    AddWhereIn(conjunction, column, paramString, decrypt, true);
        //}
        //---------------------------------------------------------------------------------------------
        //public void AddOrderByFromCheckboxList(dynamic parameterList, List<FwReportOrderByItem> allowedList)
        //{
        //    foreach(dynamic parameter in parameterList)
        //    {
        //        for (int i = 0; i < allowedList.Count; i++)
        //        {
        //            if (parameter.value == allowedList[i].value)
        //            {
        //                this.AddOrderBy(parameter.value + " " + parameter.orderbydirection);
        //                break;
        //            }
        //        }
        //    }
        //}
        //---------------------------------------------------------------------------------------------
        //protected virtual void SetGetManyQuery<T>(GetRequest request, FwCustomFields customFields = null, Type type = null)
        //{
        //    if (request == null) throw new ArgumentException("Argument 'request' cannot be null.");
        //    if (type == null)
        //    {
        //        type = typeof(T);
        //    }
        //    request.Parse();
        //    this.EnablePaging = request.PageSize > 0;
        //    this.PageNo = request.PageNo;
        //    this.PageSize = request.PageSize;
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("select");

        //    // Get all the Properties of the type
        //    PropertyInfo[] recordProperties = type.GetProperties();
        //    int colNo = 0;
        //    Dictionary<string, string> columns = new Dictionary<string, string>();
        //    string fullFieldName = "";

        //    // compute the maxFieldNameLength for formatting the SQL statement
        //    int maxFieldNameLength = 0;
        //    foreach (PropertyInfo property in recordProperties)
        //    {
        //        if (type.IsSubclassOf(typeof(FwBusinessLogic)) || type.IsSubclassOf(typeof(FwDataRecord)))
        //        {
        //            if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
        //            {
        //                FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
        //                string sqlColumnName = property.Name;
        //                columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
        //                if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
        //                {
        //                    sqlColumnName = sqlDataFieldAttribute.ColumnName;
        //                }
        //                fullFieldName = "[" + sqlColumnName + "]";
        //                maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
        //            }
        //        }
                
        //    }


        //    colNo = 0;
        //    foreach (PropertyInfo property in recordProperties)
        //    {
        //        if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
        //        {
        //            FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
        //            string sqlColumnName = property.Name;
        //            if (!string.IsNullOrEmpty(sqlDataFieldAttribute.CalculatedColumnSql))
        //            {
        //                columns[sqlColumnName] = sqlDataFieldAttribute.CalculatedColumnSql;
        //                fullFieldName = sqlDataFieldAttribute.CalculatedColumnSql;
        //            }
        //            else
        //            {
        //                columns[sqlColumnName] = sqlDataFieldAttribute.ColumnName;
        //                fullFieldName = "[" + sqlDataFieldAttribute.ColumnName + "]";
        //            }
        //            string prefix = "";
        //            if (colNo > 0)
        //            {
        //                prefix = ",\n      ";
        //            }
        //            qry.AddColumn(property.Name, property.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
        //            sb.Append(prefix + " " + fullFieldName.PadRight(maxFieldNameLength, ' ') + " as [" + property.Name + "]");

        //            colNo++;
        //        }
        //    }

        //    List<FwCustomTable> customTables = new List<FwCustomTable>();
        //    if ((customFields != null) && (customFields.Count > 0))
        //    {
        //        int customTableIndex = 1;
        //        int customFieldIndex = 1;
        //        sb.Append(",\n");
        //        sb.Append("       --//---------- begin custom fields ------------------\n");

        //        maxFieldNameLength = 0;
        //        foreach (FwCustomField customField in customFields)
        //        {
        //            fullFieldName = "[" + customField.FieldName + "]";
        //            maxFieldNameLength = (maxFieldNameLength > fullFieldName.Length ? maxFieldNameLength : fullFieldName.Length);
        //        }

        //        colNo = 0;
        //        foreach (FwCustomField customField in customFields)
        //        {
        //            //columns[customField.FieldName] = customField.FieldName;
        //            bool customTableInQuery = false;
        //            string customTableAlias = "";
        //            foreach (FwCustomTable customTable in customTables)
        //            {
        //                if (customTable.TableName.Equals(customField.CustomTableName))
        //                {
        //                    customTableInQuery = true;
        //                    customTableAlias = customTable.Alias;
        //                    break;
        //                }
        //            }
        //            if (!customTableInQuery)
        //            {
        //                customTableAlias = "customtable" + customTableIndex.ToString().PadLeft(2, '0');
        //                customTables.Add(new FwCustomTable(customField.CustomTableName, customTableAlias));
        //                customTableIndex++;
        //            }
        //            string customSqlFieldName = "[" + customTableAlias + "].[" + customField.CustomFieldName + "]";
        //            columns[customField.FieldName] = customSqlFieldName;

        //            qry.AddColumn(customField.FieldName, customField.FieldName, FwDataTypes.Text, true, false, false);
        //            if (colNo > 0)
        //            {
        //                sb.Append(",\n");
        //            }
        //            sb.Append("       ");
        //            fullFieldName = "[" + customField.FieldName + "]";
        //            sb.Append(fullFieldName.PadRight(maxFieldNameLength, ' ') + " = " + customSqlFieldName);

        //            customFieldIndex++;
        //            colNo++;
        //        }
        //        sb.Append("\n       --//---------- end custom fields --------------------");
        //    }
        //    this.Add(sb.ToString());

        //    string withNoLock = string.Empty;
        //    if (useWithNoLock)
        //    {
        //        withNoLock = " with (nolock)";
        //    }
        //    this.Add("from " + TableName + " [" + TableAlias + "]" + withNoLock);

        //    if ((customFields != null) && (customFields.Count > 0))
        //    {
        //        List<PropertyInfo> primaryKeyProperties = GetPrimaryKeyProperties();

        //        this.Add("           --//---------- begin custom tables ------------------");
        //        foreach (FwCustomTable customTable in customTables)
        //        {
        //            this.Add("           left outer join [" + customTable.TableName + "] [" + customTable.Alias + "] with (nolock) on ");
        //            this.Add("                       ( ");

        //            int k = 1;
        //            foreach (PropertyInfo primaryKeyProperty in primaryKeyProperties)
        //            {
        //                FwSqlDataFieldAttribute sqlDataFieldAttribute = primaryKeyProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
        //                string sqlColumnName = primaryKeyProperty.Name;
        //                if (!string.IsNullOrEmpty(sqlDataFieldAttribute.ColumnName))
        //                {
        //                    sqlColumnName = sqlDataFieldAttribute.ColumnName;
        //                }
        //                string customUniqueIdField = "uniqueid" + k.ToString().PadLeft(2, '0');
        //                this.Add("                       [" + TableAlias + "].[" + sqlColumnName + "] = [" + customTable.Alias + "].[" + customUniqueIdField + "]");
        //                if (k < primaryKeyProperties.Count)
        //                {
        //                    this.Add(" and ");
        //                }
        //                k++;
        //            }
        //            this.Add("                       ) ");
        //        }
        //        this.Add("           --//---------- end custom tables --------------------");
        //    }

        //    if (request.filters.Count > 0)
        //    {
        //        var needsWhere = true;
        //        var conjunction = string.Empty;
        //        foreach (var filter in request.filters)
        //        {
        //            var propInfo = recordProperties.Where<PropertyInfo>(p => p.Name == filter.Key).FirstOrDefault<PropertyInfo>();
        //            if (filter.Value.ValidateFilter && propInfo == null)
        //            {
        //                throw new ArgumentException($"Invalid filter: '{filter.Key}'.");
        //            }
        //            else
        //            {
        //                var sqlFieldName = filter.Value.FieldName;
        //                var sqlDataFieldAttribute = propInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
        //                sqlFieldName = sqlDataFieldAttribute.ColumnName;
        //                var parameterName = "@filterfield_" + sqlFieldName + ShortId.Generate(true, false, 7);
        //                var fieldSqlValue = filter.Value.FieldValue;
        //                switch (sqlDataFieldAttribute.ModelType)
        //                {
        //                    case FwDataTypes.Boolean:
        //                        switch (filter.Value.FieldValue.ToLower())
        //                        {
        //                            case "true":
        //                                fieldSqlValue = "T";
        //                                break;
        //                            case "false":
        //                                fieldSqlValue = "F";
        //                                break;
        //                            default:
        //                                throw new ArgumentException($"An invalid filter value: '{filter.Value.FieldValue}' was supplied for the field: {filter.Value.FieldName}");
        //                        }
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                switch (filter.Value.ComparisonOperator)
        //                {
        //                    case "eq":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} = {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                    case "ne":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} <> {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                    case "in":
        //                    case "ni":
        //                        {
        //                            string filterValue = fieldSqlValue;
        //                            bool inAQuote = false;
        //                            bool inAFilterValue = false;
        //                            List<string> filterValues = new List<string>();
        //                            StringBuilder currentFilter = new StringBuilder();
        //                            for (int i = 0; i < filterValue.Length; i++)
        //                            {
        //                                bool isAtTheEndOfTheFilter = (i >= filterValue.Length - 1);
        //                                char c = filterValue[i];
        //                                if (c == ',' && !inAQuote && inAFilterValue)
        //                                {
        //                                    filterValues.Add(currentFilter.ToString());
        //                                    currentFilter = new StringBuilder();
        //                                    inAFilterValue = false;
        //                                }
        //                                else if (c == ',' && !inAQuote && !inAFilterValue)
        //                                {

        //                                }
        //                                else
        //                                {
        //                                    if (c == '\"')
        //                                    {
        //                                        if (inAFilterValue)
        //                                        {
        //                                            if (inAQuote)
        //                                            {
        //                                                if (!isAtTheEndOfTheFilter)
        //                                                {
        //                                                    //if (filterValue[i + 1] == '\"')
        //                                                    //{
        //                                                    //    currentFilter.Append(c);
        //                                                    //}
        //                                                    if (filterValue[i + 1] == ',')
        //                                                    {
        //                                                        filterValues.Add(currentFilter.ToString());
        //                                                        inAQuote = false;
        //                                                        inAFilterValue = false;
        //                                                        currentFilter = new StringBuilder();
        //                                                        i++;
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        inAQuote = false;
        //                                                        inAFilterValue = false;
        //                                                    }
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i + 1}) must be escaped with a double quote.");
        //                                                //if (isAtTheEndOfTheFilter)
        //                                                //{
        //                                                //    throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i+1}) must be escaped with a double quote.");
        //                                                //}
        //                                                //else
        //                                                //{
        //                                                //    if (filterValue[i + 1] == '\"')
        //                                                //    {
        //                                                //        currentFilter.Append(c);
        //                                                //        i++;
        //                                                //    }
        //                                                //    else
        //                                                //    {
        //                                                //        throw new ArgumentException($"In filter expression for: {propInfo.Name}, double quote (character: {i+1}) must be escaped with a double quote.");
        //                                                //    }
        //                                                //}
        //                                            }
        //                                        }
        //                                        else if (!inAFilterValue && !inAQuote && filterValue[i + 1] == '\"')
        //                                        {
        //                                            currentFilter.Append(c);
        //                                            i++;
        //                                            inAFilterValue = true;
        //                                        }
        //                                        else
        //                                        {
        //                                            inAQuote = true;
        //                                            inAFilterValue = true;
        //                                        }
        //                                    }
        //                                    else if (c == '\\')
        //                                    {
        //                                        if (!isAtTheEndOfTheFilter)
        //                                        {
        //                                            if (filterValue[i + 1] == '\"')
        //                                            {
        //                                                currentFilter.Append(c);
        //                                                currentFilter.Append(filterValue[i + 1]);
        //                                                i += 2;
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        inAFilterValue = true;
        //                                        currentFilter.Append(c);
        //                                    }
        //                                    if (isAtTheEndOfTheFilter)
        //                                    {
        //                                        inAQuote = false;
        //                                        inAFilterValue = false;
        //                                        filterValues.Add(currentFilter.ToString());
        //                                        currentFilter = new StringBuilder();
        //                                        break;
        //                                    }
        //                                }
        //                            }
        //                            if (filterValues.Count > 0)
        //                            {
        //                                if (needsWhere)
        //                                {
        //                                    this.Add("where");
        //                                    needsWhere = false;
        //                                }
        //                                StringBuilder filterExpression = new StringBuilder();
        //                                filterExpression.Append($"  {conjunction}{sqlFieldName} ");
        //                                if (filter.Value.ComparisonOperator == "ni")
        //                                {
        //                                    filterExpression.Append("not ");
        //                                }
        //                                filterExpression.Append("in (");
        //                                bool firstParameter = true;
        //                                foreach (var parameterValue in filterValues)
        //                                {
        //                                    parameterName = '@' + sqlFieldName + ShortId.Generate(true, false, 7);
        //                                    if (!firstParameter)
        //                                    {
        //                                        filterExpression.Append(", ");
        //                                    }
        //                                    filterExpression.Append(parameterName);
        //                                    this.AddParameter(parameterName, parameterValue);
        //                                    firstParameter = false;
        //                                }
        //                                filterExpression.Append(")");
        //                                this.Add(filterExpression.ToString());
        //                            }
        //                            break;
        //                        }
        //                    case "sw": //starts with
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
        //                        this.AddParameter(parameterName, $"%{fieldSqlValue}");
        //                        break;
        //                    case "ew": // ends with
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
        //                        this.AddParameter(parameterName, $"%{fieldSqlValue}");
        //                        break;
        //                    case "co":  // contains
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} like {parameterName}");
        //                        this.AddParameter(parameterName, $"%{fieldSqlValue}%");
        //                        break;
        //                    case "dnc":  // does not contain
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} not like {parameterName}");
        //                        this.AddParameter(parameterName, $"%{fieldSqlValue}%");
        //                        break;
        //                    case "gt":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} > {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                    case "gte":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} >= {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                    case "lt":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} < {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                    case "lte":
        //                        if (needsWhere)
        //                        {
        //                            this.Add("where");
        //                            needsWhere = false;
        //                        }
        //                        this.Add($"  {conjunction}{sqlFieldName} <= {parameterName}");
        //                        this.AddParameter(parameterName, fieldSqlValue);
        //                        break;
        //                }
        //                if (conjunction == string.Empty)
        //                {
        //                    conjunction = " and ";
        //                }
        //            }
        //        }
        //    }

        //    if (request.Sort.Length > 0)
        //    {
        //        var sqlSortExpression = string.Empty;
        //        var sortArray = request.Sort.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (var sortExpression in sortArray)
        //        {
        //            var sortExpressionParts = sortExpression.Split(new char[] { ':' });

        //            var sortField = "";
        //            var sortDirection = "";
        //            if (sortExpressionParts.Length > 0)
        //            {
        //                sortField = sortExpressionParts[0];
        //            }
        //            if (sortExpressionParts.Length > 1)
        //            {
        //                // validate the user submitted data to protect against SQL injection attacks
        //                if (sortExpressionParts[1] == "asc" || sortExpressionParts[1] == "desc")
        //                {
        //                    sortDirection = " " + sortExpressionParts[1];
        //                }
        //                else if (sortExpressionParts[1] == "")
        //                {
        //                    // do nothing
        //                }
        //                else
        //                {
        //                    throw new ArgumentException($"Invalid sort direction: '{sortExpressionParts[1]}'");
        //                }
        //            }

        //            // validates and translate the Field Name
        //            var recordPropInfo = recordProperties.Where<PropertyInfo>(p => p.Name == sortField).FirstOrDefault<PropertyInfo>();
        //            var requestPropInfo = request.GetType().GetProperties()
        //                .Where<PropertyInfo>(
        //                    p => p.Name == sortField &&
        //                    p.GetCustomAttribute<GetRequestPropertyAttribute>() != null &&
        //                    p.GetCustomAttribute<GetRequestPropertyAttribute>().EnableSorting == true)
        //                .FirstOrDefault<PropertyInfo>();
        //            if (recordPropInfo == null && requestPropInfo == null)
        //            {
        //                throw new ArgumentException($"Invalid column name: '{sortField}' in sort expression.", sortField);
        //            }
        //            else
        //            {
        //                var sqlDataFieldAttribute = recordPropInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
        //                if (sqlSortExpression.Length > 0)
        //                {
        //                    sqlSortExpression += ", ";
        //                }
        //                if (request.PageSize == 0)
        //                {
        //                    sqlSortExpression += sqlDataFieldAttribute.ColumnName + sortDirection;
        //                }
        //                else
        //                {
        //                    sqlSortExpression += sortField + sortDirection;
        //                }
        //            }
        //        }
        //        this.Add("order by " + sqlSortExpression);
        //    }
        //}
        //---------------------------------------------------------------------------------------------
    }
}
