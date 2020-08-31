using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FwStandard.AppManager;
using System.Linq;
using WebApi.Modules.HomeControls.CompanyContact;

namespace WebApi.Modules.Agent.Deal
{
    public class CopyContactsFromCustomerRequest
    {
        public DealLogic Deal { get; set; }
    }

    public class CopyContactsFromCustomerResponse : TSpStatusResponse
    {
    }

    public static class DealFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CopyContactsFromCustomerResponse> CopyContactsFromCustomerAsync(FwApplicationConfig appConfig, FwUserSession userSession, CopyContactsFromCustomerRequest request)
        {
            CopyContactsFromCustomerResponse response = new CopyContactsFromCustomerResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                BrowseRequest contactBrowseRequest = new BrowseRequest();
                contactBrowseRequest.uniqueids = new Dictionary<string, object>();
                contactBrowseRequest.uniqueids.Add("CompanyId", request.Deal.CustomerId);

                CompanyContactLogic contactSelector = new CompanyContactLogic();
                contactSelector.SetDependencies(appConfig, userSession);
                List<CompanyContactLogic> contacts = await contactSelector.SelectAsync<CompanyContactLogic>(contactBrowseRequest, conn);

                foreach (CompanyContactLogic n in contacts)
                {
                    BrowseRequest contactCheckBrowseRequest = new BrowseRequest();
                    contactCheckBrowseRequest.uniqueids = new Dictionary<string, object>();
                    contactCheckBrowseRequest.uniqueids.Add("CompanyId", request.Deal.DealId);
                    contactCheckBrowseRequest.uniqueids.Add("ContactId", n.ContactId);

                    CompanyContactLogic contactCheckSelector = new CompanyContactLogic();
                    contactCheckSelector.SetDependencies(appConfig, userSession);
                    List<CompanyContactLogic> contactCheck = await contactCheckSelector.SelectAsync<CompanyContactLogic>(contactCheckBrowseRequest, conn);

                    bool contactExists = (contactCheck.Count > 0);

                    if (!contactExists)
                    {
                        n.SetDependencies(appConfig, userSession);
                        n.CompanyId = request.Deal.DealId;
                        n.CompanyContactId = null;
                        await n.SaveAsync(conn: conn);
                    }
                    response.success = true;
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------

    }
}
