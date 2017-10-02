using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Modules.Home.Master;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Home.Inventory
{
    public abstract class InventoryLoader : MasterLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "noavail", modeltype: FwDataTypes.Boolean)]
        public bool NoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availmanuallyresolveconflict", modeltype: FwDataTypes.Boolean)]
        public bool AvailabilityManuallyResolveConflicts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sendavailabilityalert", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool SendAvailabilityAlert { get; set; }
        //------------------------------------------------------------------------------------ 




        [FwSqlDataField(column: "primarydimensionshipweightlbs", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionShipWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightoz", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionShipWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaselbs", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWeightInCaseLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaseoz", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWeightInCaseOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthft", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthin", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightft", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionHeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightin", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionHeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthft", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionLengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthin", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionLengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightkg", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionShipWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionshipweightg", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionShipWeightG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcasekg", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWeightInCaseKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionweightwcaseg", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWeightInCaseG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWidthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthcm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionWidthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionHeightM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightcm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionHeightCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionLengthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthcm", modeltype: FwDataTypes.Integer)]
        public int PrimaryDimensionLengthCm { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "secondarydimensionshipweightlbs", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionShipWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightoz", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionShipWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaselbs", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWeightInCaseLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaseoz", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWeightInCaseOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthft", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthin", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightft", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionHeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightin", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionHeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthft", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionLengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthin", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionLengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightkg", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionShipWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionshipweightg", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionShipWeightG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcasekg", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWeightInCaseKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionweightwcaseg", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWeightInCaseG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWidthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionwidthcm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionWidthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionHeightM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionheightcm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionHeightCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionLengthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "secondarydimensionlengthcm", modeltype: FwDataTypes.Integer)]
        public int SecondaryDimensionLengthCm { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}