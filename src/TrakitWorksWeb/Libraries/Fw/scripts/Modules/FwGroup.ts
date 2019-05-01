namespace Fw.Modules {
    export class FwGroup {
        Module: string;
        apiurl: string;
        caption: string;
        id: string;
        nav: string;

        constructor() {
            this.Module = 'Group';
            this.apiurl = 'api/v1/group';
            this.caption = 'Group';
            this.id = '9BE101B6-B406-4253-B2C6-D0571C7E5916';
            this.nav = 'module/group';
        }

        getModuleScreen() {
            let screen: any = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Group');
            screen.viewModel = {};
            screen.properties = {};

            let $browse = this.openBrowse();

            screen.load = function () {
                FwModule.openModuleTab($browse, 'Group', false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = function () {
                FwBrowse.screenunload($browse);
            };

            return screen;
        }

        openBrowse() {
            let $browse = jQuery(this.getBrowseTemplate());
            FwModule.openBrowse($browse);

            return $browse;
        }

        openForm(mode: string) {
            let $form = jQuery(this.getFormTemplate());
            FwModule.openForm($form, mode);


            if (mode === 'NEW') {
                FwFormField.enable($form.find('.ifnew'))
            } else {
                FwFormField.disable($form.find('.ifnew'))
            }

            return $form;
        }

        loadGroupTree($editgrouptree, $previewgrouptree, $form, groupsid) {
            var request, _self = this;

            request = {};
            request.method = 'getapplicationtree';
            request.groupsid = groupsid;
            FwAppData.apiMethod(true, 'GET', "api/v1/group/applicationtree/" + groupsid, null, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    $previewgrouptree.data('applicationtree', response);
                    _self.render($form, $editgrouptree, $previewgrouptree, response);
                    _self.updateSecurityField($form);

                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        };

        render($form, $editgrouptree, $previewgrouptree, applicationtree) {
            var $editgrouptree_children, $previewgrouptree_children, searchbarHtml = [];

            $editgrouptree_children = jQuery('<ul class="grouptree"></ul>');
            $editgrouptree.empty()
                //.append('<div class="title">Edit Group Tree</div>')
                .append($editgrouptree_children);
            this.renderNode('edit', $form, $editgrouptree_children, applicationtree);

            var $searchbar = $form.find('.searchbar');
            searchbarHtml.push('<form id="groupsearch">')
            searchbarHtml.push('  <input type="text" id="query" class="form-control" placeholder="Search">');
            searchbarHtml.push('  <span class="input-group-addon"><i class="material-icons">search</i></span>');
            searchbarHtml.push('</form>')

            $searchbar.empty().append(searchbarHtml.join(''));

            $form.find('#groupsearch').submit(function (e) {
                e.preventDefault();
                var val = jQuery.trim($form.find('#query').val()).toUpperCase();
                var $node = $form.find('li.node');
                var checkParents = function (node) {
                    if (node.parent().closest('li').length > 0) {
                        jQuery(node).attr('data-expanded', 'T');
                        checkParents(jQuery(node).parent().closest('li'));
                    } else {
                        jQuery(node).attr('data-expanded', 'T');
                    }
                }
                //jQuery.fn.checkParents = function () {
                //    checkParents(jQuery(this).parent().closest('li'));
                //}
                if (val === "") {
                    $node.attr('data-expanded', 'T');
                }
                else {
                    $node.attr('data-expanded', 'F').children('div.content').css('background-color', 'rgba(100,100,100,.1)');
                    var $nodes: JQuery = $node.filter(function () {
                        return -1 != jQuery(this).data().propertyCaption.toUpperCase().indexOf(val);
                    });
                    $nodes.attr('data-expanded', 'T');
                    $nodes.children('div.content').css('background-color', 'cornflowerblue');
                    checkParents($nodes);
                }
            });

            //TwGroupController.renderGroupTreePreview($form);
        };

        renderGroupTreePreview($form) {
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
        }

        renderNode(mode, $form, $container, node) {
            var me = this;
            var hidenewmenuoptionsbydefault, haschildren, $node, $content, $iconexpander, $icon, $iconvisible, $iconeditable,
                $caption, nodedescription, $childrencontainer, $children;

            hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'HideNewMenuOptionsByDefault'));
            haschildren = (node.children.length > 0);
            $node = jQuery('<li class="node">')
                .attr('data-property-id', node.id)
                .attr('data-haschildren', haschildren ? 'T' : 'F')
                ;
            if (haschildren) {
                $node.attr('data-expanded', (node.id === "apptree") ? 'T' : 'F');
            }
            for (var key in node.properties) {
                $node.attr('data-property-' + key, node.properties[key]);
            }
            $content = jQuery('<div class="content">');

            //$iconexpander = jQuery('<div class="iconexpander">');
            $iconexpander = jQuery('<i class="material-icons iconexpander"></i>')
            $node.append($iconexpander);
            if (haschildren) {
                $iconexpander.on('click', function () {
                    var showchildren;
                    try {
                        showchildren = ($node.attr('data-expanded') === 'F');
                        jQuery(this).closest('.content').siblings('.childrencontainer').toggle(showchildren);
                        $node.attr('data-expanded', showchildren ? 'T' : 'F')
                    } catch (ex) {
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
                        let $li = jQuery(this).closest('li');
                        let visible = ($li.attr('data-property-visible') === 'T');
                        $li.attr('data-property-visible', visible ? 'F' : 'T');
                        if (!visible) {
                            let $parents = $li.parents('li.node');
                            for (let i = 0; i < $parents.length; i++) {
                                let $parent = $parents.eq(i);
                                if (typeof $parent.attr('data-property-visible') !== 'undefined') {
                                    $parent.attr('data-property-visible', 'T');
                                }
                            }
                        }
                        let $li_children = $li.find('li[data-property-visible]');
                        if ($li_children.length > 0) {
                            let $confirmation = FwConfirmation.renderConfirmation('Confirm...', 'Also toggle (' + (visible ? 'Off' : 'On') + ') all the children of this node?');
                            let $btnYes = FwConfirmation.addButton($confirmation, 'Yes', true);
                            $btnYes.on('click', function () {
                                $li_children.attr('data-property-visible', visible ? 'F' : 'T');
                                me.updateSecurityField($form);
                            });
                            let $btnNo = FwConfirmation.addButton($confirmation, 'No', true);
                            $btnNo.on('click', function () {
                                me.updateSecurityField($form);
                            });
                        } else {
                            me.updateSecurityField($form);
                        }
                    } catch (ex) {
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
                    } catch (ex) {
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
        }

        getGroupTreeJson($form?) {
            var $apptreenode = jQuery('.editgrouptree > ul.grouptree > li[data-property-nodetype="System"]');
            var grouptree = this.getGroupTreeJsonNode($form, null, $apptreenode);
            return grouptree;
        }

        getGroupTreeJsonNode($form, parent, $node) {
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
                    } else {
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
        }

        updateSecurityField($form) {
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };

        loadForm(uniqueids: any) {
            let $form = this.openForm('EDIT');
            FwFormField.setValueByDataField($form, 'GroupId', uniqueids.GroupId);
            FwModule.loadForm(this.Module, $form);
            return $form;
        }

        saveForm($form: any, parameters: any) {
            FwModule.saveForm(this.Module, $form, parameters);
        }

        //loadAudit($form: any) {
        //    var uniqueid;
        //    uniqueid = $form.find('div.fwformfield[data-datafield="GroupId"] input').val();
        //    FwModule.loadAudit($form, uniqueid);
        //}

        afterLoad($form: any) {
            let $editgrouptree = $form.find('.editgrouptree');
            let $previewgrouptree = $form.find('.previewgrouptree');
            let GroupId = FwFormField.getValueByDataField($form, 'GroupId');
            this.loadGroupTree($editgrouptree, $previewgrouptree, $form, GroupId);
        }

        getBrowseTemplate() {
            return `
                <div data-name="Group" data-control="FwBrowse" data-type="Browse" id="GroupBrowse" class="fwcontrol fwbrowse" data-datatable="groups" data-orderby="name" data-controller="GroupController">
                    <div class="column" data-width="0" data-visible="false">
                    <div class="field" data-isuniqueid="true" data-datafield="GroupId" data-browsedatatype="key" ></div>
                    </div>
                    <div class="column" data-width="100px" data-visible="true">
                    <div class="field" data-caption="Group" data-isuniqueid="false" data-datafield="Name" data-browsedatatype="text" data-sort="asc"></div>
                    </div>  
                </div>`;
        }

        getFormTemplate() {
            return `
                <div id="groupform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Group" data-rendermode="template" data-tablename="groups" data-mode="" data-hasaudit="false" data-controller="GroupController">
                    <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="GroupId"></div>
                        <div id="groupform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs">
                            <div class="tabs">
                                <div data-type="tab" id="grouptab" class="tab" data-tabpageid="grouptabpage" data-caption="Group"></div>
                                <div data-type="tab" id="securitytab" class="tab" data-tabpageid="securitytabpage" data-caption="Security"></div>
                            </div>
                            <div class="tabpages">
                                <div data-type="tabpage" id="grouptabpage" class="tabpage" data-tabid="grouptab">
                                    <div class="formpage">
                                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Group">
                                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-noduplicate="true" data-datafield="Name"></div>
                                            </div>
                                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Memo"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div data-type="tabpage" id="securitytabpage" class="tabpage" data-tabid="securitytab">
                                <div class="formpage">
                                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Items in Security Tree are Hidden by Default (IF YOU CHANGE THIS, SAVE AND CLOSE BEFORE EDITING THE TREE)" data-datafield="HideNewMenuOptionsByDefault"></div>
                                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="Security" style="display:none;"></div>
                                        <div style="margin:10px 10px 10px 10px;">Changing the security tree will take effect for users the next time they login.</div>
                                    </div>
                                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                        <div class="row1" style="overflow:hidden;">
                                            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="" style="float:left;overflow:hidden;">
                                                <div class="searchbar" style="float:left;"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                        <div class="row1" style="overflow:hidden;">
                                            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security" style="float:left;overflow:hidden;">
                                                <div class="editgrouptree" style="float:left;"></div>
                                                <div class="previewgrouptree" style="float:left;"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
			`;
        }
    }
}

