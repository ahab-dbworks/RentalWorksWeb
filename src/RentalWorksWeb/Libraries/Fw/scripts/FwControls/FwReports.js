//----------------------------------------------------------------------------------------------
var FwReports = {};
//----------------------------------------------------------------------------------------------
FwReports.init = function () {

};
//----------------------------------------------------------------------------------------------
FwReports.renderRuntimeHtml = function ($control) {
    var html = [];

    html.push('<div class="fwsettingsheader">');
    html.push('  <div class="input-group pull-right">');
    html.push('    <input type="text" id="settingsSearch" class="form-control" placeholder="Search..." autofocus>');
    html.push('    <span class="input-group-addon">');
    html.push('      <i class="material-icons">search</i>');
    html.push('    </span>');
    html.push('  </div>');
    html.push('</div>');
    html.push('<div class="well"></div>');

    var settingsMenu = FwReports.getHeaderView($control);

    $control.html(html.join(''));

    $control.find('.fwsettingsheader').append(settingsMenu);
};
//----------------------------------------------------------------------------------------------
FwReports.getCaptions = function (screen) {
    var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
    var modules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
    for (var i = 0; i < modules.length; i++) {
        var moduleName = modules[i].properties.controller;
        var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
        var $fwformfields = $form.find('.fwformfield[data-caption]');
        for (var j = 0; j < $fwformfields.length; j++) {
            var $field = $fwformfields.eq(j);
            var caption = $field.attr('data-caption').toUpperCase();
            if (typeof screen.moduleCaptions[caption] === 'undefined') {
                screen.moduleCaptions[caption] = {};
            }
            if (typeof screen.moduleCaptions[caption][moduleName] === 'undefined') {
                screen.moduleCaptions[caption][moduleName] = [];
            }
            screen.moduleCaptions[caption][moduleName].push($field);
        }
    }
}
//---------------------------------------------------------------------------------------------- 
FwReports.filter = [];
//---------------------------------------------------------------------------------------------- 
FwReports.renderModuleHtml = function ($control, title, moduleName, description, menu, moduleId) {
    var html = [], $settingsPageModules, $rowBody, $modulecontainer, apiurl, $body, $form, browseKeys = [], rowId, screen = { 'moduleCaptions': {} }, filter = [];

    $modulecontainer = $control.find('#' + moduleName);
    apiurl = window[moduleName + 'Controller'].apiurl;
    $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

    html.push('<div class="panel-group" id="' + moduleName + '" data-id="' + moduleId + '">');
    html.push('  <div class="panel panel-primary">');
    html.push('    <div data-toggle="collapse" data-target="' + moduleName + '" href="' + moduleName + '" class="panel-heading">');
    html.push('      <h4 class="panel-title">');
    html.push('        <a id="title" data-toggle="collapse">' + menu + ' - ' + title)
    html.push('          <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
    html.push('        </a>');
    html.push('      </h4>');
    if (description === "") {
        html.push('      <small id="description" style="display:none;">' + moduleName + '</small>');
        html.push('      <small>' + moduleName + '</small>');
    } else {
        html.push('      <small id="description" style="display:none;">' + description + '</small>');
        html.push('      <small>' + description + '</small>');
    }
    html.push('    </div>');
    html.push('    <div class="panel-collapse collapse" style="display:none; "><div class="panel-body" id="' + moduleName + '"></div></div>');
    html.push('  </div>');
    html.push('</div>');
    $settingsPageModules = jQuery(html.join(''));

    $control.find('.well').append($settingsPageModules);

    $settingsPageModules.on('click', '.btn', function (e) {
        $settingsPageModules.find('.heading-menu').next().css('display', 'none');
        $body = $control.find('#' + moduleName + '.panel-body');
    });    

    $settingsPageModules
        .on('click', '.panel-heading', function (e) {
            var $this, moduleName, $reports, $modulecontainer, apiurl, $body, rowId, formKeys = [], $form, duplicateDatafields, withoutDuplicates;

            $this = jQuery(this);
            moduleName = $this.closest('.panel-group').attr('id');
            $browse = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
            $modulecontainer = $control.find('#' + moduleName);
            apiurl = window[moduleName + 'Controller'].apiurl;
            $body = $control.find('#' + moduleName + '.panel-body');
            duplicateDatafields = {};
            withoutDuplicates = [];
            if ($body.is(':empty')) {
                $reports = window[moduleName + 'Controller'].openForm();
                window[moduleName + 'Controller'].onLoadForm($reports);
                $body.append($reports);               
            }

            if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
                $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up')
                $settingsPageModules.find('.panel-collapse').show("fast");
            } else {
                $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_down')
                $settingsPageModules.find('.panel-collapse').hide('fast');
            }
        })
        .on('click', '.heading-menu', function (e) {
            e.stopPropagation();
            if (jQuery(this).next().css('display') === 'none') {
                jQuery(this).next().css('display', 'flex');
            } else {
                jQuery(this).next().css('display', 'none');
            }
        })
        ;

    $control
        .unbind().on('click', '.row-heading', function (e) {
            e.stopPropagation();
            var formKeys = [], formData = [], recordData, $rowBody, $form, moduleName, moduleId, controller, uniqueids = {};
            recordData = jQuery(this).parent().parent().data('recorddata');
            moduleName = jQuery(this).closest('div.panel-group')[0].id;
            $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
            moduleId = jQuery($form.find('.fwformfield[data-isuniqueid="true"]')[0]).data('datafield');
            uniqueids[moduleId] = recordData[moduleId];
            $rowBody = $control.find('#' + recordData[moduleId] + '.panel-body');
            controller = $form.data('controller');

            if ($rowBody.is(':empty')) {
                $form = window[controller].openForm('EDIT');
                $rowBody.append($form);
                $form.find('.highlighted').removeClass('highlighted');
                $form.find('div[data-type="NewMenuBarButton"]').off();

            if ($rowBody.css('display') === 'none' || $rowBody.css('display') === undefined) {
                $rowBody.parent().find('.record-selector').html('keyboard_arrow_up');
                $rowBody.show("fast");
            } else {
                $rowBody.parent().find('.record-selector').html('keyboard_arrow_down');
                $rowBody.hide("fast");
                }
            }
        })


    $control.on('keypress', '#settingsSearch', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            jQuery(this).closest('.fwsettings').find('.data-panel:parent').parent().find('.row-heading').click();
            jQuery(this).closest('.fwsettings').find('.data-panel:parent').empty();

            var $settings, val, $module;

            FwReports.getCaptions(screen);
            filter = [];
            $settings = jQuery('small#description');
            $module = jQuery('a#title');
            val = jQuery.trim(this.value).toUpperCase();
            if (val === "") {
                $settings.closest('div.panel-group').show();
            } else {
                var results = [];
                $settings.closest('div.panel-group').hide();
                for (var caption in screen.moduleCaptions) {
                    if (caption.indexOf(val) !== -1) {
                        for (var moduleName in screen.moduleCaptions[caption]) {
                            filter.push(screen.moduleCaptions[caption][moduleName][0].data().datafield);
                            results.push(moduleName.toUpperCase());
                        }
                    }
                }
                FwReports.filter = filter;
                for (var i = 0; i < results.length; i++) {
                    var module = $settings.filter(function () {
                        return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                    }).closest('div.panel-group');
                    module.find('.highlighted').removeClass('highlighted');
                    module.show();
                }
                $module.filter(function () {
                    return -1 != jQuery(this).text().toUpperCase().indexOf(val);
                }).closest('div.panel-group').show();
            }
        }
    });

    return $settingsPageModules;
};


