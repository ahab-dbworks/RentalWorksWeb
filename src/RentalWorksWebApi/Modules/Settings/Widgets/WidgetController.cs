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
                            WidgetOrdersByAgent w = new WidgetOrdersByAgent();
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
                    case "dealsbytype":
                        try
                        {
                            WidgetDealsByType w = new WidgetDealsByType();
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