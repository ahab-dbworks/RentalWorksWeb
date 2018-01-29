using FwCore.Api;
using FwStandard.Models;
using FwStandard.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebLibrary.Security;

namespace WebApi
{
    public static class RwConstants
    {
        public const string ORDER_TYPE_QUOTE = "Q";
        public const string ORDER_TYPE_ORDER = "O";

        public const string QUOTE_STATUS_PROSPECT = "PROSPECT";
        public const string QUOTE_STATUS_ACTIVE = "ACTIVE";

        public const string ORDER_STATUS_CONFIRMED = "CONFIRMED";
    }
}
