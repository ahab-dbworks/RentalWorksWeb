using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FwCore.SqlServer;
using FwCore.Utilities;
using Newtonsoft.Json;

namespace FwCore.ValueTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FwApplicationTree
    {
        public Dictionary<string, FwSqlData.ApplicationOption> ApplicationOptions { get; set; }
        //---------------------------------------------------------------------------------------------
        // this needs to get loaded in the project's application start event
        public static FwApplicationTree Tree { get; set; }
        public static string CurrentApplicationId { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("applications")]
        public FwApplicationTreeNode System { get; set;}
        //---------------------------------------------------------------------------------------------
        // index the nodes in a dictionary by id for fast lookups
        public Dictionary<string, FwApplicationTreeNode> Nodes { get; set; } = new Dictionary<string, FwApplicationTreeNode>();
        public Dictionary<string, FwApplicationTreeNode> ExcludedNodes { get; set; } = new Dictionary<string, FwApplicationTreeNode>();
        //---------------------------------------------------------------------------------------------
        public FwApplicationTree()
        {
            this.ApplicationOptions = FwSqlData.GetApplicationOptions2(FwSqlConnection.AppConnection);
        }
        //---------------------------------------------------------------------------------------------
        static string StripId(string id)
        {
            string result = id;
            if ((id != null) && (id.Length == 38)) {
                result = id.Substring(1, 36);
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        private FwApplicationTreeNode Add(string parentid, string id, string caption, FwApplicationTreeNodeTypes nodeType)
        {
            FwApplicationTreeNode parentNode = null, node;
            id       = StripId(id);
            parentid = StripId(parentid);
            if (this.Nodes.ContainsKey(id)) throw new Exception("Application Tree already contains node id: " + id);
            if ((parentid != null) && (!this.Nodes.ContainsKey(parentid) && !this.ExcludedNodes.ContainsKey(parentid))) throw new Exception("Application Tree does not contain parent node id: " + parentid);
            node = new FwApplicationTreeNode(id, caption, nodeType);
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
                    parentNode.Children = new List<FwApplicationTreeNode>();
                }
                parentNode.Children.Add(node);
            }

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddSystem(string caption, string id)
        {
            id = StripId(id);
            this.System = Add(null, id, caption, FwApplicationTreeNodeTypes.System);
            return this.System;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddApplication(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id = StripId(id);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.Application);
            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddLv1ModuleMenu(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Lv1ModuleMenu);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddLv2ModuleMenu(string caption, string id, string parentid, string iconurl)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Lv2ModuleMenu);
            node.Properties["iconurl"] = iconurl;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddLv2ModuleMenu(string caption, string id, string parentid, string iconurl, string htmlcaption)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Lv2ModuleMenu);
            node.Properties["iconurl"]     = iconurl;
            node.Properties["htmlcaption"] = htmlcaption;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddLv1SubModulesMenu(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Lv1SubModulesMenu);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddLv1GridsMenu(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Lv1GridsMenu);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        // The mobile applications require the usertype parameter be set, so don't use this version in a mobile app
        public FwApplicationTreeNode AddModule(string caption, string id, string parentid, string controller, string modulenav, string iconurl)
        {
            FwApplicationTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Module);
            node.Properties["controller"] = controller;
            node.Properties["modulenav"] = modulenav;
            node.Properties["iconurl"] = iconurl;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddModule(string caption, string id, string parentid, string controller, string modulenav, string iconurl, string usertype)
        {
            FwApplicationTreeNode node;

            id = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Module);
            node.Properties["controller"] = controller;
            node.Properties["modulenav"] = modulenav;
            node.Properties["iconurl"] = iconurl;
            node.Properties["usertype"] = usertype;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddModule(string caption, string id, string parentid, string controller, string modulenav, string iconurl, string htmlcaption, string appoptions, string usertype)
        {
            FwApplicationTreeNode node = null;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
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
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Module);
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
        public FwApplicationTreeNode AddSubModule(string caption, string id, string parentid, string controller)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.SubModule);
            node.Properties["controller"] = controller;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddGrid(string caption, string id, string parentid, string controller)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Grid);
            node.Properties["controller"] = controller;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddReport(string caption, string id, string parentid, string controller, string modulenav, string iconurl)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Report);
            node.Properties["controller"] = controller;
            node.Properties["modulenav"]  = modulenav;
            node.Properties["iconurl"]    = iconurl;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddReport(string caption, string id, string parentid, string controller, string modulenav, string iconurl, string htmlcaption)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Report);
            node.Properties["controller"]  = controller;
            node.Properties["modulenav"]   = modulenav;
            node.Properties["iconurl"]     = iconurl;
            node.Properties["htmlcaption"] = htmlcaption;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddReport(string caption, string id, string parentid, string controller, string modulenav, string iconurl, string htmlcaption, string appoptions, string usertype)
        {
            FwApplicationTreeNode node;

            bool hasAppOptions = true;
            string[] appoptionsarray = appoptions.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string appoption in appoptionsarray)
            {
                if (!ApplicationOptions.ContainsKey(appoption) || !ApplicationOptions[appoption].Enabled)
                {
                    hasAppOptions = false;
                    break;
                }
            }
            id       = StripId(id);
            parentid = StripId(parentid);
            node = this.Add(parentid, id, caption, FwApplicationTreeNodeTypes.Report);
            node.Properties["controller"]  = controller;
            node.Properties["modulenav"]   = modulenav;
            node.Properties["iconurl"]     = iconurl;
            node.Properties["htmlcaption"] = htmlcaption;
            if (!hasAppOptions)
            {
                Nodes.Remove(id);
                Nodes[parentid].Children.Remove(node);
                ExcludedNodes[id] = node;
            }

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddBrowse(string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Browse", FwApplicationTreeNodeTypes.Browse);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddForm(string id, string parentid)
        {
            FwApplicationTreeNode formNode, moduleNode = null;
            string module=null, controller;

            id       = StripId(id);
            parentid = StripId(parentid);
            formNode = Add(parentid, id, "Form", FwApplicationTreeNodeTypes.Form);
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
            if (FwApplicationSchema.Current.Modules.ContainsKey(module) && FwApplicationSchema.Current.Modules[module].Form != null)
            {
                FwApplicationTreeNode nodeComponents;
                FwApplicationSchema.FormTable formTable;
                string componentsid, tabid;
                int tabno, fieldno, gridno;

                // Add Components to Form
                componentsid = id + "_components";
                nodeComponents = AddComponents(componentsid, id);
                if (FwApplicationSchema.Current.Modules[module].Form.Tabs.Count > 0)
                {
                    // add fields not on tab to Components
                    fieldno = 0;
                    foreach(KeyValuePair<string, FwApplicationSchema.FormTable> formTableItem in FwApplicationSchema.Current.Modules[module].Form.Tables)
                    {
                        formTable = formTableItem.Value;
                        foreach (KeyValuePair<string, FwApplicationSchema.Column> columnItem in formTable.Columns)
                        {
                            FwApplicationSchema.Column column;
                            column = columnItem.Value;
                            if (string.IsNullOrWhiteSpace(column.TabCaption))
                            {
                                string fieldid, caption;
                                bool addField = true;
                                fieldid = componentsid + "_field" +  fieldno.ToString();
                                caption = column.Caption;
                                if ((caption == "datestamp") || (string.IsNullOrWhiteSpace(caption)) || (caption == "chg") || (caption == "driverchg") || (caption == "fuelchg"))
                                {
                                    addField = false;
                                }
                                if (addField)
                                {
                                    AddField(fieldid, componentsid, column.Caption);
                                }
                                fieldno++;
                            }
                        }
                    }
                    
                    // Add Tabs to Components
                    tabno = 0;
                    foreach (FwApplicationSchema.Tab tab in FwApplicationSchema.Current.Modules[module].Form.Tabs)
                    {
                        tabid = componentsid + "_tab" + tabno.ToString();
                        AddTab(tabid, componentsid, tab.Caption);
                        fieldno = 0;
                        foreach(KeyValuePair<string, FwApplicationSchema.FormTable> formTableItem in FwApplicationSchema.Current.Modules[module].Form.Tables)
                        {
                            formTable = formTableItem.Value;
                            foreach (KeyValuePair<string, FwApplicationSchema.Column> columnItem in formTable.Columns)
                            {
                                FwApplicationSchema.Column column;
                                column = columnItem.Value;
                                if (column.TabCaption == tab.Caption)
                                {
                                    string fieldid, caption;
                                    bool addField = true;
                                    //fieldid = tabid + "_field" +  fieldno.ToString();
                                    fieldid = tabid + "_field-" +  Regex.Replace(column.Caption, @"[^A-Za-z0-9]", string.Empty) + "-" + Regex.Replace(formTable.TableName + column.ColumnName, @"[^A-Za-z0-9]", string.Empty);
                                    caption = column.Caption;
                                    if ((caption == "datestamp") || (string.IsNullOrWhiteSpace(caption)) || (caption == "chg") || (caption == "driverchg") || (caption == "fuelchg"))
                                    {
                                        addField = false;
                                    }
                                    if (addField)
                                    {
                                        AddField(fieldid, tabid, column.Caption);
                                    }
                                    fieldno++;
                                }
                            }
                        }
                        tabno++;
                    }

                    // Add Grids to Components
                    tabno = 0;
                    foreach (FwApplicationSchema.Tab tab in FwApplicationSchema.Current.Modules[module].Form.Tabs)
                    {
                        tabid = componentsid + "_tab" + tabno.ToString();
                        gridno = 0;
                        foreach(FwApplicationSchema.FormGrid formGrid in FwApplicationSchema.Current.Modules[module].Form.Grids)
                        {
                            if (formGrid.TabCaption == tab.Caption)
                            {
                                string gridid;
                                gridid = tabid + "_grid" +  gridno.ToString();
                                AddFormGrid(gridid, tabid, formGrid.SecurityCaption, formGrid.Grid);
                                gridno++;
                            }
                        }
                        tabno++;
                    }

                }
            }

            return formNode;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddComponents(string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Components", FwApplicationTreeNodeTypes.Components);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddTab(string id, string parentid, string caption)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.Tab);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddField(string id, string parentid, string caption)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.Field);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddFormGrid(string id, string parentid, string caption, string grid)
        {
            FwApplicationTreeNode node; 

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.FormGrid);
            node.Properties["grid"] = grid;

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddMenuBar(string id, string parentid)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, "Menu Bar", FwApplicationTreeNodeTypes.MenuBar);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddSubMenu(string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, "Sub-Menu", FwApplicationTreeNodeTypes.SubMenu);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddSubMenuGroup(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.SubMenuGroup);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddSubMenuItem(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.SubMenuItem);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddDownloadExcelSubMenuItem(string id, string parentid)
        {
            FwApplicationTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "Download Excel Workbook (*.xlsx)", FwApplicationTreeNodeTypes.DownloadExcelSubMenuItem);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddMenuBarButton(string caption, string id, string parentid)
        {
            FwApplicationTreeNode node;

            id       = StripId(id);
            parentid = StripId(parentid);
            node = Add(parentid, id, caption, FwApplicationTreeNodeTypes.MenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddNewMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "New", FwApplicationTreeNodeTypes.NewMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddEditMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "Edit", FwApplicationTreeNodeTypes.EditMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddViewMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            parentid = StripId(parentid);
            node = Add(parentid, id, "View", FwApplicationTreeNodeTypes.ViewMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddDeleteMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, "Delete", FwApplicationTreeNodeTypes.DeleteMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddSaveMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, "Save", FwApplicationTreeNodeTypes.SaveMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddPrevMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, "< Prev", FwApplicationTreeNodeTypes.PrevMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode AddNextMenuBarButton(string id, string parentid)
        {
            FwApplicationTreeNode node;

            node = Add(parentid, id, "Next >", FwApplicationTreeNodeTypes.NextMenuBarButton);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode FindById(string id)
        {
            FwApplicationTreeNode node=null;

            if (this.Nodes.ContainsKey(id))
            {
                node = this.Nodes[id];
            }
            
            return node;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode GetGroupsTree(string groupsid, bool removeHiddenNodes)
        {
            FwSqlCommand qry;
            bool hidenewmenuoptionsbydefault;
            string jsonApplicationTree, jsonSecurity;
            FwApplicationTreeNode groupTree, groupTreeNode;
            List<FwGroupSecurityNode> securityNodes;
            
            jsonApplicationTree = Newtonsoft.Json.JsonConvert.SerializeObject(FwApplicationTree.Tree.System);
            groupTree           = Newtonsoft.Json.JsonConvert.DeserializeObject<FwApplicationTreeNode>(jsonApplicationTree);
            if (removeHiddenNodes)
            {
                if (FwApplicationTree.CurrentApplicationId == null)
                {
                    throw new Exception("Need to set FwApplicationTree.CurrentApplicationId in Global.");
                }
                for (int appno = groupTree.Children.Count - 1; appno >= 0; appno--)
                {
                    if (groupTree.Children[appno].Id != StripId(FwApplicationTree.CurrentApplicationId))
                    {
                        groupTree.Children.RemoveAt(appno);
                    }
                }
            }
            qry = new FwSqlCommand(FwSqlConnection.AppConnection);
            qry.Add("select top 1 hidenewmenuoptionsbydefault, security");
            qry.Add("from groups");
            qry.Add("where groupsid = @groupsid");
            qry.AddParameter("@groupsid", groupsid);
            qry.Execute();
            hidenewmenuoptionsbydefault = qry.GetField("hidenewmenuoptionsbydefault").ToBoolean();
            jsonSecurity                = qry.GetField("security").ToString().TrimEnd();
            groupTree.InitGroupSecurityTree(hidenewmenuoptionsbydefault);
            if (!string.IsNullOrEmpty(jsonSecurity))
            {
                securityNodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FwGroupSecurityNode>>(jsonSecurity);
                foreach (FwGroupSecurityNode secnode in securityNodes)
                {
                    groupTreeNode = groupTree.FindById(secnode.Id);
                    if (groupTreeNode != null)
                    {
                        switch(groupTreeNode.Properties["nodetype"])
                        {
                            case "MenuBar":
                            case "SubMenu":
                            case "Lv1ModuleMenu":
                            case "Lv2ModuleMenu":
                            case "Module":
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
                                groupTreeNode.Properties["visible"]  = secnode.Visible;
                                break;
                            case "Field":
                                groupTreeNode.Properties["visible"]  = secnode.Visible;
                                groupTreeNode.Properties["editable"] = secnode.Editable;
                                break;
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

            return groupTree;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode GetGroupsTreeSupportSite()
        {
            string jsonApplicationTree;
            FwApplicationTreeNode groupTree;
            
            jsonApplicationTree = Newtonsoft.Json.JsonConvert.SerializeObject(FwApplicationTree.Tree.System);
            groupTree           = Newtonsoft.Json.JsonConvert.DeserializeObject<FwApplicationTreeNode>(jsonApplicationTree);
            groupTree.InitGroupSecurityTree(false);

            return groupTree;
        }
        //---------------------------------------------------------------------------------------------
    }
}
