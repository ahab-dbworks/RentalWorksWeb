class FwReportsPageClass {
    filter = [];
    //----------------------------------------------------------------------------------------------
    init() {

    }
    //----------------------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html = [];

        html.push('<div class="fwreportsheader">');
        html.push('  <div class="reports-header-title">Reports</div>');
        html.push('  <div class="input-group pull-right">');
        html.push('    <input type="text" id="reportsSearch" class="form-control" placeholder="Search..." autofocus>');
        html.push('    <span class="input-group-clear">');
        html.push('      <i class="material-icons">clear</i>');
        html.push('    </span>');
        html.push('    <span class="input-group-search">');
        html.push('      <i class="material-icons">search</i>');
        html.push('    </span>');
        html.push('  </div>');
        html.push('</div>');
        html.push('<div class="flexrow reports-content">');
        html.push('  <div class="menu-expand"><i class="material-icons">keyboard_arrow_right</i></div>');
        html.push('  <div class="navigation flexrow">');
        html.push('    <div class="navigation-menu flexcolumn"></div>');
        html.push('  </div>');
        html.push('  <div class="well"></div>');
        html.push('</div>');

        var reportsMenu = this.getHeaderView($control);
        reportsMenu.append('<div class="flexcolumn menu-collapse"><i class="material-icons">keyboard_arrow_left</i></div>');
        $control.html(html.join(''));
        let menuCollapse = reportsMenu.find('.menu-collapse');
        let menuExpand = $control.find('.menu-expand');

        menuExpand.on('click', function () {
            menuCollapse.closest('.navigation').show();
            jQuery(this).hide();
        });

        menuCollapse.on('click', function () {
            menuExpand.show();
            jQuery(this).closest('.navigation').hide();
        });

        reportsMenu.addClass('flexrow');
        reportsMenu.find('.menu').addClass('flexcolumn');

        $control.find('.navigation-menu').append(reportsMenu);
    }
    //----------------------------------------------------------------------------------------------
    getCaptions(screen) {
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, FwApplicationTree.currentApplicationId);
        var modules = FwApplicationTree.getChildrenByType(node, 'ReportsModule');
        for (var i = 0; i < modules.length; i++) {
            var moduleName = modules[i].properties.controller;
            if (typeof (<any>window[moduleName]).openForm === 'function') {
                var $form = (<any>window[moduleName]).openForm();
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
                //add section headings to search
                const $sectionHeadings = $form.find('.fwform-section[data-caption]');
                for (let l = 0; l < $sectionHeadings.length; l++) {
                    const $section = $sectionHeadings.eq(l);
                    const sectionCaption = $section.attr('data-caption').toUpperCase();
                    if (typeof screen.moduleCaptions[sectionCaption] === 'undefined') {
                        screen.moduleCaptions[sectionCaption] = {};
                    }
                    if (typeof screen.moduleCaptions[sectionCaption][moduleName] === 'undefined') {
                        screen.moduleCaptions[sectionCaption][moduleName] = [];
                    }
                    screen.moduleCaptions[sectionCaption][moduleName].push($section);
                }
            }
        }
    }
    //---------------------------------------------------------------------------------------------- 
    renderModuleHtml($control, title, moduleName, description, menu, menuCaption, moduleId) {
        var me = this;
        var html = [], $reportsPageModules, $rowBody, $modulecontainer, $body, $form, browseKeys = [], rowId, screen = { 'moduleCaptions': {} }, filter = [];

        $modulecontainer = $control.find('#' + moduleName);
        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

        html.push('<div class="panel-group" id="' + moduleName + '" data-id="' + moduleId + '" data-navigation="' + menuCaption + '">');
        html.push('  <div class="panel panel-primary">');
        html.push('    <div data-toggle="collapse" data-target="' + moduleName + '" href="' + moduleName + '" class="panel-heading">');
        html.push('      <div class="flexrow" style="max-width:none;">');
        html.push('        <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
        html.push('        <h4 class="panel-title">');
        html.push('          <a id="title" data-toggle="collapse">' + menu + ' - ' + title + '</a>');
        html.push('          <div id="myDropdown" class="dropdown-content">');
        html.push('          <div class="pop-out flexrow"><i class="material-icons">open_in_new</i>Pop Out Module</div>');
        html.push('          </div>');
        html.push('        <div style="margin-left:auto;">');
        html.push('          <i class="material-icons pop-out" title="Pop Out">open_in_new</i>');
        html.push('          <i class="material-icons heading-menu">more_vert</i>');
        html.push('        </div>');
        html.push('      </h4>');
        html.push('      </div>');
        if (description === "") {
            html.push('      <small id="description" style="display:none;">' + moduleName + '</small>');
            html.push('      <small style="margin:0 0 0 32px;" id="description-text">' + moduleName + '</small>');
        } else {
            html.push('      <small id="description" style="display:none;">' + description + '</small>');
            html.push('      <small style="margin:0 0 0 32px;" id="description-text">' + description + '</small>');
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
            program.popOutTab(`#/reports/${moduleName}`);
            if (jQuery(this).closest('#myDropdown').length !== 0) {
                jQuery(this).parent().hide();
            }
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
                let activeMenu = $control.find('.active-menu');
                let $this: any = jQuery(this);
                let $dropdown = $this.closest('.panel-title').find('#myDropdown');
                if ($dropdown.css('display') === 'none') {
                    $dropdown.css('display', 'block').addClass('active-menu');
                    jQuery(document).one('click', function closeMenu(e) {
                        if ($this.has(e.target).length === 0) {
                            $dropdown.removeClass('active-menu').css('display', 'none');
                        } else {
                            $dropdown.css('display', 'block');
                        }
                    })
                } else {
                    $dropdown.removeClass('active-menu').css('display', 'none');
                }

                if (activeMenu.length > 0) {
                    activeMenu.removeClass('active-menu').hide();
                }
            })

        $control.on('click', '.input-group-clear', function (e) {
            let event = jQuery.Event('keypress');
            event.which = 13;
            jQuery(this).parent().find('#reportsSearch').val('').trigger(event);
            jQuery(this).css('display', 'none');
        });

        $control.on('keypress', '#reportsSearch', function (e) {
            if (e.which === 13) {
                e.stopImmediatePropagation();
                jQuery(this).closest('.fwreports').find('.data-panel:parent').parent().find('.row-heading').click();
                jQuery(this).closest('.fwreports').find('.data-panel:parent').empty();
                jQuery(this).parent().find('.input-group-clear').css('display', 'table-cell');

                if (Object.keys(screen.moduleCaptions).length === 0 && screen.moduleCaptions.constructor === Object) {
                    me.getCaptions(screen);
                }

                filter = [];
                const $reportDescriptions = jQuery('small#description');
                const $reportTitles = jQuery('a#title');
                const val = jQuery.trim(this.value).toUpperCase();
                const $reports = jQuery('.panel-group');
                if (val === "") {
                    jQuery(this).parent().find('.input-group-clear').css('display', 'none');
                    $control.find('.highlighted').removeClass('highlighted');
                    $reportDescriptions.closest('div.panel-group').show();
                } else {
                    var results = [];
                    results.push(val);
                    $reportDescriptions.closest('div.panel-group').hide();
                    for (var caption in screen.moduleCaptions) {
                        if (caption.indexOf(val) !== -1) {
                            for (var moduleName in screen.moduleCaptions[caption]) {
                                filter.push(screen.moduleCaptions[caption][moduleName][0].data().datafield);
                                results.push(moduleName.toUpperCase().slice(0, -10));
                            }
                        }
                    }
                    me.filter = filter;

                    var highlightSearch = function (element, search) {
                        if (element.length !== undefined) {
                            element = element[0]
                        }
                        let searchStrLen = search.length;
                        let startIndex = 0, index, indicies = [];
                        let htmlStringBuilder = [];
                        search = search.toUpperCase();
                        if (search === '') {
                            element.innerHTML = element.textContent;
                            return
                        }
                        while ((index = element.textContent.toUpperCase().indexOf(search, startIndex)) > -1) {
                            indicies.push(index);
                            startIndex = index + searchStrLen;
                        }
                        for (var i = 0; i < indicies.length; i++) {
                            if (i === 0) {
                                htmlStringBuilder.push(jQuery(element).text().substring(0, indicies[0]));
                            } else {
                                htmlStringBuilder.push(jQuery(element).text().substring(indicies[i - 1] + searchStrLen, indicies[i]))
                            }
                            htmlStringBuilder.push('<span class="highlighted">' + jQuery(element).text().substring(indicies[0], indicies[0] + searchStrLen) + '</span>');
                            if (i === indicies.length - 1) {
                                htmlStringBuilder.push(jQuery(element).text().substring(indicies[i] + searchStrLen, jQuery(element).text().length));
                                element.innerHTML = htmlStringBuilder.join('');
                            }
                        }
                        if (indicies.length === 0 || search === '') {
                            element.innerHTML = element.textContent;
                        }
                    }

                    var matchDescriptionTitle = function ($control) {
                        for (var j = 0; j < $control.length; j++) {
                            let description = jQuery($control[j]).find('small#description-text');
                            let title = jQuery($control[j]).find('a#title');
                            let descriptionIndex = jQuery(description).text().toUpperCase().indexOf(val);
                            let titleIndex = jQuery(title).text().toUpperCase().indexOf(val);
                            if (descriptionIndex > -1) {
                                highlightSearch(description, val);
                            } else {
                                highlightSearch(description, '');
                            }
                            if (titleIndex > -1) {
                                highlightSearch(title, val);
                            } else {
                                highlightSearch(title, '');
                            }
                            jQuery($control[j]).show();
                        }
                    }

                    const modules: any = [];
                    for (var i = 0; i < results.length; i++) {
                        //check report ids for match
                        for (let k = 0; k < $reports.length; k++) {
                            if ($reports.eq(k).attr('id').toUpperCase() === results[i]) {
                                modules.push($reports.eq(k)[0]);
                            }
                        }

                        //check descriptions for match
                        const matchedDescription = $reportDescriptions.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                        }).closest('div.panel-group');
                        matchedDescription.find('.highlighted').removeClass('highlighted');
                        if (matchedDescription.length > 0) {
                            modules.push(matchedDescription);
                        }

                        //check titles for match
                        const matchedTitle = $reportTitles.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                        }).closest('div.panel-group');
                        if (matchedTitle.length > 0) {
                            modules.push(matchedTitle);
                        }
                    }
                    matchDescriptionTitle(modules);

                    let searchResults = $control.find('.panel-heading:visible');

                    if (searchResults.length === 1 && searchResults.parent().find('.panel-body').is(':empty')) {
                        searchResults[0].click();
                    }
                }
            }
        });

        return $reportsPageModules;
    };
    //---------------------------------------------------------------------------------------------- 
    getHeaderView($control) {
        var $view;

        $view = jQuery('<div class="menu-container" data-control="FwFileMenu" data-version="2" data-rendermode="template"><div class="menu"></div></div>');

        var nodeApplications, nodeApplication = null, baseiconurl, $menu, ribbonItem, dropDownMenuItems, caption;
        nodeApplications = FwApplicationTree.getMyTree();
        for (var appno = 0; appno < nodeApplications.children.length; appno++) {
            if (nodeApplications.children[appno].id === FwApplicationTree.currentApplicationId) {
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
                        this.generateDropDownModuleBtn($view, $control, 'All Reports ID', 'All Reports', null, null);
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
                                        this.generateDropDownModuleBtn($view, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                        break;
                                    case 'ReportsModule':
                                        this.generateStandardModuleBtn($view, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl, nodeLv2MenuItem.properties.controller.slice(0, -10));
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
        var $modulebtn, $reports, btnHtml, version;

        version = $menu.closest('.fwfilemenu').attr('data-version');
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnHtml = [];
                btnHtml.push('<div id="btnModule' + securityid + '" class="ddmodulebtn menu-tab" data-securityid="' + securityid + '" data-navigation="' + caption + '">');
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwReportsPage.generateDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
        }

        $modulebtn
            .on('click', function () {
                try {
                    let navigationCaption = $modulebtn.data('navigation');
                    let panels = $control.find('.panel-group');
                    if (navigationCaption === 'All Reports') {
                        let event = jQuery.Event('keypress');
                        event.which = 13;
                        $control.find('.selected').removeClass('selected');
                        $control.find('#reportsSearch').val('').trigger(event);
                        jQuery(this).addClass('selected');
                    } else if (navigationCaption != '') {
                        $control.find('.selected').removeClass('selected');
                        $control.find('#reportsSearch').val('')
                        jQuery(this).addClass('selected');
                        //if ($control.find('#' + moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                        //    $control.find('#' + moduleName + ' > div > div.panel-heading').click();
                        //}
                        //jQuery('html, body').animate({
                        //    scrollTop: $control.find('#' + moduleName).offset().top + $control.find('.well').scrollTop()
                        //}, 1);
                        for (var i = 0; i < panels.length; i++) {
                            if (jQuery(panels[i]).data('navigation') !== navigationCaption) {
                                jQuery(panels[i]).hide();
                            } else {
                                jQuery(panels[i]).show();
                            }
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })

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
                btnHtml.push('<div id="' + btnId + '" class="modulebtn menu-tab" data-securityid="' + securityid + '">');
                btnHtml.push('<div class="modulebtn-text">');
                btnHtml.push(caption);
                btnHtml.push('</div>');
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwReportsPage.generateStandardModuleBtn: ' + caption + ' caption is not defined in translation';
        }

        $modulebtn
            .on('click', function () {
                try {
                    if (modulenav != '') {
                        let panels = $control.find('.panel-group');
                        $control.find('.selected').removeClass('selected');
                        $control.find('#reportsSearch').val('')
                        jQuery(this).addClass('selected');
                        for (var i = 0; i < panels.length; i++) {
                            if (jQuery(panels[i]).attr('id') !== moduleName) {
                                jQuery(panels[i]).hide();
                            } else {
                                jQuery(panels[i]).show();
                            }

                        }
                        if ($control.find('#' + moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                            $control.find('#' + moduleName + ' > div > div.panel-heading').click();
                        }
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