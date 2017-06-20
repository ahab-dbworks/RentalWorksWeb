using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwCore.ValueTypes
{
    public partial class FwApplicationSchema
    {
        //=============================================================================================
        public class Module
        {
            public string ModuleName {get;private set;}
            public Browse Browse {get;private set;}
            public Form Form {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Module(string moduleName, Browse browse, Form form)
            {
                this.ModuleName = moduleName;
                this.Browse     = browse;
                this.Form       = form;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.ModuleXml ToModuleXml()
            {
                FwApplicationSchemaXml.ModuleXml moduleXml;
                FwApplicationSchemaXml.FormXml moduleFormXml;
                FwApplicationSchemaXml.BrowseXml moduleBrowseXml;
                
                moduleXml = null;
                moduleBrowseXml = null;
                moduleFormXml = null;
                if (this != null)
                {
                    if (this.Form != null)
                    {
                        moduleFormXml = this.Form.ToFormXml();
                    }
                    if (this.Browse != null)
                    {
                        moduleBrowseXml = this.Browse.ToBrowseXml();
                    }
                    moduleXml = new FwApplicationSchemaXml.ModuleXml(this.ModuleName, moduleBrowseXml, moduleFormXml);
                }

                return moduleXml;
            }
        }
        //=============================================================================================
        public class Grid
        {
            public string GridName {get;private set;}
            public Browse Browse {get;private set;}
            public Form Form {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Grid(string gridName, Browse browse, Form form)
            {
                this.GridName = gridName;
                this.Browse   = browse;
                this.Form     = form;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.GridXml ToGridXml()
            {
                FwApplicationSchemaXml.GridXml gridXml;
                FwApplicationSchemaXml.FormXml gridFormXml;
                FwApplicationSchemaXml.BrowseXml gridBrowseXml;
                
                gridXml       = null;
                gridFormXml   = null;
                gridBrowseXml = null;
                if (this != null)
                {
                    if (this.Form != null)
                    {
                        gridFormXml   = this.Form.ToFormXml();
                    }
                    if (this.Browse != null)
                    {
                        gridBrowseXml = this.Browse.ToBrowseXml();
                    }
                    gridXml = new FwApplicationSchemaXml.GridXml(this.GridName, gridBrowseXml, gridFormXml);
                }
                
                return gridXml;
            }
        }
        //=============================================================================================
        public class Validation
        {
            public string ValidationName {get;private set;}
            public Browse Browse {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Validation(string validationName, Browse browse)
            {
                this.ValidationName = validationName;
                this.Browse         = browse;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.ValidationXml ToValidationXml()
            {
                FwApplicationSchemaXml.ValidationXml validationXml;
                FwApplicationSchemaXml.BrowseXml validationBrowseXml;
                
                validationXml       = null;
                validationBrowseXml = null;
                if (this != null)
                {
                    if (this.Browse != null)
                    {
                        validationBrowseXml = this.Browse.ToBrowseXml();
                    }
                    validationXml = new FwApplicationSchemaXml.ValidationXml(this.ValidationName, validationBrowseXml);
                }
                
                return validationXml;
            }
        }
        //=============================================================================================
        public class Browse
        {
            public string DatabaseConnection {get;private set;}
            public string TableName {get;private set;}
            public Dictionary<string, Column> UniqueIds {get; private set;}
            public Dictionary<string, Column> Columns {get; private set;}
            //---------------------------------------------------------------------------------------------
            public Browse(string databaseConnection, string tableName, Dictionary<string, Column> uniqueIds, Dictionary<string, Column> columns)
            {
                this.DatabaseConnection = databaseConnection;
                this.TableName          = tableName;
                this.UniqueIds          = uniqueIds;
                this.Columns            = columns;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.BrowseXml ToBrowseXml()
            {
                FwApplicationSchemaXml.BrowseXml browseXml;
                List<FwApplicationSchemaXml.ColumnXml> uniqueIdsXml, columnsXml;
            
                browseXml    = null;
                uniqueIdsXml = null;
                columnsXml   = null;
                if (this != null)
                {
                    if (this.UniqueIds != null)
                    {
                        uniqueIdsXml = GetColumnsXml(this.UniqueIds);
                    }
                    if (this.Columns != null)
                    {
                        columnsXml = GetColumnsXml(this.Columns);
                    }
                    browseXml = new FwApplicationSchemaXml.BrowseXml(this.DatabaseConnection, this.TableName, uniqueIdsXml, columnsXml);
                }

                return browseXml;
            }
        }
        //=============================================================================================
        public class Form
        {
            public string DatabaseConnection {get;private set;}
            public List<Tab> Tabs {get; private set;}
            public List<FormGrid> Grids {get; private set;}
            public Dictionary<string, FormTable> Tables {get; private set;}
            public bool HasAudit {get; private set;}
            //---------------------------------------------------------------------------------------------
            public Form(string databaseConnection, List<Tab> tabs, List<FormGrid> grids, Dictionary<string, FormTable> tables, bool hasAudit)
            {
                this.DatabaseConnection = databaseConnection;
                this.Tabs               = tabs;
                this.Grids              = grids;
                this.Tables             = tables;
                this.HasAudit           = hasAudit;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.FormXml ToFormXml()
            {
                FwApplicationSchemaXml.FormXml formXml;
                List<FwApplicationSchemaXml.TabXml> tabsXml;
                List<FwApplicationSchemaXml.FormGridXml> gridsXml;
                List<FwApplicationSchemaXml.TableXml> tablesXml;
            
                formXml = null;
                if (this != null)
                {
                    tabsXml          = new List<FwApplicationSchemaXml.TabXml>();
                    foreach(Tab tab in this.Tabs)
                    {
                        string tabCaption;
                        
                        FwApplicationSchemaXml.TabXml tabXml;

                        tabCaption = tab.Caption;

                        tabXml = new FwApplicationSchemaXml.TabXml(tabCaption);
                        tabsXml.Add(tabXml);
                    }

                    gridsXml = new List<FwApplicationSchemaXml.FormGridXml>();
                    foreach(FormGrid grid in this.Grids)
                    {
                        string gridName, securityCaption, tabCaption;
                        
                        FwApplicationSchemaXml.FormGridXml gridXml;

                        gridName        = grid.Grid;
                        securityCaption = grid.SecurityCaption;
                        tabCaption      = grid.TabCaption;

                        gridXml = new FwApplicationSchemaXml.FormGridXml(gridName, securityCaption, tabCaption);
                        gridsXml.Add(gridXml);
                    }
                    
                    tablesXml          = new List<FwApplicationSchemaXml.TableXml>();
                    foreach(var tableItem in this.Tables)
                    {
                        string tableName;
                        int saveOrder;
                        FormTable table;
                        FwApplicationSchemaXml.TableXml tableXml;
                        List<FwApplicationSchemaXml.ColumnXml> uniqueIds, columns;

                        table     = tableItem.Value;
                        tableName = table.TableName;
                        saveOrder = table.SaveOrder;
                        uniqueIds = null;
                        if (table.UniqueIds != null)
                        {
                            uniqueIds = GetColumnsXml(table.UniqueIds);
                        }
                        columns = null;
                        if (table.Columns != null)
                        {
                            columns = GetColumnsXml(table.Columns);
                        }
                        tableXml = new FwApplicationSchemaXml.TableXml(tableName, saveOrder, uniqueIds, columns);
                        tablesXml.Add(tableXml);
                    }
                    formXml = new FwApplicationSchemaXml.FormXml(this.DatabaseConnection, tabsXml, gridsXml, tablesXml, this.HasAudit);
                }

                return formXml;
            }
        }
        //=============================================================================================
        public class FormGrid
        {
            public string Grid {get;private set;}    
            public string SecurityCaption {get;private set;}
            public string TabCaption {get;private set;}
            //---------------------------------------------------------------------------------------------
            public FormGrid(string grid, string securityCaption, string tabCaption)
            {
                this.Grid = grid;    
                this.SecurityCaption = securityCaption;
                this.TabCaption = tabCaption;
            }
        }
        //=============================================================================================
        public class FormTable
        { 
            public string TableName {get;private set;}
            public int SaveOrder {get;private set;}
            public Dictionary<string, Column> UniqueIds {get; private set;}
            public Dictionary<string, Column> Columns {get; private set;}
            //---------------------------------------------------------------------------------------------
            public FormTable(string tableName, int saveOrder, Dictionary<string, Column> uniqueIds, Dictionary<string, Column> columns)
            {
                this.TableName    = tableName;
                this.SaveOrder    = saveOrder;
                this.UniqueIds    = uniqueIds;
                this.Columns      = columns;
            }
        }
        //=============================================================================================
        public class Column
        {
            public string Caption {get;private set;}
            public string ColumnName {get;private set;}
            public string DataType {get;private set;}
            public string SqlDataType {get;private set;}
            public bool SqlIsNullable {get;private set;}
            public int SqlCharacterMaximumLength {get;private set;}
            public int SqlNumericPrecision {get;private set;}
            public int SqlNumericScale {get;private set;}
            public bool SqlIsIdentity {get;private set;}
            public bool ReadOnly {get;private set;}
            public bool Required {get;private set;}
            public string ValidationName {get;private set;}
            public string ValidationDisplayField {get;private set;}
            public bool NoDuplicate {get;private set;}
            public bool ExportToExcel {get;private set;}
            public string TabCaption {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Column(string caption, string columnName, string dataType, string sqlDataType, bool sqlIsNullable, int sqlCharacterMaximumLength, int sqlNumericPrecision, int sqlNumericScale, bool sqlIsIdentity, bool readOnly, bool required, string validationName, string validationDisplayField, bool noDuplicate, bool exportToExcel, string tabCaption)
            {
                this.Caption                   = caption;
                this.ColumnName                = columnName;
                this.DataType                  = dataType;
                this.SqlDataType               = sqlDataType;
                this.SqlIsNullable             = sqlIsNullable;
                this.SqlCharacterMaximumLength = sqlCharacterMaximumLength;
                this.SqlNumericPrecision       = sqlNumericPrecision;
                this.SqlNumericScale           = sqlNumericScale;
                this.SqlIsIdentity             = sqlIsIdentity;
                this.ReadOnly                  = readOnly;
                this.Required                  = required;
                this.ValidationName            = validationName;
                this.ValidationDisplayField    = validationDisplayField;
                this.NoDuplicate               = noDuplicate;
                this.ExportToExcel             = exportToExcel;
                this.TabCaption                = tabCaption;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.ColumnXml ToColumnXml()
            {
                FwApplicationSchemaXml.ColumnXml columnXml;

                columnXml = new FwApplicationSchemaXml.ColumnXml(this.Caption, this.ColumnName, this.DataType, this.SqlDataType, this.SqlIsNullable, this.SqlCharacterMaximumLength, this.SqlNumericPrecision, this.SqlNumericScale, this.SqlIsIdentity, this.ReadOnly, this.Required, this.ValidationName, this.ValidationDisplayField, this.NoDuplicate, this.ExportToExcel, this.TabCaption);
                
                return columnXml;

            }
        }
        //=============================================================================================
        public class Tab
        {
            public string Caption {get;private set;}
            public List<Field> Fields {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Tab(string caption)
            {
                this.Caption = caption;
                Fields = new List<Field>();
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.TabXml ToTabXml()
            {
                FwApplicationSchemaXml.TabXml TabXml;

                TabXml = new FwApplicationSchemaXml.TabXml(this.Caption);

                return TabXml;
            }
        }
        //=============================================================================================
        public class Field
        {
            public string Caption {get;private set;}
            //---------------------------------------------------------------------------------------------
            public Field(string caption)
            {
                this.Caption = caption;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchemaXml.TabXml ToTabXml()
            {
                FwApplicationSchemaXml.TabXml TabXml;

                TabXml = new FwApplicationSchemaXml.TabXml(this.Caption);

                return TabXml;
            }
        }
        //=============================================================================================
    }
}
