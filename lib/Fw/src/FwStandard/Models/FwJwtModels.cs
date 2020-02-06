using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.AccountServices.Jwt
{
    public class OktaRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
    public class OktaSessionRequest
    {
        public string Token { get; set; } = string.Empty;
        public string Apiurl { get; set; } = string.Empty;
        public FwApplicationConfig appConfig { get; set; }
    }
    public class OktaSessionResponseModel
    {
        public string Status { get; set; } = string.Empty;
    }
    public class OktaVerifyEmailResponse
    {
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
    public class OktaVerifyEmailRequest
    {
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;

    }
}
