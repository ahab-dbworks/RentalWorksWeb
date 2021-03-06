﻿using Fw.Json.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Fw.Json.Utilities;
using System.Dynamic;
using Fw.Json.Services;

namespace Fw.Json.SqlServer
{
    public class FwSqlSelect
    {
        //---------------------------------------------------------------------------------------------
        private enum Clauses { None, Unknown, Union, Select, From, Where, GroupBy, Having, OrderBy }

        public StringBuilder OriginalSQL {get;set;}
        public List<FwSqlSelectStatement> SelectStatements {get;set;}
        public List<string> OrderBy {get;set;}
        public Dictionary<string, SqlParameter> Parameters {get;set;}
        public int Top {get;set;}
        public bool EnablePaging {get;set;}
        public int PageNo {get;set;}
        public int PageSize {get;set;}
        public FwDatabases DatabaseConnection {get;set;}
        public FwSqlConnection SqlConnection;
        public FwSqlCommand SqlCommand;

        private bool parsed;
        private Dictionary<string,string> tags;
        //---------------------------------------------------------------------------------------------
        public FwSqlSelect()
        {
            Clear();
        }
        //---------------------------------------------------------------------------------------------
        public void Clear()
        {
            this.OriginalSQL      = new StringBuilder();
            this.SelectStatements = new List<FwSqlSelectStatement>();
            this.OrderBy          = new List<string>();
            this.Parameters       = new Dictionary<string, SqlParameter>();
            this.Top              = 0;
            this.EnablePaging     = false;
            this.PageNo           = 0;
            this.PageSize         = 10;
            this.parsed           = false;
            this.tags             = new Dictionary<string,string>();
        }
        //---------------------------------------------------------------------------------------------
        public void Parse()
        {
            List<string> lines;
            Clauses lastClause;
            Clauses currentClause;

            if (parsed)
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
            parsed = true;
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
            if (!parsed)
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
        public void AddTag(string key, string value)
        {
            tags.Add(key, value);
        }
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
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
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

            if (!parsed)
            {
                this.Parse();
            }
            sb = new StringBuilder();
            AddParameter("@fwtop", Top);
            if (EnablePaging)
            {
                rowNoStart = ((PageNo - 1) * PageSize) + 1;
                rowNoEnd = ((PageNo - 1) * PageSize) + PageSize;
                AddParameter("@fwrownostart", rowNoStart);
                AddParameter("@fwrownoend", rowNoEnd);
                sb.AppendLine(";with main_cte as(");
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
                        line = line.Insert("select ".Length, "top (@fwtop) ");
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
            if (EnablePaging)
            {
                sb.AppendLine(")");
                sb.AppendLine(", count_cte as (");
                sb.AppendLine("    select count(*) as [totalrows]");
                sb.AppendLine("    from main_cte with (nolock)");
                sb.AppendLine(")");
                sb.AppendLine(", paging_cte as (");
                sb.Append("    select top(@fwrownoend) row_number() over (");
                if (OrderBy.Count == 0)
                {
                    //throw new Exception("A sort expression is required for paged queries.");
                    OrderBy.Add("order by 1"); //justin 04/24/2018
                }
                foreach (string line in OrderBy)
                {
                    sb.Append(line);
                }
                sb.AppendLine(") as rowno, *");
                sb.AppendLine("    from main_cte with (nolock), count_cte with (nolock)");
                sb.AppendLine(")");
                sb.AppendLine("select *");
                sb.AppendLine("from paging_cte with (nolock)");
                sb.AppendLine("where rowno between @fwrownostart and @fwrownoend");
            }
            if (!EnablePaging)
            {
                foreach (string line in OrderBy)
                {
                    sb.Append(line);
                }
            }
            query = sb.ToString();
            query = Mustache.Render.StringToString(query, tags);
            cmd.Clear();
            cmd.Add(query);
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
        public void AddWhereIn(string conjunction, string column, string parameterList, bool decrypt)
        {
            AddWhereIn(conjunction, column, parameterList, decrypt, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string column, string parameterList, bool decrypt, bool selectAllIfEmpty)
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
                    parameterValue = decrypt ? FwCryptography.AjaxDecrypt(fields[i]) : fields[i];
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
        public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after, bool decrypt)
        {
            AddWhereIn(conjunction, paramternameprefix, before, parameterList, after, decrypt, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereIn(string conjunction, string paramternameprefix, string before, string parameterList, string after, bool decrypt, bool selectAllIfEmpty)
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
                    parameterValue = decrypt ? FwCryptography.AjaxDecrypt(fields[i]) : fields[i];
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
        public void AddWhereInFromCheckboxList(string conjunction, string column, dynamic parameterList, bool decrypt)
        {
            StringBuilder sb;
            string paramString;

            sb = new StringBuilder();
            foreach(dynamic parameter in parameterList)
            {
                sb.Append(parameter.value);
            }
            paramString = sb.ToString();
            AddWhereIn(conjunction, column, paramString, decrypt, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddWhereInFromCheckboxList(string conjunction, string column, dynamic parameterList, List<FwReportStatusItem> allowedList, bool decrypt)
        {
            StringBuilder sb;
            string paramString;

            sb = new StringBuilder();
            foreach(dynamic parameter in parameterList)
            {
                for (int i = 0; i < allowedList.Count; i++)
                {
                    if (parameter.value == allowedList[i].value)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(",");
                        }
                        sb.Append(parameter.value);
                        break;
                    }
                }
            }
            paramString = sb.ToString();
            AddWhereIn(conjunction, column, paramString, decrypt, true);
        }
        //---------------------------------------------------------------------------------------------
        public void AddOrderByFromCheckboxList(dynamic parameterList, List<FwReportOrderByItem> allowedList)
        {
            foreach(dynamic parameter in parameterList)
            {
                for (int i = 0; i < allowedList.Count; i++)
                {
                    if (parameter.value == allowedList[i].value)
                    {
                        this.AddOrderBy(parameter.value + " " + parameter.orderbydirection);
                        break;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
