using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        protected async Task<ActionResult<List<FwAppImageModel>>> DoGetManyAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            if (uniqueid1 == null)
            {
                ModelState.AddModelError("uniqueid1", "uniqueid1 is required");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            var getAppImagesResult = await appImageLogic.GetManyAsync(uniqueid1, uniqueid2, uniqueid3, description, rectype);
            return new OkObjectResult(getAppImagesResult);
        }
        //------------------------------------------------------------------------------------
        protected async Task<ActionResult<List<FwAppImageModel>>> DoGetOneAsync(
            [Required,MinLength(1)]string appimageid, 
            string thumbnail, 
            [Required,MinLength(1)]string uniqueid1, 
            string uniqueid2, 
            string uniqueid3, 
            string orderby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            var getAppImagesResult = await appImageLogic.GetOneAsync(appimageid, thumbnail, uniqueid1, uniqueid2, uniqueid3, orderby);
            if (getAppImagesResult == null)
            {
                return NotFound("404 - Image not found");
            }
            return new FileContentResult(getAppImagesResult.Image, getAppImagesResult.MimeType);
        }
        //------------------------------------------------------------------------------------
        protected async Task<ActionResult> DoAddAsync(string uniqueid1, string uniqueid2, string uniqueid3, string description, string extension, string rectype, string imagedataurl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            await appImageLogic.AddAsync(uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, imagedataurl);
            return new OkObjectResult(new object());
        }
        //------------------------------------------------------------------------------------
        protected async Task<ActionResult> DoRepositionAsync(string appimageid, int orderby)
        {
            //jh wip 08/12/2019 #868
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            await appImageLogic.RepositionAsync(appimageid, orderby);
            return new OkObjectResult(new object());
        }
        //------------------------------------------------------------------------------------
        protected async Task<ActionResult> DoDeleteAsync(string appimageid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appImageLogic = new FwAppImageLogic(this.AppConfig);
            await appImageLogic.DeleteAsync(appimageid);
            return new OkObjectResult(new object());
        }
        //------------------------------------------------------------------------------------
    }
}
