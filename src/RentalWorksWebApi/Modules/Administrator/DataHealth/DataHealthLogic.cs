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
        public DataHealthLogic()
        {
            dataRecords.Add(dataHealth);
            dataLoader = dataHealthLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "2MVI0lynHBsO", IsPrimaryKey: true)]
        public int? DataHealthId { get { return dataHealth.DataHealthId; } set { dataHealth.DataHealthId = value; } }

        [FwLogicProperty(Id: "aC3PyMIjJ0FV")]
        public string DataHealthType { get { return dataHealth.DataHealthType; } set { dataHealth.DataHealthType = value; } }

        [FwLogicProperty(Id: "qY9Y4gZZOqoI")]
        public string CaptureDateTime { get { return dataHealth.CaptureDateTime; } set { dataHealth.CaptureDateTime = value; } }

        [FwLogicProperty(Id: "qkT1ZbF4CUDI")]
        public string Json { get { return dataHealth.Json; } set { dataHealth.Json = value; } }

        [FwLogicProperty(Id: "S5UrLHX24aGx")]
        public string Notes { get { return dataHealth.Notes; } set { dataHealth.Notes = value; } }

        [FwLogicProperty(Id: "A3KHXh6Khvwc")]
        public bool? Resolved { get { return dataHealth.Resolved; } set { dataHealth.Resolved = value; } }
        //------------------------------------------------------------------------------------
    }
}
