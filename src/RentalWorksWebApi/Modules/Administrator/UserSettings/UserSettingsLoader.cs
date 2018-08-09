using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApi.Modules.Administrator.UserSettings
{
    [FwSqlTable("dbo.funcwebusersettings2(@webusersid)")]
    public class UserSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "browsedefaultrows", modeltype: FwDataTypes.Text)]
        public string BrowseDefaultRows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applicationtheme", modeltype: FwDataTypes.Text)]
        public string ApplicationTheme { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successsoundid", modeltype: FwDataTypes.Text)]
        public string SuccessSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successsound", modeltype: FwDataTypes.Text)]
        public string SuccessSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "successsoundfilename", modeltype: FwDataTypes.Text)]
        public string SuccessSoundFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorsoundid", modeltype: FwDataTypes.Text)]
        public string ErrorSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorsound", modeltype: FwDataTypes.Text)]
        public string ErrorSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "errorsoundfilename", modeltype: FwDataTypes.Text)]
        public string ErrorSoundFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationsoundid", modeltype: FwDataTypes.Text)]
        public string NotificationSoundId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationsound", modeltype: FwDataTypes.Text)]
        public string NotificationSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notificationsoundfilename", modeltype: FwDataTypes.Text)]
        public string NotificationSoundFileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(username > '')");
            //addFilterToSelect("LocationId", "locationid", select, request);
            //addFilterToSelect("WarehouseId", "warehouseid", select, request);
            //addFilterToSelect("GroupId", "groupsid", select, request);

            if (!string.IsNullOrEmpty(UserId))
            {
                select.AddParameter("@webusersid", UserId);
            }


        }
        //------------------------------------------------------------------------------------ 
    }
}