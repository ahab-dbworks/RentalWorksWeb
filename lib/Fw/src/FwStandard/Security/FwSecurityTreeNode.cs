using Newtonsoft.Json;
using System.Collections.Generic;

namespace FwStandard.Security
{
    //---------------------------------------------------------------------------------------------
    public enum FwSecurityTreeNodeTypes
    {
        System, Application, Lv1ModuleMenu, Lv2ModuleMenu, Module, Lv1SettingsMenu, SettingsMenu, SettingsModule, Lv1SubModulesMenu, SubModule, Lv1GridsMenu, Grid, Report, Form, Browse, MenuBar, SubMenu, SubMenuGroup, SubMenuItem,
        MenuBarButton, NewMenuBarButton, EditMenuBarButton, ViewMenuBarButton, DeleteMenuBarButton, SaveMenuBarButton, PrevMenuBarButton, NextMenuBarButton,
        DownloadExcelSubMenuItem, Components, Tab, Field, FormGrid, NewVersionSubMenuItem, PrintSubMenuItem, CreateOrderSubMenuItem, PrintMenuBarButton,
        Controller, ControllerMethod
    }
    //---------------------------------------------------------------------------------------------
    [JsonObject(MemberSerialization.OptIn)]
    public class FwSecurityTreeNode
    {
        //---------------------------------------------------------------------------------------------
        [JsonProperty("id")]
        public string Id { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("children")]
        public List<FwSecurityTreeNode> Children { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwSecurityTreeNode Parent { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwSecurityTreeNode()
        {
            this.Id = string.Empty;
            this.Parent = null;
            this.Children = new List<FwSecurityTreeNode>();
            this.Properties = new Dictionary<string, string>();
        }
        //---------------------------------------------------------------------------------------------
        public FwSecurityTreeNode(string id, string caption, FwSecurityTreeNodeTypes nodeType) : this()
        {
            this.Id = id;
            this.Properties["caption"] = caption;
            this.Properties["nodetype"] = nodeType.ToString();
            switch (nodeType)
            {
                case FwSecurityTreeNodeTypes.System:
                case FwSecurityTreeNodeTypes.Application:
                case FwSecurityTreeNodeTypes.Browse:
                case FwSecurityTreeNodeTypes.Components:
                case FwSecurityTreeNodeTypes.Form:
                case FwSecurityTreeNodeTypes.Lv1SubModulesMenu:
                case FwSecurityTreeNodeTypes.Lv1GridsMenu:
                case FwSecurityTreeNodeTypes.Grid:
                case FwSecurityTreeNodeTypes.SubModule:
                    break;
                default:
                    this.Properties["visible"] = "T";
                    break;
            }
            switch (nodeType)
            {
                case FwSecurityTreeNodeTypes.Field:
                    this.Properties["editable"] = "T";
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        public void InitGroupSecurityTree(bool hidenewmenuoptionsbydefault)
        {
            FwSecurityTreeNode.InitGroupSecurityTreeRecursive(null, this, hidenewmenuoptionsbydefault);
        }
        //---------------------------------------------------------------------------------------------
        public static void InitGroupSecurityTreeRecursive(FwSecurityTreeNode parent, FwSecurityTreeNode node, bool hidenewmenuoptionsbydefault)
        {
            if (parent == null)
            {
                if (node.Properties.ContainsKey("visible"))
                {
                    node.Properties["visible"] = "T";
                }
            }
            else
            {
                node.Parent = parent;
                if (node.Properties.ContainsKey("visible"))
                {
                    node.Properties["visible"] = hidenewmenuoptionsbydefault ? "F" : "T";
                }
            }
            foreach (FwSecurityTreeNode child in node.Children)
            {
                FwSecurityTreeNode.InitGroupSecurityTreeRecursive(node, child, hidenewmenuoptionsbydefault);
            }
        }
        //---------------------------------------------------------------------------------------------
        public FwSecurityTreeNode FindById(string id)
        {
            FwSecurityTreeNode node = null;

            if (id.StartsWith("{") && id.EndsWith("}"))
            {
                if (id.Length == 2)
                {
                    id = string.Empty;
                }
                else
                {
                    id = id.Substring(1, id.Length - 2);
                }
            }
            node = FwSecurityTreeNode.FindByIdRecursive(this, id);

            return node;
        }
        //---------------------------------------------------------------------------------------------
        public static FwSecurityTreeNode FindByIdRecursive(FwSecurityTreeNode node, string id)
        {
            FwSecurityTreeNode resultNode = null;

            if (node != null)
            {
                resultNode = (node.Id == id) ? node : null;
                if ((resultNode == null) && (node.Children != null))
                {
                    foreach (FwSecurityTreeNode childNode in node.Children)
                    {
                        resultNode = FwSecurityTreeNode.FindByIdRecursive(childNode, id);
                        if (resultNode != null) break;
                    }
                }
            }

            return resultNode;
        }
        //---------------------------------------------------------------------------------------------
        public void RemoveHiddenNodes()
        {
            FwSecurityTreeNode.RemoveHiddenNodesRecursive(this);
        }
        //---------------------------------------------------------------------------------------------
        public static void RemoveHiddenNodesRecursive(FwSecurityTreeNode node)
        {
            if ((node.Properties.ContainsKey("visible")) && (node.Properties["visible"] == "F") && (node.Parent != null))
            {
                node.Parent.Children.Remove(node);
                return;
            }
            else
            {
                for (int childNo = node.Children.Count - 1; childNo >= 0; childNo--)
                {
                    RemoveHiddenNodesRecursive(node.Children[childNo]);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
