namespace Fw.Modules {
    export class FwGroup {
        Module: string;
        apiurl: string;
        caption: string;
        id: string;
        nav: string;
        exportExcelSecurityId: string;

        constructor() {
            this.Module = 'Group';
            this.apiurl = 'api/v1/group';
            this.caption = 'Group';
            this.id = 'NFcnktYjQafU';
            this.nav = 'module/group';
        }

        addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
            options.hasInactive = false;
            FwMenu.addBrowseMenuButtons(options);
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
            //$browse.data('addBrowseMenuItems', (options: IAddBrowseMenuOptions) => {});
            FwModule.openBrowse($browse);

            return $browse;
        }

        openForm(mode: string) {
            let $form = jQuery(this.getFormTemplate());
            //$form.data('addFormMenuItems', (options: IAddFormMenuOptions) => {});
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
            }, null, $form);
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
                        let isMatch = false;
                        const caption = jQuery(this).data('property-caption');
                        if (caption !== undefined) {
                            isMatch = -1 != jQuery(this).data('property-caption').toUpperCase().indexOf(val);
                        }
                        return isMatch;
                    });
                    $nodes.attr('data-expanded', 'T');
                    $nodes.children('div.content').css('background-color', 'cornflowerblue');
                    checkParents($nodes);
                }
            });

            //TwGroupController.renderGroupTreePreview($form);
        };

        renderGroupTreePreview($form: JQuery) {
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

        renderNode(mode, $form: JQuery, $container: JQuery, node: IGroupSecurityNode) {
            var me = this;
            var hidenewmenuoptionsbydefault, haschildren, $node: JQuery, $content, $iconexpander, $icon, $iconvisible, $iconeditable,
                $caption, nodedescription, $childrencontainer, $children;

            let $waitOverlay;
            if (node.nodetype === 'System') {
               $waitOverlay = FwOverlay.showPleaseWaitOverlay($form.find('.securitytabpage'), FwAppData.generateUUID());
            }
            hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'HideNewMenuOptionsByDefault'));
            haschildren = (node.children.length > 0);
            $node = jQuery('<li class="node">');
            switch (node.nodetype) {
                case 'ModuleActions':
                    if ((node.children.length === 0) ||
                       (node.children.length === 1 && typeof node.children[0].properties.action === 'string' && node.children[0].properties.action === 'Browse' )) {
                        $node.hide();
                        const $parent = $node.closest('li.node');
                        const $moduleAction = $parent.find('.children li.node:visible');
                        if ($moduleAction.length === 0) {
                            $parent.find('.iconexpander').hide();
                        }
                    }
                    break;
                case 'ControllerMethod':
                    $node.addClass('advancedmode-remove');
                    break;
                case 'ModuleAction':
                case 'ControlAction':
                    switch (node.properties.action) {
                        case 'Browse':
                        case 'ControlBrowse':
                            $node.addClass('advancedmode-remove');
                            break;
                    }
                    break;
            }

            $node.attr('data-property-id', node.id)
                .attr('data-haschildren', haschildren ? 'T' : 'F')
                .attr('data-nodetype', node.nodetype)
                .attr('data-property-caption', node.caption)
                ;
            if (haschildren) {
                $node.attr('data-expanded', (node.id === "apptree") ? 'T' : 'F');
            }
            for (var key in node.properties) {
                if ((key === 'visible') &&
                    ((node.nodetype === 'System') || 
                    (node.nodetype === 'Controls') || 
                    (node.nodetype === 'ModuleActions') || 
                    (node.nodetype === 'ControlActions') || 
                    (node.nodetype === 'ModuleOptions') || 
                    (node.nodetype === 'ControlOptions') || 
                    (node.nodetype === 'ModuleAction' && node.properties['action'] === 'Browse') ||
                    (node.nodetype === 'ControlAction' && node.properties['action'] === 'ControlBrowse') ||
                    (node.nodetype === 'ControllerMethod'))) {
                    //$node.attr('data-property-visible', 'T');
                }
                else {
                    $node.attr('data-property-' + key, node.properties[key]);
                }
            }
            $content = jQuery('<div class="content">');

            //$iconexpander = jQuery('<div class="iconexpander">');
            $iconexpander = jQuery('<i class="material-icons iconexpander"></i>')
            switch (node.nodetype) {
                case 'ModuleAction':
                case 'ControlAction':
                case 'ControllerMethod':
                case 'ModuleOption':
                case 'ControlOption':
                    $iconexpander.addClass('advancedmode-hidden')
                    break;
            }
            $node.append($iconexpander);
            if (haschildren) {
                $iconexpander.on('click', function () {
                    var showchildren;
                    try {
                        showchildren = ($node.attr('data-expanded') === 'F');
                        jQuery(this).closest('.content').siblings('.childrencontainer').toggle(showchildren);
                        $node.attr('data-expanded', showchildren ? 'T' : 'F');
                        //if ($node.attr('data-nodetype') === 'Module') {
                        //    $node.find('li.node:not([data-nodetype="Control"],[data-nodetype="ModuleAction"],[data-nodetype="ModuleOptions"],[data-nodetype="ControlAction"],[data-nodetype="ControlOptions"])').attr('data-expanded', showchildren);
                        //}
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }

            $icon = jQuery('<i class="material-icons nodetypeicon"></i>');
            $node.append($icon);

            if ((mode === 'edit') && (typeof $node.attr('data-property-visible') === 'string')) {
                
                if (node.nodetype === 'Category' || 
                    (node.nodetype === 'ModuleAction' && (node.properties['action'] !== 'Browse')) || 
                    node.nodetype === 'Module' || 
                    node.nodetype === 'Control' ||
                    (node.nodetype === 'ControlAction' && (node.properties['action'] !== 'ControlBrowse')) || 
                    node.nodetype === 'ModuleOption' || node.nodetype == 'ControlOption') {
                    
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
                            // mv this is the code that prompts you if you want to toggle all children on or off.  I'm finding it more pestering than useful lately, so I have disabled it for now
                            //if ($li_children.length > 0) {
                            //    let $confirmation = FwConfirmation.renderConfirmation('Confirm...', 'Also toggle (' + (visible ? 'Off' : 'On') + ') all the children of this node?');
                            //    let $btnYes = FwConfirmation.addButton($confirmation, 'Yes', true);
                            //    $btnYes.on('click', function () {
                            //        $li_children.attr('data-property-visible', visible ? 'F' : 'T');
                            //        me.updateSecurityField($form);
                            //    });
                            //    let $btnNo = FwConfirmation.addButton($confirmation, 'No', true);
                            //    $btnNo.on('click', function () {
                            //        me.updateSecurityField($form);
                            //    });
                            //} else {
                            //    me.updateSecurityField($form);
                            //}
                            $li_children.attr('data-property-visible', visible ? 'F' : 'T');
                            me.updateSecurityField($form);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } 
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

            const caption = this.getCaptionFromConstantsFile(node);

            if (node.nodetype !== 'ControllerMethod') {
                const $contextmenuicon = jQuery(`<i class="material-icons" style="cursor:pointer;color:#607d8b;font-size:1.5em;">more_vert</i>`);
                $content.append($contextmenuicon);
                $contextmenuicon.on('click', async (event: JQuery.ClickEvent) => {
                    try {
                        var $contextmenu = FwContextMenu.render(null, 'bottomleft', $contextmenu, event);
                        FwContextMenu.addMenuItem($contextmenu, 'Copy to Groups...', async (event: JQuery.ClickEvent) => {
                            if ($form.attr('data-modified') === 'true') {
                                FwFunc.showMessage('You need to Save this Group before you can copy nodes to another Group.');
                                return;
                            }
                            let requestLookupGroup: FwAjaxRequest<any>;
                            try {
                                var $confirmation = FwConfirmation.renderConfirmation(`Copy node \'${caption}\' to...`, '');
                            
                                var $btnCopy = FwConfirmation.addButton($confirmation, 'Copy', false);
                                $btnCopy.on('click', async (event: JQuery.ClickEvent) => {
                                    let requestCopySecurityNode: FwAjaxRequest<any>;
                                    try {
                                        const $groups = $content.find('.available-groups');
                                        var selectedItems: string = FwFormField_checkboxlist.getValue2($groups);
                                        //console.log(selectedItems);
                                        requestCopySecurityNode = new FwAjaxRequest<any>();
                                        requestCopySecurityNode.httpMethod = "POST";
                                        requestCopySecurityNode.url = encodeURI(applicationConfig.apiurl + 'api/v1/group/copysecuritynode');
                                        requestCopySecurityNode.addAuthorizationHeader = true;
                                        requestCopySecurityNode.$elementToBlock = $content;
                                        requestCopySecurityNode.data = {
                                            FromGroupId: FwFormField.getValueByDataField($form, 'GroupId'),
                                            ToGroupIds: selectedItems,
                                            SecurityId: node.id
                                        };
                                        const response = await FwAjax.callWebApi<any, any>(requestCopySecurityNode);
                                        FwConfirmation.destroyConfirmation($confirmation);
                                    } catch (ex) {
                                        FwFunc.showWebApiError(requestCopySecurityNode.xmlHttpRequest.status, ex, requestCopySecurityNode.xmlHttpRequest.responseText, requestCopySecurityNode.url);
                                    }
                                });
                                var $btnCancel = FwConfirmation.addButton($confirmation, 'Cancel', false);
                                $btnCancel.on('click', (event: JQuery.ClickEvent) => {
                                    try {
                                        FwConfirmation.destroyConfirmation($confirmation);
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            
                                let html = 
`<div class="flexrow">
    <div class="flexcolumn">
        <div data-datafield="groups" data-control="FwFormField" data-returncsv="true" data-type="checkboxlist" data-share="true" data-listtype="standard" data-showcheckboxes="true" class="fwcontrol fwformfield available-groups" data-caption="Available Groups" data-sortable="false" data-orderby="false"></div>
    </div>
</div>
`;
                                const $content = jQuery(html);
                                FwConfirmation.addJqueryControl($confirmation, $content);
                                $content.find('.fwformfield[data-datafield="groups"] .fwformfield-control ol')
                                    .css({
                                        minHeight: 'auto',
                                        minWidth: 'auto'
                                    });

                                const $availableGroups = $content.find('.available-groups');
                                requestLookupGroup = new FwAjaxRequest<any>();
                                requestLookupGroup.httpMethod = "GET";
                                requestLookupGroup.url = encodeURI(applicationConfig.apiurl + 'api/v1/group/lookupgroup');
                                requestLookupGroup.addAuthorizationHeader = true;
                                requestLookupGroup.$elementToBlock = $content;
                                const response = await FwAjax.callWebApi<any, any>(requestLookupGroup);
                                const availableGroups = [];
                                for (let i = 0; i < response.Items.length; i++) {
                                    const group = response.Items[i];
                                    availableGroups.push({text: group.Name, value: group.GroupId});
                                }
                                FwFormField.loadItems($availableGroups, availableGroups, true);
                            } catch (ex) {
                                FwFunc.showWebApiError(requestLookupGroup.xmlHttpRequest.status, ex, requestLookupGroup.xmlHttpRequest.responseText, requestLookupGroup.url);
                            }
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }

            $caption = jQuery('<div class="caption">');
            nodedescription = '';
            switch (node.nodetype) {
                case 'System':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">verified_user</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                case 'Category':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">folder</i> <span style="color:#607d8b;">${caption}</span>`
                    break;
                case 'Module':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;font-size:1.5em;color:#607d8b;">extension</i> <span style="color:#607d8b;">${caption}</span>`
                    break;
                case 'ModuleActions':
                case 'ControlActions':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">play_for_work</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                case 'Controls':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">art_track</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                case 'ModuleOptions':
                case 'ControlOptions':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">menu</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                case 'ModuleAction':
                case 'ControlAction':
                    switch (node.properties.action) {
                        case 'Browse':
                        case 'ControlBrowse':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .2em;color:#607d8b;font-size:1.5em;">search</i> <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        case 'View':
                        case 'ControlView':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">tv</i> <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        case 'New':
                        case 'ControlNew':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">add_circle</i> <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        case 'Edit':
                        case 'ControlEdit':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">create</i>' <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        case 'Save':
                        case 'ControlSave':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">save</i> <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        case 'Delete':
                        case 'ControlDelete':
                            nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">clear</i> <span style="color:#607d8b;">${caption}</span>`;
                            break;
                        default:
                            nodedescription = caption;
                            break;
                    }
                    break;
                case 'Control':
                    if (typeof node.properties.controltype === 'string') {
                        nodedescription = `${node.properties.controltype}: ${caption}`;
                    }
                    break;
                case 'ControllerMethod':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">filter_drama</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                case 'ModuleOption':
                case 'ControlOption':
                    nodedescription = `<i class="material-icons" style="margin:0 .2em 0 .5em;color:#607d8b;font-size:1.5em;">touch_app</i> <span style="color:#607d8b;">${caption}</span>`;
                    break;
                default:
                    nodedescription = `<span style="font-size:.8em;color:#607d8b;">${node.nodetype}:</span> ${caption}`
                    break;
            }
            $caption.html(nodedescription);
            $content.append($caption);

            $node.append($content);
            $container.append($node);

            if (haschildren) {
                $childrencontainer = jQuery('<div class="childrencontainer">');
                $children = jQuery('<ul class="children">');
                $childrencontainer.append($children);
                $node.append($childrencontainer);
                for (var i = 0; i < node.children.length; i++) {
                    const nodeChild = node.children[i];
                    if (((mode === 'edit') || (typeof nodeChild.properties.visible === 'undefined') || (nodeChild.properties.visible === 'T')) &&
                        (nodeChild.id !== 'AdministratorControls' && nodeChild.id !== 'HomeControls' && nodeChild.id !== 'SharedControls' && nodeChild.id !== 'UtilitiesControls')) {
                        this.renderNode(mode, $form, $children, nodeChild);
                    }
                }
            }
            if (node.nodetype === 'System') {
                FwOverlay.hideOverlay($waitOverlay);
                $node.children('.iconexpander').click();
            }
        }

        getCaptionFromConstantsFile(securityNode: IGroupSecurityNode): string {
            let caption = securityNode.caption;
            const categories = (<any>window).Constants.Modules;
            for (const categoryName in categories) {
                const category = categories[categoryName];
                const result = this.getCaptionFromConstantsFileRecursive(category, securityNode.id);
                if (typeof result === 'string') {
                    caption = result;
                    break;
                }
            }
            return caption;
        }

        getCaptionFromConstantsFileRecursive(node:IGroupSecurityNode, securityid: string): string {
            let caption = null;
            if (node.id === securityid) {
                caption = node.caption;
            }
            if (caption === null) {
                for (const key in node.children) {
                    const childNode = node.children[key];
                    const result = this.getCaptionFromConstantsFileRecursive(childNode, securityid);
                    if (typeof result === 'string') {
                        caption = result;
                        break;
                    }
                }
            }
            return caption
        }

        getGroupTreeJson($form: JQuery) {
            var $apptreenode = $form.find('.editgrouptree > ul.grouptree > li');
            var grouptree = this.getGroupTreeJsonNode($form, null, $apptreenode);
            return grouptree;
        }

        getGroupTreeJsonNode($form: JQuery, parent: IGroupSecurityNode[], $node: JQuery) {
            let node: IGroupSecurityNode = {
                id: '',
                nodetype: '',
                properties: {},
                children: []
            };
            // process node's attributes
            for (let attributeIndex = 0; attributeIndex < $node[0].attributes.length; attributeIndex++) {
                const attribute = $node[0].attributes[attributeIndex];
                if (attribute.name.indexOf('data-property-') === 0) {
                    let property = attribute.name.substring(14, attribute.name.length);
                    if (property === 'id') {
                        node.id = attribute.value;
                    } 
                    else {
                        node.properties[property] = attribute.value;
                    }
                }
            }
            // process node's children
            const $children = $node.find('> .childrencontainer > ul.children > li');
            for (let childIndex = 0; childIndex < $children.length; childIndex++) {
                const $child = $children.eq(childIndex);
                const child = this.getGroupTreeJsonNode($form, node.children, $child);
                node.children.push(child);
            }

            return node;
        }

        updateSecurityField($form: JQuery) {
            var apptreenode, hidenewmenuoptionsbydefault, securitynodes, securityJson;
            try {
                apptreenode = this.getGroupTreeJson($form);
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

        saveForm($form: JQuery, parameters: any) {
            FwModule.saveForm(this.Module, $form, parameters);
        }

        //loadAudit($form: any) {
        //    var uniqueid;
        //    uniqueid = $form.find('div.fwformfield[data-datafield="GroupId"] input').val();
        //    FwModule.loadAudit($form, uniqueid);
        //}

        afterLoad($form: JQuery) {
            let $editgrouptree = $form.find('.editgrouptree');
            let $previewgrouptree = $form.find('.previewgrouptree');
            let GroupId = FwFormField.getValueByDataField($form, 'GroupId');

            $form.find('[data-type="tab"][data-caption="Security"]').one('click', e => {
                this.loadGroupTree($editgrouptree, $previewgrouptree, $form, GroupId);
            });

            $form.find('.cbAdvancedMode input.fwformfield-value').on('click', (e: JQuery.ClickEvent) => {
                try {
                    const isAdvancedMode = jQuery(e.currentTarget).prop('checked');
                    $form.find('.editgrouptree').attr('data-advancedmode', isAdvancedMode);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
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
                <div id="groupform" class="fwcontrol fwcontainer fwform fwgroup" data-control="FwContainer" data-type="form" data-version="1" data-caption="Group" data-rendermode="template" data-tablename="groups" data-mode="" data-hasaudit="false" data-controller="GroupController">
                    <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="GroupId"></div>
                        <div id="groupform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs">
                            <div class="tabs">
                                <div data-type="tab" id="grouptab" class="tab" data-tabpageid="grouptabpage" data-caption="Group"></div>
                                <div data-type="tab" id="securitytab" class="tab" data-tabpageid="securitytabpage" data-caption="Security"></div>
                            </div>
                            <div class="tabpages">
                                <div data-type="tabpage" id="grouptabpage" class="tabpage grouptabpage" data-tabid="grouptab">
                                    <div class="formpage">
                                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Group">
                                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Name" data-required="true"></div>
                                            </div>
                                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Memo"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div data-type="tabpage" id="securitytabpage" class="tabpage securitytabpage" data-tabid="securitytab">
                                <div class="formpage">
                                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="display:none">
                                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Items in Security Tree are Hidden by Default (IF YOU CHANGE THIS, SAVE AND CLOSE BEFORE EDITING THE TREE)" data-datafield="HideNewMenuOptionsByDefault"></div>
                                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="Security" data-allcaps="false" style="display:none;"></div>
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
                                                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield cbAdvancedMode" data-caption="Advanced View (shows REST endpoints)" style="display:none"></div>
                                                <div class="editgrouptree" data-advancedmode="false" style="float:left;"></div>
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

