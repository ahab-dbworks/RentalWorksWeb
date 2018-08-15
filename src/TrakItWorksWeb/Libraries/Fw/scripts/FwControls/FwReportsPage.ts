class FwReportsPageClass {
    filter = [];
    //----------------------------------------------------------------------------------------------
    init() {

    }
    //----------------------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html = [];

        html.push('<div class="fwreportsheader">');
        html.push('  <div class="input-group pull-right">');
        html.push('    <input type="text" id="reportsSearch" class="form-control" placeholder="Search..." autofocus>');
        html.push('    <span class="input-group-addon">');
        html.push('      <i class="material-icons">search</i>');
        html.push('    </span>');
        html.push('  </div>');
        html.push('</div>');
        html.push('<div class="well"></div>');

        var reportsMenu = this.getHeaderView($control);

        $control.html(html.join(''));

        $control.find('.fwreportsheader').append(reportsMenu);
    }
    //----------------------------------------------------------------------------------------------
    getCaptions(screen) {
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '7FEC9D55-336E-44FE-AE01-96BF7B74074C');
        var modules = FwApplicationTree.getChildrenByType(node, 'ReportsModule');
        for (var i = 0; i < modules.length; i++) {
            var moduleName = modules[i].properties.controller;
            var $form = window[moduleName].openForm();
            var $fwformfields = $form.find('.fwformfield[data-caption]');
            for (var j = 0; j < $fwformfields.length; j++) {
                var $field = $fwformfields.eq(j);
                var caption = $field.attr('data-caption').toUpperCase();
                if ($field.attr('data-type') === 'radio') {
                    var radioCaptions = $field.find('div');
                    for (var k = 0; k < radioCaptions.length; k++) {
                        var radioCaption = jQuery(radioCaptions[k]).attr('data-caption').toUpperCase()
                        screen.moduleCaptions[radioCaption] = {};
                        screen.moduleCaptions[radioCaption][moduleName] = [];
                        screen.moduleCaptions[radioCaption][moduleName].push($field);
                    }
                }
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
    renderModuleHtml($control, title, moduleName, description, menu, moduleId) {
        var me = this;
        var html = [], $reportsPageModules, $rowBody, $modulecontainer, $body, $form, browseKeys = [], rowId, screen = { 'moduleCaptions': {} }, filter = [];

        $modulecontainer = $control.find('#' + moduleName);
        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

        html.push('<div class="panel-group" id="' + moduleName + '" data-id="' + moduleId + '">');
        html.push('  <div class="panel panel-primary">');
        html.push('    <div data-toggle="collapse" data-target="' + moduleName + '" href="' + moduleName + '" class="panel-heading">');
        html.push('      <h4 class="panel-title">');
        html.push('        <a id="title" data-toggle="collapse">' + menu + ' - ' + title)
        html.push('          <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
        html.push('        </a>');
        html.push('        <i class="material-icons heading-menu">more_vert</i>');
        html.push('        <div id="myDropdown" class="dropdown-content">');
        html.push('          <a class="pop-out">Pop Out Module</a>');
        html.push('        </div>');
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
        $reportsPageModules = jQuery(html.join(''));

        $control.find('.well').append($reportsPageModules);

        $reportsPageModules.on('click', '.btn', function (e) {
            $reportsPageModules.find('.heading-menu').next().css('display', 'none');
            $body = $control.find('#' + moduleName + '.panel-body');
        });

        $reportsPageModules.on('click', '.pop-out', function (e) {
            e.stopPropagation();
            program.popOutTab('#/module/' + moduleName.slice(2));
            jQuery(this).parent().hide();
        });

        $reportsPageModules
            .on('click', '.panel-heading', function (e) {
                var $this, moduleName, $reports, $body;

                $this = jQuery(this);
                moduleName = $this.closest('.panel-group').attr('id');
                $body = $control.find('#' + moduleName + '.panel-body');

                if ($body.is(':empty')) {
                    $reports = window[moduleName + 'Controller'].openForm();
                    window[moduleName + 'Controller'].onLoadForm($reports);
                    $body.append($reports);

                    for (var i = 0; i < me.filter.length; i++) {
                        var filterField = $reports.find('[data-datafield="' + me.filter[i] + '"]');
                        if (filterField.length > 0 && filterField.attr('data-type') === 'checkbox') {
                            $reports.find('[data-datafield="' + me.filter[i] + '"] label').addClass('highlighted');
                        } else if (filterField.length > 0) {
                            $reports.find('[data-datafield="' + me.filter[i] + '"]').find('.fwformfield-caption').addClass('highlighted');
                        }
                    };
                }

                if ($reportsPageModules.find('.panel-collapse').css('display') === 'none') {
                    $reportsPageModules.find('.arrow-selector').html('keyboard_arrow_up')
                    $reportsPageModules.find('.panel-collapse').show("fast");
                } else {
                    $reportsPageModules.find('.arrow-selector').html('keyboard_arrow_down')
                    $reportsPageModules.find('.panel-collapse').hide('fast');
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


        $control.on('keypress', '#reportsSearch', function (e) {
            if (e.which === 13) {
                e.stopImmediatePropagation();
                jQuery(this).closest('.fwreports').find('.data-panel:parent').parent().find('.row-heading').click();
                jQuery(this).closest('.fwreports').find('.data-panel:parent').empty();

                var $reports, val, $module;

                me.getCaptions(screen);
                filter = [];
                $reports = jQuery('small#description');
                $module = jQuery('a#title');
                val = jQuery.trim(this.value).toUpperCase();
                if (val === "") {
                    $reports.closest('div.panel-group').show();
                } else {
                    var results = [];
                    $reports.closest('div.panel-group').hide();
                    for (var caption in screen.moduleCaptions) {
                        if (caption.indexOf(val) !== -1) {
                            for (var moduleName in screen.moduleCaptions[caption]) {
                                filter.push(screen.moduleCaptions[caption][moduleName][0].data().datafield);
                                results.push(moduleName.toUpperCase().slice(0, -10));
                            }
                        }
                    }
                    me.filter = filter;
                    for (var i = 0; i < results.length; i++) {
                        var module = $reports.filter(function () {
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

        return $reportsPageModules;
    };
    //---------------------------------------------------------------------------------------------- 
    getHeaderView($control) {
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
                                                dropDownMenuItems.push({ id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl, moduleName: nodeLv3MenuItem.properties.controller.slice(0, -10) });
                                            }
                                        }
                                        this.generateDropDownModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                        break;
                                    case 'ReportsModule':
                                        this.generateStandardModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl, nodeLv2MenuItem.properties.controller.slice(0, -10));
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
    generateDropDownModuleBtn($menu, $control, securityid, caption, imgurl, subitems) {
        var $modulebtn, $reports, btnHtml, subitemHtml, $subitem, version;

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
    generateStandardModuleBtn($menu, $control, securityid, caption, modulenav, imgurl, moduleName) {
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
    }
    //----------------------------------------------------------------------------------------------
}

var FwReportsPage = new FwReportsPageClass();