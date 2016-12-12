//----------------------------------------------------------------------------------------------
var RwGroupController = {
    Module: 'Group'
};
RwGroupController.ModuleOptions = jQuery.extend({}, FwModule.ModuleOptions, RwGroupController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwGroupController.addGridSubMenu = function($control, $menu) {
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
RwGroupController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwGroupController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwGroupController.openBrowse();

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
RwGroupController.openBrowse = function() {
    var $browse;
    $browse = jQuery(jQuery('#tmpl-modules-GroupBrowse').html());
    $browse = FwModule.openBrowse($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwGroupController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-GroupForm').html());
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwGroupController.loadForm = function(uniqueids) {
    var $form, $editgrouptree, $previewgrouptree;
    
    $form = RwGroupController.openForm('EDIT');

    //$form.on('change', '[data-datafield="groups.security"]', function() {
    //    TwGroupController.renderGroupTreePreview($form);
    //});

    $form.find('div.fwformfield[data-datafield="groups.groupsid"] input').val(uniqueids.groupsid);
    FwModule.loadForm(RwGroupController.Module, $form);
    $editgrouptree = $form.find('.editgrouptree');
    $previewgrouptree = $form.find('.previewgrouptree');
    RwGroupController.loadGroupTree($editgrouptree, $previewgrouptree, $form, uniqueids.groupsid);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwGroupController.loadGroupTree = function($editgrouptree, $previewgrouptree, $form, groupsid) {
    var request;
    
    request = {};
    request.method   = 'getapplicationtree';
    request.groupsid = groupsid;
    FwModule.getData($form, request, 
        function(response) {
            try {
                if (typeof response.applicationtree === 'object') {
                    $previewgrouptree.data('applicationtree', response.applicationtree);
                    RwGroupController.render($form, $editgrouptree, $previewgrouptree, response.applicationtree);
                    RwGroupController.updateSecurityField($form);
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        },
        $form
    );
};
//----------------------------------------------------------------------------------------------
RwGroupController.render = function($form, $editgrouptree, $previewgrouptree, applicationtree) {
    var $editgrouptree_children, $previewgrouptree_children
    
    $editgrouptree_children = jQuery('<ul class="grouptree"></ul>');
    $editgrouptree.empty()
        //.append('<div class="title">Edit Group Tree</div>')
        .append($editgrouptree_children);
    RwGroupController.renderNode('edit', $form, $editgrouptree_children, applicationtree);

    //TwGroupController.renderGroupTreePreview($form);
};
//----------------------------------------------------------------------------------------------
RwGroupController.renderGroupTreePreview = function($form) {
    var $previewgrouptree, $previewgrouptree_children, applicationtree;
    $previewgrouptree = $form.find('.previewgrouptree');
    $previewgrouptree_children = jQuery('<ul class="grouptree"></ul>');
    $previewgrouptree.empty().append('<div class="title">Preview Group Tree</div>', $previewgrouptree_children);
    applicationtree = $previewgrouptree.data('applicationtree');
    RwGroupController.renderNode('preview', $form, $previewgrouptree_children, applicationtree);
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
RwGroupController.renderNode = function(mode, $form, $container, node) {
    var hidenewmenuoptionsbydefault, haschildren, $node, $content, $iconexpander, $icon, $iconvisible, $iconeditable,
        $caption, nodedescription, $childrencontainer, $children;

    hidenewmenuoptionsbydefault = (FwFormField.getValueByDataField($form, 'groups.hidenewmenuoptionsbydefault'));
    haschildren = (node.children.length > 0);
    $node = jQuery('<li>')
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
                            RwGroupController.updateSecurityField($form);
                        });
                } else {
                    RwGroupController.updateSecurityField($form);
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
                    RwGroupController.updateSecurityField($form);
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
                RwGroupController.renderNode(mode, $form, $children, node.children[i]);
            }
        }
    }
};
//----------------------------------------------------------------------------------------------
RwGroupController.getGroupTreeJson = function($form) {
    var $apptreenode = jQuery('.editgrouptree > ul.grouptree > li[data-property-nodetype="System"]');
    var grouptree = RwGroupController.getGroupTreeJsonNode($form, null, $apptreenode);
    return grouptree;
};
//----------------------------------------------------------------------------------------------
RwGroupController.getGroupTreeJsonNode = function($form, parent, $node) {
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
        child = RwGroupController.getGroupTreeJsonNode($form, node.children, $child);
        node.children.push(child);
    }

    return node;
};
//----------------------------------------------------------------------------------------------
RwGroupController.updateSecurityField = function($form) {
    var apptreenode, hidenewmenuoptionsbydefault, securitynodes, securityJson;
    try {
        apptreenode                 = RwGroupController.getGroupTreeJson();
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
RwGroupController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwGroupController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwGroupController.deleteForm = function($form) {
    FwModule.deleteForm(RwGroupController.Module, $form);
};
//----------------------------------------------------------------------------------------------
RwGroupController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="groups.groupsid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------