FwReports.getHeaderView = function ($control) {
    var $view;

    $view = jQuery('<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>');

    FwControl.renderRuntimeControls($view);

    var nodeApplications, nodeApplication = null, baseiconurl, $menu, ribbonItem, dropDownMenuItems, caption;
    nodeApplications = FwApplicationTree.getMyTree();
    for (var appno = 0; appno < nodeApplications.children.length; appno++) {
        if (nodeApplications.children[appno].id === '0A5F2584-D239-480F-8312-7C2B552A30BA') {
            nodeApplication = nodeApplications.children[appno];
        }
    }
    if (nodeApplication === null) {
        sessionStorage.clear();
        window.location.reload(true);
    }
    baseiconurl = 'theme/images/icons/home/';
    for (var lv1childno = 0; lv1childno < nodeApplication.children.length; lv1childno++) {
        var nodeLv1MenuItem = nodeApplication.children[lv1childno];
        if (nodeLv1MenuItem.properties.visible === 'T' && nodeLv1MenuItem.properties.caption === 'Reports') {
            switch (nodeLv1MenuItem.properties.nodetype) {
                case 'Lv1ReportsMenu':
                    $menu = FwFileMenu.addMenu($view, nodeLv1MenuItem.properties.caption)
                    for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                        var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                        if (nodeLv2MenuItem.properties.visible === 'T') {
                            switch (nodeLv2MenuItem.properties.nodetype) {
                                case 'ReportsMenu':
                                    dropDownMenuItems = [];
                                    for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                        var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                        if (nodeLv3MenuItem.properties.visible === 'T') {
                                            dropDownMenuItems.push({ id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl, moduleName: nodeLv3MenuItem.properties.controller.slice(0, -10)});
                                        }
                                    }
                                    FwReports.generateDropDownModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                    break;
                                case 'ReportsModule':
                                    FwReports.generateStandardModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl, nodeLv2MenuItem.properties.controller.slice(0, -10));
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    return $view;
};
//----------------------------------------------------------------------------------------------
FwReports.generateDropDownModuleBtn = function ($menu, $control, securityid, caption, imgurl, subitems) {
    var $modulebtn, $settings, btnHtml, subitemHtml, $subitem, version;

    version = $menu.closest('.fwfilemenu').attr('data-version');
    securityid = (typeof securityid === 'string') ? securityid : '';
    $modulebtn = jQuery();
    if ((caption !== '') && (typeof caption !== 'undefined')) {
        try {
            btnHtml = [];
            btnHtml.push('<div id="btnModule' + securityid + '" class="ddmodulebtn" data-securityid="' + securityid + '">');
            btnHtml.push('<div class="ddmodulebtn-caption">');
            btnHtml.push('<div class="ddmodulebtn-text">');
            btnHtml.push(caption);
            btnHtml.push('</div>');
            if (version == '1') { btnHtml.push('<i class="material-icons">&#xE315;</i>'); } //keyboard_arrow_right
            if (version == '2') { btnHtml.push('<i class="material-icons">&#xE5CC;</i>'); } //chevron_right
            btnHtml.push('</div>');
            btnHtml.push('<div class="ddmodulebtn-dropdown" style="display:none"></div>');
            btnHtml.push('</div>');
            $modulebtn = jQuery(btnHtml.join(''));

            subitemHtml = [];
            subitemHtml.push('<div id="" class="ddmodulebtn-dropdown-btn">');
            subitemHtml.push('<div class="ddmodulebtn-dropdown-btn-text"></div>');
            subitemHtml.push('</div>');
            jQuery.each(subitems, function (index, value) {
                $subitem = jQuery(subitemHtml.join(''));
                $subitem.attr('data-securityid', subitems[index].id);
                $subitem.find('.ddmodulebtn-dropdown-btn-text').html(value.caption);

                $subitem.on('click', function () {
                    try {
                        if ($control.find('#' + value.moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                            $control.find('#' + value.moduleName + ' > div > div.panel-heading').click();
                        }
                        jQuery('html, body').animate({
                            scrollTop: $control.find('#' + value.moduleName).offset().top
                        }, 1);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                $modulebtn.find('.ddmodulebtn-dropdown').append($subitem);
            });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        throw 'FwRibbon.generateDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
    }

    $menu.find('.menu').append($modulebtn);
};
//----------------------------------------------------------------------------------------------
FwReports.generateStandardModuleBtn = function ($menu, $control, securityid, caption, modulenav, imgurl, moduleName) {
    var $modulebtn, btnHtml, btnId, version;
    securityid = (typeof securityid === 'string') ? securityid : '';
    $modulebtn = jQuery();
    if ((caption !== '') && (typeof caption !== 'undefined')) {
        try {
            btnId = 'btnModule' + securityid;
            btnHtml = [];
            btnHtml.push('<div id="' + btnId + '" class="modulebtn" data-securityid="' + securityid + '">');
            btnHtml.push('<div class="modulebtn-text">');
            btnHtml.push(caption);
            btnHtml.push('</div>');
            btnHtml.push('</div>');
            $modulebtn = $modulebtn.add(btnHtml.join(''));
        } catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        throw 'FwRibbon.generateStandardModuleBtn: ' + caption + ' caption is not defined in translation';
    }

    $modulebtn
        .on('click', function () {
            try {
                if (modulenav != '') {
                    if ($control.find('#' + moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                        $control.find('#' + moduleName + ' > div > div.panel-heading').click();
                    }
                    jQuery('html, body').animate({
                        scrollTop: $control.find('#' + moduleName).offset().top
                    }, 1);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        ;

    $menu.find('.menu').append($modulebtn);
};
//----------------------------------------------------------------------------------------------