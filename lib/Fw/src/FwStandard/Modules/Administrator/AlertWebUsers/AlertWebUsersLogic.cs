using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;

namespace FwStandard.Modules.Administrator.AlertWebUsers
{
    [FwLogic(Id: "ffGajJFtaGnS")]
    public class AlertWebUsersLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AlertWebUsersRecord alertWebUsers = new AlertWebUsersRecord();
        AlertWebUsersLoader alertWebUsersLoader = new AlertWebUsersLoader();
        public AlertWebUsersLogic()
        {
            dataRecords.Add(alertWebUsers);
            dataLoader = alertWebUsersLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "j9k9lMzO90EO", IsPrimaryKey: true)]
        public string AlertWebUserId { get { return alertWebUsers.AlertWebUserId; } set { alertWebUsers.AlertWebUserId = value; } }
        [FwLogicProperty(Id: "fnAzFYyhDHbz")]
        public string AlertId { get { return alertWebUsers.AlertId; } set { alertWebUsers.AlertId = value; } }
        [FwLogicProperty(Id: "xjdUQ60z7AWZ", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "AyUDxTSjiAaV")]
        public string WebUserId { get { return alertWebUsers.WebUserId; } set { alertWebUsers.WebUserId = value; } }
        [FwLogicProperty(Id: "o7U2C9NdNY67", IsReadOnly: true)]
        public string UserId { get; set; }
        [FwLogicProperty(Id: "SRc4NrYe1lN", IsReadOnly: true)]
        public string UserName { get; set; }
        [FwLogicProperty(Id: "3mPYmmmN8C1H", IsReadOnly: true)]
        public string Email { get; set; }
        [FwLogicProperty(Id: "IxC6HRHoR3FZ")]
        public string DateStamp { get { return alertWebUsers.DateStamp; } set { alertWebUsers.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if ((string.IsNullOrEmpty(WebUserId)) && (!string.IsNullOrEmpty(UserId)))
            {
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    WebUserId = FwSqlCommand.GetStringDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "webusers", "usersid", UserId, "webusersid").Result;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
