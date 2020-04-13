using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers.SharedControls.AppImage
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    //[FwController(Id: "9roLDBnwGjrG")]
    public class AppImageController : FwAppImageController
    {
        //------------------------------------------------------------------------------------
        public AppImageController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)     
        {

        }
        //------------------------------------------------------------------------------------
        // GET api/v1/appimage/getmany?&uniqueid1=value&uniqueid2=value&uniqueid3=value&description=value&rectype=value 
        [HttpGet("getimages")]
        [FwControllerMethod(Id: "iMxGUJzMSzOQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<List<FwAppImageModel>>> GetImagesAsync([FromQuery]string uniqueid1, [FromQuery]string uniqueid2, [FromQuery]string uniqueid3, [FromQuery]string description, [FromQuery]string rectype)
        {
            return await base.DoGetManyAsync(uniqueid1, uniqueid2, uniqueid3, description, rectype);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/appimage/getimage?appimageid=value&thumbnail=value
        [HttpGet("getimage")]
        [FwControllerMethod(Id: "6VM5MbZwucZi", ActionType: FwControllerActionTypes.Browse, AllowAnonymous: true)]
        public async Task<ActionResult<List<FwAppImageModel>>> GetImageAsync([FromQuery]string appimageid, [FromQuery]string thumbnail="")
        {
            return await base.DoGetOneAsync(appimageid, thumbnail, null, null, null, null);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/appimage?thumbnail=value&uniqueid1=value&uniqueid2=value&uniqueid3=value&orderby=value
        [HttpGet("getimagefor")]
        [FwControllerMethod(Id: "2TvTfVEfur8c", ValidateSecurityGroup: false)]
        public async Task<ActionResult<List<FwAppImageModel>>> GetImageForAsync([FromQuery]string thumbnail, [FromQuery]string uniqueid1, [FromQuery]string uniqueid2, [FromQuery]string uniqueid3, [FromQuery]string orderby)
        {
            return await base.DoGetOneAsync(null, thumbnail, uniqueid1, uniqueid2, uniqueid3, orderby);
        }
        //------------------------------------------------------------------------------------
        public class RepositionAsyncRequest
        {
            public string AppImageId { get; set; }
            public int OrderBy { get; set; }
        }

        // POST api/v1/appimage/repositionimage
        [HttpPost("repositionimage")]
        [FwControllerMethod(Id: "OFOzYGj2Lppbd", ValidateSecurityGroup: false)]
        public async Task<ActionResult> RepositionAsync([FromBody]RepositionAsyncRequest request)
        {
            return await base.DoRepositionAsync(request.AppImageId, request.OrderBy);
        }
        //------------------------------------------------------------------------------------
        public class AddAsyncRequest
        {
            public string Uniqueid1 { get; set; }
            public string Uniqueid2 { get; set; }
            public string Uniqueid3 { get; set; }
            public string Description { get; set; }
            public string Extension { get; set; }
            public string RecType { get; set; }
            public string ImageDataUrl { get; set; }
        }
        // POST api/v1/appimage 
        [HttpPost]
        [FwControllerMethod(Id: "Ilka63gr9i15", ValidateSecurityGroup: false)]
        public async Task<ActionResult> AddAsync([FromBody]AddAsyncRequest request)
        {
            return await base.DoAddAsync(request.Uniqueid1, request.Uniqueid2, request.Uniqueid3, request.Description, request.Extension, request.RecType, request.ImageDataUrl);
        }
        //----------------------------------------------------------------------------------, ActionType: FwControllerActionTypes.Delete--
        public class DeleteAsyncRequest
        {
            public string AppImageId { get; set; }
        }
        // DELETE api/v1/appimage/A0000001 
        [HttpDelete]
        [FwControllerMethod(Id: "DixsvcLqBocO", ValidateSecurityGroup: false)]
        public async Task<ActionResult> DeleteAsync([FromBody]DeleteAsyncRequest request)
        {
            return await base.DoDeleteAsync(request.AppImageId);
        }
        //------------------------------------------------------------------------------------
    }
}