using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FwCore.ValueTypes
{
    //---------------------------------------------------------------------------------------------
    public enum FwApplicationTreeNodeTypes
    {
        System, Application, Lv1ModuleMenu, Lv2ModuleMenu, Module, Lv1SubModulesMenu, SubModule, Lv1GridsMenu, Grid, Report, Form, Browse, MenuBar, SubMenu, SubMenuGroup, SubMenuItem,
        MenuBarButton, NewMenuBarButton, EditMenuBarButton, ViewMenuBarButton, DeleteMenuBarButton, SaveMenuBarButton, PrevMenuBarButton, NextMenuBarButton,
        DownloadExcelSubMenuItem, Components, Tab, Field, FormGrid, NewVersionSubMenuItem, PrintSubMenuItem, CreateOrderSubMenuItem, PrintMenuBarButton
    }
    //---------------------------------------------------------------------------------------------
    [JsonObject(MemberSerialization.OptIn)]
    public class FwApplicationTreeNode
    {
        //---------------------------------------------------------------------------------------------
        [JsonProperty("id")]
        public string Id {get;set;}
        //---------------------------------------------------------------------------------------------
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties {get;set;}
        //---------------------------------------------------------------------------------------------
        [JsonProperty("children")]
        public List<FwApplicationTreeNode> Children {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode Parent {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode()
        {
            this.Id = string.Empty;
            this.Parent = null;
            this.Children = new List<FwApplicationTreeNode>();
            this.Properties = new Dictionary<string, string>();
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode(string id, string caption, FwApplicationTreeNodeTypes nodeType) : this()
        {
            this.Id = id;
            this.Properties["caption"]  = caption;
            this.Properties["nodetype"] = nodeType.ToString();
            switch(nodeType) {
                case FwApplicationTreeNodeTypes.System:
                case FwApplicationTreeNodeTypes.Application:
                case FwApplicationTreeNodeTypes.Browse:
                case FwApplicationTreeNodeTypes.Components:
                case FwApplicationTreeNodeTypes.Form:
                case FwApplicationTreeNodeTypes.Lv1SubModulesMenu:
                case FwApplicationTreeNodeTypes.Lv1GridsMenu:
                case FwApplicationTreeNodeTypes.Grid:
                case FwApplicationTreeNodeTypes.SubModule:
                    break;
                default:
                    this.Properties["visible"]  = "T";
                    break;
            }
            switch(nodeType) {
                case FwApplicationTreeNodeTypes.Field:
                    this.Properties["editable"] = "T";
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        public void InitGroupSecurityTree(bool hidenewmenuoptionsbydefault)
        {
            FwApplicationTreeNode.InitGroupSecurityTreeRecursive(null, this, hidenewmenuoptionsbydefault);
        }
        //---------------------------------------------------------------------------------------------
        public static void InitGroupSecurityTreeRecursive(FwApplicationTreeNode parent, FwApplicationTreeNode node, bool hidenewmenuoptionsbydefault)
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
            foreach (FwApplicationTreeNode child in node.Children)
            {
                FwApplicationTreeNode.InitGroupSecurityTreeRecursive(node, child, hidenewmenuoptionsbydefault);
            }
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationTreeNode FindById(string id)
        {
            FwApplicationTreeNode node=null;

            node = FwApplicationTreeNode.FindByIdRecursive(this, id);
            
            return node;
        }
        //---------------------------------------------------------------------------------------------
        public static FwApplicationTreeNode FindByIdRecursive(FwApplicationTreeNode node, string id)
        {
            FwApplicationTreeNode resultNode=null;

            if (node != null)
            {
                resultNode = (node.Id == id) ? node : null;
                if ((resultNode == null) && (node.Children != null))
                {
                    foreach (FwApplicationTreeNode childNode in node.Children)
                    {
                        resultNode = FwApplicationTreeNode.FindByIdRecursive(childNode, id);
                        if (resultNode != null) break;
                    }
                }
            }
            
            return resultNode;
        }
        //---------------------------------------------------------------------------------------------
        //public void RemoveById(string id)
        //{
        //    FwApplicationTreeNode node;
            
        //    node = FwApplicationTreeNode.FindByIdRecursive(this, id);
        //    if ((node != null) && (node.Parent != null))
        //    {
        //        node.Parent.Children.Remove(node);
        //    }
        //}
        //---------------------------------------------------------------------------------------------
        public void RemoveHiddenNodes()
        {
            FwApplicationTreeNode.RemoveHiddenNodesRecursive(this);
        }
        //---------------------------------------------------------------------------------------------
        public static void RemoveHiddenNodesRecursive(FwApplicationTreeNode node)
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
