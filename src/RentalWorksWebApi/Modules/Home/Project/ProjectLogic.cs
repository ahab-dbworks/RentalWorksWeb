using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebApi.Modules.Home.Quote;
using WebLibrary;

namespace WebApi.Modules.Home.Project
{
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
            project.AfterSave += OnAfterSaveProjectRecord;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectId { get { return project.OrderId; } set { project.OrderId = value; projectDetail.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ProjectNumber { get { return project.OrderNumber; } set { project.OrderNumber = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Project { get { return project.Description; } set { project.Description = value; } }
        public string OfficeLocationId { get { return project.OfficeLocationId; } set { project.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string WarehouseId { get { return project.WarehouseId; } set { project.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public string DepartmentId { get { return project.DepartmentId; } set { project.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string DealId { get { return project.DealId; } set { project.DealId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        public string Status { get { return project.Status; } set { project.Status = value; } }
        public string StatusDate { get { return project.StatusDate; } set { project.StatusDate = value; } }
        public string ProjectManagerId { get { return project.ProjectManagerId; } set { project.ProjectManagerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManager { get; set; }
        public string AgentId { get { return project.AgentId; } set { project.AgentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryContact { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RequestedBy { get; set; }
        public string SalesRepresentativeId { get { return projectDetail.SalesRepresentativeId; } set { projectDetail.SalesRepresentativeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesRepresentative { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectDescription { get { return project.FromWarehouseNotes; } set { project.FromWarehouseNotes = value; } }
        public bool? Rental { get { return project.Rental; } set { project.Rental = value; } }
        public bool? Sales { get { return project.Sales; } set { project.Sales = value; } }
        public bool? Facilities { get { return project.Facilities; } set { project.Facilities = value; } }
        public bool? Labor { get { return project.Labor; } set { project.Labor = value; } }
        public bool? Miscellaneous { get { return project.Miscellaneous; } set { project.Miscellaneous = value; } }
        public bool? Transportation { get { return project.Transportation; } set { project.Transportation = value; } }
        //public bool? RentalSale { get { return project.RentalSale; } set { project.RentalSale = value; } }
        public string PickDate { get { return project.PickDate; } set { project.PickDate = value; } }
        public string PickTime { get { return project.PickTime; } set { project.PickTime = value; } }
        public string EstimatedStartDate { get { return project.EstimatedStartDate; } set { project.EstimatedStartDate = value; } }
        public string EstimatedStartTime { get { return project.EstimatedStartTime; } set { project.EstimatedStartTime = value; } }
        public string EstimatedStopDate { get { return project.EstimatedStopDate; } set { project.EstimatedStopDate = value; } }
        public string EstimatedStopTime { get { return project.EstimatedStopTime; } set { project.EstimatedStopTime = value; } }
        public bool? CcPrimaryApproverWhenEmailingBackupApprover { get { return projectDetail.CcPrimaryApproverWhenEmailingBackupApprover; } set { projectDetail.CcPrimaryApproverWhenEmailingBackupApprover = value; } }
        public bool? Inactive { get; set; }
        [JsonIgnore]
        public string Type { get { return project.Type; } set { project.Type = value; } }
        //------------------------------------------------------------------------------------
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
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
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
        public void OnBeforeSaveProjectRecord(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = project.SetNumber().Result;
                StatusDate = FwConvert.ToString(DateTime.Today);
            }
            else
            {
                ProjectLogic l2 = null;
                l2 = new ProjectLogic();
                l2.SetDependencies(this.AppConfig, this.UserSession);
                l2.ProjectId = ProjectId;
                bool b = l2.LoadAsync<ProjectLogic>().Result;
                //if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
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
        public void OnAfterSaveProjectRecord(object sender, AfterSaveEventArgs e)
        {
            if (e.SavePerformed)
            {
                //OrderLogic l2 = new OrderLogic();
                //l2.SetDependencies(this.AppConfig, this.UserSession);
                //object[] pk = GetPrimaryKeys();
                //bool b = l2.LoadAsync<OrderLogic>(pk).Result;
                //BillToAddressId = l2.BillToAddressId;
                //TaxId = l2.TaxId;


                //if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)) && (TaxId != null) && (!TaxId.Equals(string.Empty)))
                //{
                //    b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId).Result;
                //}

            }
        }
        //------------------------------------------------------------------------------------        
        public async Task<QuoteLogic> CreateQuoteAsync()
        {
            string newQuoteId = await project.CreateQuoteFromProject();
            string[] keys = { newQuoteId };

            QuoteLogic q = new QuoteLogic();
            q.AppConfig = AppConfig;
            q.UserSession = UserSession;
            bool x = await q.LoadAsync<QuoteLogic>(keys);
            return q;

        }
        //------------------------------------------------------------------------------------
    }
}
