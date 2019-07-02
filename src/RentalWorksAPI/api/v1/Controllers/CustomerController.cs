using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace RentalWorksAPI.api.v1
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion1:apiVersion1Constraint(v1)}")] 
    public class CustomerController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("customer")]
        public HttpResponseMessage GetCustomers()
        {
            List<Customer> result = new List<Customer>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = CustomerData.GetCustomer("", "");

            return Request.CreateResponse(HttpStatusCode.OK, new { customers = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("customer/{customerid:alpha}")]
        public HttpResponseMessage GetCustomer([FromUri]string customerid)
        {
            List<Customer> result = new List<Customer>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = CustomerData.GetCustomer(customerid, "");

            if (result.Count == 0)
                ThrowError("404", "The requested customer was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { customer = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("customer/customerno={customerno:alpha}")]
        public HttpResponseMessage GetCustomerByNo([FromUri]string customerno)
        {
            List<Customer> result = new List<Customer>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = CustomerData.GetCustomer("", customerno);

            if (result.Count == 0)
                ThrowError("404", "The requested customer was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { customer = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("customer/{customerid:alpha}/orders")]
        public HttpResponseMessage GetCustomerOrders([FromUri]string customerid, [FromBody]OrderParameters request)
        {
            List<Customer> customerinfo;
            CustomerOrders result = new CustomerOrders();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            customerinfo = CustomerData.GetCustomer(customerid, "");

            if (customerinfo.Count == 0)
                ThrowError("404", "The requested customer was not found.");

            result.customerid = customerinfo[0].customerid;
            result.Orders     = CustomerData.GetCustomerOrders(customerinfo[0].customerid, request);

            return Request.CreateResponse(HttpStatusCode.OK, new { customerorders = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("customer/save")]
        public HttpResponseMessage ProcessCustomer([FromBody]Customer customer)
        {
            List<Customer> result = new List<Customer>();
            dynamic postdata      = new ExpandoObject();
            var watch             = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (string.IsNullOrEmpty(customer.customerid) && string.IsNullOrEmpty(customer.customerno)) {
                watch.Stop();
                AppData.LogWebApiAudit(((customer.customerid == null) ? "" : customer.customerid), "v1/customer/save", new JavaScriptSerializer().Serialize(customer), "", "409: Customer no is required.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("409", "Customer no is required.");
            }

            if ((!string.IsNullOrEmpty(customer.customerid)) && (CustomerData.GetCustomer(customer.customerid, "").Count == 0)) {
                watch.Stop();
                AppData.LogWebApiAudit(((customer.customerid == null) ? "" : customer.customerid), "v1/customer/save", new JavaScriptSerializer().Serialize(customer), "", "404: The requested customer was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested customer was not found.");
            }

            try {
                postdata = CustomerData.ProcessCustomer(customer);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(((customer.customerid == null) ? "" : customer.customerid), "v1/customer/save", new JavaScriptSerializer().Serialize(customer), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            if (postdata.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(((customer.customerid == null) ? "" : customer.customerid), "v1/customer/save", new JavaScriptSerializer().Serialize(customer), "", "404: " + postdata.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", postdata.errmsg);
            }

            result = CustomerData.GetCustomer(postdata.customerid, "");
            watch.Stop();
            AppData.LogWebApiAudit(((customer.customerid == null) ? result[0].customerid : customer.customerid), "v1/customer/save", new JavaScriptSerializer().Serialize(customer), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { customer = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPut]
        [Route("customer/customerno={customerno:alpha}")]
        public HttpResponseMessage ProcessCustomerByNo([FromBody]Customer customer, [FromUri]string customerno)
        {
            List<Customer> result       = new List<Customer>();
            List<Customer> findcustomer = new List<Customer>();
            dynamic saveresponse        = new ExpandoObject();
            var watch                   = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            findcustomer = CustomerData.GetCustomer("", customerno);
            if (findcustomer.Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit("", "v1/customer/customerno=" + customerno, new JavaScriptSerializer().Serialize(customer), "", "404: The requested customer was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested customer was not found.");
            }

            try {
                customer.customerid = findcustomer[0].customerid;
                customer.customerno = customerno;
                saveresponse        = CustomerData.ProcessCustomer(customer);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(customer.customerid, "v1/customer/customerno=" + customerno, new JavaScriptSerializer().Serialize(customer), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            if (saveresponse.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(customer.customerid, "v1/customer/customerno=" + customerno, new JavaScriptSerializer().Serialize(customer), "", "404: " + saveresponse.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", saveresponse.errmsg);
            }

            result = CustomerData.GetCustomer(saveresponse.customerid, "");
            watch.Stop();
            AppData.LogWebApiAudit(customer.customerid, "v1/customer/customerno=" + customerno, new JavaScriptSerializer().Serialize(customer), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { customer = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        private void ThrowError(string errno, string errmsg)
        {
            switch (errno)
            {
                case "400": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,          ModelState));
                case "404": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,            errmsg));
                case "409": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,            errmsg));
                case "500": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
