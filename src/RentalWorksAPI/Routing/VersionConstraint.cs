using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace RentalWorksAPI.Routing
{
    //===================================================================================================
    public class RwAPIv1Constraint : IHttpRouteConstraint
    {
        //----------------------------------------------------------------------------------------------------
        public string AllowedVersion { get; private set; }
        //----------------------------------------------------------------------------------------------------
        public RwAPIv1Constraint(string allowedversion)
        {
            AllowedVersion = allowedversion.ToLowerInvariant();
        }
        //----------------------------------------------------------------------------------------------------
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            bool versionallowed = false;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                versionallowed = AllowedVersion.Equals(value.ToString().ToLowerInvariant());
            }
            return versionallowed;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //===================================================================================================
    public class RwAPIv2Constraint : IHttpRouteConstraint
    {
        //----------------------------------------------------------------------------------------------------
        public string AllowedVersion2 { get; private set; }
        //----------------------------------------------------------------------------------------------------
        public RwAPIv2Constraint(string allowedversion)
        {
            AllowedVersion2 = allowedversion.ToLowerInvariant();
        }
        //----------------------------------------------------------------------------------------------------
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            bool versionallowed = false;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                versionallowed = AllowedVersion2.Equals(value.ToString().ToLowerInvariant());
            }
            return versionallowed;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //===================================================================================================
}