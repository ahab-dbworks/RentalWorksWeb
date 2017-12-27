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
            return await Task<IActionResult>.Run(() =>
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                switch (widgetName)
                {
                    case "ordersbystatus":
                        try
                        {
                            WidgetOrdersByStatus w = new WidgetOrdersByStatus();
                            w.SetDbConfig(_appConfig.DatabaseSettings);
                            bool b = w.LoadAsync().Result;
                            return new OkObjectResult(w);

                        }
                        catch (Exception ex)
                        {
                            FwApiException jsonException = new FwApiException();
                            jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                            jsonException.Message = ex.Message;
                            jsonException.StackTrace = ex.StackTrace;
                            return StatusCode(jsonException.StatusCode, jsonException);
                        };
                    case "ordersbyagent":
                        try
                        {
                            Widget w = new Widget();
                            w.type = "pie";
                            w.data.labels.Add("Garrett, Tyler");
                            w.data.labels.Add("Guirguis, Ahab");
                            w.data.labels.Add("Hoffman, Justin");

                            w.options.title.text = "Orders By Agent";

                            List<int> data = new List<int>();
                            data.Add(33);  // confirmed
                            data.Add(356);    // hold
                            data.Add(623);  // active

                            w.data.datasets.Add(new WidgetDataSet());
                            w.data.datasets[0].data = data;


                            List<string> backgroundColor = new List<string>();
                            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
                            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
                            backgroundColor.Add("rgba(54, 162, 235, 0.2)");
                            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
                            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
                            backgroundColor.Add("rgba(255, 159, 64, 0.2)");

                            w.data.datasets[0].backgroundColor = backgroundColor;

                            List<string> borderColor = new List<string>();
                            borderColor.Add("rgba(153, 102, 255, 1");
                            borderColor.Add("rgba(75, 192, 192, 1)");
                            borderColor.Add("rgba(54, 162, 235, 1)");
                            borderColor.Add("rgba(75, 192, 192, 1)");
                            borderColor.Add("rgba(153, 102, 255, 1)");
                            borderColor.Add("rgba(255, 159, 64, 1)");

                            w.data.datasets[0].borderColor = borderColor;
                        
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
                    case "dealsbytype":
                        try
                        {
                            Widget w = new Widget();
                            w.type = "horizontalBar";
                            w.data.labels.Add("Original Show");
                            w.data.labels.Add("Movie");
                            w.data.labels.Add("Customer Rentals");

                            w.options.title.text = "Deals by Type";

                            List<int> data = new List<int>();
                            data.Add(5);  // confirmed
                            data.Add(3);    // hold
                            data.Add(58);  // active

                            w.data.datasets.Add(new WidgetDataSet());
                            w.data.datasets[0].data = data;


                            List<string> backgroundColor = new List<string>();
                            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
                            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
                            backgroundColor.Add("rgba(255, 159, 64, 0.2)");
                            backgroundColor.Add("rgba(75, 192, 192, 0.2)");
                            backgroundColor.Add("rgba(153, 102, 255, 0.2)");
                            backgroundColor.Add("rgba(255, 159, 64, 0.2)");

                            w.data.datasets[0].backgroundColor = backgroundColor;

                            List<string> borderColor = new List<string>();
                            borderColor.Add("rgba(153, 102, 255, 1");
                            borderColor.Add("'rgba(75, 192, 192, 1)");
                            borderColor.Add("rgba(255, 159, 64, 1)");
                            borderColor.Add("rgba(75, 192, 192, 1)");
                            borderColor.Add("rgba(153, 102, 255, 1)");
                            borderColor.Add("rgba(255, 159, 64, 1)");

                            w.data.datasets[0].borderColor = borderColor;
                        
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
                    default:
                        FwApiException widgetException = new FwApiException();
                        widgetException.StatusCode = StatusCodes.Status500InternalServerError;
                        widgetException.Message = "Invalid widget name";
                        return StatusCode(widgetException.StatusCode, widgetException);
                };
            });
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/widget/ordersbystatus
        [HttpGet("{widgetName}")]
        public async Task<IActionResult> GetAsync([FromRoute]string widgetName)
        {
            return await DoGetWidget(widgetName);
        }
        //------------------------------------------------------------------------------------
    }
}