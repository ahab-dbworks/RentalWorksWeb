using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi;

namespace WebApi.Modules.Settings.SubCategory
{
    [FwSqlTable("subcategoryview")]
    public class SubCategoryLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SubCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string TypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer)]
        public int? PickListOrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected void addRecTypeToSelect(string filterFieldName, FwSqlSelect select, BrowseRequest request = null)
        {
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey(filterFieldName))
                {
                    string recTypeValue = string.Empty;
                    switch (filterFieldName)
                    {
                        case "Rental":
                            recTypeValue = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
                            break;
                        case "Sales":
                            recTypeValue = RwConstants.INVENTORY_AVAILABLE_FOR_SALE;
                            break;
                        case "Parts":
                            recTypeValue = RwConstants.INVENTORY_AVAILABLE_FOR_PARTS;
                            break;
                        case "Misc":
                            recTypeValue = RwConstants.RATE_AVAILABLE_FOR_MISC;
                            break;
                        case "Labor":
                            recTypeValue = RwConstants.RATE_AVAILABLE_FOR_LABOR;
                            break;
                        default:
                            recTypeValue = string.Empty;
                            break;
                    }

                    string paramName = "@rectype" + filterFieldName;

                    if ((bool)uniqueIds[filterFieldName])
                    {
                        select.AddWhere("rectype = " + paramName);
                    }
                    else
                    {
                        select.AddWhere("rectype <> " + paramName);
                    }
                    select.AddParameter(paramName, recTypeValue);
                }

            }
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("CategoryId", "categoryid", select, request);
            addFilterToSelect("TypeId", "inventorydepartmentid", select, request);
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
            addFilterToSelect("RecType", "rectype", select, request);

            addRecTypeToSelect("Rental",  select, request);
            addRecTypeToSelect("Sales", select, request);
            addRecTypeToSelect("Parts", select, request);
            addRecTypeToSelect("Misc", select, request);
            addRecTypeToSelect("Labor", select, request);

        }
        //------------------------------------------------------------------------------------
    }
}
