using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Agent.Quote;
using WebApi.Modules.HomeControls.DealOrder;
using WebApi.Modules.HomeControls.DealOrderDetail;
using WebLibrary;

namespace WebApi.Modules.Agent.Project
{
    [FwLogic(Id:"HgbJcfzk4tG14")]
    public class ProjectLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord project = new DealOrderRecord();
        DealOrderDetailRecord projectDetail = new DealOrderDetailRecord();
        ProjectLoader projectLoader = new ProjectLoader();
        public ProjectLogic()
        {
            dataRecords.Add(project);
            dataRecords.Add(projectDetail);
            dataLoader = projectLoader;

            Type = RwConstants.ORDER_TYPE_PROJECT;


            BeforeValidate += BeforeValidateProject;
            BeforeSave += OnBeforeSave;
            project.BeforeSave += OnBeforeSaveProjectRecord;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"Msb0GmJEAILYZ", IsPrimaryKey:true)]
        public string ProjectId { get { return project.OrderId; } set { project.OrderId = value; projectDetail.OrderId = value; } }

        [FwLogicProperty(Id:"Msb0GmJEAILYZ", IsRecordTitle:true)]
        public string ProjectNumber { get { return project.OrderNumber; } set { project.OrderNumber = value; } }

        [FwLogicProperty(Id:"Msb0GmJEAILYZ", IsRecordTitle:true)]
        public string Project { get { return project.Description; } set { project.Description = value; } }

        [FwLogicProperty(Id:"qDdffRVdbpaB")]
        public string OfficeLocationId { get { return project.OfficeLocationId; } set { project.OfficeLocationId = value; } }

        [FwLogicProperty(Id:"dBrjdGmCYBnGU", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"AJiFvsw2cX1M")]
        public string WarehouseId { get { return project.WarehouseId; } set { project.WarehouseId = value; } }

        [FwLogicProperty(Id:"ci1uTvqcZuleu", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"XFyPTTQMxg40")]
        public string DepartmentId { get { return project.DepartmentId; } set { project.DepartmentId = value; } }

        [FwLogicProperty(Id:"CbLceIjQNKBKf", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"tYiXTtAKrUM8")]
        public string DealId { get { return project.DealId; } set { project.DealId = value; } }

        [FwLogicProperty(Id:"9nEC8o1Qv0Jvn", IsReadOnly:true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id:"V4VWJmnvpExV")]
        public string Status { get { return project.Status; } set { project.Status = value; } }

        [FwLogicProperty(Id:"fystLmxaQ1zr")]
        public string StatusDate { get { return project.StatusDate; } set { project.StatusDate = value; } }

        [FwLogicProperty(Id:"Zc0eNVIwEboR")]
        public string ProjectManagerId { get { return project.ProjectManagerId; } set { project.ProjectManagerId = value; } }

        [FwLogicProperty(Id:"Msb0GmJEAILYZ", IsReadOnly:true)]
        public string ProjectManager { get; set; }

        [FwLogicProperty(Id:"8vX0Oqx4twI6")]
        public string AgentId { get { return project.AgentId; } set { project.AgentId = value; } }

        [FwLogicProperty(Id:"dJdvfx4JN9OXz", IsReadOnly:true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id:"v7NncLlWsF02I", IsReadOnly:true)]
        public string PrimaryContact { get; set; }

        [FwLogicProperty(Id:"AxUzPACUSgENF", IsReadOnly:true)]
        public string RequestedBy { get; set; }

        [FwLogicProperty(Id:"k5aGNphTO44X")]
        public string OutsideSalesRepresentativeId { get { return projectDetail.OutsideSalesRepresentativeId; } set { projectDetail.OutsideSalesRepresentativeId = value; } }

        [FwLogicProperty(Id:"dde25PvjdfMCx", IsReadOnly:true)]
        public string OutsideSalesRepresentative { get; set; }

        [FwLogicProperty(Id:"Msb0GmJEAILYZ", IsReadOnly:true)]
        public string ProjectDescription { get { return project.FromWarehouseNotes; } set { project.FromWarehouseNotes = value; } }

        [FwLogicProperty(Id:"x3RoQTrgQRrO")]
        public bool? Rental { get { return project.Rental; } set { project.Rental = value; } }

        [FwLogicProperty(Id:"niwdaLZY6ieY")]
        public bool? Sales { get { return project.Sales; } set { project.Sales = value; } }

        [FwLogicProperty(Id:"UGDygBOAvboK")]
        public bool? Facilities { get { return project.Facilities; } set { project.Facilities = value; } }

        [FwLogicProperty(Id:"qTRNVe9fybmQ")]
        public bool? Labor { get { return project.Labor; } set { project.Labor = value; } }

        [FwLogicProperty(Id:"PibmfqCevAcH")]
        public bool? Miscellaneous { get { return project.Miscellaneous; } set { project.Miscellaneous = value; } }

        [FwLogicProperty(Id:"JXyrb1LxBxPD")]
        public bool? Transportation { get { return project.Transportation; } set { project.Transportation = value; } }

        //[FwLogicProperty(Id:"MZ5k0Hb8FeZ4")]
        //public bool? RentalSale { get { return project.RentalSale; } set { project.RentalSale = value; } }

        [FwLogicProperty(Id:"plwsN6FoWcZY")]
        public string PickDate { get { return project.PickDate; } set { project.PickDate = value; } }

        [FwLogicProperty(Id:"0zyKtBAAxr8G")]
        public string PickTime { get { return project.PickTime; } set { project.PickTime = value; } }

        [FwLogicProperty(Id:"BFbG1wYTjg6d")]
        public string EstimatedStartDate { get { return project.EstimatedStartDate; } set { project.EstimatedStartDate = value; } }

        [FwLogicProperty(Id:"JGdWPd0cAtyg")]
        public string EstimatedStartTime { get { return project.EstimatedStartTime; } set { project.EstimatedStartTime = value; } }

        [FwLogicProperty(Id:"DtARW0y8Vm7i")]
        public string EstimatedStopDate { get { return project.EstimatedStopDate; } set { project.EstimatedStopDate = value; } }

        [FwLogicProperty(Id:"GSkKFFZruR7u")]
        public string EstimatedStopTime { get { return project.EstimatedStopTime; } set { project.EstimatedStopTime = value; } }

        [FwLogicProperty(Id:"SA8ydsY2RJLr")]
        public bool? CcPrimaryApproverWhenEmailingBackupApprover { get { return projectDetail.CcPrimaryApproverWhenEmailingBackupApprover; } set { projectDetail.CcPrimaryApproverWhenEmailingBackupApprover = value; } }

        [FwLogicProperty(Id:"uYl1I9L2kyAQ")]
        public bool? Inactive { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"45RVbc0emq1I")]
        public string Type { get { return project.Type; } set { project.Type = value; } }

        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"Q5poiu6FFxri")]
        public string DateStamp { get { return project.DateStamp; } set { project.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        private void BeforeValidateProject(object sender, BeforeValidateEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(Status))
                {
                    Status = RwConstants.PROJECT_STATUS_NEW;
                }
            }
        }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(ProjectLogic).GetProperty(nameof(ProjectLogic.Status));
                string[] acceptableValues = { RwConstants.PROJECT_STATUS_NEW, RwConstants.PROJECT_STATUS_ACTIVE, RwConstants.PROJECT_STATUS_CLOSED };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                //OrderDate = FwConvert.ToString(DateTime.Today);
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveProjectRecord(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = project.SetNumber(e.SqlConnection).Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
            }
            else
            {
                //ProjectLogic l2 = null;
                //l2 = new ProjectLogic();
                //l2.SetDependencies(this.AppConfig, this.UserSession);
                //l2.ProjectId = ProjectId;
                //bool b = l2.LoadAsync<ProjectLogic>().Result;
                ////if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
                //{
                //    tax.TaxId = l2.TaxId;
                //}
                //if (string.IsNullOrEmpty(OutDeliveryId))
                //{
                //    OutDeliveryId = l2.OutDeliveryId;
                //}
                //if (string.IsNullOrEmpty(InDeliveryId))
                //{
                //    InDeliveryId = l2.InDeliveryId;
                //}
            }

        }
        //------------------------------------------------------------------------------------        
    }
}
