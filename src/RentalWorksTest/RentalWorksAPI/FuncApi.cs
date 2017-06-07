using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace RentalWorksTest.RentalWorksAPI
{
    static class FuncApi
    {
        //----------------------------------------------------------------------------------------------------
        public static HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/RentalWorksAPI/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"DefaultUser:DefaultPassword")));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        //----------------------------------------------------------------------------------------------------
        public static void LogWebServiceError(ITestOutputHelper output, HttpResponseMessage message)
        {
            WebServiceException exception = message.Content.ReadAsAsync<WebServiceException>().Result;
            output.WriteLine("Message: " + exception.Message);
            output.WriteLine("ExceptionMessage: " + exception.ExceptionMessage);
            output.WriteLine("ExceptionType: " + exception.ExceptionType);
            string[] stack = exception.StackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            output.WriteLine("StackTrace:");
            for (int i = 0; i < stack.Length; i++)
            {
                output.WriteLine(stack[i]);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
