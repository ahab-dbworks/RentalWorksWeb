class FwRibbonClass {
    //---------------------------------------------------------------------------------
    upgrade($control: JQuery) {
        var properties, i, data_type;
        //sync properties
        data_type = $control.attr('data-type');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            if (typeof $control.attr(properties[i].attribute) === 'undefined') {
                $control.attr(properties[i].attribute, properties[i].defaultvalue);
            }
        }
    }
    //---------------------------------------------------------------------------------
    init($control: JQuery) {
        var me = this;
        this.upgrade($control);
        $control
            .off('click', '> .designer > .tabs > .new')
            .on('click', '> .designer > .tabs > .new', function() {
                var $newtab, $tab, $tabpage, tabid, tabpageid, tabHtml;
                try {
                    tabid     = FwControl.generateControlId('tab');
                    tabpageid = FwControl.generateControlId('tabpage');
                    tabHtml = [];
                    tabHtml.push(`<div id="${tabid}" class="tab" data-tabpageid="${tabpageid}" data-caption="${tabid}" draggable="true">`);
                    tabHtml.push(`  <div class="caption">${tabid}</div>`);
                    tabHtml.push('  <div class="delete">x</div>');
                    tabHtml.push('</div>');
                    tabHtml = tabHtml.join('');
                    $tab      = jQuery(tabHtml);
                    $tabpage  = jQuery(`<div id="${tabpageid}" class="tabpage" data-tabid="${tabid}" designer-dropcontainer="true"></div>`);
                    $control.children('.designer').children('.tabs').find('.new').before($tab);
                    $control.children('.designer').children('.tabpages').append($tabpage);
                    me.setActiveTab($control, $tab);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .designer > .tabs > .tab > .caption,> .runtime > .tabs > .tab > .caption')
            .on('click', '> .designer > .tabs > .tab > .caption,> .runtime > .tabs > .tab > .caption', function() {
                var $tab;
                try {
                    $tab = jQuery(this).closest('.tab');
                    me.setActiveTab($control, $tab);
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('dblclick', '> .designer > .tabs > .tab')
            .on('dblclick', '> .designer > .tabs > .tab', function() {
                var $tab, $caption, caption;
                try {
                    $tab = jQuery(this);
                    $caption = $tab.find('.caption')
                    caption = $caption.html();
                    $caption.html('<input class="editcaption" type="text" value="' + caption + '" />');
                    $caption.find('.editcaption').select();
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('keypress', '> .designer > .tabs > .tab > .caption > input.editcaption')
            .on('keypress', '> .designer > .tabs > .tab > .caption > input.editcaption', function(event) {
                var $this, caption, $caption;
                try {
                    if (event.keyCode === 13) {
                        $this = jQuery(this);
                        caption = $this.val();
                        $caption = $this.closest('.caption');
                        $caption.html(caption);
                        $caption.closest('.tab').attr('data-caption', caption);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('dragstart', '> .designer > .tabs > .tab')
            .on('dragstart', '> .designer > .tabs > .tab', function(event) {
                var $this;
                try {
                    $this = jQuery(this);
                    (<any>event.originalEvent).dataTransfer.setData('selector', '#' + $this.attr('id'));
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('drop', '> .designer > .tabs > div')
            .on('drop', '> .designer > .tabs > div', function(event) {
                var draggedtabSelector, $draggedtab, $draggedtabpage, $tab, $tabs, $tabpage, $tabpages;
                try {
                    if (typeof event.preventDefault  === 'function') event.preventDefault();
                    if (typeof event.stopPropagation === 'function') event.stopPropagation();
                    $tab = jQuery(this);
                    draggedtabSelector = (<any>event.originalEvent).dataTransfer.getData('selector');
                    $draggedtab = jQuery(draggedtabSelector);
                    if (($tab.attr('id') !== $draggedtab.attr('id')) && ($draggedtab.hasClass('tab'))) {
                        $draggedtab.detach();
                        $tab.before($draggedtab);
                        $tabpages = $control.children('.designer').children('.tabpages');
                        $tabpage = $tabpages.children('.tabpage[data-tabid="' + $tab.attr('id') + '"]');
                        $draggedtabpage = $tabpages.find('#' + $draggedtab.attr('data-tabpageid'));
                        $draggedtabpage.detach();
                        $tabpage.before($draggedtabpage);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .designer > .tabs > .tab > .delete')
            .on('click', '> .designer > .tabs > .tab > .delete', function() {
                var $delete, $tab, $tabpage, tabpageid, $tabs, $newactivetab, isactivetab;
                try {
                    if (confirm('Delete tab?')) {
                        $delete   = jQuery(this);
                        $tab      = $delete.closest('.tab');
                        isactivetab = $tab.hasClass('active');
                        $tabs     = $tab.closest('.tabs');
                        tabpageid = $tab.attr('data-tabpageid');
                        $tabpage  = jQuery('#' + tabpageid);
                        $tab.remove();
                        $tabpage.remove();
                        $newactivetab = $tabs.find('.tab').first();
                        if (isactivetab) {
                            me.setActiveTab($control, $newactivetab);
                        }
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    };
    //---------------------------------------------------------------------------------
    setActiveTab($control: JQuery, $tab: JQuery) {
        var $tabpage, tabid, tabpageid;
        if ($tab.length > 0) {
            tabpageid = $tab.attr('data-tabpageid');
            $tabpage = $control.find('#' + tabpageid);
            $control.find('> .designer > .tabs > .tab,> .runtime > .tabs > .tab').removeClass('active').addClass('inactive');
            $control.find('> .designer > .tabpages > .tabpage,> .runtime > .tabpages > .tabpage').removeClass('active').addClass('inactive').hide();
            $tab.removeClass('inactive').addClass('active').show()
            $tabpage.removeClass('inactive').addClass('active').show();
        }
    };
    //---------------------------------------------------------------------------------
    getHtmlTag(data_type: string) {
        var html, properties, i;
        properties = this.getDesignerProperties(data_type);
        html = [];
        html.push('<div ');
        for (i = 0; i < properties.length; i++) {
            html.push(properties[i].attribute + '="' + properties[i].defaultvalue + '"');
        }
        html.push('>');
        html.push('<div class="tabs"></div>');
        html.push('<div class="tabpages"></div>');
        html.push('</div>');
        html = html.join('');
        return html;
    };
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type: string) {
        var properties = [], propId, propClass, propDataControl, propDataType, propRenderMode, propDataVersion;
        propId          = { caption: 'ID',          datatype: 'string', attribute: 'id',              defaultvalue: FwControl.generateControlId('tabs'), visible: true,  enabled: true };
        propClass       = { caption: 'CSS Class',   datatype: 'string', attribute: 'class',           defaultvalue: 'fwcontrol fwribbon',                visible: false, enabled: false };
        propDataControl = { caption: 'Control',     datatype: 'string', attribute: 'data-control',    defaultvalue: 'FwRibbon',                          visible: true,  enabled: false };
        propDataType    = { caption: 'Type',        datatype: 'string', attribute: 'data-type',       defaultvalue: data_type,                           visible: false, enabled: false };
        propDataVersion = { caption: 'Version',     datatype: 'string', attribute: 'data-version',    defaultvalue: '1',                                 visible: false, enabled: false };
        propRenderMode  = { caption: 'Render Mode', datatype: 'string', attribute: 'data-rendermode', defaultvalue: 'template',                          visible: false, enabled: false };
        
        properties = [propId, propClass, propDataControl, propDataType, propDataVersion, propRenderMode];
    
        return properties;
    };
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery) {
        var data_type, data_rendermode, html, $tabsChildren, $tabpagesChildren, $tabs, $newtab, $activetab;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'designer');
        html = [];
        html.push('<div class="designer">');
            html.push(FwControl.generateDesignerHandle('Tabs', $control.attr('id')));
            html.push('<div class="tabs"></div>');
            html.push('<div class="tabpages"></div>');
        html.push('</div>');
        switch(data_rendermode) {
            case 'designer':
                $tabsChildren     = $control.children('.designer').children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.designer').children('.tabpages').children().detach();
                break;
            case 'runtime':
                $tabsChildren     = $control.children('.runtime').children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.runtime').children('.tabpages').children().detach();
                break;
            case 'template':
                $tabsChildren     = $control.children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.tabpages').children().detach();
                break;
        }
        $control.html(html.join(''));
        $tabs = $control.children('.designer').children('.tabs');
        $newtab = jQuery('<div class="new">+</div>');
        $tabs
            .append($tabsChildren)
            .append($newtab)
        ;
        $tabsChildren
            .each(function(index, tab) {
                var $tab, tabHtml;
                $tab = jQuery(tab);
                $tab.empty();
                $tab.attr('draggable', 'true');
                tabHtml = [];
                tabHtml.push('<div class="caption">' + $tab.attr('data-caption') + '</div>');
                tabHtml.push('<div class="delete">x</div>');
                tabHtml = tabHtml.join('');
                $tab.html(tabHtml);
            })
        ;
        $tabpagesChildren.attr('designer-dropcontainer', 'true');
        $control.children('.designer').children('.tabpages').append($tabpagesChildren);
        $activetab = $tabs.find('.tab').first();
        this.setActiveTab($control, $activetab);
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery) {
        var data_type, data_rendermode, html, $dashboardChildren, $tabsChildren, $usercontrolChildren, $tabpagesChildren, $tabs, $newtab, $activetab;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'runtime');
        html = [];
        html.push('<div class="runtime">');
        html.push('  <div class="dashboard"></div>');
        html.push('  <div class="tabs"></div>');
        html.push('  <div class="usercontrol"></div>');
        html.push('  <div class="tabpages"></div>');
        html.push('</div>');
        switch(data_rendermode) {
            case 'designer':
                $tabsChildren     = $control.children('.designer').children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.designer').children('.tabpages').children().detach();
                break;
            case 'runtime':
                $tabsChildren     = $control.children('.runtime').children('.tabs').children().detach();
                $tabpagesChildren = $control.children('.runtime').children('.tabpages').children()
                    .removeAttr('designer-dropcontainer')
                    .detach()
                ;
                break;
            case 'template':
                $dashboardChildren   = $control.children('.dashboard').children().detach();
                $tabsChildren        = $control.children('.tabs').children().detach();
                $usercontrolChildren = $control.children('.usercontrol').children().detach();
                $tabpagesChildren    = $control.children('.tabpages').children().detach();
                break;
        }
        $control.html(html.join(''));
        $tabs = $control.children('.runtime').children('.tabs');
        $tabs.append($tabsChildren);
        $tabsChildren
            .each(function(index, tab) {
                var $tab, tabHtml;
                $tab = jQuery(tab);
                $tab.empty();
                tabHtml = [];
                tabHtml.push('<div class="caption">' + $tab.attr('data-caption') + '</div>');
                tabHtml = tabHtml.join('');
                $tab.html(tabHtml);
            })
        ;
        $control.children('.runtime').children('.dashboard').append($dashboardChildren);
        $control.children('.runtime').children('.usercontrol').append($usercontrolChildren);
        $control.children('.runtime').children('.tabpages').append($tabpagesChildren);
        $activetab = $tabs.find('.tab').first();
        this.setActiveTab($control, $activetab);
    };
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control: JQuery) {
        var data_type, data_rendermode, html, $tabsChildren, $tabpagesChildren;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
        html = [];
        html.push('<div class="tabs">');
        html.push('</div>');
        html.push('<div class="tabpages">');
        html.push('</div>');
        switch(data_rendermode) {
            case 'designer':
                $tabsChildren     = $control.children('.designer').children('.tabs').children('.tab')
                    .detach()
                    .removeClass('inactive')
                    .removeClass('active')
                    .removeAttr('draggable')
                ;
                $tabpagesChildren = $control.children('.designer').children('.tabpages').children()
                    .detach()
                    .removeClass('inactive')
                    .removeClass('active')
                    .removeAttr('style')
                    .removeAttr('designer-dropcontainer')
                ;
                $control.html(html.join(''));
                $tabsChildren.empty();
                $control.children('.tabs').append($tabsChildren);
                $control.children('.tabpages').append($tabpagesChildren);
                break;
            case 'runtime':
                $tabsChildren     = $control.children('.runtime').children('.tabs').children('.tab')
                    .detach()
                    .removeClass('inactive')
                    .removeClass('active')
                ;
                $tabpagesChildren = $control.children('.runtime').children('.tabpages').children()
                    .detach()
                    .removeClass('inactive')
                    .removeClass('active')
                    .removeAttr('style')
                ;
                $control.html(html.join(''));
                $tabsChildren.empty();
                $control.children('.tabs').append($tabsChildren);
                $control.children('.tabpages').append($tabpagesChildren);
                break;
        }
    };
    //---------------------------------------------------------------------------------
    addTab($control: JQuery, caption: string) {
        var $newtab, $tab, $tabpage, tabHtml, tabid, tabpageid, idprefix;
        try {
            idprefix = caption.replace(/[^a-zA-Z0-9]+/g, '');
            tabid     = 'tab' + idprefix;
            tabpageid = 'tabpage' + idprefix;
            tabHtml             = [];
            tabHtml.push(`<div data-type="tab" id="${tabid}" class="tab" data-tabpageid="${tabpageid}" data-caption="${caption}">`);
            tabHtml.push(`  <div class="caption">${caption}</div>`);
            tabHtml.push('</div>');
            tabHtml  = tabHtml.join('');
            $tab     = jQuery(tabHtml);
            $tabpage = jQuery(`<div data-type="tabpage" id="${tabpageid}" class="tabpage" data-tabid="${tabid}"></div>`);
            $control.children('.tabs').append($tab);
            $control.children('.tabpages').append($tabpage);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    
        return $tabpage;
    };
    //---------------------------------------------------------------------------------
    generateStandardModuleBtn(securityid: string, caption: string, modulenav: string, imgurl: string) {
        var $modulebtn, btnHtml, btnId;
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnId = 'btnModule' + securityid;
                btnHtml = [];
                btnHtml.push(`<div id="${btnId}" class="modulebtn" data-securityid="${securityid}">`);
                btnHtml.push(`  <div class="modulebtn-icon"><img src="${imgurl}" class="btn-icon"></div>`);
                btnHtml.push(`  <div class="modulebtn-text">${caption}</div>`);
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw `FwRibbon.generateStandardModuleBtn: ${caption} caption is not defined in translation`;
        }
    
        $modulebtn
            .on('click', function() {
                try {
                    if (modulenav != '') {
                        program.getModule(modulenav);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;
    
        return $modulebtn;
    };
    //---------------------------------------------------------------------------------
    generateDropDownModuleBtn(securityid: string, caption: string, imgurl: string, subitems: any[]) {
        var $modulebtn, btnHtml, subitemHtml, $subitem;
        
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnHtml = [];
                btnHtml.push('<div id="btnModule' + securityid + '" class="ddmodulebtn" data-securityid="' + securityid + '">');
                    btnHtml.push('<div class="ddmodulebtn-icon"><img src="' + imgurl + '" class="btn-icon"></div>');
                    btnHtml.push('<div class="ddmodulebtn-text">');
                        btnHtml.push(caption);
                        btnHtml.push(' <img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/arrow_down.001.png" style="width:5px;height:3px;" />');
                    btnHtml.push('</div>');
                    btnHtml.push('<div class="ddmodulebtn-dropdown"></div>');
                btnHtml.push('</div>');
                $modulebtn = jQuery(btnHtml.join(''));
    
                $modulebtn.on('click', function(e) {
                    var $this, maxZIndex;
                    $this = jQuery(this);
                    e.preventDefault();
                    if (!$this.hasClass('active')) {
                        maxZIndex = FwFunc.getMaxZ('*');
                        $this.find('.ddmodulebtn-dropdown').css('z-index', maxZIndex+1);
                        $this.addClass('active');
    
                        jQuery(document).one('click', function closeMenu(e) {
                            if ($this.has(e.target).length === 0) {
                                $this.removeClass('active');
                                $this.find('.ddmodulebtn-dropdown').css('z-index', '0');
                            } else if ($this.hasClass('active')) {
                                jQuery(document).one('click', closeMenu);
                            }
                        });
                    } else {
                        $this.removeClass('active');
                        $this.find('.ddmodulebtn-dropdown').css('z-index', '0');
                    }
                });
    
                subitemHtml = [];
                subitemHtml.push('<div id="" class="ddmodulebtn-dropdown-btn">');
                    subitemHtml.push('<div class="ddmodulebtn-dropdown-btn-icon"><img src="" class="ddbtn-icon"></div>');
                    subitemHtml.push('<div class="ddmodulebtn-dropdown-btn-text"></div>');
                subitemHtml.push('</div>');
                jQuery.each(subitems, function(index, value) {
                    $subitem = jQuery(subitemHtml.join(''));
                    $subitem.attr('data-securityid', subitems[index].id);
                    $subitem.find('.ddmodulebtn-dropdown-btn-text').html(value.caption);
                    $subitem.find('.ddmodulebtn-dropdown-btn-icon img').attr('src', value.imgurl);
    
                    $subitem.on('click', function() {
                        try {
                            if (value.modulenav) {
                                program.getModule(value.modulenav);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
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
    
        return $modulebtn;
    };
    //---------------------------------------------------------------------------------
    generateSplitDropDownModuleBtn(securityid: string, caption: string, modulenav: string, imgurl: string, subitems: any[]) {
        var $modulebtn, btnHtml, btnId;
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnId = 'btnModule' + securityid;
                btnHtml = [];
                btnHtml.push('<div id="' + btnId + '" class="splitddmodulebtn" data-securityid="' + securityid + '">');
                    btnHtml.push('<div class="splitddmodulebtn-icon"><img src="' + imgurl + '" class="btn-icon"></div>');
                    btnHtml.push('<div class="splitddmodulebtn-text">');
                        btnHtml.push(caption.replace(/\s+/g, '<br />'));
                        btnHtml.push(' <img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/arrow_down.001.png" style="width:5px;height:3px;" />');
                    btnHtml.push('</div>');
                    btnHtml.push('<div class="splitddmodulebtn-dropdown" style="display:none;">');
                    for (var i = 0; i < subitems.length; i++) {
                        btnHtml.push('<div id="ddBtn' + subitems[i].id + '" class="splitddmodulebtn-dropdown-btn" data-securityid="' + subitems[i].id + '">');
                            btnHtml.push('<div class="splitddmodulebtn-dropdown-btn-icon"><img src="' + subitems[i].imgurl +'" class="ddbtn-icon"></div>');
                            btnHtml.push('<div class="splitddmodulebtn-dropdown-btn-text">' + subitems[i].caption + '</div>');
                        btnHtml.push('</div>');
                    }
                    btnHtml.push('</div>');
                    btnHtml.push('<div class="splitddmodulebtn-overlay"></div>');
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwRibbon.generateSplitDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
        }
    
        $modulebtn
            .on('click', '.splitddmodulebtn-icon', function() {
                try {
                    if (modulenav != '') {
                        program.getModule(modulenav);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('mouseover', function() {
                var $this;
                $this = jQuery(this);
    
                $this.addClass('splitddmodulebtn-hover');
            })
            .on('mouseout', function() {
                var $this;
                $this = jQuery(this);
    
                $this.removeClass('splitddmodulebtn-hover');
            })
            .on('click', '.splitddmodulebtn-text', function() {
                var $this;
                $this = jQuery(this);
    
                $this.addClass('splitddmodulebtn-text-hover');
                $this.siblings('.splitddmodulebtn-dropdown').show();
                $this.siblings('.splitddmodulebtn-overlay').show();
            })
            .on('click', '.splitddmodulebtn-overlay', function() {
                var $this;
                $this = jQuery(this);
    
                $this.siblings('.splitddmodulebtn-text').removeClass('splitddmodulebtn-text-hover');
                $this.siblings('.splitddmodulebtn-dropdown').hide();
                $this.hide();
            })
            .on('click', '.splitddmodulebtn-dropdown-btn', function() {
                var $this;
                $this = jQuery(this);
    
                $this.parent().siblings('.splitddmodulebtn-overlay').click();
            })
        ;
    
        for (var i = 0; i < subitems.length; i++) {
            $modulebtn.on('click', '#ddBtn' + subitems[i].id, function() {
                var subitem, id;
    
                id = this.id.replace('ddBtn', '');
                subitem = jQuery.grep(subitems, function(e: Element) { return e.id == id });
    
                try {
                    if (subitem[0].modulenav != '') {
                        program.getModule(subitem[0].modulenav);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    
        return $modulebtn;
    };
    //---------------------------------------------------------------------------------
}

var FwRibbon = new FwRibbonClass();