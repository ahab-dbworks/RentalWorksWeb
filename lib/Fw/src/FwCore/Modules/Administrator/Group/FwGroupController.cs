using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FwCore.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    public abstract class FwGroupController : FwDataController
    {
        public FwGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        protected async Task<ActionResult<FwAmSecurityTreeNode>> DoGetApplicationTree([FromRoute]string id)
        {
            try
            {
                FwAmGroupTree groupTree = await FwAppManager.Tree.GetGroupsTreeAsync(id, false);
                return new OkObjectResult(groupTree.RootNode);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
                }
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //---------------------------------------------------------------------------------------------
        protected async Task<ActionResult<FwAmSecurityTreeNode>> DoCopySecurityGroup([FromBody]CopySecurityNodeRequest request)
        {
            try
            {
                var fromGroupsTree = await FwAppManager.Tree.GetGroupsTreeAsync(request.FromGroupId, false);
                var groupIds = request.ToGroupIds.Split(new char[] { ',' });
                for (int i = 0; i < groupIds.Length; i++)
                {
                    var groupId = groupIds[i];
                    var toGroupsTree = await FwAppManager.Tree.GetGroupsTreeAsync(groupId, false);
                    var fromNode = fromGroupsTree.RootNode.FindById(request.SecurityId);
                    var fromNodeJson = JsonConvert.SerializeObject(fromNode);
                    var toNode = toGroupsTree.RootNode.FindById(request.SecurityId);
                    for(int j = 0; j < toNode.Parent.Children.Count; j++)
                    {
                        if (toNode.Parent.Children[j].Id == request.SecurityId)
                        {
                            toNode.Parent.Children[j] = JsonConvert.DeserializeObject<FwAmSecurityTreeNode>(fromNodeJson);
                            break;
                        }
                    }
                    await toGroupsTree.SaveToDatabaseAsync(this.AppConfig);
                }
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
                }
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //---------------------------------------------------------------------------------------------
        protected async Task<ActionResult<GetResponse<LookupGroupResponse>>> DoLookupGroup(LookupGroupRequest request)
        {
            try
            {
                return await this.DoGetManyAsync<LookupGroupResponse>(request, typeof(FwGroupLogic));
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
                }
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //---------------------------------------------------------------------------------------------

    }
}