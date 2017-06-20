using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FwCore.ValueTypes
{
    // this class provides a nicer api for FwApplicationSchemaXml
    public partial class FwApplicationSchema
    {
        //---------------------------------------------------------------------------------------------
        public static FwApplicationSchema Current {get;set;}
        public Dictionary<string, Module> Modules {get;private set;}
        public Dictionary<string, Grid> Grids {get;private set;}
        public Dictionary<string, Validation> Validations {get;private set;}
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema()
        {
            this.Modules     = new Dictionary<string,Module>();
            this.Grids       = new Dictionary<string,Grid>();
            this.Validations = new Dictionary<string,Validation>();
        }
        //---------------------------------------------------------------------------------------------
        public static Dictionary<string, Column> GetColumns(List<FwApplicationSchemaXml.ColumnXml> columnsXml)
        {
            Dictionary<string,Column> columns;

            columns   = new Dictionary<string,Column>();
            if (columnsXml != null)
            {
                foreach(var columnXml in columnsXml)
                {
                    Column column;
                    string caption, columnName, dataType, sqlDataType, validationName, validationDisplayField, tabCaption;
                    bool isNullable;
                    int characterMaximumLength, numericPrecision, numericScale;
                    bool required, isIdentity, readOnly, noDuplicate, exportToExcel;
                        
                    caption                 = columnXml.Caption;
                    columnName              = columnXml.ColumnName;
                    dataType                = columnXml.DataType;
                    sqlDataType             = columnXml.SqlDataType;
                    isNullable              = columnXml.SqlIsNullable;
                    characterMaximumLength  = Convert.ToInt32(columnXml.SqlCharacterMaximumLength);
                    numericPrecision        = Convert.ToInt32(columnXml.SqlNumericPrecision);
                    numericScale            = Convert.ToInt32(columnXml.SqlNumericScale);
                    isIdentity              = columnXml.SqlIsIdentity;
                    readOnly                = columnXml.ReadOnly;
                    required                = columnXml.Required;
                    validationName          = columnXml.ValidationName;
                    validationDisplayField  = columnXml.ValidationDisplayField;
                    noDuplicate             = columnXml.NoDuplicate;
                    exportToExcel           = columnXml.ExportToExcel;
                    tabCaption              = columnXml.TabCaption;
                    column                  = new Column(caption, columnName, dataType, sqlDataType, isNullable, characterMaximumLength, numericPrecision, numericScale, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                    columns[columnName]     = column;
                }
            }

            return columns;
        }
        //---------------------------------------------------------------------------------------------
        // loads the serialization class FwApplicationSchemaXml into this class, so we can provide a better structure of the data to program against 
        // (Dictionaries don't serialize well)
        public static void Load(string path)
        {
            FwApplicationSchemaXml schemaXml;
            FwApplicationSchema schema;
            string xml;
            XmlSerializer serializer;
            StringReader reader;
            
            FwApplicationSchema.Current = new FwApplicationSchema();
            xml        = File.ReadAllText(path);
            serializer = new XmlSerializer(typeof(FwApplicationSchemaXml));
            reader     = new StringReader(xml);
            schemaXml  = (FwApplicationSchemaXml)serializer.Deserialize(reader);
            schema     = new FwApplicationSchema();
            
            // load the modules from the xml class
            foreach(FwApplicationSchemaXml.ModuleXml moduleXml in schemaXml.Modules)
            {
                schema.Modules[moduleXml.ModuleName] = moduleXml.ToModule();
            }
            
            // load the grids from the xml class
            schema.Grids = new Dictionary<string,Grid>();
            foreach(FwApplicationSchemaXml.GridXml gridXml in schemaXml.Grids)
            {
                schema.Grids[gridXml.GridName] = gridXml.ToGrid();
            }
            
            // load the validations from the xml class
            schema.Validations = new Dictionary<string,Validation>();
            foreach(FwApplicationSchemaXml.ValidationXml validationXml in schemaXml.Validations)
            {
                schema.Validations[validationXml.ValidationName] = validationXml.ToValidation();
            }
            
            FwApplicationSchema.Current = schema;
        }
        //---------------------------------------------------------------------------------------------
        public static List<FwApplicationSchemaXml.ModuleXml> GetModulesXml(Dictionary<string,Module> modules)
        {
            List<FwApplicationSchemaXml.ModuleXml> modulesXml;

            modulesXml = new List<FwApplicationSchemaXml.ModuleXml>();
            foreach(var moduleItem in modules)
            {
                Module module;
                FwApplicationSchemaXml.ModuleXml moduleXml;

                module    = moduleItem.Value;
                moduleXml = module.ToModuleXml();
                modulesXml.Add(moduleXml);
            }

            return modulesXml;
        }
        //---------------------------------------------------------------------------------------------
        public static List<FwApplicationSchemaXml.GridXml> GetGridsXml(Dictionary<string,Grid> grids)
        {
            List<FwApplicationSchemaXml.GridXml> gridsXml;

            gridsXml = new List<FwApplicationSchemaXml.GridXml>();
            if (grids != null)
            {
                foreach(var gridItem in grids)
                {
                    gridsXml.Add(gridItem.Value.ToGridXml());
                }
            }

            return gridsXml;
        }
        //---------------------------------------------------------------------------------------------
        public static List<FwApplicationSchemaXml.ValidationXml> GetValidationsXml(Dictionary<string,Validation> validations)
        {
            List<FwApplicationSchemaXml.ValidationXml> validationsXml;

            validationsXml = new List<FwApplicationSchemaXml.ValidationXml>();
            if (validations != null)
            {
                foreach(var validationItem in validations)
                {
                    validationsXml.Add(validationItem.Value.ToValidationXml());
                }
            }

            return validationsXml;
        }
        //---------------------------------------------------------------------------------------------
        public static List<FwApplicationSchemaXml.ColumnXml> GetColumnsXml(Dictionary<string,FwApplicationSchema.Column> columns)
        {
            List<FwApplicationSchemaXml.ColumnXml> columnsXml;

            columnsXml   = new List<FwApplicationSchemaXml.ColumnXml>();
            if (columns != null)
            {
                foreach(var columnItem in columns)
                {
                    columnsXml.Add(columnItem.Value.ToColumnXml());
                }
            }

            return columnsXml;
        }
        //---------------------------------------------------------------------------------------------
        public void Save(string path)
        {
            XmlSerializer serializer;
            StringBuilder sb;

            sb         = new StringBuilder();
            serializer = new XmlSerializer(typeof(FwApplicationSchemaXml));
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, this.ToFwApplicationSchemaXml());
            }
            File.WriteAllText(path, sb.ToString());
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchemaXml ToFwApplicationSchemaXml()
        {
            FwApplicationSchemaXml schemaXml;
            List<FwApplicationSchemaXml.ModuleXml> modulesXml;
            List<FwApplicationSchemaXml.GridXml> gridsXml;
            List<FwApplicationSchemaXml.ValidationXml> validationsXml;
            
            modulesXml     = GetModulesXml(this.Modules);
            gridsXml       = GetGridsXml(this.Grids);
            validationsXml = GetValidationsXml(this.Validations);
            schemaXml      = new FwApplicationSchemaXml(modulesXml, gridsXml, validationsXml);
            
            return schemaXml;
        }
        //---------------------------------------------------------------------------------------------
    }
}
