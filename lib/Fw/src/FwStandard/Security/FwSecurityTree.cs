﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FwStandard.Security
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FwSecurityTree
    {
        private SqlServerConfig _sqlServerOptions;
        public Dictionary<string, FwSqlData.ApplicationOption> ApplicationOptions { get; set; }
        public Dictionary<string, FwSecurityTreeNode> GroupTrees { get; private set; } = new Dictionary<string, FwSecurityTreeNode>();
        //--------------------------------------------------------------------------------------------- 
        // this needs to get loaded in the project's application start event 
        public static FwSecurityTree Tree { get; set; }
        public static string CurrentApplicationId { get; set; }
        //--------------------------------------------------------------------------------------------- 
        [JsonProperty("applications")]
        public FwSecurityTreeNode System { get; set; }
        //--------------------------------------------------------------------------------------------- 
        // index the nodes in a dictionary by id for fast lookups 
        public Dictionary<string, FwSecurityTreeNode> Nodes { get; set; } = new Dictionary<string, FwSecurityTreeNode>();
        public Dictionary<string, FwSecurityTreeNode> ExcludedNodes { get; set; } = new Dictionary<string, FwSecurityTreeNode>();
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTree(SqlServerConfig sqlServerOptions, string currentApplicationId)
        {
            _sqlServerOptions = sqlServerOptions;
            FwSecurityTree.CurrentApplicationId = currentApplicationId;
            Task.Run(async () =>
            {
                await this.InitAsync();
            }).Wait();
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task InitAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_sqlServerOptions.ConnectionString))
            {
                this.ApplicationOptions = await FwSqlData.GetApplicationOptions2Async(conn, _sqlServerOptions);
            }
        }
        //--------------------------------------------------------------------------------------------- 
        static string StripId(string id)
        {
            string result = id;
            if ((id != null) && (id.Length == 38))
            {
                result = id.Substring(1, 36);
            }
            return result;
        }
        //--------------------------------------------------------------------------------------------- 
        private FwSecurityTreeNode Add(string parentid, string id, string caption, FwSecurityTreeNodeTypes nodeType, bool allowonnewform = false)
        {
            FwSecurityTreeNode parentNode = null, node;
            id = StripId(id);
            parentid = StripId(parentid);
            if (this.Nodes.ContainsKey(id)) throw new Exception("Application Tree already contains node id: " + id);
            if ((parentid != null) && (!this.Nodes.ContainsKey(parentid) && !this.ExcludedNodes.ContainsKey(parentid))) throw new Exception("Application Tree does not contain parent node id: " + parentid);
            node = new FwSecurityTreeNode(id, caption, nodeType, allowonnewform);
            this.Nodes[id] = node;
            if (parentid != null)
            {
                if (this.Nodes.ContainsKey(parentid))
                {
                    parentNode = this.Nodes[parentid];
                }
                else if (this.ExcludedNodes.ContainsKey(parentid))
                {
                    parentNode = this.ExcludedNodes[parentid];
                }
                node.Parent = parentNode;
                if (parentNode.Children == null)
                {
                    parentNode.Children = new List<FwSecurityTreeNode>();
                }
                parentNode.Children.Add(node);
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSystem(string caption, string id)
        {
            id = StripId(id);
            this.System = Add(null, id, caption, FwSecurityTreeNodeTypes.System);
            return this.System;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddApplication(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.Application);
            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLv1ModuleMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv1ModuleMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLv2ModuleMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv2ModuleMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLv1SubModulesMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv1SubModulesMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLv1GridsMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv1GridsMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        // The mobile applications require the usertype parameter be set, so don't use this version in a mobile app 
        public FwSecurityTreeNode AddModule(string caption, string id, string parentid, string controller)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Module);
            node.Properties["controller"] = controller;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddModule(string caption, string id, string parentid, string controller, string usertype)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Module);
            node.Properties["controller"] = controller;
            node.Properties["usertype"] = usertype;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddModule(string caption, string id, string parentid, string controller, string appoptions, string usertype, string color = "")
        {
            FwSecurityTreeNode node = null;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Module);
            node.Properties["controller"] = controller;
            node.Properties["usertype"] = usertype;
            node.Properties["color"] = color;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        // For the mobile apps
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddModule(string caption, string id, string parentid, string controller, string modulenav, string iconurl, string htmlcaption, string appoptions, string usertype)
        {
            FwSecurityTreeNode node = null;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Module);
            node.Properties["controller"] = controller;
            node.Properties["modulenav"] = modulenav;
            node.Properties["iconurl"] = iconurl;
            node.Properties["htmlcaption"] = htmlcaption;
            node.Properties["usertype"] = usertype;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLvl1SettingsMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv1SettingsMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSettingsMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.SettingsMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSettingsModule(string caption, string id, string parentid, string controller, string appoptions = "", string usertype = "", string color = "", string description = "")
        {
            FwSecurityTreeNode node = null;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.SettingsModule);
            node.Properties["controller"] = controller;
            node.Properties["usertype"] = usertype;
            node.Properties["color"] = color;
            node.Properties["description"] = description;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddLv1ReportsMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Lv1ReportsMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddReportsMenu(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.ReportsMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddReportsModule(string caption, string id, string parentid, string controller, string appoptions = "", string usertype = "", string color = "", string description = "")
        {
            FwSecurityTreeNode node = null;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.ReportsModule);
            node.Properties["controller"] = controller;
            node.Properties["usertype"] = usertype;
            node.Properties["color"] = color;
            node.Properties["description"] = description;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSubModule(string caption, string id, string parentid, string controller)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.SubModule);
            node.Properties["controller"] = controller;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddGrid(string caption, string id, string parentid, string controller)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Grid);
            node.Properties["controller"] = controller;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddReport(string caption, string id, string parentid, string controller)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Report);
            node.Properties["controller"] = controller;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddReport(string caption, string id, string parentid, string controller, string appoptions, string usertype)
        {
            FwSecurityTreeNode node;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Report);
            node.Properties["controller"] = controller;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddBrowse(string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Browse", FwSecurityTreeNodeTypes.Browse);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddForm(string id, string parentid)
        {
            FwSecurityTreeNode formNode, moduleNode = null;
            string module = null, controller;

            id = StripId(id);
            parentid = StripId(parentid);
            formNode = Add(parentid, id, "Form", FwSecurityTreeNodeTypes.Form);
            if (this.Nodes.ContainsKey(parentid))
            {
                moduleNode = this.Nodes[parentid];
            }
            if (this.ExcludedNodes.ContainsKey(parentid))
            {
                moduleNode = this.ExcludedNodes[parentid];
            }
            controller = moduleNode.Properties["controller"].ToString();
            if (controller.Substring(0, 2) == "Fw")
            {
                module = controller.Substring(2, controller.Length - 12);
            }
            else if (controller.Substring(0, 2) == "Tw")
            {
                module = controller.Substring(2, controller.Length - 12);
            }
            else if (controller.Substring(0, 2) == "Rw")
            {
                module = controller.Substring(2, controller.Length - 12);
            }
            else
            {
                module = controller;
            }
            if (module == null) throw new Exception("Can't find module: " + module);
            //if (FwApplicationSchema.Current.Modules.ContainsKey(module) && FwApplicationSchema.Current.Modules[module].Form != null) 
            //{ 
            //    FwSecurityTreeNode nodeComponents; 
            //    FwApplicationSchema.FormTable formTable; 
            //    string componentsid, tabid; 
            //    int tabno, fieldno, gridno; 

            //    // Add Components to Form 
            //    componentsid = id + "_components"; 
            //    nodeComponents = AddComponents(componentsid, id); 
            //    if (FwApplicationSchema.Current.Modules[module].Form.Tabs.Count > 0) 
            //    { 
            //        // add fields not on tab to Components 
            //        fieldno = 0; 
            //        foreach(KeyValuePair<string, FwApplicationSchema.FormTable> formTableItem in FwApplicationSchema.Current.Modules[module].Form.Tables) 
            //        { 
            //            formTable = formTableItem.Value; 
            //            foreach (KeyValuePair<string, FwApplicationSchema.Column> columnItem in formTable.Columns) 
            //            { 
            //                FwApplicationSchema.Column column; 
            //                column = columnItem.Value; 
            //                if (string.IsNullOrWhiteSpace(column.TabCaption)) 
            //                { 
            //                    string fieldid, caption; 
            //                    bool addField = true; 
            //                    fieldid = componentsid + "_field" +  fieldno.ToString(); 
            //                    caption = column.Caption; 
            //                    if ((caption == "datestamp") || (string.IsNullOrWhiteSpace(caption)) || (caption == "chg") || (caption == "driverchg") || (caption == "fuelchg")) 
            //                    { 
            //                        addField = false; 
            //                    } 
            //                    if (addField) 
            //                    { 
            //                        AddField(fieldid, componentsid, column.Caption); 
            //                    } 
            //                    fieldno++; 
            //                } 
            //            } 
            //        } 

            //        // Add Tabs to Components 
            //        tabno = 0; 
            //        foreach (FwApplicationSchema.Tab tab in FwApplicationSchema.Current.Modules[module].Form.Tabs) 
            //        { 
            //            tabid = componentsid + "_tab" + tabno.ToString(); 
            //            AddTab(tabid, componentsid, tab.Caption); 
            //            fieldno = 0; 
            //            foreach(KeyValuePair<string, FwApplicationSchema.FormTable> formTableItem in FwApplicationSchema.Current.Modules[module].Form.Tables) 
            //            { 
            //                formTable = formTableItem.Value; 
            //                foreach (KeyValuePair<string, FwApplicationSchema.Column> columnItem in formTable.Columns) 
            //                { 
            //                    FwApplicationSchema.Column column; 
            //                    column = columnItem.Value; 
            //                    if (column.TabCaption == tab.Caption) 
            //                    { 
            //                        string fieldid, caption; 
            //                        bool addField = true; 
            //                        //fieldid = tabid + "_field" +  fieldno.ToString(); 
            //                        fieldid = tabid + "_field-" +  Regex.Replace(column.Caption, @"[^A-Za-z0-9]", string.Empty) + "-" + Regex.Replace(formTable.TableName + column.ColumnName, @"[^A-Za-z0-9]", string.Empty); 
            //                        caption = column.Caption; 
            //                        if ((caption == "datestamp") || (string.IsNullOrWhiteSpace(caption)) || (caption == "chg") || (caption == "driverchg") || (caption == "fuelchg")) 
            //                        { 
            //                            addField = false; 
            //                        } 
            //                        if (addField) 
            //                        { 
            //                            AddField(fieldid, tabid, column.Caption); 
            //                        } 
            //                        fieldno++; 
            //                    } 
            //                } 
            //            } 
            //            tabno++; 
            //        } 

            //        // Add Grids to Components 
            //        tabno = 0; 
            //        foreach (FwApplicationSchema.Tab tab in FwApplicationSchema.Current.Modules[module].Form.Tabs) 
            //        { 
            //            tabid = componentsid + "_tab" + tabno.ToString(); 
            //            gridno = 0; 
            //            foreach(FwApplicationSchema.FormGrid formGrid in FwApplicationSchema.Current.Modules[module].Form.Grids) 
            //            { 
            //                if (formGrid.TabCaption == tab.Caption) 
            //                { 
            //                    string gridid; 
            //                    gridid = tabid + "_grid" +  gridno.ToString(); 
            //                    AddFormGrid(gridid, tabid, formGrid.SecurityCaption, formGrid.Grid); 
            //                    gridno++; 
            //                } 
            //            } 
            //            tabno++; 
            //        } 
            //    } 
            //} 

            return formNode;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddComponents(string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Components", FwSecurityTreeNodeTypes.Components);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddTab(string id, string parentid, string caption)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.Tab);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddField(string id, string parentid, string caption)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.Field);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddFormGrid(string id, string parentid, string caption, string grid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.FormGrid);
            node.Properties["grid"] = grid;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddMenuBar(string id, string parentid)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, "Menu Bar", FwSecurityTreeNodeTypes.MenuBar);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSubMenu(string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Sub-Menu", FwSecurityTreeNodeTypes.SubMenu);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSubMenuGroup(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.SubMenuGroup);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSubMenuItem(string caption, string id, string parentid, bool allowonnewform = false)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.SubMenuItem, allowonnewform);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddDownloadExcelSubMenuItem(string id, string parentid)
        {
            FwSecurityTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "Download Excel Workbook (*.xlsx)", FwSecurityTreeNodeTypes.DownloadExcelSubMenuItem);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddMenuBarButton(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwSecurityTreeNodeTypes.MenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddNewMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "New", FwSecurityTreeNodeTypes.NewMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddEditMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "Edit", FwSecurityTreeNodeTypes.EditMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddViewMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "View", FwSecurityTreeNodeTypes.ViewMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddDeleteMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, "Delete", FwSecurityTreeNodeTypes.DeleteMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddSaveMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, "Save", FwSecurityTreeNodeTypes.SaveMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddPrevMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, "< Prev", FwSecurityTreeNodeTypes.PrevMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddNextMenuBarButton(string id, string parentid)
        {
            FwSecurityTreeNode node;

            node = Add(parentid, id, "Next >", FwSecurityTreeNodeTypes.NextMenuBarButton);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddController(string caption, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.Controller);

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode AddControllerMethod(string caption, string method, string id, string parentid)
        {
            FwSecurityTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwSecurityTreeNodeTypes.ControllerMethod);
            node.Properties["method"] = method;

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public FwSecurityTreeNode FindById(string id)
        {
            FwSecurityTreeNode node = null;

            id = StripId(id);
            if (this.Nodes.ContainsKey(id))
            {
                node = this.Nodes[id];
            }

            return node;
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task<FwSecurityTreeNode> GetGroupsTreeAsync(string groupsid, bool removeHiddenNodes)
        {
            bool hidenewmenuoptionsbydefault;
            string jsonApplicationTree, jsonSecurity;
            FwSecurityTreeNode groupTree, groupTreeNode;
            List<FwGroupSecurityNode> securityNodes;

            if (GroupTrees.ContainsKey(groupsid))
            {
                groupTree = GroupTrees[groupsid];
            }
            else
            {
                jsonApplicationTree = Newtonsoft.Json.JsonConvert.SerializeObject(FwSecurityTree.Tree.System);
                groupTree = Newtonsoft.Json.JsonConvert.DeserializeObject<FwSecurityTreeNode>(jsonApplicationTree);
                if (removeHiddenNodes)
                {
                    if (FwSecurityTree.CurrentApplicationId == null)
                    {
                        throw new Exception("Need to set FwSecurityTree.CurrentApplicationId in Global.");
                    }
                    for (int appno = groupTree.Children.Count - 1; appno >= 0; appno--)
                    {
                        if (groupTree.Children[appno].Id != StripId(FwSecurityTree.CurrentApplicationId))
                        {
                            groupTree.Children.RemoveAt(appno);
                        }
                    }
                }
                using (FwSqlConnection conn = new FwSqlConnection(_sqlServerOptions.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, _sqlServerOptions.QueryTimeout);
                    qry.Add("select top 1 hidenewmenuoptionsbydefault, security");
                    qry.Add("from groups");
                    qry.Add("where groupsid = @groupsid");
                    qry.AddParameter("@groupsid", groupsid);
                    await qry.ExecuteAsync();
                    hidenewmenuoptionsbydefault = qry.GetField("hidenewmenuoptionsbydefault").ToBoolean();
                    jsonSecurity = qry.GetField("security").ToString().TrimEnd();
                }

                groupTree.InitGroupSecurityTree(hidenewmenuoptionsbydefault);
                if (!string.IsNullOrEmpty(jsonSecurity))
                {
                    securityNodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FwGroupSecurityNode>>(jsonSecurity);
                    foreach (FwGroupSecurityNode secnode in securityNodes)
                    {
                        groupTreeNode = groupTree.FindById(secnode.Id);
                        if (groupTreeNode != null)
                        {
                            switch (groupTreeNode.Properties["nodetype"])
                            {
                                case "MenuBar":
                                case "SubMenu":
                                case "Lv1ModuleMenu":
                                case "Lv2ModuleMenu":
                                case "Module":
                                case "SettingsMenu":
                                case "SettingsModule":
                                case "ReportsMenu":
                                case "ReportsModule":
                                case "SubMenuGroup":
                                case "SubMenuItem":
                                case "DownloadExcelSubMenuItem":
                                case "MenuBarButton":
                                case "NewMenuBarButton":
                                case "ViewMenuBarButton":
                                case "EditMenuBarButton":
                                case "DeleteMenuBarButton":
                                case "SaveMenuBarButton":
                                case "Tab":
                                case "FormGrid":
                                case "Controller":
                                case "ControllerMethod":
                                    groupTreeNode.Properties["visible"] = secnode.Visible;
                                    break;
                                case "Field":
                                    groupTreeNode.Properties["visible"] = secnode.Visible;
                                    groupTreeNode.Properties["editable"] = secnode.Editable;
                                    break;
                                case "System":
                                case "Application":
                                case "Browse":
                                case "Form":
                                case "Components":
                                case "Lv1SubModulesMenu":
                                case "Lv1GridsMenu":
                                case "Grid":
                                case "SubModule":
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return groupTree;
        }
        //--------------------------------------------------------------------------------------------- 
    }
}