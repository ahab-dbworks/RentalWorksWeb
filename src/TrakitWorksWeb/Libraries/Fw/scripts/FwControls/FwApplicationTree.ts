//---------------------------------------------------------------------------------
class FwApplicationTree {
    static tree: IGroupSecurityNode = null;
    static clickEvents: any = {};
    static currentApplicationId: string = '';
    //---------------------------------------------------------------------------------
    static getMyTree() {
        var hasApplicationTreeInSessionStorage, applicationtree=null;
        if (FwApplicationTree.tree !== null) {
            applicationtree = FwApplicationTree.tree;
        }
        else {
            hasApplicationTreeInSessionStorage = ((typeof sessionStorage.getItem('applicationtree') === 'string') && (sessionStorage.getItem('applicationtree').length > 0));
            if (hasApplicationTreeInSessionStorage) {
                applicationtree = JSON.parse(sessionStorage.getItem('applicationtree'));
                FwApplicationTree.tree = applicationtree;
            } else {
                sessionStorage.clear();
                location.reload(false);
            }
        }
        return applicationtree;
    };
    //---------------------------------------------------------------------------------
    //static getNodeByController(controller) {
    //    var tree = FwApplicationTree.getMyTree();
    //    var node = FwApplicationTree.getNodeByControllerRecursive(tree, controller);
    //    return node;
    //};
    //---------------------------------------------------------------------------------
    //static getNodeByControllerRecursive = function(node, controller) {
    //    var resultnode=null;
    //    if (node !== null) {
    //        var data_nodetype   = node.nodetype;
    //        var data_controller = node.properties.controller;
    //        var isModule        = ((typeof data_nodetype   === 'string') && (data_nodetype === 'Module' || data_nodetype === 'SettingsModule'))
    //        var isSubModule     = ((typeof data_nodetype   === 'string') && (data_nodetype === 'SubModule'))
    //        var isGrid          = ((typeof data_nodetype   === 'string') && (data_nodetype === 'Grid'))
    //        var foundController = ((typeof data_controller === 'string') && (data_controller.length > 0) && (data_controller === controller));
    //        if ((isModule || isSubModule || isGrid) && foundController) {
    //            resultnode = node;
    //        } else if (resultnode === null) {
    //            for (var childno = 0; childno < node.children.length; childno++) {
    //                resultnode = FwApplicationTree.getNodeByControllerRecursive(node.children[childno], controller);
    //                if (resultnode !== null) {
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    return resultnode;
    //};
    //---------------------------------------------------------------------------------
    static getNodeByFuncRecursive(node, args, func: (node: any, args: any) => any): IGroupSecurityNode {
        var resultnode: IGroupSecurityNode = null;
        if (node !== null) {
            var ismatch = func(node, args);
            if (ismatch) {
                resultnode = node;
            } else if (typeof node.children !== 'undefined') {
                for (var childno = 0; childno < node.children.length; childno++) {
                    resultnode = FwApplicationTree.getNodeByFuncRecursive(node.children[childno], args, func);
                    if (resultnode !== null) {
                        break;
                    }
                }
            }
        }
        return resultnode;
    };
    //---------------------------------------------------------------------------------
    static getNodeById(node, id): IGroupSecurityNode {
        var resultNode: IGroupSecurityNode = null;
        if (node !== null) {
            var data_nodetype = node.id;
            if (node.id === id) {
                resultNode = node;
            }
            if ((resultNode === null) && (typeof node.children !== 'undefined')) {
                for (var childno = 0; childno < node.children.length; childno++) {
                    resultNode = FwApplicationTree.getNodeById(node.children[childno], id);
                    if (resultNode !== null) break;
                }
            }
        }
        return resultNode;
    };
    //---------------------------------------------------------------------------------
    static getChildByType(node, nodetype): IGroupSecurityNode {
        var resultNode: IGroupSecurityNode = null, foundNodeType, childno, currentNode;
        if (node !== null) {
            for (childno = 0; childno < node.children.length; childno++) {
                currentNode = node.children[childno];
                foundNodeType = ((currentNode.hasOwnProperty('properties')) && (currentNode.hasOwnProperty('nodetype')) && (currentNode.nodetype === nodetype))
                if (foundNodeType) {
                    resultNode = currentNode;
                    break;
                }
            }
        }
        return resultNode;
    };
    //---------------------------------------------------------------------------------
    static getChildrenByType(node: any, nodetype: string): IGroupSecurityNode[] {
        var children: IGroupSecurityNode[] = [];
        FwApplicationTree.getChildrenByTypeRecursive(node, nodetype, children);
        return children;
    };
    //---------------------------------------------------------------------------------
    private static getChildrenByTypeRecursive(node: any, nodetype: string, children: IGroupSecurityNode[]): void {
        if (node !== null && typeof node.children !== 'undefined') {
            for (var childno = 0; childno < node.children.length; childno++) {
                var currentNode = node.children[childno];
                var foundNodeType = ((currentNode.hasOwnProperty('properties')) && (currentNode.hasOwnProperty('nodetype')) && (currentNode.nodetype === nodetype))
                if (foundNodeType) {
                    children.push(currentNode);
                }
                FwApplicationTree.getChildrenByTypeRecursive(currentNode, nodetype, children);
            }
        }
    };
    //---------------------------------------------------------------------------------
    static getNodeType(node) {
        var nodetype=null;
        if (node !== null) {
            nodetype = node.nodetype;
        }
        return nodetype;
    };
    //---------------------------------------------------------------------------------
    static getSecurityNodes(node, hidenewmenuoptionsbydefault) {
        var securitynodes;

        securitynodes        = [];
        FwApplicationTree.getSecurityNodesRecursive(securitynodes, node, hidenewmenuoptionsbydefault);

        return securitynodes;
    }
    //---------------------------------------------------------------------------------
    static getSecurityNodesRecursive(securitynodes: IDbSecurityNode[], node: IGroupSecurityNode, hidenewmenuoptionsbydefault: boolean) {
        var includenode, newNode: IDbSecurityNode, childno, visible, editable;

        visible  = ((typeof node.properties.visible  === 'string') && 
                    ((hidenewmenuoptionsbydefault !== true) && (node.properties.visible === 'F')) || ((hidenewmenuoptionsbydefault === true) && (node.properties.visible === 'T'))) ? node.properties.visible : null;
        editable = ((typeof node.properties.editable === 'string') && (node.properties.editable === 'F')) ? node.properties.editable : null;
        includenode  = (visible !== null) || (editable !== null);
        if (includenode) {
            newNode = {
                id: node.id,
                properties: {
                    visible: (visible !== null) ? visible : 'F'
                }
            };
            if (editable !== null) {
                newNode.properties['visible'].editable = editable;
            }
            securitynodes.push(newNode);
        }
        if (node.hasOwnProperty('children')) {
            for (childno = 0; childno < node.children.length; childno++) {
                FwApplicationTree.getSecurityNodesRecursive(securitynodes, node.children[childno], hidenewmenuoptionsbydefault);
            }
        }

        return securitynodes;
    }
    //---------------------------------------------------------------------------------
    static isVisibleInSecurityTree(secid: string): boolean {
        let isVisible = true;
        if (typeof secid === 'string') {
            const nodeButton = FwApplicationTree.getNodeById(FwApplicationTree.tree, secid);
            isVisible = nodeButton.properties.hasOwnProperty('visible') && nodeButton.properties.visible === 'T';
        }
        return isVisible;
    }
    //---------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------
interface IDbSecurityNode {
    id: string;
    properties?: any;
}
//---------------------------------------------------------------------------------
interface IGroupSecurityNode {
    id: string
    nodetype: string
    caption?: string
    children?: IGroupSecurityNode[]
    properties?: IGroupSecurityNodeProperties
}
interface IGroupSecurityNodeProperties {
    visible?: string
    [key: string]: any
}
//---------------------------------------------------------------------------------
