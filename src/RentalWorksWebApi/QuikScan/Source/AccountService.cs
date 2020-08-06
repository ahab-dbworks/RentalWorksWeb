using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Modules;
using System;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Source
{
    public class AccountService : FwAccountService
    {
        public static AccountService Current;

        public AccountService(FwApplicationConfig applicationConfig, FwUserSession userSession) : base(applicationConfig, userSession)
        {

        }
        //----------------------------------------------------------------------------------------------------
        public override async Task LoadApplicationAuthenticationInformationAsync(FwSqlConnection conn, dynamic request, dynamic response, dynamic session, dynamic tokenData, dynamic webUserData)
        {
            response.webUser.iscrew      = (FwValidate.IsPropertyDefined(webUserData, "iscrew"))     ? webUserData.iscrew     : string.Empty;
            tokenData.webUser.locationid = (FwValidate.IsPropertyDefined(webUserData, "locationid")) ? webUserData.locationid : string.Empty;

            //Exposes application options to rthe front end
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "packagetruck"))  response.applicationOptions.packagetruck  = session.applicationOptions.packagetruck;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "props"))         response.applicationOptions.props         = session.applicationOptions.props;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "tieredpricing")) response.applicationOptions.tieredpricing = session.applicationOptions.tieredpricing;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "rfid"))          response.applicationOptions.rfid          = session.applicationOptions.rfid;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "container"))     response.applicationOptions.container     = session.applicationOptions.container;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "crew"))          response.applicationOptions.crew          = session.applicationOptions.crew;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "production"))    response.applicationOptions.production    = session.applicationOptions.production;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "quikin"))        response.applicationOptions.quikin        = session.applicationOptions.quikin;

            Staging staging = new Staging(this.ApplicationConfig);
            response.stagingSuspendedSessionsEnabled = staging.IsSuspendedSessionsEnabled();

            await DepartmentFilter.LoadUserDepartmentFilterAsync(this.ApplicationConfig, session.security.webUser.usersid, session);
        }
        //----------------------------------------------------------------------------------------------------
        public override dynamic GetGroupsTree(string groupsid, bool removeHiddenNodes)
        {
            return null;
        }

        internal dynamic GetAuthTokenData(object userData)
        {
            throw new NotImplementedException();
        }
        //----------------------------------------------------------------------------------------------------
    }
}
