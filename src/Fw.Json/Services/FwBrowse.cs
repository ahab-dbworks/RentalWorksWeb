using Fw.Json.SqlServer;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;

namespace Fw.Json.Services
{
    public class FwBrowse
    {
        //---------------------------------------------------------------------------------------------
        public FwDatabases Database {get;private set;}
        public FwSqlConnection DatabaseConnection {get{return new FwSqlConnection(this.Database);}}
        public FwApplicationSchema.Browse browseSchema {get;private set;}
        //---------------------------------------------------------------------------------------------
        public FwBrowse(FwApplicationSchema.Browse browseSchema)
        {
            this.browseSchema       = browseSchema;
            this.Database = (FwDatabases)Enum.Parse(typeof(FwDatabases), browseSchema.DatabaseConnection);
            //LoadSchemaTables();
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlSelect GetBrowseQry(int pageNo, int pageSize, List<string> searchfields, List<string> searchfieldoperators, List<string> searchfieldvalues, string orderby)
        {
            FwApplicationSchema.Column columnSchema;
            bool isFirstColumn;
            FwSqlSelect selectQry;
            List<string> columns;

            columns = new List<string>();
            selectQry = new FwSqlSelect();
            selectQry.DatabaseConnection = this.Database;
            selectQry.SqlConnection      = new FwSqlConnection(selectQry.DatabaseConnection);
            selectQry.SqlCommand         = new FwSqlCommand(selectQry.SqlConnection);
            selectQry.Add("select ");
            isFirstColumn = true;
            foreach(var item in browseSchema.UniqueIds)
            {
                columnSchema = item.Value;
                if (!columns.Contains(columnSchema.ColumnName))
                {
                    if (isFirstColumn)
                    {
                        selectQry.Add(columnSchema.ColumnName);
                        isFirstColumn = false;
                    }
                    else
                    {
                        selectQry.Add(", " + columnSchema.ColumnName);
                    }
                    columns.Add(columnSchema.ColumnName);
                }
            }
            foreach(var item in browseSchema.Columns)
            {
                columnSchema = item.Value;
                if (!columns.Contains(columnSchema.ColumnName))
                {
                    if (isFirstColumn)
                    {
                        selectQry.Add(columnSchema.ColumnName);
                        isFirstColumn = false;
                    }
                    else
                    {
                        selectQry.Add(", " + columnSchema.ColumnName);
                    }
                    columns.Add(columnSchema.ColumnName);
                }
            }
            if (!browseSchema.TableName.Contains("("))
            {
                selectQry.Add("from " + browseSchema.TableName + " with (nolock)");
            }
            else
            {
                selectQry.Add("from " + browseSchema.TableName);
            }
            selectQry.PageNo = pageNo;
            selectQry.PageSize = pageSize;
            selectQry.EnablePaging = ((pageNo != 0) && (pageSize != 0));
            selectQry.Parse();
            
            // build search query
            for(int i = 0; i < searchfields.Count; i++)
            {
                string searchfield, searchfieldoperator, searchfieldvalue;
                FwApplicationSchema.Column schemaColumn;

                searchfield         = searchfields[i];
                searchfieldoperator = searchfieldoperators[i];
                searchfieldvalue    = searchfieldvalues[i];
                if (!string.IsNullOrEmpty(searchfield))
                {
                    if (browseSchema.UniqueIds.ContainsKey(searchfield))
                    {
                        schemaColumn = browseSchema.UniqueIds[searchfield];
                    }
                    else if (browseSchema.Columns.ContainsKey(searchfield))
                    {
                        schemaColumn = browseSchema.Columns[searchfield];
                    }
                    else
                    {
                        throw new Exception("Invalid search column: " + searchfield + ". [FwBrowse.cs:GetBrowseQry]");
                    }
                    switch (searchfieldoperator) {
                        case "like":
                        case "=":
                        case "<>":
                            break;
                        default:
                            throw new Exception("Invalid search operator: " + searchfieldoperator + ". [FwBrowse.cs:GetBrowseQry]");
                    }
                    switch(schemaColumn.SqlDataType)
                    {
                        case "datetime":
                            selectQry.AddWhere(searchfield + " between @" + searchfield + "1 and @" + searchfield + "2");
                            selectQry.AddParameter("@" + searchfield + "1", searchfieldvalue);
                            selectQry.AddParameter("@" + searchfield + "2", searchfieldvalue + " 23:59:59.997");
                            break;
                        case "varchar":
                        case "char":
                        default:
                            switch(searchfieldoperator)
                            {
                                case "like":
                                    selectQry.AddWhere(searchfield + " " + searchfieldoperator + " @" + searchfield);
                                    selectQry.AddParameter("@" + searchfield, searchfieldvalue.Trim() + "%");
                                    break;
                                case "=":
                                case "<>":
                                    selectQry.AddWhere(searchfield + " " + searchfieldoperator + " @" + searchfield);
                                    selectQry.AddParameter("@" + searchfield, searchfieldvalue.Trim());
                                    break;
                                default:
                                    throw new Exception("Invalid search operator: " + searchfieldoperator + ". [FwBrowse.cs:GetBrowseQry]");
                            }
                            break;
                    }
                
                }
            }
            foreach(var item in browseSchema.UniqueIds)
            {
                columnSchema = item.Value;
                selectQry.SqlCommand.AddColumn(columnSchema.ColumnName, true);
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = columnSchema.ColumnName;
                }
            }
            foreach(var item in browseSchema.Columns)
            {
                columnSchema = item.Value;
                selectQry.SqlCommand.AddColumn(columnSchema.ColumnName, false);
            }
            selectQry.AddOrderBy(orderby);

            return selectQry;
        }
        //---------------------------------------------------------------------------------------------
    }
}
