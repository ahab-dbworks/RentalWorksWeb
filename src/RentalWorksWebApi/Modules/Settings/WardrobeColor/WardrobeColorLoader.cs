﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.WardrobeColor
{
    [FwSqlTable("wardrobecolorview")]
    public class WardrobeColorLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "colorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WardrobeColorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Text)]
        public string WardrobeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "colortype", modeltype: FwDataTypes.Text)]
        public string ColorType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
