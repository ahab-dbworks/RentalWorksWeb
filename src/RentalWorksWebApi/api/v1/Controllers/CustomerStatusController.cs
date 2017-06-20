using AutoMapper;
//using Fw.Json.Services;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksApi2.Models;
using RentalWorksLogic.Settings;
using RentalWorksWebApi;
using System.Collections.Generic;

namespace RentalWorksCoreApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerStatusController : Controller
    {
        private readonly ApplicationConfig _appConfig;
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            List<CustomerStatusLogic> customerStatusRecords = csl.Browse(request);
            List<CustomerStatusDto> customerStatusDtos = Mapper.Map<List<CustomerStatusDto>>(customerStatusRecords);
            FwJsonDataTable dt = new FwJsonDataTable();
            dt.PageNo = request.pageno;
            dt.PageSize = request.pagesize;
            dt.Columns.Add(new FwJsonDataTableColumn() { Name = "CustomerStatus ID", DataType = FwJsonDataTableColumn.DataTypes.Text, DataField = "CustomerStatusId", IsUniqueId = true, IsVisible = false });
            dt.Columns.Add(new FwJsonDataTableColumn() { Name = "Customer Status", DataType = FwJsonDataTableColumn.DataTypes.Text, DataField = "CustomerStatus", IsUniqueId = false, IsVisible = true });
            dt.Columns.Add(new FwJsonDataTableColumn() { Name = "Status Type", DataType = FwJsonDataTableColumn.DataTypes.Text, DataField = "StatusType", IsUniqueId = false, IsVisible = true });
            dt.Columns.Add(new FwJsonDataTableColumn() { Name = "CreditStatus ID", DataType = FwJsonDataTableColumn.DataTypes.Text, DataField = "CreditStatusId", IsUniqueId = false, IsVisible = true });
            dt.Columns.Add(new FwJsonDataTableColumn() { Name = "DateStamp", DataType = FwJsonDataTableColumn.DataTypes.Text, DataField = "DateStamp", IsUniqueId = false, IsVisible = true });
            foreach (CustomerStatusDto dto in customerStatusDtos)
            {
                dt.Rows.Add(new List<object>() { dto.customerStatusId, dto.customerStatus, dto.statusType, dto.creditStatusId, dto.dateStamp});
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatusy
        [HttpGet]
        public List<CustomerStatusDto> Get(int pageno, int pagesize, int orderbyindex)
        {
            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            //request.orderby = "CustomerStatus";
            //request.orderbydirection = BrowseRequestDto.OrderByDirections.asc;
            CustomerStatusLogic cs = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            List<CustomerStatusLogic> customerStatusRecords = cs.Browse(request);
            List<CustomerStatusDto> customerStatusDtos = Mapper.Map<List<CustomerStatusDto>>(customerStatusRecords);
            return customerStatusDtos;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{customerstatusid}")]
        public CustomerStatusDto Get(string customerstatusid)
        {
            GetRequestDto request = new GetRequestDto();
            request.uniqueIds["custstatusid"] = customerstatusid;
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            csl = csl.Get(customerstatusid);
            CustomerStatusDto response = new CustomerStatusDto();
            response.customerStatusId = csl.CustomerStatusId;
            response.customerStatus = csl.CustomerStatus;
            response.statusType = csl.StatusType;
            response.creditStatusId = csl.CreditStatusId;
            return response;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost("{customerstatusid}")]
        public void Post(string customerstatusid, [FromBody]CustomerStatusDto model)
        {
            UpdateRequestDto request = new UpdateRequestDto();
            request.uniqueIds["custstatusid"] = customerstatusid;

            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            csl.CustomerStatusId = model.customerStatusId;
            csl.CustomerStatus = model.customerStatus;
            csl.StatusType = model.statusType;
            csl.CreditStatusId = model.creditStatusId;
            csl.Update(request);
        }
        //------------------------------------------------------------------------------------
        // PUT api/<controller>/5
        [HttpPut("{customerstatusid}")]
        public void Put(string customerstatusid, [FromBody]CustomerStatusDto model)
        {
            InsertRequestDto request = new InsertRequestDto();
            request.uniqueIds = new Dictionary<string, object>();
            request.fields = new Dictionary<string, object>();
            request.uniqueIds["custstatusid"] = customerstatusid;
            request.fields["custstatus"] = model.customerStatus;
            request.fields["statustype"] = model.statusType;
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            csl.Insert(request);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/<controller>/5
        [HttpDelete("{custstatusid}")]
        public void Delete(string customerstatusid)
        {
            DeleteRequestDto request = new DeleteRequestDto();
            request.uniqueIds["custstatusid"] = customerstatusid;
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            csl.Delete(request);
        }
        //------------------------------------------------------------------------------------
    }
}