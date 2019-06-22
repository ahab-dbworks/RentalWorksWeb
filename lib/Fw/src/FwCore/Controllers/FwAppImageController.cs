﻿using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    public class FwAppImageController : FwController
    {
        FwApplicationConfig appConfig;
        //------------------------------------------------------------------------------------
        public FwAppImageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        public async Task<ActionResult<List<FwAppImageModel>>> DoGetManyAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            var getAppImagesResult = await appImageLogic.GetManyAsync(uniqueid1, uniqueid2, uniqueid3, description, rectype);
            return new OkObjectResult(getAppImagesResult);
        }
        //------------------------------------------------------------------------------------
        public async Task<ActionResult<List<FwAppImageModel>>> DoGetOneAsync(string appimageid, string thumbnail, string uniqueid1, string uniqueid2, string uniqueid3, string orderby)
        {
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            var getAppImagesResult = await appImageLogic.GetOneAsync(appimageid, thumbnail, uniqueid1, uniqueid2, uniqueid3, orderby);
            return new FileContentResult(getAppImagesResult.Image, getAppImagesResult.MimeType);
        }
        //------------------------------------------------------------------------------------
        public async Task<ActionResult> DoAddAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string extension, string rectype, string imagedataurl)
        {
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            await appImageLogic.AddAsync(uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, imagedataurl);
            return new OkObjectResult(new object());
        }
        //------------------------------------------------------------------------------------
        public async Task<ActionResult> DoDeleteAsync(string appimageid)
        {
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            await appImageLogic.DeleteAsync(appimageid);
            return new OkObjectResult(new object());
        }
        //------------------------------------------------------------------------------------
    }
}
