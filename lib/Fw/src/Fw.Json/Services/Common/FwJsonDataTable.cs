using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Fw.Json.Services
{
    public class FwJsonDataTable
    {
        //---------------------------------------------------------------------------------------------
        public Dictionary<string, int> ColumnIndex;
        public List<FwJsonDataTableColumn> Columns {get; set;}
        public List<List<object>> Rows {get; set;}
        public int PageNo {get; set;}
        public int PageSize {get; set;}
        public int TotalPages {get; set;}
        public int TotalRows {get; set;}
        public Dictionary<int, string> ColumnNameByIndex
        {
            get
            {
                Dictionary<int, string> reverseColumnIndex;

                reverseColumnIndex = new Dictionary<int,string>();
                foreach(KeyValuePair<string,int> item in this.ColumnIndex)
                {
                    reverseColumnIndex[item.Value] = item.Key;
                }

                return reverseColumnIndex;
            }
        }
        //---------------------------------------------------------------------------------------------
        [JsonIgnore]
        public DataTable DebugDataTable
        {
            get
            {
                DataTable dt;
                DataRow row;

                dt = new DataTable();
                for (int colno = 0; colno < Columns.Count; colno++)
                {
                    dt.Columns.Add(ColumnNameByIndex[colno], typeof(string));
                }
                for (int rowno = 0; rowno < Rows.Count; rowno++)
                {
                    row = dt.NewRow();
                    for (int colno = 0; colno < Columns.Count; colno++)
                    {
                        if (Rows[rowno][colno] != null)
                        {
                            row[colno] = Rows[rowno][colno].ToString();
                        }
                        else
                        {
                            row[colno] = string.Empty;
                        }
                    }
                    dt.Rows.Add(row);
                }

                return dt;
            }
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTable() : base()
        {
            ColumnIndex = new Dictionary<string,int>();
            Columns = new List<FwJsonDataTableColumn>();
            Rows = new List<List<object>>();
            PageNo = 0;
            PageSize = -1;
            TotalPages = 0;
            TotalRows = 0;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTable(int pageNo, int pageSize, int totalRows, List<FwJsonDataTableColumn> columns) : this()
        {
            PageNo    = pageNo;
            PageSize  = pageSize;
            TotalRows = totalRows;
            Columns   = columns;
        }
        //---------------------------------------------------------------------------------------------
        public void SetValue(int rowno, string columnname, object value)
        {
            int colno;
            colno = this.GetColumnNo(columnname);
            this.Rows[rowno][colno] = value;
        }
        //---------------------------------------------------------------------------------------------
        public int GetColumnNo(string columnname)
        {
            int result;

            result = -1;
            for (int colno = 0; colno < this.Columns.Count; colno++)
            {
                if (this.Columns[colno].DataField == columnname)
                {
                    result = colno;
                    break;
                }
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public FwDatabaseField GetValue(int rowno, string columnname)
        {
            FwDatabaseField result;
            int colno = this.GetColumnNo(columnname);
            result = GetValue(rowno, colno);
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public FwDatabaseField GetValue(int rowno, int colno)
        {
            FwDatabaseField result;
            result = new FwDatabaseField(this.Rows[rowno][colno]);
            return result;
        }
        //---------------------------------------------------------------------------------------------
        /// <summary>
        /// It gives you back the url you can download the file from.
        /// </summary>
        /// <returns>downloadurl</returns>
        public string ExportXLSX(string saveas)
        {
            ExcelWorksheet worksheet;
            string filename, path, downloadurl;
            int worksheetcol=1,  colcount=0;
            FwJsonDataTableColumn col;
            
            filename    = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".xlsx";
            path        = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
            downloadurl = VirtualPathUtility.ToAbsolute("~/fwdownload.ashx?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(saveas) + "&asattachment=true");
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                worksheet = package.Workbook.Worksheets.Add("Worksheet 1");
                worksheetcol = 1;
                for(int colno = 0; colno < this.Columns.Count; colno++)
                {
                    col = this.Columns[colno];
                    if ((!col.IsUniqueId) && (col.IsVisible))
                    {
                        worksheet.Cells[1, worksheetcol].Value = this.Columns[colno].Name;
                        worksheet.Cells[1, worksheetcol].Style.Font.Bold = true;
                        worksheet.Cells[1, worksheetcol].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheetcol++;
                        colcount++;
                    }
                }
                worksheet.Cells[1, 1, 1, colcount].AutoFilter = true;
                for(int rowno = 0; rowno < this.Rows.Count; rowno++)
                {
                    worksheetcol = 1;
                    for(int colno = 0; colno < this.Columns.Count; colno++)
                    {
                        col = this.Columns[colno];
                        if ((!col.IsUniqueId) && (col.IsVisible))
                        {
                            worksheet.Cells[rowno + 2, worksheetcol].Value = this.GetValue(rowno, colno).ToString();
                            worksheetcol++;
                        }
                    }
                }
                worksheet.Cells[1, 1, this.Rows.Count + 1, this.Columns.Count].AutoFitColumns();
                package.Save();
            }

            return downloadurl;
        }
        //---------------------------------------------------------------------------------------------
        public void FillExcelWorksheet(ExcelWorksheet worksheet)
        {
            int worksheetcol = 1, colcount = 0;
            FwJsonDataTableColumn col;

            worksheetcol = 1;
            for (int colno = 0; colno < this.Columns.Count; colno++)
            {
                col = this.Columns[colno];
                if ((!col.IsUniqueId) && (col.IsVisible))
                {
                    worksheet.Cells[1, worksheetcol].Value = this.Columns[colno].Name;
                    worksheet.Cells[1, worksheetcol].Style.Font.Bold = true;
                    worksheet.Cells[1, worksheetcol].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheetcol++;
                    colcount++;
                }
            }
            worksheet.Cells[1, 1, 1, colcount].AutoFilter = true;
            for (int rowno = 0; rowno < this.Rows.Count; rowno++)
            {
                worksheetcol = 1;
                for (int colno = 0; colno < this.Columns.Count; colno++)
                {
                    col = this.Columns[colno];
                    if ((!col.IsUniqueId) && (col.IsVisible))
                    {
                        worksheet.Cells[rowno + 2, worksheetcol].Value = this.GetValue(rowno, colno).ToString();
                        worksheetcol++;
                    }
                }
            }
            worksheet.Cells[1, 1, this.Rows.Count + 1, this.Columns.Count].AutoFitColumns();
        }
        //---------------------------------------------------------------------------------------------
        public List<object> NewRow()
        {
            object[] rowArray;
            List<object> row;
                        
            rowArray = new object[this.Columns.Count];
            row = new List<object>(rowArray);

            return row;
        }
        //---------------------------------------------------------------------------------------------
        public void InsertSubTotalRows(string nameGroupbyColumn, string nameRowTypeColumn, string[] nameSumColumns)
        {
            int indexGroupByColumn, indexRowTypeColumn, rowcount;
            string thisRowGroupByText, nextRowGroupByText;
            decimal[] subtotals;
            decimal cellvalue;
            object cellvalueobj;
            List<object> row;
            int[] indexSumColumns;
            bool isLastRow, isNextRowNewGroup;

            thisRowGroupByText = "!@#NOT_DEFINED!@#";
            nextRowGroupByText = "!@#NOT_DEFINED!@#";
            indexGroupByColumn = this.ColumnIndex[nameGroupbyColumn];
            indexRowTypeColumn = this.ColumnIndex[nameRowTypeColumn];
            indexSumColumns    = new int[nameSumColumns.Length];
            isNextRowNewGroup  = true;
            
            // cache the columnn index of each column that will be added
            subtotals  = new decimal[nameSumColumns.Length];
            for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
            {
                indexSumColumns[sumcolno] = this.ColumnIndex[nameSumColumns[sumcolno]];
                subtotals[sumcolno]  = 0;
            }
            
            // subtotal the columns 
            rowcount  = this.Rows.Count;
            for (int rowno = 0; rowno < rowcount; rowno++)
            {
                // add group header row
                if (isNextRowNewGroup)
                {
                    row = NewRow();
                    row[indexRowTypeColumn] = nameGroupbyColumn + "header";
                    row[indexGroupByColumn] = Rows[rowno][indexGroupByColumn].ToString();
                    Rows.Insert(rowno, row);
                    rowno++;
                    rowcount++;
                }
                
                // sum the detail row
                thisRowGroupByText = Rows[rowno][indexGroupByColumn].ToString();
                for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
                {
                    cellvalueobj = Rows[rowno][indexSumColumns[sumcolno]];
                    if ((cellvalueobj is Decimal) ||
                        (cellvalueobj is Int16) ||
                        (cellvalueobj is Int32) ||
                        (cellvalueobj is Int64) ||
                        (cellvalueobj is Single) ||
                        (cellvalueobj is Double))
                    {
                        cellvalue = FwConvert.ToDecimal(cellvalueobj);
                    }
                    else if ((cellvalueobj is string) && (decimal.TryParse(cellvalueobj.ToString().Replace("%", string.Empty), out cellvalue)))
                    {
                        // the TryParse already stored the value in cellvalue
                    }
                    else if (cellvalueobj == null)
                    {
                        cellvalue = 0;
                    }
                    else if ((cellvalueobj is String) && (cellvalueobj.ToString().TrimEnd() == string.Empty))
                    {
                        cellvalue = 0;
                    }
                    else
                    {
                        throw new Exception("Invalid type: " + cellvalueobj.GetType().FullName + " for column: " + nameSumColumns[sumcolno] + " [index: " + sumcolno.ToString() + "] row: " + rowno.ToString() + " value: \"" + cellvalueobj.ToString() + "\""); 
                    }
                    subtotals[sumcolno] += cellvalue;
                }

                isLastRow = ((rowno + 1) == this.Rows.Count);
                if (!isLastRow)
                {
                    nextRowGroupByText = Rows[rowno + 1][indexGroupByColumn].ToString();
                    isNextRowNewGroup = (thisRowGroupByText != nextRowGroupByText);
                }

                // add a group footer row to the data table
                if (isLastRow || isNextRowNewGroup)
                {
                    row = NewRow();
                    row[indexRowTypeColumn] = nameGroupbyColumn + "footer";
                    //row[indexGroupByColumn] = "Subtotal";
                    for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
                    {
                        switch(Columns[indexSumColumns[sumcolno]].DataType)
                        {
                            case FwJsonDataTableColumn.DataTypes.CurrencyString:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyString(subtotals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSign(subtotals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(subtotals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.Decimal:
                                row[indexSumColumns[sumcolno]] = subtotals[sumcolno];
                                break;
                            case FwJsonDataTableColumn.DataTypes.Integer:
                                row[indexSumColumns[sumcolno]] = subtotals[sumcolno];
                                break;
                            case FwJsonDataTableColumn.DataTypes.Percentage:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSign(subtotals[sumcolno]) + "%";
                                break;
                            default:
                                row[indexSumColumns[sumcolno]] = subtotals[sumcolno];
                                break;

                        }
                        //row[indexSumColumns[sumcolno]] = subtotals[sumcolno];
                        subtotals[sumcolno] = 0;
                    }
                    Rows.Insert(rowno + 1, row);
                    rowno++;
                    rowcount++;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void InsertTotalRow(string nameRowTypeColumn, string rowTypeFilter, string newTotalRowType, string[] nameSumColumns)
        {
            int indexRowTypeColumn, rowcount;
            int[] indexSumColumns;
            decimal[] totals;
            decimal cellvalue;
            object cellvalueobj;
            bool isLastRow;
            List<object> row;

            indexRowTypeColumn = this.ColumnIndex[nameRowTypeColumn];
            indexSumColumns    = new int[nameSumColumns.Length];
            totals             = new decimal[nameSumColumns.Length];
            for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
            {
                indexSumColumns[sumcolno] = this.ColumnIndex[nameSumColumns[sumcolno]];
                totals[sumcolno]  = 0;
            }
            rowcount  = this.Rows.Count;
            for (int rowno = 0; rowno < rowcount; rowno++)
            {
                if (rowTypeFilter == Rows[rowno][indexRowTypeColumn].ToString())
                {
                    for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
                    {
                        cellvalueobj = Rows[rowno][indexSumColumns[sumcolno]];
                        if ((cellvalueobj is Decimal) ||
                            (cellvalueobj is Int16) ||
                            (cellvalueobj is Int32) ||
                            (cellvalueobj is Int64) ||
                            (cellvalueobj is Single) ||
                            (cellvalueobj is Double))
                        {
                            cellvalue = FwConvert.ToDecimal(cellvalueobj);
                        }
                        else if ((cellvalueobj is string) && (decimal.TryParse(cellvalueobj.ToString().Replace("%", string.Empty).Replace("(", "-").Replace(")", string.Empty), out cellvalue)))
                        {
                            // the TryParse already stored the value in cellvalue
                        }
                        else if (cellvalueobj == null)
                        {
                            cellvalue = 0;
                        }
                        else if ((cellvalueobj is String) && (cellvalueobj.ToString().TrimEnd() == string.Empty))
                        {
                            cellvalue = 0;
                        }
                        else
                        {
                            throw new Exception("Invalid type: " + cellvalueobj.GetType().FullName + " for column: " + nameSumColumns[sumcolno] + " [index: " + sumcolno.ToString() + "] row: " + rowno.ToString()); 
                        }
                        totals[sumcolno] += cellvalue;
                    }
                }
                isLastRow = ((rowno + 1) == this.Rows.Count);
                if (isLastRow)
                {
                    row = NewRow();
                    row[indexRowTypeColumn] = newTotalRowType;
                    for (int sumcolno = 0; sumcolno < nameSumColumns.Length; sumcolno++)
                    {
                        switch(Columns[indexSumColumns[sumcolno]].DataType)
                        {
                            case FwJsonDataTableColumn.DataTypes.CurrencyString:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyString(totals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSign(totals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(totals[sumcolno]);
                                break;
                            case FwJsonDataTableColumn.DataTypes.Decimal:
                                row[indexSumColumns[sumcolno]] = totals[sumcolno];
                                break;
                            case FwJsonDataTableColumn.DataTypes.Integer:
                                row[indexSumColumns[sumcolno]] = totals[sumcolno];
                                break;
                            case FwJsonDataTableColumn.DataTypes.Percentage:
                                row[indexSumColumns[sumcolno]] = FwConvert.ToCurrencyStringNoDollarSign(totals[sumcolno]) + "%";
                                break;
                            default:
                                row[indexSumColumns[sumcolno]] = totals[sumcolno];
                                break;

                        }
                        //row[indexSumColumns[sumcolno]] = totals[sumcolno];
                    }
                    Rows.Insert(rowno + 1, row);
                    break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void FormatColumn(string columnName, FwJsonDataTableColumn.DataTypes dataType)
        {
            int colno;
            object cell;
            
            colno = this.ColumnIndex[columnName];
            for (int rowno = 0; rowno < this.Rows.Count; rowno++)
            {
                cell = this.Rows[rowno][colno];
                switch(dataType)
                {
                    case FwJsonDataTableColumn.DataTypes.CurrencyString:
                        this.Rows[rowno][colno] = FwConvert.ToCurrencyString(new FwDatabaseField(cell).ToDecimal());
                        break;
                    case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign:
                        this.Rows[rowno][colno] = FwConvert.ToCurrencyStringNoDollarSign(new FwDatabaseField(cell).ToDecimal());
                        break;
                    case FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSignNoDecimalPlaces:
                        this.Rows[rowno][colno] = FwConvert.ToCurrencyStringNoDollarSignNoDecimalPlaces(new FwDatabaseField(cell).ToDecimal());
                        break;
                    case FwJsonDataTableColumn.DataTypes.Decimal:
                        this.Rows[rowno][colno] = new FwDatabaseField(cell).ToDecimal();
                        break;
                    case FwJsonDataTableColumn.DataTypes.Integer:
                        this.Rows[rowno][colno] = new FwDatabaseField(cell).ToInt32();
                        break;
                    case FwJsonDataTableColumn.DataTypes.Percentage:
                        this.Rows[rowno][colno] = FwConvert.ToCurrencyStringNoDollarSign(new FwDatabaseField(cell).ToDecimal()) + "%";
                        break;
                    default:
                        this.Rows[rowno][colno] = new FwDatabaseField(cell).ToString();
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
