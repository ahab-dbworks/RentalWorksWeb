﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.WardrobeGender
{
    [FwSqlTable("gender")]
    public class WardrobeGenderRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "genderid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string WardrobeGenderId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string WardrobeGender { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
