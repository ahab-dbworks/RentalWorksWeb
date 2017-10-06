//----------------------------------------------------------------------------------------------
var GroupController = {
    Module: 'Group'
};
GroupController.ModuleOptions = jQuery.extend({}, FwModule.ModuleOptions, GroupController.ModuleOptions);
//----------------------------------------------------------------------------------------------
GroupController.addGridSubMenu = function($control, $menu) {
    // Import Security Tree...
    FwApplicationTree.clickEvents['{28C16B0D-70CD-461B-A78E-967135300B56}'] = function(event) {
        var $confirmation;
        $confirmation = FwConfirmation.renderConfirmation('Import File', 'Select a file to import...');
    };

    // Export Security Tree...
    FwApplicationTree.clickEvents['{0324DFE7-D8E5-4BE7-8F3C-3D18B6AF8469}'] = function(event) {

    };
}
//----------------------------------------------------------------------------------------------
GroupController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl(this.Module + 'Controller');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = GroupController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Groups', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
GroupController.openBrowse = function() {
    var $browse;
    $browse = jQuery(jQuery('#tmpl-modules-GroupBrowse').html());
    $browse = FwModule.openBrowse($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
GroupController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-GroupForm').html());
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
GroupController.loadForm = function(uniqueids) {
    var $form, $editgrouptree, $previewgrouptree;
    
    $form = GroupController.openForm('EDIT');

    //$form.on('change', '[data-datafield="groups.security"]', function() {
    //    TwGroupController.renderGroupTreePreview($form);
    //});

    $form.find('div.fwformfield[data-datafield="groups.groupsid"] input').val(uniqueids.groupsid);
    FwModule.loadForm(GroupController.Module, $form);
    $editgrouptree = $form.find('.editgrouptree');
    $previewgrouptree = $form.find('.previewgrouptree');
    GroupController.loadGroupTree($editgrouptree, $previewgrouptree, $form, uniqueids.groupsid);

    return $form;
};
//----------------------------------------------------------------------------------------------
GroupController.loadGroupTree = function($editgrouptree, $previewgrouptree, $form, groupsid) {
    var request;
    
    request = {};
    request.method   = 'getapplicationtree';
    request.groupsid = groupsid;
    FwModule.getData($form, request, 
        function(response) {
            try {
                if (typeof response.applicationtree === 'object') {
                    $previewgrouptree.data('applicationtree', response.applicationtree);
                    GroupController.render($form, $editgrouptree, $previewgrouptree, response.applicationtree);
                    GroupController.updateSecurityField($form);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        },
        $form
    );
};
//----------------------------------------------------------------------------------------------
GroupController.render = function($form, $editgrouptree, $previewgrouptree, applicationtree) {
    var $editgrouptree_children, $previewgrouptree_children, searchbarHtml = [];
    
    $editgrouptree_children = jQuery('<ul class="grouptree"></ul>');
    $editgrouptree.empty()
        //.append('<div class="title">Edit Group Tree</div>')
        .append($editgrouptree_children);
    GroupController.renderNode('edit', $form, $editgrouptree_children, applicationtree);

    $searchbar = $form.find('.searchbar');
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
        jQuery.fn.checkParents = function () {
                checkParents(jQuery(this).parent().closest('li'));
        }
        if (val === "") {
            $node.attr('data-expanded', 'T');
        }
        else {
            $node.attr('data-expanded', 'F').children('div.content').css('background-color', 'rgba(100,100,100,.1)');
            $node.filter(function () {
                return -1 != jQuery(this).data().propertyCaption.toUpperCase().indexOf(val);        
            }).attr('data-expanded', 'T').children('div.content').css('background-color', 'cornflowerblue').checkParents();
        }
    });

    //TwGroupController.renderGroupTreePreview($form);
};
//----------------------------------------------------------------------------------------------
GroupController.renderGroupTreePreview = function($form) {
    var $previewgrouptree, $previewgrouptree_children, applicationtree;
    $previewgrouptree = $form.find('.previewgrouptree');
    $previewgrouptree_children = jQuery('<ul class="grouptree"></ul>');
    $previewgrouptree.empty().append('<div class="title">Preview Group Tree</div>', $previewgrouptree_children);
    applicationtree = $previewgrouptree.data('applicationtree');
    GroupController.renderNode('preview', $form, $previewgrouptree_children, applicationtree);
    var $previewSubModules = jQuery('.previewgrouptree li[data-property-id="712A2E4B-4387-4D55-9B35-0C2DCBD9B284"]');
    if ($previewSubModules.find('> .childrencontainer > ul.children > li').length === 0) {
        $previewSubModules.remove();
    }
    var $previewGrids = jQuery('.previewgrouptree li[data-property-id="86E25EC7-76D6-4BA5-9997-494441FEC432"]');
    if ($previewGrids.find('> .childrencontainer > ul.children > li').length === 0) {
        $previewGrids.remove();
    }
};
//----------------------------------------------------------------------------------------------
GroupController.renderNode = function(mode, $form, $container, node) {
    var hidenewmenuoptionsbydefault, haschildren, $node, $content, $iconexpander, $icon, $iconvisible, $iconeditable,
        $caption, nodedescription, $childrencontainer, $children;

    hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'groups.hidenewmenuoptionsbydefault'));
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

    $iconexpander = jQuery('<div class="iconexpander">');
    $node.append($iconexpander);
    if (haschildren) {
        $iconexpander.on('click', function() {
            var showchildren;
            try {
                showchildren = ($node.attr('data-expanded') === 'F');
                jQuery(this).closest('.content').siblings('.childrencontainer').toggle(showchildren);
                $node.attr('data-expanded', showchildren ? 'T' : 'F')
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    $icon = jQuery('<div class="icon">');
    if (typeof node.properties.iconurl === 'string') {
        $icon.css({
            'background-image': 'url(' + node.properties.iconurl + ')',
            'display': 'block'
        });
    }
    $node.append($icon);

    if ((mode === 'edit') && (typeof $node.attr('data-property-visible') === 'string')) {
        $iconvisible = jQuery('<div class="iconvisible">');
        $content.append($iconvisible);
        $iconvisible.on('click', function() {
            var $li, visible, $li_children;
            try {
                $li     = jQuery(this).closest('li');
                visible = ($li.attr('data-property-visible') === 'T');
                $li.attr('data-property-visible', visible ? 'F' : 'T');
                $li_children = $li.find('li[data-property-visible]');
                if ($li_children.length > 0) {
                    FwConfirmation.yesNo('Alert', (visible ? 'Hide' : 'Show') + ' all the children of this node?', 
                        function() {
                            $li.find('li[data-property-visible]').attr('data-property-visible', visible ? 'F' : 'T');
                            GroupController.updateSecurityField($form);
                        });
                } else {
                    GroupController.updateSecurityField($form);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    if ((mode === 'edit') && (typeof $node.attr('data-property-editable') === 'string')) {
        $iconeditable = jQuery('<div class="iconeditable">');
        $content.append($iconeditable);
        $iconeditable.on('click', function() {
            var $li, visible, editable;
            try {
                $li      = jQuery(this).closest('li');
                visible  = ($li.attr('data-property-visible') === 'T');
                if (visible) {
                    editable = ($li.attr('data-property-editable') === 'T');
                    $li.attr('data-property-editable', (!editable) ? 'T' : 'F');
                    GroupController.updateSecurityField($form);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }
    
    $caption = jQuery('<div class="caption">');
    nodedescription = '';
    switch(node.properties.nodetype) {
        case 'Browse':
        case 'Form':
        case 'Components':
        case 'MenuBar':
        case 'Lv1SubModulesMenu':
        case 'Lv1GridsMenu':
        case 'SubMenu':
            break;
        case 'Lv1ModuleMenu':
            nodedescription = 'Group: ';
            break;
        case 'Lv2ModuleMenu':
            nodedescription = 'Menu: ';
            break;
        case 'SubModule':
            nodedescription = 'SubModule: ';
            break;
        case 'SubMenuGroup':
            nodedescription = 'Group: ';
            break;
        case 'SubMenuItem':
        case 'DownloadExcelSubMenuItem':
            nodedescription = 'MenuItem: ';
            break;
        case 'NewMenuBarButton':
        case 'ViewMenuBarButton':
        case 'EditMenuBarButton':
        case 'DeleteMenuBarButton':
        case 'SaveMenuBarButton':
            nodedescription = 'MenuBarButton: ';
            break;
         case 'FormGrid':
            nodedescription = 'Grid: ';
            break;
        default:
            nodedescription = node.properties.nodetype + ': '
            break;
    }
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
                GroupController.renderNode(mode, $form, $children, node.children[i]);
            }
        }
    }
};
//----------------------------------------------------------------------------------------------
GroupController.getGroupTreeJson = function($form) {
    var $apptreenode = jQuery('.editgrouptree > ul.grouptree > li[data-property-nodetype="System"]');
    var grouptree = GroupController.getGroupTreeJsonNode($form, null, $apptreenode);
    return grouptree;
};
//----------------------------------------------------------------------------------------------
GroupController.getGroupTreeJsonNode = function($form, parent, $node) {
    var node, attribute, property, $children, $child, child, index;
    
    node = {
        id: '',
        properties: {},
        children: []
    };
    // process node's attributes
    for (index= 0; index < $node[0].attributes.length; index++) {
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
        child = GroupController.getGroupTreeJsonNode($form, node.children, $child);
        node.children.push(child);
    }

    return node;
};
//----------------------------------------------------------------------------------------------
GroupController.updateSecurityField = function($form) {
    var apptreenode, hidenewmenuoptionsbydefault, securitynodes, securityJson;
    try {
        apptreenode                 = GroupController.getGroupTreeJson();
        hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'groups.hidenewmenuoptionsbydefault'));
        securitynodes               = FwApplicationTree.getSecurityNodes(apptreenode, hidenewmenuoptionsbydefault);
        securityJson                = JSON.stringify(securitynodes);
        if (securityJson === '[]') {
            securityJson = '';
        }
        FwFormField.setValueByDataField($form, 'groups.security', securityJson, null, true);
    } catch(ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
GroupController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(GroupController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
GroupController.deleteForm = function($form) {
    FwModule.deleteForm(GroupController.Module, $form);
};
//----------------------------------------------------------------------------------------------
GroupController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="groups.groupsid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------