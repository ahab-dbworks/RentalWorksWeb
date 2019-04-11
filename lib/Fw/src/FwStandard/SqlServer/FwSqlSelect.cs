using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FwStandard.SqlServer
{
    public class FwSqlSelect
    {
        //---------------------------------------------------------------------------------------------
        private enum Clauses { None, Unknown, Union, Select, From, Where, GroupBy, Having, OrderBy }

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
                sb.AppendLine(";with main_cte as(");
                cmd.PageNo = PageNo;
                cmd.PageSize = PageSize;
            }
            if (EnablePaging && PagingCompatibility == PagingCompatibilities.Sql2012)
            {
                AddParameter("@fwpageno", PageNo);
                AddParameter("@fwpagesize", PageSize);
                sb.AppendLine(";with main_cte as(");
                cmd.PageNo = PageNo;
                cmd.PageSize = PageSize;
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
        public void AddWhereIn(string column, string parameterList)
        {
            AddWhereIn("and", column, parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterList)
        {
            AddWhereIn(conjunction, column, parameterList, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string column, string parameterList, bool selectAllIfEmpty)
        {
            AddWhereIn("and", column, parameterList, selectAllIfEmpty);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterList, bool selectAllIfEmpty)
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
                    parameterName  = "@" + column.Replace('.', '_') + i.ToString();
                    parameterValue = fields[i];
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
        public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after)
        {
            AddWhereIn(conjunction, paramternameprefix, before, parameterList, after, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after, bool selectAllIfEmpty)
        {
            string[] fields;
            StringBuilder sb;
            string result, parameterName, parameterValue;

            sb = new StringBuilder();
            if ((selectAllIfEmpty && !string.IsNullOrWhiteSpace(parameterList)) || (!selectAllIfEmpty))
            {
                fields = parameterList.Split(new char[]{','}, StringSplitOptions.None);
                sb.Append(before);
                sb.Append(" in (");
                for (int i = 0; i < fields.Length; i++)
                {
                    parameterName  = "@" + paramternameprefix + i.ToString();
                    parameterValue = fields[i];
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(parameterName);
                    this.AddParameter(parameterName, parameterValue);
                }
                sb.Append(")");
                sb.Append(after);
                result = sb.ToString();
                this.AddWhere(conjunction, result);
            }
        }
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
    }
}
