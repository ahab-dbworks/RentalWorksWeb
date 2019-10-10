using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebLibrary;

namespace WebApi.Modules.Settings.Template
{
    [FwLogic(Id:"8TDUl9uGziWvw")]
    public class TemplateLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord template = new DealOrderRecord();
        TemplateLoader templateLoader = new TemplateLoader();
        public TemplateLogic()
        {
            dataRecords.Add(template);
            dataLoader = templateLoader;
            Type = RwConstants.ORDER_TYPE_TEMPLATE;

            BeforeValidate += OnBeforeValidateTemplate;
            BeforeSave += OnBeforeSaveTemplate;

        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"NlbMiAuYAHlv4", IsPrimaryKey:true)]
        public string TemplateId { get { return template.OrderId; } set { template.OrderId = value; } }

        [FwLogicProperty(Id:"5gqItS0Myeb4K", IsRecordTitle:true)]
        public string Description { get { return template.Description; } set { template.Description = value; } }

        [FwLogicProperty(Id:"foDrf1GOvTLq")]
        public string DepartmentId { get { return template.DepartmentId; } set { template.DepartmentId = value; } }

        [FwLogicProperty(Id:"8MvC3YjXBm1VG", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "x1LAc7QekCIOK")]
        public string OfficeLocationId { get { return template.OfficeLocationId; } set { template.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"LNLBhYWa4Qr5")]
        public string WarehouseId { get { return template.WarehouseId; } set { template.WarehouseId = value; } }

        [FwLogicProperty(Id:"piEJEGPoMeQQo", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"KXyBqUhWOqFl")]
        public string RateType { get { return template.RateType; } set { template.RateType = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"aD1GgT9XnimI")]
        public string Type { get { return template.Type; } set { template.Type = value; } }

        [FwLogicProperty(Id:"jQqmj2r9wRWU")]
        public bool? Rental { get { return template.Rental; } set { template.Rental = value; } }

        [FwLogicProperty(Id:"j3qkYn9krADb")]
        public bool? Sales { get { return template.Sales; } set { template.Sales = value; } }

        [FwLogicProperty(Id:"BIcFPBUmohyL")]
        public bool? Miscellaneous { get { return template.Miscellaneous; } set { template.Miscellaneous = value; } }

        [FwLogicProperty(Id:"IX3lsSSlG8Ra")]
        public bool? Labor { get { return template.Labor; } set { template.Labor = value; } }

        [FwLogicProperty(Id:"ZNf2qHcufo6D")]
        public bool? Facilities { get { return template.Facilities; } set { template.Facilities = value; } }

        [FwLogicProperty(Id:"9nGVlwOywOOQ")]
        public bool? Transportation { get { return template.Transportation; } set { template.Transportation = value; } }

        [FwLogicProperty(Id:"chw7HQwo29eb6", IsReadOnly:true)]
        public int? Lines { get; set; }

        [FwLogicProperty(Id:"toXyqBSvziLJ")]
        public string DateStamp { get { return template.DateStamp; } set { template.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        private void OnBeforeValidateTemplate(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                //fields are required for a order - tempoarary
                OfficeLocationId = "X";
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveTemplate(object sender, BeforeSaveEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert) && (OfficeLocationId.Equals("X")))
            {
                //fields are required for a order - tempoarary
                OfficeLocationId = "";
            }
        }
        //------------------------------------------------------------------------------------
    }
}
