using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DBWorksDesigner.Logic.Administrative
{
    public class Editor
    {
        public Folder GenerateFullStructure(string path, string folderName, string menuPath)
        {
            var dfg = new Utilities.IO.DOMFileManipulation(path, folderName);
            var hasAllFilesGenerated = new List<bool>();
            var hasGenerated = false;                        
            var reqFiles = GetRequiredDesignFiles(folderName);
            var folder = new Folder();
            var currentPath = path;
            folder.files = new List<File>();
            folder.folderName = folderName.Replace(" ", string.Empty);
            folder.parentPath = path;

            if (dfg.createFolder())
            {

                reqFiles.ForEach(_files => { hasAllFilesGenerated.Add(dfg.createDOMFiles(_files.Item1, (Enums.DOM)_files.Item3, _files.Item2)); });
                hasGenerated = hasAllFilesGenerated.All(isGenerated => isGenerated == true);

                if (hasGenerated)
                {
                    reqFiles.ForEach(_files => {

                        //if(((Enums.DOM)_files.Item3).ToString().ToLower() == "cs")
                        //{
                        //    currentPath = menuPath;
                        //}
                        //else
                        //{
                        //    currentPath = path;
                        //}

                        folder.files.Add(new File()
                        {
                            fileName = _files.Item1 + "." + ((Enums.DOM)_files.Item3).ToString().ToLower(),
                            fileContents = dfg.readDOMContents(_files.Item1, (Enums.DOM)_files.Item3),
                            ext = ((Enums.DOM)_files.Item3).ToString().ToLower(),
                            path = currentPath + "\\" + folderName,
                            isEditable = true
                        });

                    });

                }

                // generate the Menu file
                folderName = folderName + "Menu";

                var splitMenuPath = menuPath.Split('\\');
                var menuFolder = splitMenuPath[splitMenuPath.Length - 1];
                var cleanMenuPath = new List<string>(splitMenuPath);
                cleanMenuPath.RemoveAt(splitMenuPath.Length - 1);
                var updatedMenuPath = string.Join('\\', cleanMenuPath);
                var dfgMenu = new Utilities.IO.DOMFileManipulation(updatedMenuPath, menuFolder);
                    dfgMenu.createDOMFiles(folderName.Replace(" ", string.Empty), Enums.DOM.CS, GeneratedCSMenuFile(folderName));                

                folder.files.Add(new File()
                {
                    fileName = folderName.Replace(" ", string.Empty) + "." + Enums.DOM.CS.ToString().ToLower(),
                    fileContents = dfgMenu.readDOMContents(folderName.Replace(" ", string.Empty), Enums.DOM.CS),
                    ext = Enums.DOM.CS.ToString().ToLower(),
                    path = updatedMenuPath + "\\" + menuFolder,
                    isEditable = true
                });

            }            

            return folder;

        }

        public Folder OpenModule(string path, string folderName, string menuPath)
        {
            var folder = new Folder();
            var fullPath = path; 
            if(folderName != null)
            {
                folder.folderName = folderName.Replace(" ", string.Empty);
                fullPath = path + "\\" + folderName.Replace(" ", string.Empty);
            }
            folder.parentPath = path;
            folder.files = new List<Administrative.File>();
            var filter = "*";
            
  
                var originalFiles = Directory.GetFiles(fullPath, filter, SearchOption.AllDirectories);

                for (int i = 0; i < originalFiles.Length; i++)
                {
                    var file = new Administrative.File()
                    {
                        fileName = Path.GetFileName(originalFiles[i]),
                        ext = originalFiles[i].Split('.')[1],
                        path = fullPath,
                        fileContents = Utilities.IO.DOMFileManipulation.readFileContents(originalFiles[i]),
                        isEditable = true,
                        hasChanged = false
                    };

                    folder.files.Add(file);

                }
                // menu file
                if(menuPath != "null")
                {
                    var file = new Administrative.File()
                    {
                        fileName = folderName + "Menu.cs",
                        ext = "cs",
                        path = menuPath,
                        fileContents = Utilities.IO.DOMFileManipulation.readFileContents(menuPath + "\\" + folderName + "Menu.cs"),
                        isEditable = true,
                        hasChanged = false
                    };

                    folder.files.Add(file);
                }

                      
            return folder;            
        }

        private List<Tuple<string, string, int>> GetRequiredDesignFiles(string folderName)
        {
            var files = new List<Tuple<string, string, int>>();
            var folderNameConcat = folderName.Replace(" ", string.Empty);

            //files.Add(new Tuple<string, string, int>(folderNameConcat + "Menu", GeneratedCSMenuFile(folderName), (int)Enums.DOM.CS));
            files.Add(new Tuple<string, string, int>(folderNameConcat, GeneratedTSFile(folderName), (int)Enums.DOM.TS));
            files.Add(new Tuple<string, string, int>(folderNameConcat + "Browse", GeneratedHTMLBrowseFile(folderName), (int)Enums.DOM.HTM));
            files.Add(new Tuple<string, string, int>(folderNameConcat + "Form", GeneratedHTMLFormFile(folderName), (int)Enums.DOM.HTM));

            return files;
        }

        public void SaveStructure(List<File> files)
        {
            var dfg = new Utilities.IO.DOMFileManipulation();

            try
            {
                foreach (var file in files)
                {
                    dfg.updateDOMFile(file.fileName, file.fileContents, file.path);
                }
            }
            catch (Exception e)
            {
                Utilities.Security.Logging.RecordError(e);
            }
        }

        public List<string> GetPathFolderAndFileStructure(string folderName, string path, string menuPath)
        {
            var files = new Folder().GetPathFolderAndFileStructure(path);
            // lets add the menu files back into the path

            if(menuPath != "null")
            {
                //files.Add(menuPath + "\\" + folderName + "Menu.cs");
                var menuFiles = new Folder().GetPathFolderAndFileStructure(menuPath);
                
                foreach (var menu in menuFiles)
                {
                    files.Add("Security Menu\\" + menu);
                }

            }            

            return files;
        }

        private string GeneratedCSMenuFile(string folderName)
        {
            var guids = GenerateGuids();
            var folderNameConcat = folderName.Replace(" ", string.Empty);
            var _cs = $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{{
    public class {folderName}Menu : FwApplicationTreeBranch
    {{
        //---------------------------------------------------------------------------------------------
        public {folderName}Menu() : base(""{{{guids[0]}}}"") {{ }}
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {{
            // Browse
            var nodeBrowse = tree.AddBrowse(""{{{guids[1]}}}"", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar(""{{{guids[2]}}}"", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu(""{{{guids[3]}}}"", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup(""Export"", ""{{{guids[4]}}}"", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem(""{{{guids[5]}}}"", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton(""{{{guids[6]}}}"", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton(""{{{guids[7]}}}"", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton(""{{{guids[8]}}}"", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton(""{{{guids[9]}}}"", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm(""{{{guids[10]}}}"", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar(""{{{guids[11]}}}"", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu(""{{{guids[12]}}}"", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton(""{{{guids[13]}}}"", nodeFormMenuBar.Id);
        }}
        //---------------------------------------------------------------------------------------------
    }}
}}";

            return _cs;
        }

        private string GeneratedTSFile(string folderName)
        {
            var folderNameConcat = folderName.Replace(" ", string.Empty);
            var folderNameConcatLower = folderNameConcat.ToLower();
            var _ts = $@"declare var FwModule: any;
declare var FwBrowse: any;

    class {folderNameConcat} {{
        Module: string;
        apiurl: string;

        constructor() {{
            this.Module = '{folderNameConcat}';
            this.apiurl = 'api/v1/{folderNameConcatLower}';
        }}

        getModuleScreen() {{
            var screen, $browse;

            screen = {{}};
            screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
            screen.viewModel = {{}};
            screen.properties = {{}};

            $browse = this.openBrowse();

            screen.load = function () {{
                FwModule.openModuleTab($browse, '{folderName}', false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            }};
            screen.unload = function () {{
                FwBrowse.screenunload($browse);
            }};

            return screen;
        }}

        openBrowse() {{
            var $browse;

            $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse = FwModule.openBrowse($browse);
            FwBrowse.init($browse);

            return $browse;
        }}

        openForm(mode: string) {{
            var $form;

            $form = FwModule.loadFormFromTemplate(this.Module);
            $form = FwModule.openForm($form, mode);

            return $form;
        }}

        loadForm(uniqueids: any) {{
            var $form;

            $form = this.openForm('EDIT');
            $form.find('div.fwformfield[data-datafield=""{folderNameConcat}Id""] input').val(uniqueids.{folderNameConcat}Id);
            FwModule.loadForm(this.Module, $form);

                return $form;
        }}

        saveForm($form: any, closetab: boolean, navigationpath: string)
            {{
                FwModule.saveForm(this.Module, $form, closetab, navigationpath);
            }}

        loadAudit($form: any) {{
                var uniqueid;
                uniqueid = $form.find('div.fwformfield[data-datafield=""{folderNameConcat}Id""] input').val();
                FwModule.loadAudit($form, uniqueid);
        }}

        afterLoad($form: any) {{

        }}

    }}

(<any>window).{folderNameConcat}Controller = new {folderNameConcat}();";

            return _ts;
        }

        private string GeneratedHTMLBrowseFile(string folderName)
        {
            var folderNameConcat = folderName.Replace(" ", string.Empty);
            var _htm = $@"<div data-name=""{folderName}"" data-control=""FwBrowse"" data-type=""Browse"" id=""{folderNameConcat}Browse"" class=""fwcontrol fwbrowse"" data-orderby="""" data-controller=""{folderNameConcat}Controller"">
    <div class=""column"" data-width=""0"" data-visible=""false"">
        <div class=""field"" data-isuniqueid=""true"" data-browsedatafield=""{folderNameConcat}Id"" data-browsedatatype=""key"" data-formdatafield=""{folderNameConcat}Id""></div>
    </div>
    <div class=""column"" data-width=""300px"" data-visible=""true"">
        <div class=""field"" data-caption=""{folderName}"" data-browsedatafield=""{folderNameConcat}"" data-browsedatatype=""text"" data-sort=""asc""></div>
    </div>
    <div class=""column"" data-width=""auto"" data-visible=""true""></div>
</div>
";
            return _htm;
        }

        private string GeneratedHTMLFormFile(string folderName)
        {
            var folderNameConcatLower = folderName.Replace(" ", string.Empty).ToLower();
            var folderNameContact = folderName.Replace(" ", string.Empty);
            var _htm = $@"<div id=""{folderNameConcatLower}form"" class=""fwcontrol fwcontainer fwform"" data-control=""FwContainer"" data-type=""form"" data-version=""1"" data-caption=""{folderName}"" data-rendermode=""template"" data-mode="""" data-hasaudit=""false"" data-controller=""{folderNameContact}Controller"">
    <div data-control=""FwFormField"" data-type=""key"" class=""fwcontrol fwformfield"" data-isuniqueid=""true"" data-saveorder=""1"" data-caption="""" data-datafield=""{folderNameContact}Id""></div>
    <div id=""{folderNameConcatLower}form-tabcontrol"" class=""fwcontrol fwtabs"" data-control=""FwTabs"" data-type="""">
        <div class=""tabs"">
            <div data-type=""tab"" id=""generaltab"" class=""tab"" data-tabpageid=""generaltabpage"" data-caption=""General""></div>
        </div>
        <div class=""tabpages"">
            <div data-type=""tabpage"" id=""generaltabpage"" class=""tabpage"" data-tabid=""generaltab"">
                <div class=""formpage"">
                    <div class=""fwcontrol fwcontainer fwform-section"" data-control=""FwContainer"" data-type=""section"" data-caption=""{folderName}"">
                        <div data-control=""FwFormField"" data-type=""text"" class=""fwcontrol fwformfield"" data-caption=""{folderName}"" data-datafield=""{folderNameContact}"" data-noduplicate=""true"" style=""float:left;width:150px;""></div>
                        <div data-control=""FwFormField"" data-type=""checkbox"" class=""fwcontrol fwformfield"" data-caption=""Inactive"" data-datafield=""Inactive"" style=""float:left;width:150px;""></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>";
            return _htm;
        }

        private List<string> GenerateGuids()
        {
            var guids = new List<string>();
            var numOfGuids = 14;

            for(var i = 0; i < numOfGuids; i++)
            {
                guids.Add(Guid.NewGuid().ToString().ToUpper());
            }

            return guids;
        }

    }
}
