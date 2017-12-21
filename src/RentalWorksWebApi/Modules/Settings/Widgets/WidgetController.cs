using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.Widgets
{


    [Route("api/v1/[controller]")]
    public class WidgetController : AppDataController
    {
        public WidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }

        //------------------------------------------------------------------------------------
        private async Task<IActionResult> DoGetWidget(string widgetName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Widget w = new Widget();
                w.ChartType = "bar";
                w.Data.Labels.Add("Confirmed");
                w.Data.Labels.Add("Hold");
                w.Data.Labels.Add("Active");
                w.Data.Labels.Add("Complete");
                w.Data.Labels.Add("Closed");
                w.Data.Labels.Add("Cancelled");

                w.Options.Title.Text = "Orders By Status";

                List<int> data = new List<int>();
                data.Add(251);  // confirmed
                data.Add(5);    // hold
                data.Add(170);  // active
                data.Add(102);  // complete
                data.Add(84);   // closed
                data.Add(200);  // cancelled

                w.Data.DataSets.Add(new WidgetDataSet());
                w.Data.DataSets[0].Data = data;


                List<string> backgroundColor = new List<string>();
                backgroundColor.Add("rgba(255, 99, 132, 0.2)");
                backgroundColor.Add("rgba(54, 162, 235, 0.2)");
                backgroundColor.Add("rgba(255, 206, 86, 0.2)");
                backgroundColor.Add("rgba(75, 192, 192, 0.2)");
                backgroundColor.Add("rgba(153, 102, 255, 0.2)");
                backgroundColor.Add("rgba(255, 159, 64, 0.2)");

                w.Data.DataSets[0].BackgroundColor = backgroundColor;

                List<string> borderColor = new List<string>();
                borderColor.Add("rgba(255,99,132,1)");
                borderColor.Add("rgba(54, 162, 235, 1)");
                borderColor.Add("rgba(255, 206, 86, 1)");
                borderColor.Add("rgba(75, 192, 192, 1)");
                borderColor.Add("rgba(153, 102, 255, 1)");
                borderColor.Add("rgba(255, 159, 64, 1)");

                w.Data.DataSets[0].BorderColor = borderColor;


                return new OkObjectResult(w);

            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------

        // GET api/v1/widget/ordersbystatus
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAsync([FromRoute]string widgetName)
        {
            return await DoGetWidget(widgetName);
        }

    }
}