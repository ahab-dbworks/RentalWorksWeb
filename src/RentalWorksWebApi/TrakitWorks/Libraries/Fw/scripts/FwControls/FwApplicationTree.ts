//---------------------------------------------------------------------------------
class FwApplicationTreeClass {
    tree: IGroupSecurityNode = null;
    clickEvents: any = {};
    currentApplicationId: string = '';
    //---------------------------------------------------------------------------------
    getMyTree() {
        var hasApplicationTreeInSessionStorage, applicationtree = null;
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
    //getNodeByController(controller) {
    //    var tree = FwApplicationTree.getMyTree();
    //    var node = FwApplicationTree.getNodeByControllerRecursive(tree, controller);
    //    return node;
    //};
    //---------------------------------------------------------------------------------
    //getNodeByControllerRecursive = function(node, controller) {
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
    getNodeByFuncRecursive(node, args, func: (node: any, args: any) => any): IGroupSecurityNode {
        let resultNode: IGroupSecurityNode = null;
        if (node !== null) {
            const ismatch: any = func(node, args);
            if (ismatch) {
                resultNode = node;
            } else if (typeof node.children !== 'undefined') {
                for (let childno = 0; childno < node.children.length; childno++) {
                    resultNode = FwApplicationTree.getNodeByFuncRecursive(node.children[childno], args, func);
                    if (resultNode !== null) {
                        break;
                    }
                }
            }
        }
        return resultNode;
    };
    //---------------------------------------------------------------------------------
    getNodeById(node, id): IGroupSecurityNode {
        let resultNode: IGroupSecurityNode = null;
        if (node !== null) {
            var data_nodetype = node.id;
            if (node.id === id) {
                resultNode = node;
            }
            if ((resultNode === null) && (typeof node.children !== 'undefined')) {
                for (let childno = 0; childno < node.children.length; childno++) {
                    resultNode = FwApplicationTree.getNodeById(node.children[childno], id);
                    if (resultNode !== null) break;
                }
            }
        }
        return resultNode;
    };
    //---------------------------------------------------------------------------------
    getChildByType(node, nodetype): IGroupSecurityNode {
        let resultNode: IGroupSecurityNode = null, foundNodeType, childno, currentNode;
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
    getChildrenByType(node: any, nodetype: string): IGroupSecurityNode[] {
        var children: IGroupSecurityNode[] = [];
        FwApplicationTree.getChildrenByTypeRecursive(node, nodetype, children);
        return children;
    };
    //---------------------------------------------------------------------------------
    private getChildrenByTypeRecursive(node: any, nodetype: string, children: IGroupSecurityNode[]): void {
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
    getNodeType(node) {
        var nodetype = null;
        if (node !== null) {
            nodetype = node.nodetype;
        }
        return nodetype;
    };
    //---------------------------------------------------------------------------------
    getSecurityNodes(node, hidenewmenuoptionsbydefault) {
        var securitynodes;

        securitynodes = [];
        FwApplicationTree.getSecurityNodesRecursive(securitynodes, node, hidenewmenuoptionsbydefault);

        return securitynodes;
    }
    //---------------------------------------------------------------------------------
    getSecurityNodesRecursive(securitynodes: IDbSecurityNode[], node: IGroupSecurityNode, hidenewmenuoptionsbydefault: boolean) {
        const visible = (node.properties.visible !== undefined) ? node.properties.visible : null;
        //const visible = ((typeof node.properties.visible === 'string') &&
        //    ((hidenewmenuoptionsbydefault !== true) && (node.properties.visible === 'F')) || ((hidenewmenuoptionsbydefault === true) && (node.properties.visible === 'T'))) ? node.properties.visible : null;
        //const editable = ((typeof node.properties.editable === 'string') && (node.properties.editable === 'F')) ? node.properties.editable : null;
        const includenode = (visible !== null && ((hidenewmenuoptionsbydefault && node.properties.visible === 'T') || (!hidenewmenuoptionsbydefault && node.properties.visible === 'F')));// || (editable !== null);
        if (includenode) {
            const newNode: IDbSecurityNode = {
                id: node.id,
                properties: {
                    visible: (visible !== null) ? visible : 'F'
                }
            };
            //if (editable !== null) {
            //    newNode.properties['visible'].editable = editable;
            //}
            securitynodes.push(newNode);
        }
        if (visible === null || node.properties.visible === 'T') {
            if (node.hasOwnProperty('children')) {
                for (let childno = 0; childno < node.children.length; childno++) {
                    FwApplicationTree.getSecurityNodesRecursive(securitynodes, node.children[childno], hidenewmenuoptionsbydefault);
                }
            }
        }

        return securitynodes;
    }
    //---------------------------------------------------------------------------------
    isVisibleInSecurityTree(secid: string): boolean {
        let isVisible = true;
        if (typeof secid === 'string') {
            const nodeButton = FwApplicationTree.getNodeById(FwApplicationTree.tree, secid);
            isVisible = nodeButton.properties.hasOwnProperty('visible') && nodeButton.properties.visible === 'T';
        }
        return isVisible;
    }
    //----------------------------------------------------------------------------------------------
    getAllReports(addModulePrefix: boolean, addCategoryNamesToCaption: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): any[] {
        const modules: any[] = [];
        for (const key in (<any>window).Constants.Modules) {
            const node = (<any>window).Constants.Modules[key];
            let category = '';
            if (addModulePrefix) {
                category = 'Module > ';
            }
            this.getAllReportsRecursive(modules, category, key, node, addCategoryNamesToCaption, onAddArrayItem);
        }
        return modules;
    }
    //----------------------------------------------------------------------------------------------
    private getAllReportsRecursive(modules: any[], category: string, nodeKey: string, currentNode: any, addCategoryNamesToCaption: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): void {
        if (currentNode.nodetype === 'Module') {
            const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, currentNode.id);
            const moduleControllerName = nodeKey + "Controller";
            if (typeof window[moduleControllerName] !== 'undefined') {
                const moduleController = window[moduleControllerName];
                let moduleCaption = `${addCategoryNamesToCaption ? category : ''}${currentNode.caption}`;
                onAddArrayItem(modules, moduleCaption, nodeKey, category, currentNode, nodeModule, false, false, false, moduleController);
            }
        }
        else if (currentNode.nodetype === 'Category' && nodeKey === 'Reports') {
            for (let childNodeKey in currentNode.children) {
                const childNode = currentNode.children[childNodeKey];
                let childCategory = category;
                if (addCategoryNamesToCaption) {
                    childCategory += currentNode.caption + ' > ';
                }
                if (childNode.hasOwnProperty('children')) {
                    for (let key in childNode.children) {
                        const grandChildNode = childNode.children[key];
                        this.getAllReportsRecursive(modules, childCategory, key, grandChildNode, addCategoryNamesToCaption, onAddArrayItem);
                    }
                } else {
                    this.getAllReportsRecursive(modules, childCategory, childNodeKey, childNode, addCategoryNamesToCaption, onAddArrayItem);
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    getAllModules(addModulePrefix: boolean, addCategoryNamesToCaption: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): any[] {
        const modules: any[] = [];
        for (const key in (<any>window).Constants.Modules) {
            const node = (<any>window).Constants.Modules[key];
            let category = '';
            if (addModulePrefix) {
                category = 'Module > ';
            }
            this.getAllModulesRecursive(modules, category, key, node, addCategoryNamesToCaption, onAddArrayItem);
        }
        return modules;
    }
    //----------------------------------------------------------------------------------------------
    private getAllModulesRecursive(modules: any[], category: string, nodeKey: string, currentNode: any, addCategoryNamesToCaption: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): void {
        if (currentNode.nodetype === 'Module') {
            const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, currentNode.id);
            const nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                return node.nodetype === 'ModuleActions';
            });
            const nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeModuleActions, {}, (node: any, args: any) => {
                return node.nodetype === 'ModuleAction' && typeof node.properties === 'object' &&
                    typeof node.properties.action === 'string' && node.properties.action === 'View';
            });
            const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModuleActions, {}, (node: any, args: any) => {
                return node.nodetype === 'ModuleAction' && typeof node.properties === 'object' &&
                    typeof node.properties.action === 'string' && node.properties.action === 'New';
            });
            const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModuleActions, {}, (node: any, args: any) => {
                return node.nodetype === 'ModuleAction' && typeof node.properties === 'object' &&
                    typeof node.properties.action === 'string' && node.properties.action === 'Edit';
            });
            const hasView: boolean = nodeView !== null && nodeView.properties.visible === 'T';
            const hasNew: boolean = nodeNew !== null && nodeNew.properties.visible === 'T';
            const hasEdit: boolean = nodeEdit !== null && nodeEdit.properties.visible === 'T';
            const moduleControllerName = nodeKey + "Controller";
            if (typeof window[moduleControllerName] !== 'undefined') {
                const moduleController = window[moduleControllerName];
                let moduleCaption = `${addCategoryNamesToCaption ? category : ''}${currentNode.caption}`;
                onAddArrayItem(modules, moduleCaption, nodeKey, category, currentNode, nodeModule, hasView, hasNew, hasEdit, moduleController);
            }
        }
        else if (currentNode.nodetype === 'Category' && nodeKey !== 'Reports') {
            for (let childNodeKey in currentNode.children) {
                const childNode = currentNode.children[childNodeKey];
                let childCategory = category;
                if (addCategoryNamesToCaption) {
                    childCategory += currentNode.caption + ' > ';
                }
                this.getAllModulesRecursive(modules, childCategory, childNodeKey, childNode, addCategoryNamesToCaption, onAddArrayItem);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    getAllGrids(addGridPrefix: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): any[] {
        const modules: any[] = [];
        for (const key in (<any>window).Constants.Grids) {
            const node = (<any>window).Constants.Grids[key];
            let category = '';
            if (addGridPrefix) {
                category = 'Grid > ';
            }
            this.getAllGridsRecursive(modules, category, key, node, addGridPrefix, onAddArrayItem);
        }
        return modules;
    }
    //----------------------------------------------------------------------------------------------
    getAllGridsRecursive(modules: any[], category: string, nodeKey: string, currentNode: any, addGridPrefix: boolean, onAddArrayItem: (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => void): void {
        const nodeGrid = FwApplicationTree.getNodeById(FwApplicationTree.tree, currentNode.id);
        const nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeGrid, {}, (node: any, args: any) => {
            return node.nodetype === 'ModuleActions' || node.nodetype === 'ControlActions';
        });
        const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModuleActions, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleAction' || node.nodetype === 'ControlAction') && typeof node.properties === 'object' &&
                typeof node.properties.action === 'string' && node.properties.action === 'New';
        });
        const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModuleActions, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleAction' || node.nodetype === 'ControlAction') && typeof node.properties === 'object' &&
                typeof node.properties.action === 'string' && node.properties.action === 'Edit';
        });
        const hasNew: boolean = nodeNew !== null && nodeNew.properties.visible === 'T';
        const hasEdit: boolean = nodeEdit !== null && nodeEdit.properties.visible === 'T';
        if (hasNew || hasEdit) {
            const moduleNav = nodeKey.slice(0, -4);
            const moduleControllerName = nodeKey + "Controller";
            if (typeof window[moduleControllerName] !== 'undefined') {
                const moduleController = window[moduleControllerName];
                let moduleCaption = `${addGridPrefix ? category : ''}${currentNode.caption}`;
                onAddArrayItem(modules, moduleCaption, moduleNav, category, currentNode, nodeGrid, hasNew, hasEdit, moduleController);
            }
        }
    }
    //---------------------------------------------------------------------------------
    sortModules(modules: any[]): void {
        modules.sort((a, b) => {
            if (a.text < b.text)
                return -1;
            if (a.text > b.text)
                return 1;
            return 0;
        });
    }
    //---------------------------------------------------------------------------------
    moduleHasNew(moduleId: string) {
        let nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleId);
        let hasNew = false;
        if (nodeModule !== null) {
            const nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                return node.nodetype === 'ModuleActions';
            });
            if (nodeModuleActions !== null) {
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'New';
                });
                hasNew = (nodeNew !== null && nodeNew.properties.visible === 'T');
            }
        }
    }
    //---------------------------------------------------------------------------------
    moduleHasEdit(moduleId: string): boolean {
        let nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleId);
        let hasEdit = false;
        if (nodeModule !== null) {
            const nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                return node.nodetype === 'ModuleActions';
            });
            if (nodeModuleActions !== null) {
                const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'Edit';
                });
                hasEdit = (nodeEdit !== null && nodeEdit.properties.visible === 'T');
            }
        }
        return hasEdit;
    }
    //---------------------------------------------------------------------------------
    moduleHasNewOrEdit(moduleId: string): boolean {
        let nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleId);
        let hasModify = false;
        if (nodeModule !== null) {
            const nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                return node.nodetype === 'ModuleActions';
            });
            if (nodeModuleActions !== null) {
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'New';
                });
                const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node, args) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'Edit';
                });
                const hasNew = (nodeNew !== null && nodeNew.properties.visible === 'T');
                const hasEdit = (nodeEdit !== null && nodeEdit.properties.visible === 'T');
                hasModify = hasNew || hasEdit;
            }
        }
        return hasModify;
    }
    //---------------------------------------------------------------------------------
}
var FwApplicationTree = new FwApplicationTreeClass();
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
