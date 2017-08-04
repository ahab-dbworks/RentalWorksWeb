﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.PoClassification
{
    [FwSqlTable("poclassification")]
    public class PoClassificationRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poclassificationid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PoClassificationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poclassification", modeltype: FwDataTypes.Text, maxlength: 40)]
        public string PoClassification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "excludefromroa", modeltype: FwDataTypes.Boolean)]
        public bool ExcludeFromRoa { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactiveflg", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
