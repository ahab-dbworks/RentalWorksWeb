var Fw;
(function (Fw) {
    var Modules;
    (function (Modules) {
        var FwGroup = /** @class */ (function () {
            function FwGroup() {
                this.Module = 'Group';
                this.apiurl = 'api/v1/group';
            }
            FwGroup.prototype.getModuleScreen = function () {
                var screen = {};
                screen.$view = FwModule.getModuleControl(this.Module + 'Group');
                screen.viewModel = {};
                screen.properties = {};
                var $browse = this.openBrowse();
                screen.load = function () {
                    FwModule.openModuleTab($browse, 'Group', false, 'BROWSE', true);
                    FwBrowse.databind($browse);
                    FwBrowse.screenload($browse);
                };
                screen.unload = function () {
                    FwBrowse.screenunload($browse);
                };
                return screen;
            };
            FwGroup.prototype.openBrowse = function () {
                var $browse = jQuery(this.getBrowseTemplate());
                FwModule.openBrowse($browse);
                return $browse;
            };
            FwGroup.prototype.openForm = function (mode) {
                var $form = jQuery(this.getFormTemplate());
                FwModule.openForm($form, mode);
                if (mode === 'NEW') {
                    FwFormField.enable($form.find('.ifnew'));
                }
                else {
                    FwFormField.disable($form.find('.ifnew'));
                }
                return $form;
            };
            FwGroup.prototype.loadGroupTree = function ($editgrouptree, $previewgrouptree, $form, groupsid) {
                var request, _self = this;
                request = {};
                request.method = 'getapplicationtree';
                request.groupsid = groupsid;
                FwAppData.apiMethod(true, 'GET', "api/v1/group/applicationtree/" + groupsid, null, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        $previewgrouptree.data('applicationtree', response);
                        _self.render($form, $editgrouptree, $previewgrouptree, response);
                        _self.updateSecurityField($form);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, null);
            };
            ;
            FwGroup.prototype.render = function ($form, $editgrouptree, $previewgrouptree, applicationtree) {
                var $editgrouptree_children, $previewgrouptree_children, searchbarHtml = [];
                $editgrouptree_children = jQuery('<ul class="grouptree"></ul>');
                $editgrouptree.empty()
                    //.append('<div class="title">Edit Group Tree</div>')
                    .append($editgrouptree_children);
                this.renderNode('edit', $form, $editgrouptree_children, applicationtree);
                var $searchbar = $form.find('.searchbar');
                searchbarHtml.push('<form id="groupsearch">');
                searchbarHtml.push('  <input type="text" id="query" class="form-control" placeholder="Search">');
                searchbarHtml.push('  <span class="input-group-addon"><i class="material-icons">search</i></span>');
                searchbarHtml.push('</form>');
                $searchbar.empty().append(searchbarHtml.join(''));
                $form.find('#groupsearch').submit(function (e) {
                    e.preventDefault();
                    var val = jQuery.trim($form.find('#query').val()).toUpperCase();
                    var $node = $form.find('li.node');
                    var checkParents = function (node) {
                        if (node.parent().closest('li').length > 0) {
                            jQuery(node).attr('data-expanded', 'T');
                            checkParents(jQuery(node).parent().closest('li'));
                        }
                        else {
                            jQuery(node).attr('data-expanded', 'T');
                        }
                    };
                    //jQuery.fn.checkParents = function () {
                    //    checkParents(jQuery(this).parent().closest('li'));
                    //}
                    if (val === "") {
                        $node.attr('data-expanded', 'T');
                    }
                    else {
                        $node.attr('data-expanded', 'F').children('div.content').css('background-color', 'rgba(100,100,100,.1)');
                        var $nodes = $node.filter(function () {
                            return -1 != jQuery(this).data().propertyCaption.toUpperCase().indexOf(val);
                        });
                        $nodes.attr('data-expanded', 'T');
                        $nodes.children('div.content').css('background-color', 'cornflowerblue');
                        checkParents($nodes);
                    }
                });
                //TwGroupController.renderGroupTreePreview($form);
            };
            ;
            FwGroup.prototype.renderGroupTreePreview = function ($form) {
                var $previewgrouptree, $previewgrouptree_children, applicationtree;
                $previewgrouptree = $form.find('.previewgrouptree');
                $previewgrouptree_children = jQuery('<ul class="grouptree"></ul>');
                $previewgrouptree.empty().append('<div class="title">Preview Group Tree</div>', $previewgrouptree_children);
                applicationtree = $previewgrouptree.data('applicationtree');
                this.renderNode('preview', $form, $previewgrouptree_children, applicationtree);
                var $previewSubModules = jQuery('.previewgrouptree li[data-property-id="712A2E4B-4387-4D55-9B35-0C2DCBD9B284"]');
                if ($previewSubModules.find('> .childrencontainer > ul.children > li').length === 0) {
                    $previewSubModules.remove();
                }
                var $previewGrids = jQuery('.previewgrouptree li[data-property-id="86E25EC7-76D6-4BA5-9997-494441FEC432"]');
                if ($previewGrids.find('> .childrencontainer > ul.children > li').length === 0) {
                    $previewGrids.remove();
                }
            };
            FwGroup.prototype.renderNode = function (mode, $form, $container, node) {
                var me = this;
                var hidenewmenuoptionsbydefault, haschildren, $node, $content, $iconexpander, $icon, $iconvisible, $iconeditable, $caption, nodedescription, $childrencontainer, $children;
                hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'HideNewMenuOptionsByDefault'));
                haschildren = (node.children.length > 0);
                $node = jQuery('<li class="node">')
                    .attr('data-property-id', node.id)
                    .attr('data-haschildren', haschildren ? 'T' : 'F');
                if (haschildren) {
                    $node.attr('data-expanded', (node.id === "apptree") ? 'T' : 'F');
                }
                for (var key in node.properties) {
                    $node.attr('data-property-' + key, node.properties[key]);
                }
                $content = jQuery('<div class="content">');
                //$iconexpander = jQuery('<div class="iconexpander">');
                $iconexpander = jQuery('<i class="material-icons iconexpander"></i>');
                $node.append($iconexpander);
                if (haschildren) {
                    $iconexpander.on('click', function () {
                        var showchildren;
                        try {
                            showchildren = ($node.attr('data-expanded') === 'F');
                            jQuery(this).closest('.content').siblings('.childrencontainer').toggle(showchildren);
                            $node.attr('data-expanded', showchildren ? 'T' : 'F');
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                $icon = jQuery('<i class="material-icons nodetypeicon"></i>');
                $node.append($icon);
                if ((mode === 'edit') && (typeof $node.attr('data-property-visible') === 'string')) {
                    //$iconvisible = jQuery('<div class="iconvisible">');
                    $iconvisible = jQuery('<i class="material-icons iconvisible"></i>');
                    $content.append($iconvisible);
                    $iconvisible.on('click', function () {
                        try {
                            var $li = jQuery(this).closest('li');
                            var visible_1 = ($li.attr('data-property-visible') === 'T');
                            $li.attr('data-property-visible', visible_1 ? 'F' : 'T');
                            if (!visible_1) {
                                var $parents = $li.parents('li.node');
                                for (var i_1 = 0; i_1 < $parents.length; i_1++) {
                                    var $parent = $parents.eq(i_1);
                                    if (typeof $parent.attr('data-property-visible') !== 'undefined') {
                                        $parent.attr('data-property-visible', 'T');
                                    }
                                }
                            }
                            var $li_children_1 = $li.find('li[data-property-visible]');
                            if ($li_children_1.length > 0) {
                                var $confirmation = FwConfirmation.renderConfirmation('Confirm...', 'Also toggle (' + (visible_1 ? 'Off' : 'On') + ') all the children of this node?');
                                var $btnYes = FwConfirmation.addButton($confirmation, 'Yes', true);
                                $btnYes.on('click', function () {
                                    $li_children_1.attr('data-property-visible', visible_1 ? 'F' : 'T');
                                    me.updateSecurityField($form);
                                });
                                var $btnNo = FwConfirmation.addButton($confirmation, 'No', true);
                                $btnNo.on('click', function () {
                                    me.updateSecurityField($form);
                                });
                            }
                            else {
                                me.updateSecurityField($form);
                            }
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                if ((mode === 'edit') && (typeof $node.attr('data-property-editable') === 'string')) {
                    $iconeditable = jQuery('<div class="iconeditable">');
                    $content.append($iconeditable);
                    $iconeditable.on('click', function () {
                        var $li, visible, editable;
                        try {
                            $li = jQuery(this).closest('li');
                            visible = ($li.attr('data-property-visible') === 'T');
                            if (visible) {
                                editable = ($li.attr('data-property-editable') === 'T');
                                $li.attr('data-property-editable', (!editable) ? 'T' : 'F');
                                this.updateSecurityField($form);
                            }
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                $caption = jQuery('<div class="caption">');
                nodedescription = '';
                //switch (node.properties.nodetype) {
                //    case 'System':
                //    //case 'Application':
                //    case 'Browse':
                //    case 'Form':
                //    case 'Components':
                //    case 'MenuBar':
                //    case 'Lv1SubModulesMenu':
                //    case 'Lv1GridsMenu':
                //    case 'SubMenu':
                //    case 'Lv1SettingsMenu':
                //    case 'SettingsMenu':
                //        nodedescription = '';
                //        break;
                //    case 'Lv1ModuleMenu':
                //        nodedescription = 'Menu: ';
                //        break;
                //    case 'Lv2ModuleMenu':
                //        nodedescription = 'Menu: ';
                //        break;
                //    case 'SubModule':
                //        nodedescription = 'SubModule: ';
                //        break;
                //    case 'SubMenuGroup':
                //        nodedescription = 'Menu: ';
                //        break;
                //    case 'SubMenuItem':
                //    case 'DownloadExcelSubMenuItem':
                //        nodedescription = 'MenuItem: ';
                //        break;
                //    case 'NewMenuBarButton':
                //    case 'ViewMenuBarButton':
                //    case 'EditMenuBarButton':
                //    case 'DeleteMenuBarButton':
                //    case 'SaveMenuBarButton':
                //        nodedescription = 'MenuBarButton: ';
                //        break;
                //    case 'FormGrid':
                //        nodedescription = 'Grid: ';
                //        break;
                //    case 'SettingsModule':
                //        nodedescription = 'Setting: ';
                //        break;
                //    case 'ControllerMethod':
                //        nodedescription = 'Method: ';
                //        break;
                //    default:
                //        nodedescription = node.properties.nodetype + ': '
                //        break;
                //}
                $caption.text(nodedescription + node.properties.caption);
                $content.append($caption);
                $node.append($content);
                $container.append($node);
                if (haschildren) {
                    $childrencontainer = jQuery('<div class="childrencontainer">');
                    $children = jQuery('<ul class="children">');
                    $childrencontainer.append($children);
                    $node.append($childrencontainer);
                    for (var i = 0; i < node.children.length; i++) {
                        if ((mode === 'edit') || (typeof node.children[i].properties.visible === 'undefined') || (node.children[i].properties.visible === 'T')) {
                            this.renderNode(mode, $form, $children, node.children[i]);
                        }
                    }
                }
            };
            FwGroup.prototype.getGroupTreeJson = function ($form) {
                var $apptreenode = jQuery('.editgrouptree > ul.grouptree > li[data-property-nodetype="System"]');
                var grouptree = this.getGroupTreeJsonNode($form, null, $apptreenode);
                return grouptree;
            };
            FwGroup.prototype.getGroupTreeJsonNode = function ($form, parent, $node) {
                var node, attribute, property, $children, $child, child, index;
                node = {
                    id: '',
                    properties: {},
                    children: []
                };
                // process node's attributes
                for (index = 0; index < $node[0].attributes.length; index++) {
                    attribute = $node[0].attributes[index];
                    if (attribute.name.indexOf('data-property-') === 0) {
                        property = attribute.name.substring(14, attribute.name.length);
                        if (property === 'id') {
                            node.id = attribute.value;
                        }
                        else {
                            node.properties[property] = attribute.value;
                        }
                    }
                }
                // process node's children
                $children = $node.find('> .childrencontainer > ul.children > li');
                for (index = 0; index < $children.length; index++) {
                    $child = $children.eq(index);
                    child = this.getGroupTreeJsonNode($form, node.children, $child);
                    node.children.push(child);
                }
                return node;
            };
            FwGroup.prototype.updateSecurityField = function ($form) {
                var apptreenode, hidenewmenuoptionsbydefault, securitynodes, securityJson;
                try {
                    apptreenode = this.getGroupTreeJson();
                    hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'HideNewMenuOptionsByDefault'));
                    securitynodes = FwApplicationTree.getSecurityNodes(apptreenode, hidenewmenuoptionsbydefault);
                    securityJson = JSON.stringify(securitynodes);
                    if (securityJson === '[]') {
                        securityJson = '';
                    }
                    FwFormField.setValueByDataField($form, 'Security', securityJson, null, true);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            };
            ;
            FwGroup.prototype.loadForm = function (uniqueids) {
                var $form = this.openForm('EDIT');
                FwFormField.setValueByDataField($form, 'GroupId', uniqueids.GroupId);
                FwModule.loadForm(this.Module, $form);
                return $form;
            };
            FwGroup.prototype.saveForm = function ($form, parameters) {
                FwModule.saveForm(this.Module, $form, parameters);
            };
            //loadAudit($form: any) {
            //    var uniqueid;
            //    uniqueid = $form.find('div.fwformfield[data-datafield="GroupId"] input').val();
            //    FwModule.loadAudit($form, uniqueid);
            //}
            FwGroup.prototype.afterLoad = function ($form) {
                var $editgrouptree = $form.find('.editgrouptree');
                var $previewgrouptree = $form.find('.previewgrouptree');
                var GroupId = FwFormField.getValueByDataField($form, 'GroupId');
                this.loadGroupTree($editgrouptree, $previewgrouptree, $form, GroupId);
            };
            FwGroup.prototype.getBrowseTemplate = function () {
                return "\n                <div data-name=\"Group\" data-control=\"FwBrowse\" data-type=\"Browse\" id=\"GroupBrowse\" class=\"fwcontrol fwbrowse\" data-datatable=\"groups\" data-orderby=\"name\" data-controller=\"GroupController\">\n                    <div class=\"column\" data-width=\"0\" data-visible=\"false\">\n                    <div class=\"field\" data-isuniqueid=\"true\" data-datafield=\"GroupId\" data-browsedatatype=\"key\" ></div>\n                    </div>\n                    <div class=\"column\" data-width=\"100px\" data-visible=\"true\">\n                    <div class=\"field\" data-caption=\"Group\" data-isuniqueid=\"false\" data-datafield=\"Name\" data-browsedatatype=\"text\" data-sort=\"asc\"></div>\n                    </div>  \n                </div>";
            };
            FwGroup.prototype.getFormTemplate = function () {
                return "\n                <div id=\"groupform\" class=\"fwcontrol fwcontainer fwform\" data-control=\"FwContainer\" data-type=\"form\" data-version=\"1\" data-caption=\"Group\" data-rendermode=\"template\" data-tablename=\"groups\" data-mode=\"\" data-hasaudit=\"false\" data-controller=\"GroupController\">\n                    <div data-control=\"FwFormField\" data-type=\"key\" class=\"fwcontrol fwformfield\" data-isuniqueid=\"true\" data-saveorder=\"1\" data-caption=\"\" data-datafield=\"GroupId\"></div>\n                        <div id=\"groupform-tabcontrol\" class=\"fwcontrol fwtabs\" data-control=\"FwTabs\">\n                            <div class=\"tabs\">\n                                <div data-type=\"tab\" id=\"grouptab\" class=\"tab\" data-tabpageid=\"grouptabpage\" data-caption=\"Group\"></div>\n                                <div data-type=\"tab\" id=\"securitytab\" class=\"tab\" data-tabpageid=\"securitytabpage\" data-caption=\"Security\"></div>\n                            </div>\n                            <div class=\"tabpages\">\n                                <div data-type=\"tabpage\" id=\"grouptabpage\" class=\"tabpage\" data-tabid=\"grouptab\">\n                                    <div class=\"formpage\">\n                                        <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Group\">\n                                            <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                                                <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Name\" data-noduplicate=\"true\" data-datafield=\"Name\"></div>\n                                            </div>\n                                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                                            <div data-control=\"FwFormField\" data-type=\"textarea\" class=\"fwcontrol fwformfield\" data-caption=\"Notes\" data-datafield=\"Memo\"></div>\n                                        </div>\n                                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                                            <div data-control=\"FwFormField\" data-type=\"checkbox\" class=\"fwcontrol fwformfield\" data-caption=\"Items in Security Tree are Hidden by Default (IF YOU CHANGE THIS, SAVE AND CLOSE BEFORE EDITING THE TREE)\" data-datafield=\"HideNewMenuOptionsByDefault\"></div>\n                                            <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-datafield=\"Security\" style=\"display:none;\"></div>\n                                            <div style=\"margin:10px 10px 10px 10px;\">Changing the security tree will take effect for users the next time they login.</div>\n                                        </div>\n                                    </div>\n                                </div>\n                            </div>\n                            <div data-type=\"tabpage\" id=\"securitytabpage\" class=\"tabpage\" data-tabid=\"securitytab\">\n                                <div class=\"formpage\">\n                                    <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                                        <div class=\"row1\" style=\"overflow:hidden;\">\n                                            <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"\" style=\"float:left;overflow:hidden;\">\n                                                <div class=\"searchbar\" style=\"float:left;\"></div>\n                                            </div>\n                                        </div>\n                                    </div>\n                                    <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                                        <div class=\"row1\" style=\"overflow:hidden;\">\n                                            <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Security\" style=\"float:left;overflow:hidden;\">\n                                                <div class=\"editgrouptree\" style=\"float:left;\"></div>\n                                                <div class=\"previewgrouptree\" style=\"float:left;\"></div>\n                                            </div>\n                                        </div>\n                                    </div>\n                                </div>\n                            </div>\n                        </div>\n                    </div>\n                </div>\n\t\t\t";
            };
            return FwGroup;
        }());
        Modules.FwGroup = FwGroup;
    })(Modules = Fw.Modules || (Fw.Modules = {}));
})(Fw || (Fw = {}));
//# sourceMappingURL=FwGroup.js.map