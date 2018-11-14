using FwStandard.AppManager;
﻿using DBWorksDesigner.Logic.Administrative;
using FwCore.Controllers;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace WebApi.Modules.Administrator.Designer
{
    [Route("api/v1/designer")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class DesignerController : FwController
    {
        public DesignerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {

        }
        [HttpGet("createmodule")]
        public Folder CreateModule(string moduleName, string modulePath, string menuPath)
        {
            return new DBWorksDesigner.Logic.Administrative.Editor().GenerateFullStructure(modulePath, moduleName, menuPath);
        }

        [HttpGet("openmodule")]
        public Folder OpenModule(string modulePath, string moduleName, string menuPath)
        {
            return new DBWorksDesigner.Logic.Administrative.Editor().OpenModule(modulePath, moduleName, menuPath);
        }

        [HttpPost("savestructure")]
        public void SaveStructure([FromBody]List<File> files)
        {
            new DBWorksDesigner.Logic.Administrative.Editor().SaveStructure(files);
        }

        [HttpGet("getpathfolderandfilestructure")]
        public List<string> GetPathFolderAndFileStructure(string folderName, string path, string menuPath)
        {
            return new Editor().GetPathFolderAndFileStructure(folderName, path, menuPath);
        }
    }
}
