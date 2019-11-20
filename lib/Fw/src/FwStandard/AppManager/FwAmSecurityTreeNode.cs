using Newtonsoft.Json;
using System.Collections.Generic;

namespace FwStandard.AppManager
{
    //---------------------------------------------------------------------------------------------
    public enum FwAmSecurityTreeNodeTypes
    {
        System,
        Module,
        Category,
        ModuleAction,
        ModuleActions,
        ModuleOptions,
        ModuleOption,
        ModuleApiMethods,
        ModuleComponents,
        ControllerMethod,
        Controls,
        Control,
        ControlActions,
        ControlAction,
        ControlOptions,
        ControlOption
    }
    //---------------------------------------------------------------------------------------------
    [JsonObject(MemberSerialization.OptIn)]
    public class FwAmSecurityTreeNode
    {
        //---------------------------------------------------------------------------------------------
        [JsonProperty("id")]
        public string Id { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("caption")]
        public string Caption { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("nodetype")]
        public string NodeType { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }
        //---------------------------------------------------------------------------------------------
        [JsonProperty("children")]
        public List<FwAmSecurityTreeNode> Children { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwAmSecurityTreeNode Parent { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwAmSecurityTreeNode()
        {
            this.Id = string.Empty;
            this.Parent = null;
            this.Children = new List<FwAmSecurityTreeNode>();
            this.Properties = new Dictionary<string, string>();
        }
        //---------------------------------------------------------------------------------------------
        public FwAmSecurityTreeNode(string id, string caption, FwAmSecurityTreeNodeTypes nodeType) : this()
        {
            this.Id = id;
            this.Caption = caption;
            this.NodeType = nodeType.ToString();
            switch (nodeType)
            {
                case FwAmSecurityTreeNodeTypes.System:
                //case FwAmSecurityTreeNodeTypes.Application:
                //case FwAmSecurityTreeNodeTypes.Browse:
                //case FwAmSecurityTreeNodeTypes.Components:
                //case FwAmSecurityTreeNodeTypes.Form:
                //case FwAmSecurityTreeNodeTypes.Lv1SubModulesMenu:
                //case FwAmSecurityTreeNodeTypes.Lv1GridsMenu:
                //case FwAmSecurityTreeNodeTypes.Grid:
                //case FwAmSecurityTreeNodeTypes.SubModule:
                    break;
                default:
                    this.Properties["visible"] = "T";
                    break;
            }
            //switch (nodeType)
            //{
            //    case FwAmSecurityTreeNodeTypes.Field:
            //        this.Properties["editable"] = "T";
            //        break;
            //}
        }
        //---------------------------------------------------------------------------------------------
        public void InitGroupSecurityTree(bool hidenewmenuoptionsbydefault)
        {
            FwAmSecurityTreeNode.InitGroupSecurityTreeRecursive(null, this, hidenewmenuoptionsbydefault);
        }
        //---------------------------------------------------------------------------------------------
        public static void InitGroupSecurityTreeRecursive(FwAmSecurityTreeNode parent, FwAmSecurityTreeNode node, bool hidenewmenuoptionsbydefault)
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
                    if (hidenewmenuoptionsbydefault &&
                        ((node.NodeType == "System") ||
                        (node.NodeType == "Controls") ||
                        (node.NodeType == "ModuleActions") ||
                        (node.NodeType == "ControlActions") ||
                        (node.NodeType == "ModuleOptions") ||
                        (node.NodeType == "ControlOptions") ||
                        (node.NodeType == "ModuleAction" && node.Properties["action"] == "Browse") ||
                        (node.NodeType == "ControlAction" && node.Properties["action"] == "ControlBrowse") ||
                        (node.NodeType == "ControllerMethod")
                        )) {
                        node.Properties["visible"] = "T";
                    } else
                    {
                        node.Properties["visible"] = hidenewmenuoptionsbydefault ? "F" : "T";
                    }
                }
            }
            foreach (FwAmSecurityTreeNode child in node.Children)
            {
                FwAmSecurityTreeNode.InitGroupSecurityTreeRecursive(node, child, hidenewmenuoptionsbydefault);
            }
        }
        //---------------------------------------------------------------------------------------------
        public FwAmSecurityTreeNode FindById(string id)
        {
            FwAmSecurityTreeNode node = FwAmSecurityTreeNode.FindByIdRecursive(this, id);
            return node;
        }
        //---------------------------------------------------------------------------------------------
        public static FwAmSecurityTreeNode FindByIdRecursive(FwAmSecurityTreeNode node, string id)
        {
            FwAmSecurityTreeNode resultNode = null;
            if (node != null)
            {
                resultNode = (node.Id == id) ? node : null;
                if ((resultNode == null) && (node.Children != null))
                {
                    foreach (FwAmSecurityTreeNode childNode in node.Children)
                    {
                        resultNode = FwAmSecurityTreeNode.FindByIdRecursive(childNode, id);
                        if (resultNode != null) break;
                    }
                }
            }
            return resultNode;
        }
        //---------------------------------------------------------------------------------------------
        public void RemoveHiddenNodes()
        {
            FwAmSecurityTreeNode.RemoveHiddenNodesRecursive(this);
        }
        //---------------------------------------------------------------------------------------------
        public static void RemoveHiddenNodesRecursive(FwAmSecurityTreeNode node)
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
