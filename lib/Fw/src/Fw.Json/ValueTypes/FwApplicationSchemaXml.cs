using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Fw.Json.ValueTypes
{
    //=============================================================================================
    [XmlRoot("FwApplicationSchema")]
    public class FwApplicationSchemaXml
    {
        //---------------------------------------------------------------------------------------------
        [XmlArray("Modules")]
        [XmlArrayItem("Module")]
        public List<ModuleXml> Modules {get;set;}
        //---------------------------------------------------------------------------------------------
        [XmlArray("Grids")]
        [XmlArrayItem("Grid")]
        public List<GridXml> Grids {get;set;}
        //---------------------------------------------------------------------------------------------
        [XmlArray("Validations")]
        [XmlArrayItem("Validation")]
        public List<ValidationXml> Validations {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchemaXml()
        {
            this.Modules     = new List<ModuleXml>();
            this.Grids       = new List<GridXml>();
            this.Validations = new List<ValidationXml>();
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchemaXml(List<ModuleXml> modules, List<GridXml> grids, List<ValidationXml> validations)
        {
            this.Modules     = modules;
            this.Grids       = grids;
            this.Validations = validations;
        }
        //=============================================================================================
        [XmlRoot("Module")]
        public class ModuleXml
        {
            [XmlAttribute("ModuleName")]
            public string ModuleName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlElement("Browse")]
            public BrowseXml Browse {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlElement("Form")]
            public FormXml Form {get;set;}
            //---------------------------------------------------------------------------------------------
            public ModuleXml()
            {
                this.ModuleName = string.Empty;
                this.Browse     = null;
                this.Form       = null;
            }
            //---------------------------------------------------------------------------------------------
            public ModuleXml(string moduleName, BrowseXml browse, FormXml form)
            {
                this.ModuleName = moduleName;
                this.Browse     = browse;
                this.Form       = form;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchema.Module ToModule()
            {
                FwApplicationSchema.Module module;
                FwApplicationSchema.Browse browse;
                FwApplicationSchema.Form form;

                browse = (this.Browse != null) ? this.Browse.ToBrowse() : null;
                form   = (this.Form   != null) ? this.Form.ToForm()     : null;
                module = new FwApplicationSchema.Module(this.ModuleName, browse, form);

                return module;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Grid")]
        public class GridXml
        {
            [XmlAttribute("GridName")]
            public string GridName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlElement("Browse")]
            public BrowseXml Browse {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlElement("Form")]
            public FormXml Form {get;set;}
            //---------------------------------------------------------------------------------------------
            public GridXml()
            {
                this.GridName = string.Empty;
                this.Browse   = null;
                this.Form     = null;
            }
            //---------------------------------------------------------------------------------------------
            public GridXml(string gridName, BrowseXml browse, FormXml form)
            {
                this.GridName = gridName;
                this.Browse   = browse;
                this.Form     = form;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchema.Grid ToGrid()
            {
                FwApplicationSchema.Grid grid;
                FwApplicationSchema.Browse browse;
                FwApplicationSchema.Form form;
                
                browse = (this.Browse != null) ? this.Browse.ToBrowse() : null;
                form   = (this.Form   != null) ? this.Form.ToForm()     : null;
                grid   = new FwApplicationSchema.Grid(this.GridName, browse, form);

                return grid;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Validation")]
        public class ValidationXml
        {
            [XmlAttribute("ValidationName")]
            public string ValidationName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlElement("Browse")]
            public BrowseXml Browse {get;set;}
            //---------------------------------------------------------------------------------------------
            public ValidationXml()
            {
                this.ValidationName = string.Empty;
                this.Browse   = new BrowseXml();
            }
            //---------------------------------------------------------------------------------------------
            public ValidationXml(string validationName, BrowseXml browse)
            {
                this.ValidationName = validationName;
                this.Browse         = browse;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchema.Validation ToValidation()
            {
                FwApplicationSchema.Validation validation;
                FwApplicationSchema.Browse browse;

                browse     = (this.Browse != null) ? this.Browse.ToBrowse() : null;
                validation = new FwApplicationSchema.Validation(this.ValidationName, browse);

                return validation;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Browse")]
        public class BrowseXml
        {
            [XmlAttribute("DatabaseConnection")]
            public string DatabaseConnection {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("TableName")]
            public string TableName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlArray("UniqueIds")]
            [XmlArrayItem("Column")]
            public List<ColumnXml> UniqueIds {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlArray("Columns")]
            [XmlArrayItem("Column")]
            public List<ColumnXml> Columns {get;set;}
            //---------------------------------------------------------------------------------------------
            public BrowseXml()
            {
                this.DatabaseConnection = string.Empty;
                this.TableName          = string.Empty;
                this.Columns            = new List<ColumnXml>();
            }
            //---------------------------------------------------------------------------------------------
            public BrowseXml(string databaseConnection, string tableName, List<ColumnXml> uniqueIds, List<ColumnXml> columns)
            {
                this.DatabaseConnection = databaseConnection;
                this.TableName          = tableName;
                this.UniqueIds          = uniqueIds;
                this.Columns            = columns;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchema.Browse ToBrowse()
            {
                Dictionary<string, FwApplicationSchema.Column> browseUniqueIds, browseColumns;
                FwApplicationSchema.Browse browse;

                browseUniqueIds = (this.UniqueIds != null) ? FwApplicationSchema.GetColumns(this.UniqueIds) : null;
                browseColumns   = (this.Columns   != null) ? FwApplicationSchema.GetColumns(this.Columns)   : null;
                browse          = new FwApplicationSchema.Browse(this.DatabaseConnection, this.TableName, browseUniqueIds, browseColumns);

                return browse;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Form")]
        public class FormXml
        {
            [XmlAttribute("DatabaseConnection")]
            public string DatabaseConnection {get;set;}
            
            [XmlAttribute("HasAudit")]
            public bool HasAudit {get;set;}

            [XmlArray("Tabs")]
            [XmlArrayItem("Tab")]
            public List<TabXml> Tabs {get;set;}

            [XmlArray("Grids")]
            [XmlArrayItem("Grid")]
            public List<FormGridXml> Grids {get;set;}

            [XmlArray("Tables")]
            [XmlArrayItem("Table")]
            public List<TableXml> Tables {get;set;}
            //---------------------------------------------------------------------------------------------
            public FormXml()
            {
                this.DatabaseConnection = string.Empty;
                this.Tabs               = new List<TabXml>();
                this.Grids              = new List<FormGridXml>();
                this.Tables             = new List<TableXml>();
            }
            //---------------------------------------------------------------------------------------------
            public FormXml(string databaseConnection, List<TabXml> tabs, List<FormGridXml> grids, List<TableXml> tables, bool hasAudit)
            {
                this.DatabaseConnection = databaseConnection;
                this.Tabs               = tabs;
                this.Grids              = grids;
                this.Tables             = tables;
                this.HasAudit           = hasAudit;
            }
            //---------------------------------------------------------------------------------------------
            public FwApplicationSchema.Form ToForm()
            {
                List<FwApplicationSchema.Tab> Tabs;
                List<FwApplicationSchema.FormGrid> Grids;
                Dictionary<string,FwApplicationSchema.FormTable> formTables;
                FwApplicationSchema.Form form;

                Tabs = new List<FwApplicationSchema.Tab>();
                foreach(var TabXml in this.Tabs)
                {
                    FwApplicationSchema.Tab Tab;
                    string tabCaption;
                    
                    tabCaption       = TabXml.Caption;
                    Tab              = new FwApplicationSchema.Tab(tabCaption);
                    Tabs.Add(Tab);
                }

                Grids = new List<FwApplicationSchema.FormGrid>();
                foreach(var FormGridXml in this.Grids)
                {
                    FwApplicationSchema.FormGrid Grid;
                    string grid, securityCaption, tabCaption;
                    
                    grid              = FormGridXml.Grid;
                    securityCaption   = FormGridXml.SecurityCaption;
                    tabCaption        = FormGridXml.TabCaption;
                    Grid              = new FwApplicationSchema.FormGrid(grid, securityCaption, tabCaption);
                    Grids.Add(Grid);
                }

                formTables = new Dictionary<string,FwApplicationSchema.FormTable>();
                foreach(var formTableXml in this.Tables)
                {
                    FwApplicationSchema.FormTable formTable;
                    int saveOrder;
                    string formTableName;
                    Dictionary<string,FwApplicationSchema.Column> formUniqueIds, formColumns;
                    
                    formTableName = formTableXml.TableName;
                    saveOrder     = formTableXml.SaveOrder;
                    formUniqueIds = (formTableXml.UniqueIds != null) ? FwApplicationSchema.GetColumns(formTableXml.UniqueIds) : null;
                    formColumns   = (formTableXml.Columns   != null) ? FwApplicationSchema.GetColumns(formTableXml.Columns)   : null;
                    formTable     = new FwApplicationSchema.FormTable(formTableName, saveOrder, formUniqueIds, formColumns);
                    formTables[formTableName] = formTable;
                }
                form = new FwApplicationSchema.Form(this.DatabaseConnection, Tabs, Grids, formTables, this.HasAudit);

                return form;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Grid")]
        public class FormGridXml
        {
            [XmlAttribute("Grid")]
            public string Grid {get;set;}

            [XmlAttribute("SecurityCaption")]
            public string SecurityCaption {get;set;}

            [XmlAttribute("TabCaption")]
            public string TabCaption {get;set;}
            //---------------------------------------------------------------------------------------------
            public FormGridXml()
            {
                this.Grid = string.Empty;
                this.SecurityCaption = string.Empty;
                this.TabCaption = string.Empty;
            }
            //---------------------------------------------------------------------------------------------
            public FormGridXml(string grid, string securityCaption, string tabCaption)
            {
                this.Grid            = grid;    
                this.SecurityCaption = securityCaption;
                this.TabCaption      = tabCaption;
            }
        }
        //=============================================================================================
        [XmlRoot("Table")]
        public class TableXml
        { 
            [XmlAttribute("TableName")]
            public string TableName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SaveOrder")]
            public int SaveOrder {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlArray("UniqueIds")]
            [XmlArrayItem("Column")]
            public List<ColumnXml> UniqueIds {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlArray("Columns")]
            [XmlArrayItem("Column")]
            public List<ColumnXml> Columns {get;set;}
            //---------------------------------------------------------------------------------------------
            public TableXml()
            {
                this.TableName = string.Empty;
                this.SaveOrder = -1;
                this.UniqueIds = new List<ColumnXml>();
                this.Columns   = new List<ColumnXml>();
            }
            //---------------------------------------------------------------------------------------------
            public TableXml(string tableName, int saveOrder, List<ColumnXml> uniqueIds, List<ColumnXml> columns)
            {
                this.TableName = tableName;
                this.SaveOrder = saveOrder;
                this.UniqueIds = uniqueIds;
                this.Columns   = columns;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
        [XmlRoot("Column")]
        public class ColumnXml
        {
            [XmlAttribute("Caption")]
            public string Caption {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("ColumnName")]
            public string ColumnName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("DataType")]
            public string DataType {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlDataType")]
            public string SqlDataType {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlIsNullable")]
            public bool SqlIsNullable {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlCharacterMaximumLength")]
            public string SqlCharacterMaximumLength {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlNumericPrecision")]
            public string SqlNumericPrecision {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlNumericScale")]
            public string SqlNumericScale {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("SqlIsIdentity")]
            public bool SqlIsIdentity {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("ReadOnly")]
            public bool ReadOnly {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("Required")]
            public bool Required {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("ValidationName")]
            public string ValidationName {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("ValidationDisplayField")]
            public string ValidationDisplayField {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("NoDuplicate")]
            public bool NoDuplicate {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("ExportToExcel")]
            public bool ExportToExcel {get;set;}
            //---------------------------------------------------------------------------------------------
            [XmlAttribute("TabCaption")]
            public string TabCaption {get;set;}
            //---------------------------------------------------------------------------------------------
            public ColumnXml()
            {
                this.Caption                   = string.Empty;
                this.ColumnName                = string.Empty;
                this.DataType                  = string.Empty;
                this.SqlDataType               = string.Empty;
                this.SqlIsNullable             = false;
                this.SqlCharacterMaximumLength = string.Empty;
                this.SqlNumericPrecision       = string.Empty;
                this.SqlNumericScale           = string.Empty;
                this.SqlIsIdentity             = false;
                this.ReadOnly                  = false;
                this.Required                  = false;
                this.ValidationName            = string.Empty;
                this.ValidationDisplayField    = string.Empty;
                this.NoDuplicate               = false;
                this.ExportToExcel             = true;
                this.TabCaption                = string.Empty;
            }
            //---------------------------------------------------------------------------------------------
            public ColumnXml(string caption, string columnName, string dataType, string sqlDataType, bool sqlIsNullable, int sqlCharacterMaximumLength, int sqlNumericPrecision, int sqlNumericScale, bool sqlIsIdentity, bool readOnly, bool required, string validationName, string validationDisplayField, bool noDuplicate, bool exportToExcel, string tabCaption)
            {
                this.Caption                   = caption;
                this.ColumnName                = columnName;
                this.DataType                  = dataType;
                this.SqlDataType               = sqlDataType;
                this.SqlIsNullable             = sqlIsNullable;
                this.SqlCharacterMaximumLength = sqlCharacterMaximumLength.ToString();
                this.SqlNumericPrecision       = sqlNumericPrecision.ToString();
                this.SqlNumericScale           = sqlNumericScale.ToString();
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
        }
        //=============================================================================================
        [XmlRoot("Tab")]
        public class TabXml
        {
            [XmlAttribute("Caption")]
            public string Caption {get;set;}
            //---------------------------------------------------------------------------------------------
            public TabXml()
            {
                this.Caption = string.Empty;
            }
            //---------------------------------------------------------------------------------------------
            public TabXml(string caption)
            {
                this.Caption = caption;
            }
            //---------------------------------------------------------------------------------------------
        }
        //=============================================================================================
    }
}
