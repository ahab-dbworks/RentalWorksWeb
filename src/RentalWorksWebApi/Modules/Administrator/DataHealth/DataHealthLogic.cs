using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Administrator.DataHealth
{
    [FwLogic(Id: "SgSLHzluaM9Kz")]
    public class DataHealthLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DataHealthRecord dataHealth = new DataHealthRecord();
        DataHealthLoader dataHealthLoader = new DataHealthLoader();
        DataHealthBrowseLoader dataHealthBrowseLoader = new DataHealthBrowseLoader();
        public DataHealthLogic()
        {
            dataRecords.Add(dataHealth);
            dataLoader = dataHealthLoader;
            browseLoader = dataHealthBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2MVI0lynHBsO", IsPrimaryKey: true)]
        public int? DataHealthId { get { return dataHealth.DataHealthId; } set { dataHealth.DataHealthId = value; } }

        [FwLogicProperty(Id: "aC3PyMIjJ0FV", IsRecordTitle: true)]
        public string DataHealthType { get { return dataHealth.DataHealthType; } set { dataHealth.DataHealthType = value; } }

        [FwLogicProperty(Id: "qY9Y4gZZOqoI")]
        public string CaptureDateTime { get { return dataHealth.CaptureDateTime; } set { dataHealth.CaptureDateTime = value; } }

        [FwLogicProperty(Id: "lJyFix5gV5rV0", IsReadOnly: true)]
        public string CaptureDate { get; set; }

        [FwLogicProperty(Id: "qkT1ZbF4CUDI")]
        public string Json { get { return dataHealth.Json; } set { dataHealth.Json = value; } }

        [FwLogicProperty(Id: "nuJsdFyLNdOy9")]
        public string Severity { get { return dataHealth.Severity; } set { dataHealth.Severity = value; } }

        [FwLogicProperty(Id: "T0T66uoZ76zaj", IsReadOnly: true)]
        public string SeverityColor { get; set; }

        [FwLogicProperty(Id: "S5UrLHX24aGx")]
        public string Notes { get { return dataHealth.Notes; } set { dataHealth.Notes = value; } }

        [FwLogicProperty(Id: "A3KHXh6Khvwc")]
        public bool? Resolved { get { return dataHealth.Resolved; } set { dataHealth.Resolved = value; } }

        [FwLogicProperty(Id: "BwOqYK2OWXMCY", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        //------------------------------------------------------------------------------------
    }
}
