using FwStandard.BusinessLogic;
using FwStandard.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FwStandard.Modules.Administrator.WebAuditJson
{

    public static class WebAuditJsonFunc
    {
        //------------------------------------------------------------------------------------ 
        public static async Task AddAuditAsync(FwApplicationConfig appConfig, FwUserSession userSession, FwBusinessLogic oldObject, FwBusinessLogic newObject)
        {
            WebAuditJsonLogic audit = new WebAuditJsonLogic();
            audit.AppConfig = appConfig;
            audit.UserSession = userSession;
            audit.ModuleName = newObject.BusinessLogicModuleName;
            string recordTitle = newObject.RecordTitle;
            if ((string.IsNullOrEmpty(recordTitle)) && (oldObject != null))
            {
                recordTitle = oldObject.RecordTitle;
            }
            audit.Title = recordTitle;
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            List<FwBusinessLogicFieldDelta> deltas = newObject.GetChanges(oldObject);
            if (deltas.Count > 0)
            {
                audit.Json = JsonConvert.SerializeObject(deltas, jsonSerializerSettings);
                object[] keys = newObject.GetPrimaryKeys();
                if (keys.Length > 0)
                {
                    audit.UniqueId1 = keys[0].ToString();
                }
                if (keys.Length > 1)
                {
                    audit.UniqueId2 = keys[1].ToString();
                }
                if (keys.Length > 2)
                {
                    audit.UniqueId3 = keys[2].ToString();
                }
                audit.WebUserId = userSession.WebUsersId;
                await audit.SaveAsync(null);
            }
        }
        //------------------------------------------------------------------------------------    
    }
}