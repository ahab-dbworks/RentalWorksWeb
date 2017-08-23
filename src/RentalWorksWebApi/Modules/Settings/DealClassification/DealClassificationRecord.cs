﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealClassification
{
    [FwSqlTable("dealclassification")]
    public class DealClassificationRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealclassificationid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DealClassificationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealclassification", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string DealClassification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
