using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Api
{
    public class TempPhysicalFileResult : PhysicalFileResult
    {
        public TempPhysicalFileResult(string fileName, string contentType)
                     : base(fileName, contentType) { }
        //public TempPhysicalFileResult(string fileName, MediaTypeHeaderValue contentType)
        //             : base(fileName, contentType) { }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            await base.ExecuteResultAsync(context);
            File.Delete(FileName);
        }
    }
}
