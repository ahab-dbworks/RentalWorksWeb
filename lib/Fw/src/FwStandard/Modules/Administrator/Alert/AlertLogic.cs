using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertCondition;
using System.Collections.Generic;

namespace FwStandard.Modules.Administrator.Alert
{
    public class AlertCondition
    {
        public string AlertConditionId { get; set; }
        public string AlertId { get; set; }
        public string FieldName { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
    }

    [FwLogic(Id: "uDfoWwYV6HwI")]
    public class AlertLogic : FwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AlertRecord alert = new AlertRecord();
        public AlertLogic()
        {
            dataRecords.Add(alert);
            AfterSave += OnAfterSave;
            AfterDelete += OnAfterDeleteAlert;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "6c9PL4mHxpfO", IsPrimaryKey:true)]
        public string AlertId { get { return alert.AlertId; } set { alert.AlertId = value; } }
        [FwLogicProperty(Id: "QlcNGC0VsxNE", IsRecordTitle:true)]
        public string AlertName { get { return alert.AlertName; } set { alert.AlertName = value; } }
        [FwLogicProperty(Id: "iL9L6gQI0Bh6")]
        public string ModuleName { get { return alert.ModuleName; } set { alert.ModuleName = value; } }
        [FwLogicProperty(Id: "QlDlFJ3f12N")]
        public string AlertSubject { get { return alert.AlertSubject; } set { alert.AlertSubject = value; } }
        [FwLogicProperty(Id: "9aGNy3nvxSx")]
        public string AlertBody { get { return alert.AlertBody; } set { alert.AlertBody = value; } }
        [FwLogicProperty(Id: "bOJyRGKEFtWo")]
        public bool? Inactive { get { return alert.Inactive; } set { alert.Inactive = value; } }
        [FwLogicProperty(Id: "bXWbP0NW8cY", IsNotAudited: true)]
        public List<AlertCondition> AlertConditionList { get; set; }
        [FwLogicProperty(Id: "d3w1qNkDKUVBP")]
        public string DateStamp { get { return alert.DateStamp; } set { alert.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            AlertFunc.RefreshAlerts(AppConfig);
            List<AlertConditionLogic> previousConditionData = new List<AlertConditionLogic>();

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                AlertConditionLogic acLoader = new AlertConditionLogic();
                acLoader.SetDependencies(AppConfig, UserSession);
                acLoader.AlertId = AlertId;
                BrowseRequest request = new BrowseRequest();
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                uniqueIds.Add("AlertId", AlertId);
                request.uniqueids = uniqueIds;
                previousConditionData = acLoader.SelectAsync<AlertConditionLogic>(request).Result;
            }

            // iterate through the PREVIOUS list.  compare each entry with the ones provided by the user.  If the amount is changed, we need to save the modifications to the database
            foreach (AlertConditionLogic acPrev in previousConditionData)
            {
                foreach (AlertCondition acNew in AlertConditionList)  // iterate through the list provided by the user
                {
                    if (acPrev.AlertConditionId.ToString().Equals(acNew.AlertConditionId)) // find the matching record
                    {
                        if ((!acPrev.FieldName.Equals(acNew.FieldName)) || (!acPrev.Condition.Equals(acNew.Condition)) || (!acPrev.Value.Equals(acNew.Value)))
                        {
                            AlertConditionLogic cNew = acPrev.MakeCopy<AlertConditionLogic>();
                            cNew.FieldName = acNew.FieldName;
                            cNew.Condition = acNew.Condition;
                            cNew.Value = acNew.Value;
                            cNew.SetDependencies(AppConfig, UserSession);
                            int saveCount = cNew.SaveAsync(acPrev, conn: e.SqlConnection).Result;
                        }
                    }
                }
            }

            // iterate through the NEW list.  anything without an AlertConditionId is new, we need to save these
            foreach (AlertCondition ac in AlertConditionList)
            {
                if ((string.IsNullOrEmpty(ac.AlertConditionId)))
                {
                    AlertConditionLogic acNew = new AlertConditionLogic();
                    acNew.SetDependencies(AppConfig, UserSession);
                    acNew.AlertId = AlertId;
                    acNew.FieldName = ac.FieldName;
                    acNew.Condition = ac.Condition;
                    acNew.Value = ac.Value;
                    acNew.SetDependencies(AppConfig, UserSession);
                    int saveCount = acNew.SaveAsync(null, conn: e.SqlConnection).Result;
                    ac.AlertConditionId = acNew.AlertConditionId.ToString();
                }
            }
        }
        public void OnAfterDeleteAlert(object sender, AfterDeleteEventArgs e)
        {
            AlertFunc.RefreshAlerts(AppConfig);
        }
    }
}
