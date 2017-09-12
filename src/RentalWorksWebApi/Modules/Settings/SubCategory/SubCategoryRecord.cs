﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.SubCategory
{
    [FwSqlTable("subcategory")]
    public class SubCategoryRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string SubCategoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text, maxlength: 25, required: true)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string InventoryCategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, precision: 5, scale: 1)]
        public decimal OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderbypicklist", modeltype: FwDataTypes.Integer)]
        public int PickListOrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
