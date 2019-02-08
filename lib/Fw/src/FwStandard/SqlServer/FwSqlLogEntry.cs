using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FwStandard.SqlServer
{
    public class FwSqlLogEntry
    {
        public static bool LogSql = true;
        public static bool LogSqlContext = true;
        public static int Counter = 0;

        public string SqlForHtml = string.Empty;
        public string Sql = string.Empty;
        public DateTime StartTime = DateTime.MinValue;
        public DateTime StopTime = DateTime.MinValue;
        private string StackTrace = string.Empty;

        public FwSqlLogEntry(string label, SqlCommand command, string stackTrace = "")
        {
            Counter++;
            StringBuilder sqlForHtml, sql;
            int maxParameterWidth = 0;
            List<string> outputParameterNames = new List<string>();

            this.StackTrace = stackTrace;
            sqlForHtml = new StringBuilder();
            sql = new StringBuilder();
            if (!string.IsNullOrEmpty(label))
            {
                sqlForHtml.Append("<div>");
                sqlForHtml.Append(label + ": ");
                sqlForHtml.Append("</div>");

                sql.Append(label + ":\n");
            }
            if (command.Parameters.Count > 0)
            {
                sqlForHtml.Append("declare<br/>  ");
                sql.AppendLine("declare");
                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    if (command.Parameters[i].ParameterName.Length > maxParameterWidth)
                    {
                        maxParameterWidth = command.Parameters[i].ParameterName.Length;
                    }
                }
                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    sql.Append("  ");
                    sqlForHtml.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));
                    sqlForHtml.Append(" ");
                    sql.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));
                    sql.Append(" ");
                    switch (command.Parameters[i].SqlDbType)
                    {
                        case SqlDbType.BigInt:
                            sqlForHtml.Append("bigint");
                            sql.Append("bigint");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Binary:
                            sqlForHtml.Append("binary(8000)");
                            sql.Append("binary(8000)");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Bit:
                            sqlForHtml.Append("bit");
                            sql.Append("bit");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Char:
                            sqlForHtml.Append("char(8000)");
                            sql.Append("char(8000)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Date:
                            sqlForHtml.Append("date");
                            sql.Append("date");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTime:
                            sqlForHtml.Append("datetime");
                            sql.Append("datetime");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTime2:
                            sqlForHtml.Append("datetime2");
                            sql.Append("datetime2");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.DateTimeOffset:
                            sqlForHtml.Append("datetimeoffset");
                            sql.Append("datetimeoffset");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Decimal:
                            sqlForHtml.Append("decimal(16,4)");
                            sql.Append("decimal(16,4)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Float:
                            sqlForHtml.Append("float");
                            sql.Append("float");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Image:
                            sqlForHtml.Append("image");
                            sql.Append("image");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Int:
                            sqlForHtml.Append("int");
                            sql.Append("int");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Money:
                            sqlForHtml.Append("money");
                            sql.Append("money");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.NChar:
                            sqlForHtml.Append("nchar(8000)");
                            sql.Append("nchar(8000)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.NText:
                            sqlForHtml.Append("ntext");
                            sql.Append("ntext");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.NVarChar:
                            sqlForHtml.Append("nvarchar(max)");
                            sql.Append("nvarchar(max)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Real:
                            sqlForHtml.Append("real");
                            sql.Append("real");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.SmallDateTime:
                            sqlForHtml.Append("smalldatetime");
                            sql.Append("smalldatetime");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.SmallInt:
                            sqlForHtml.Append("smallint");
                            sql.Append("smallint");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.SmallMoney:
                            sqlForHtml.Append("smallmoney");
                            sql.Append("smallmoney");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Structured:
                            sqlForHtml.Append("structured");
                            sql.Append("structured");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Time:
                            sqlForHtml.Append("time");
                            sql.Append("time");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Timestamp:
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.TinyInt:
                            sqlForHtml.Append("tinyint");
                            if ((command.Parameters[i].Value != null) && (!string.IsNullOrEmpty(command.Parameters[i].Value.ToString())))
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Udt:
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.UniqueIdentifier:
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.VarBinary:
                            sqlForHtml.Append("varbinary(max)");
                            sql.Append("varbinary(max)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Text:
                        case SqlDbType.VarChar:
                            sqlForHtml.Append("varchar(max)");
                            sql.Append("varchar(max)");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                        case SqlDbType.Variant:
                            sqlForHtml.Append("sql_variant");
                            sql.Append("sql_variant");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = " + command.Parameters[i].Value.ToString());
                                sql.Append(" = " + command.Parameters[i].Value.ToString());
                            }
                            break;
                        case SqlDbType.Xml:
                            sqlForHtml.Append("xml");
                            sql.Append("xml");
                            if (command.Parameters[i].Value != null)
                            {
                                sqlForHtml.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                                sql.Append(" = '" + command.Parameters[i].Value.ToString() + "'");
                            }
                            break;
                    }

                    if (i < command.Parameters.Count - 1)
                    {
                        sqlForHtml.Append(",");
                        sql.Append(",");
                    }
                    sqlForHtml.Append("<br/>");
                    sql.Append(Environment.NewLine);

                }
                sqlForHtml.Append("<br/><br/>");
                sql.Append(Environment.NewLine);
            }

            if (command.CommandType == CommandType.Text)
            {
                sqlForHtml.Append(command.CommandText.Replace(Environment.NewLine, "<br/>"));
                sql.Append(command.CommandText);
            }
            else if (command.CommandType == CommandType.StoredProcedure)
            {
                sql.AppendLine("exec " + command.CommandText);
                sqlForHtml.Append("exec " + command.CommandText + "<br/>  ");
                for (int i = 0; i < command.Parameters.Count; i++)
                {
                    sql.Append("  ");

                    sqlForHtml.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' ') + " = ");
                    sql.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' ') + " = ");
                    if ((command.Parameters[i].Direction == ParameterDirection.InputOutput) || (command.Parameters[i].Direction == ParameterDirection.Output))
                    {
                        sqlForHtml.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));
                        sql.Append(command.Parameters[i].ParameterName.PadRight(maxParameterWidth, ' '));

                        sqlForHtml.Append(" output");
                        sql.Append(" output");

                        outputParameterNames.Add(command.Parameters[i].ParameterName);
                    }
                    else
                    {
                        sqlForHtml.Append(command.Parameters[i].ParameterName);
                        sql.Append(command.Parameters[i].ParameterName);
                    }

                    if (i < command.Parameters.Count - 1)
                    {
                        sqlForHtml.Append(",");
                        sql.Append(",");
                    }
                    sqlForHtml.Append("<br/>");
                    sql.Append(Environment.NewLine);

                }
                if (outputParameterNames.Count > 0)
                {
                    sqlForHtml.Append("<br/><br/>select<br/>  ");
                    sql.Append(Environment.NewLine + Environment.NewLine + "select" + Environment.NewLine + " ");
                    for (int i = 0; i < outputParameterNames.Count; i++)
                    {
                        sql.Append("  ");

                        sqlForHtml.Append(outputParameterNames[i].PadRight(maxParameterWidth - 2, ' ').Replace("@", string.Empty) + " = " + outputParameterNames[i].PadRight(maxParameterWidth, ' '));
                        sql.Append(outputParameterNames[i].PadRight(maxParameterWidth - 2, ' ').Replace("@", string.Empty) + " = " + outputParameterNames[i].PadRight(maxParameterWidth, ' '));

                        if (i < outputParameterNames.Count - 1)
                        {
                            sqlForHtml.Append(",");
                            sql.Append(",");
                        }
                        sqlForHtml.Append("<br/>");
                        sql.Append(Environment.NewLine);

                    }
                }
            }
            SqlForHtml = sqlForHtml.ToString().Replace(" ", "&nbsp;");

            if (FwSqlLogEntry.LogSqlContext && !string.IsNullOrEmpty(this.StackTrace))
            {
                sql.Insert(0, this.StackTrace + Environment.NewLine);
            }
            sql.Insert(0, $"----{Counter.ToString()} - {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")}-----------------------------------------" + Environment.NewLine);
            
            WriteToConsole(sql.ToString());

        }

        public FwSqlLogEntry(SqlCommand command, string stackTrace = "") : this(string.Empty, command, stackTrace)
        {
            this.StackTrace = stackTrace;
        }

        public FwSqlLogEntry(string message)
        {
            SqlForHtml = message;
        }

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            StopTime = DateTime.Now;
            WriteToConsole("completed", true);
        }
        public void WriteToConsole(string str, bool includeDuration = false)
        {
            if (FwSqlLogEntry.LogSql)
            {
                if (((str.Contains("controlclient")) && (str.Contains("options"))) || ((str.Contains("dbo.decrypt")) || (str.Contains("dbo.encrypt"))))
                {
                    str = "(encrypted sql)";
                }
                if (includeDuration)
                {
                    str = "----" + Counter.ToString() + " " + str + " in " + GetExecutionTime() + "------------------------------------------";
                }
                Console.WriteLine(str);
            }
        }

        public string GetExecutionTime()
        {
            return DateTime.Now.Subtract(StartTime).TotalMilliseconds.ToString();
        }
    }

    //public class FwSqlLog : List<FwSqlLogEntry>
    //{
    //    //--------------------------------------------------------------------------------
    //    public string Render()
    //    {
    //        StringBuilder log = new StringBuilder();
    //        bool isAlternateRow = false;
    //        log.AppendLine();
    //        log.AppendLine("<div class=\"FwSqlLog\">");
    //        log.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\">");
    //        log.AppendLine("<thead>");
    //        log.AppendLine("<tr>");
    //        log.AppendLine("<th>Execution Time (ms)</th>");
    //        log.AppendLine("<th>SQL</th>");
    //        log.AppendLine("</tr>");
    //        log.AppendLine("</thead>");
    //        log.AppendLine("<tbody>");
    //        for (int i = 0; i < this.Count; i++)
    //        {
    //            if (!isAlternateRow)
    //            {
    //                log.AppendLine("<tr>");
    //            }
    //            else
    //            {
    //                log.AppendLine("<tr class=\"Alternate\">");
    //            }
    //            log.AppendLine("<td>" + this[i].GetExecutionTime() + "</td>");
    //            log.AppendLine("<td>" + this[i].SqlForHtml + "</td>");
    //            log.AppendLine("</tr>");
    //            isAlternateRow = !isAlternateRow;
    //        }
    //        log.AppendLine("</tbody>");
    //        log.AppendLine("</table>");
    //        log.AppendLine("</div>");
    //        this.Clear();
    //        return log.ToString();
    //    }
    //    //--------------------------------------------------------------------------------
    //}
}

