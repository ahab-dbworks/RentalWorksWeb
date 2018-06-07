using DBWorksDesigner.Logic.Administrative;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Administrator.Designer
{
    [Route("api/v1/designer")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class DesignerController : Controller
    {
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