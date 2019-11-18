using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Utilities.BrowseActiveViewFields
{
    [FwLogic(Id: "0gbWXsuKZuZHC")]
    public class BrowseActiveViewFieldsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BrowseActiveViewFieldsRecord browseActiveViewFields = new BrowseActiveViewFieldsRecord();
        public BrowseActiveViewFieldsLogic()
        {
            dataRecords.Add(browseActiveViewFields);
            HasAudit = false;

            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "wAsUeb8VpiUq", IsPrimaryKey: true)]
        public int? Id { get { return browseActiveViewFields.Id; } set { browseActiveViewFields.Id = value; } }
        [FwLogicProperty(Id: "es9T2LFwIqm0")]
        public string WebUserId { get { return browseActiveViewFields.WebUserId; } set { browseActiveViewFields.WebUserId = value; } }
        [FwLogicProperty(Id: "aKirfbKY3affQ")]
        public string OfficeLocationId { get { return browseActiveViewFields.OfficeLocationId; } set { browseActiveViewFields.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "nJf8RP0xmvDf")]
        public string ModuleName { get { return browseActiveViewFields.ModuleName; } set { browseActiveViewFields.ModuleName = value; } }
        [FwLogicProperty(Id: "xqie0ki1JOdZl")]
        public string ActiveViewFields { get { return browseActiveViewFields.ActiveViewFields; } set { browseActiveViewFields.ActiveViewFields = value; } }
        [FwLogicProperty(Id: "l2zexVFqDjTd")]
        public string DateStamp { get { return browseActiveViewFields.DateStamp; } set { browseActiveViewFields.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                browseActiveViewFields.DeleteOthers(e.SqlConnection);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
