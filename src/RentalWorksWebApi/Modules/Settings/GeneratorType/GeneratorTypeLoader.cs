﻿using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Modules.Settings.VehicleType;

namespace RentalWorksWebApi.Modules.Settings.GeneratorType
{
    public class GeneratorTypeLoader : VehicleTypeBaseLoader 
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GeneratorTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string GeneratorType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmcycleperiod", modeltype: FwDataTypes.Integer)]
        public int? PreventiveMaintenanceCycleHours { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(vehicletype = 'GENERATOR')");
        }
        //------------------------------------------------------------------------------------
    }
}
