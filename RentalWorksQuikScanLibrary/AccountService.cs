﻿using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScanLibrary.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RentalWorksQuikScanLibrary
{
    public class AccountService : FwAccountService
    {
        public static AccountService Current;
        public override void LoadApplicationAuthenticationInformation(FwSqlConnection conn, dynamic request, dynamic response, dynamic session, dynamic tokenData, dynamic webUserData)
        {
            response.webUser.iscrew      = (FwValidate.IsPropertyDefined(webUserData, "iscrew"))     ? webUserData.iscrew     : string.Empty;
            tokenData.webUser.locationid = (FwValidate.IsPropertyDefined(webUserData, "locationid")) ? webUserData.locationid : string.Empty;

            //Exposes application options to the front end
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "packagetruck"))  response.applicationOptions.packagetruck  = session.applicationOptions.packagetruck;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "props"))         response.applicationOptions.props         = session.applicationOptions.props;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "tieredpricing")) response.applicationOptions.tieredpricing = session.applicationOptions.tieredpricing;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "rfid"))          response.applicationOptions.rfid          = session.applicationOptions.rfid;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "container"))     response.applicationOptions.container     = session.applicationOptions.container;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "crew"))          response.applicationOptions.crew          = session.applicationOptions.crew;
            if (FwValidate.IsPropertyDefined(session.applicationOptions, "production"))    response.applicationOptions.production    = session.applicationOptions.production;

            response.stagingSuspendedSessionsEnabled = Staging.IsSuspendedSessionsEnabled();

            DepartmentFilter.LoadUserDepartmentFilter(session.security.webUser.usersid, session);
        }
    }
}
