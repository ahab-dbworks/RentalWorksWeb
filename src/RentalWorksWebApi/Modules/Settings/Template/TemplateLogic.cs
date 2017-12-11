using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;

namespace WebApi.Modules.Settings.Template
{
    public class TemplateLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord template = new DealOrderRecord();
        TemplateLoader templateLoader = new TemplateLoader();
        public TemplateLogic()
        {
            dataRecords.Add(template);
            dataLoader = templateLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string TemplateId { get { return template.OrderId; } set { template.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return template.Description; } set { template.Description = value; } }
        public string DepartmentId { get { return template.DepartmentId; } set { template.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string WarehouseId { get { return template.WarehouseId; } set { template.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [JsonIgnore]
        public string Type { get { return template.Type; } set { template.Type = value; } }
        public bool? Rental { get { return template.Rental; } set { template.Rental = value; } }
        public bool? Sales { get { return template.Sales; } set { template.Sales = value; } }
        public bool? Miscellaneous { get { return template.Miscellaneous; } set { template.Miscellaneous = value; } }
        public bool? Labor { get { return template.Labor; } set { template.Labor = value; } }
        public bool? Facilities { get { return template.Facilities; } set { template.Facilities = value; } }
        public bool? Transportation { get { return template.Transportation; } set { template.Transportation = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Lines { get; set; }
        public string DateStamp { get { return template.DateStamp; } set { template.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            Type = "M";
        }
        //------------------------------------------------------------------------------------
    }
}