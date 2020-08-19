class FwTabsClass {
    //---------------------------------------------------------------------------------
    upgrade($control) {
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
    init($control) {
        //FwTabs.upgrade($control);
        //if (typeof $control.attr('data-tabmode') === 'undefined') {
        //    $control.attr('data-tabmode', 'form');
        //}
        if (typeof $control.attr('data-version') === 'undefined') {
            $control.attr('data-version', '2');
        }
        if (typeof $control.attr('data-rendermode') === 'undefined') {
            $control.attr('data-rendermode', 'template');
        }
        $control
            .off('click', '> .designer > .tabs > .new')
            .on('click', '> .designer > .tabs > .new', function () {
                var $tab, $tabpage, tabid, tabpageid, tabHtml;
                try {
                    tabid = FwControl.generateControlId('tab');
                    tabpageid = FwControl.generateControlId('tabpage');
                    tabHtml = [];
                    tabHtml.push('<div id="' + tabid + '" class="tab" data-tabpageid="' + tabpageid + '" data-caption="' + tabid + '" draggable="true">');
                    tabHtml.push('<div class="caption">' + tabid + '</div>');
                    tabHtml.push('<div class="delete">x</div>');
                    tabHtml.push('</div>');
                    tabHtml = tabHtml.join('');
                    $tab = jQuery(tabHtml);
                    $tabpage = jQuery('<div id="' + tabpageid + '" class="tabpage" data-tabid="' + tabid + '" designer-dropcontainer="true"></div>');
                    $control.children('.designer').children('.tabs').find('.new').before($tab);
                    $control.children('.designer').children('.tabpages').append($tabpage);
                    FwTabs.setActiveTab($control, $tab);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .designer > .tabs > .tab > .caption')
            .on('click', '> .designer > .tabs > .tab > .caption', function () {
                var $tab;
                try {
                    $tab = jQuery(this).closest('.tab');
                    FwTabs.setActiveTab($control, $tab);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('dblclick', '> .designer > .tabs > .tab')
            .on('dblclick', '> .designer > .tabs > .tab', function () {
                var $tab, $caption, caption;
                try {
                    $tab = jQuery(this);
                    $caption = $tab.find('.caption')
                    caption = $caption.html();
                    $caption.html('<input class="editcaption" type="text" value="' + caption + '" />');
                    $caption.find('.editcaption').select();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('keypress', '> .designer > .tabs > .tab > .caption > input.editcaption')
            .on('keypress', '> .designer > .tabs > .tab > .caption > input.editcaption', function (event) {
                var $this, caption, $caption, keycode;
                try {
                    keycode = event.keyCode || event.which;
                    if (keycode === 13) {
                        $this = jQuery(this);
                        caption = $this.val();
                        $caption = $this.closest('.caption');
                        $caption.html(caption);
                        $caption.closest('.tab').attr('data-caption', caption);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('dragstart', '> .designer > .tabs > .tab')
            .on('dragstart', '> .designer > .tabs > .tab', function (event) {
                var $this;
                try {
                    $this = jQuery(this);
                    event.originalEvent.dataTransfer.setData('selector', '#' + $this.attr('id'));
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('drop', '> .designer > .tabs > div')
            .on('drop', '> .designer > .tabs > div', function (event) {
                var draggedtabSelector, $draggedtab, $draggedtabpage, $tab, $tabs, $tabpage, $tabpages;
                try {
                    if (typeof event.preventDefault === 'function') event.preventDefault();
                    if (typeof event.stopPropagation === 'function') event.stopPropagation();
                    $tab = jQuery(this);
                    draggedtabSelector = event.originalEvent.dataTransfer.getData('selector');
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .designer > .tabs > .tab > .delete')
            .on('click', '> .designer > .tabs > .tab > .delete', function () {
                var $delete, $tab, $tabpage, tabpageid, $tabs, $newactivetab, isactivetab;
                try {
                    if (confirm('Delete tab?')) {
                        $delete = jQuery(this);
                        $tab = $delete.closest('.tab');
                        isactivetab = $tab.hasClass('active');
                        $tabs = $tab.closest('.tabs');
                        tabpageid = $tab.attr('data-tabpageid');
                        $tabpage = jQuery('#' + tabpageid);
                        $tab.remove();
                        $tabpage.remove();
                        $newactivetab = $tabs.find('.tab').first();
                        if (isactivetab) {
                            FwTabs.setActiveTab($control, $newactivetab);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .tabs > .tabcontainer > .tab')
            .on('click', '> .tabs > .tabcontainer > .tab', function () {
                var $tab, $tabs;
                try {
                    $tab = jQuery(this);
                    if ((typeof $tab.attr('data-enabled') === 'undefined') || ($tab.attr('data-enabled') === 'true')) {
                        $tabs = $tab.closest('.tabs');
                        if ((typeof $tab.data('onautosave') === 'function') && ($tab.closest('.fwform').attr('data-mode') === 'NEW')) {
                            $tab.data('onautosave')($control, $tab);
                        } else {
                            FwTabs.setActiveTab($control, $tab);
                        }
                        if (typeof $control.data('ontabchange') === 'function') {
                            $control.data('ontabchange')($tab);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .off('click', '> .tabs > .tabcontainer > .tab .delete')
            .on('click', '> .tabs > .tabcontainer > .tab .delete', function (event) {
                var $tab, $tabControl, $newactivetab, isactivetab, $form;
                try {
                    event.stopPropagation();
                    $tab = jQuery(this).closest('.tab');
                    $form = jQuery('#' + $tab.attr('data-tabpageid')).find('.fwform');
                    $tabControl = jQuery('#moduletabs');

                    if (typeof $form !== 'undefined') {
                        FwModule.closeForm($form, $tab);
                    } else {
                        isactivetab = $tab.hasClass('active');
                        $newactivetab = (($tab.next().length > 0) ? $tab.next() : $tab.prev());
                        FwTabs.removeTab($tab);
                        if (isactivetab) {
                            FwTabs.setActiveTab($control, $newactivetab);
                        }
                    }
                    if ($tabControl.find('.tab').length < 2) {
                        $tabControl.find('.closetabbutton').html('');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
    };
    //---------------------------------------------------------------------------------
    setActiveTab($control, $tab) {
        var $tabpage, $fwcontrols, allowTabChange;
        allowTabChange = true;
        if (typeof $control.data('beforetabchange') === 'function') {
            allowTabChange = $control.data('beforetabchange')($tab);
            if (typeof allowTabChange !== 'boolean') {
                allowTabChange = true;
            }
        }
        if ((allowTabChange) && ($tab.length > 0)) {
            $tabpage = FwTabs.getTabPageByTab($tab);
            $control.find('> .designer > .tabs > .tab,> .tabs .tabcontainer > .tab').removeClass('active').addClass('inactive');
            $control.find('> .designer > .tabpages > .tabpage,> .tabpages > .tabpage').removeClass('active').addClass('inactive').hide();
            $tab.removeClass('inactive').addClass('active').show();
            $tabpage.removeClass('inactive').addClass('active').show();

            if (typeof $control.data('ontabchange') === 'function') {
                $control.data('ontabchange')($tab);
            }

            // call event handlers on any child fwcontrols that subscribed to the onactivatetab event (this was created for fwscheduler)
            $fwcontrols = $tabpage.find('.fwcontrol');//.not('.tabpage .tabpage .fwcontrol');
            $fwcontrols.each(function (index, element) {
                var $thisfwcontrol = jQuery(element);
                $thisfwcontrol.triggerHandler('onactivatetab', {
                    $tab: $tab,
                    $tabpage: $tabpage
                });
            });
        }
    };
    //---------------------------------------------------------------------------------
    getHtmlTag(data_type) {
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
    getDesignerProperties(data_type) {
        var properties = [], propId, propClass, propDataControl, propDataType, propRenderMode, propDataVersion, propDataCaption, propDataEnabled, propDataOriginalValue, propDataImageUrl, propDataField, propDataFieldCount, propFieldCount;
        propId = { caption: 'ID', datatype: 'string', attribute: 'id', defaultvalue: FwControl.generateControlId('tabs'), visible: true, enabled: true };
        propClass = { caption: 'CSS Class', datatype: 'string', attribute: 'class', defaultvalue: 'fwcontrol fwtabs', visible: false, enabled: false };
        propDataControl = { caption: 'Control', datatype: 'string', attribute: 'data-control', defaultvalue: 'FwTabs', visible: true, enabled: false };
        propDataType = { caption: 'Type', datatype: 'string', attribute: 'data-type', defaultvalue: data_type, visible: false, enabled: false };
        propDataVersion = { caption: 'Version', datatype: 'string', attribute: 'data-version', defaultvalue: '1', visible: false, enabled: false };
        propRenderMode = { caption: 'Render Mode', datatype: 'string', attribute: 'data-rendermode', defaultvalue: 'template', visible: false, enabled: false };

        properties = [propId, propClass, propDataControl, propDataType, propDataVersion, propRenderMode];

        return properties;
    };
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control) {
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
        switch (data_rendermode) {
            case 'designer':
                $tabsChildren = $control.children('.designer').children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.designer').children('.tabpages').children().detach();
                break;
            case 'runtime':
                $tabsChildren = $control.children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.tabpages').children().detach();
                break;
            case 'template':
                $tabsChildren = $control.children('.tabs').children('.tab').detach();
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
            .each(function (index, tab) {
                var $tab, tabHtml;
                $tab = jQuery(tab);
                $tab.empty();
                $tab.attr('draggable', 'true');
                tabHtml = [];
                tabHtml.push('<div class="caption">' + $tab.attr('data-caption') + '</div>');
                tabHtml.push('<div class="delete"></div>');
                tabHtml = tabHtml.join('');
                $tab.html(tabHtml);
            })
            ;
        $tabpagesChildren.attr('designer-dropcontainer', 'true');
        $control.children('.designer').children('.tabpages').append($tabpagesChildren);
        $activetab = $tabs.find('.tab').first();
        FwTabs.setActiveTab($control, $activetab);
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var data_type, data_rendermode, data_version, html, $tabsChildren, $tabpagesChildren, $tabs, $activetab;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        data_version = $control.attr('data-version');
        $control.attr('data-rendermode', 'runtime');
        html = [];
        html.push('<div class="tabs">');
        html.push('  <div class="tabcontainer"></div>');
        if ($control.attr('data-version') === '1') {
            html.push('<div class="rightsidebuttons">')
            html.push('  <div class="newtabbutton"></div>');
            html.push('  <div class="closetabbutton"></div>');
            html.push('</div>');
        }
        html.push('</div>');
        html.push('<div class="tabpages"></div>');
        switch (data_rendermode) {
            case 'designer':
                $tabsChildren = $control.children('.designer').children('.tabs').children('.tab').detach();
                $tabpagesChildren = $control.children('.designer').children('.tabpages').children().detach();
                break;
            case 'runtime':
                $tabsChildren = $control.children('.tabs').children('.tabcontainer').children().detach();
                $tabpagesChildren = $control.children('.tabpages').children()
                    .removeAttr('designer-dropcontainer')
                    .detach()
                    ;
                break;
            case 'template':
                $tabsChildren = $control.children('.tabs').children().detach();
                $tabpagesChildren = $control.children('.tabpages').children().detach();
                break;
        }
        $control.html(html.join(''));
        $tabs = $control.children('.tabs').children('.tabcontainer');
        $tabs.append($tabsChildren);
        $tabsChildren
            .each(function (index, tab) {
                var $tab, tabHtml, tabcolor, newtabids: any = {};
                $tab = jQuery(tab);
                $tab.empty();
                if ($tab.attr('data-color')) {
                    tabcolor = $tab.attr('data-color');
                }
                newtabids.tabid = FwControl.generateControlId($tab.attr('id'));
                newtabids.tabpageid = FwControl.generateControlId($tab.attr('data-tabpageid'));
                tabHtml = [];
                if (data_version == '2' && tabcolor) {
                    tabHtml.push('<div class="border" style="background-color:' + tabcolor + '"></div>');
                } else if (data_version == '2' && !tabcolor) {
                    tabHtml.push('<div class="border"></div>');
                }
                tabHtml.push('<div class="caption">' + $tab.attr('data-caption') + '</div>');
                tabHtml = tabHtml.join('');
                $tab.html(tabHtml);
                $tabpagesChildren.find('#' + $tab.attr('data-tabpageid')).addBack('#' + $tab.attr('data-tabpageid')).attr('id', newtabids.tabpageid).attr('data-tabid', newtabids.tabid);
                $tab.attr('id', newtabids.tabid).attr('data-tabpageid', newtabids.tabpageid);
                if ((typeof $tab.attr('data-tabdisplay') !== 'undefined') && ($tab.attr('data-tabdisplay') === 'hidden')) {
                    $tab.attr('data-enabled', 'false');
                }
            })
            ;
        $control.children('.tabpages').append($tabpagesChildren);
        $activetab = $tabs.find('.tab').first();
        FwTabs.setActiveTab($control, $activetab);
    };
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control) {
        var data_type, data_rendermode, html, $tabsChildren, $tabpagesChildren;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
        html = [];
        html.push('<div class="tabs"></div>');
        html.push('<div class="tabpages"></div>');
        switch (data_rendermode) {
            case 'designer':
                $tabsChildren = $control.children('.designer').children('.tabs').children('.tabcontainer').children('.tab')
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
                $tabsChildren = $control.children('.tabs').children('.tab')
                    .detach()
                    .removeClass('inactive')
                    .removeClass('active')
                    ;
                $tabpagesChildren = $control.children('.tabpages').children()
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
    addTab($control, caption, hasClose, tabType, setActive) {
        var $tab, $tabpage, tabHtml, newtabids, version;
        newtabids = {};
        version = $control.attr('data-version');
        try {
            if (tabType === 'AUDIT') {
                newtabids.tabid = FwControl.generateControlId('audittab');
                newtabids.tabpageid = FwControl.generateControlId('audittabpage');
            } else {
                newtabids.tabid = FwControl.generateControlId('tab');
                newtabids.tabpageid = FwControl.generateControlId('tabpage');
            }
            tabHtml = [];
            tabHtml.push('<div data-type="tab" id="' + newtabids.tabid + '" class="tab inactive" data-tabpageid="' + newtabids.tabpageid + '" data-caption="' + caption + '" data-tabtype="' + tabType + '">');
            if (version === '2') {
                tabHtml.push('<div class="border"></div>');
            }
            tabHtml.push('<div class="caption">' + caption + '</div>');
            tabHtml.push('<div class="modified"></div>');
            if (hasClose) {
                tabHtml.push('<div class="delete"><i class="material-icons">close</i></div>');
            }
            tabHtml.push('</div>');
            tabHtml = tabHtml.join('');
            $tab = jQuery(tabHtml);
            $tabpage = jQuery('<div data-type="tabpage" id="' + newtabids.tabpageid + '" class="tabpage inactive" data-tabid="' + newtabids.tabid + '" data-tabtype="' + tabType + '" style="display:none;"></div>');
            $control.children('.tabs').children('.tabcontainer').append($tab);
            $control.children('.tabpages').append($tabpage);
            if (setActive === true) {
                FwTabs.setActiveTab($control, $tab);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return newtabids;
    };
    //---------------------------------------------------------------------------------
    removeTab($tab) {
        var $tabpage, tabpageid;
        tabpageid = $tab.attr('data-tabpageid');
        $tabpage = $tab.closest('.tabs').siblings('.tabpages').find('#' + tabpageid);
        $tab.remove();
        $tabpage.remove();
    };
    //---------------------------------------------------------------------------------
    removeTabBySelector($control, tabselector) {
        var $tab;
        $tab = $control.find(tabselector);
        FwTabs.removeTab($tab);
    };
    //---------------------------------------------------------------------------------
    closeTabs($control) {
        var $tabs, $tabpages;

        $tabs = $control.find('.tabs > .tabcontainer');
        $tabpages = $control.find('.tabpages');

        $tabs.empty();
        $tabpages.empty();
    };
    //---------------------------------------------------------------------------------
    getTabPageByElement($element) {
        var $tabpage
        $tabpage = $element.closest('.tabpage');
        return $tabpage;
    };
    //---------------------------------------------------------------------------------
    getTabByElement($element) {
        var $tabpage, $tab, tabid, $fwtabs;
        $tabpage = FwTabs.getTabPageByElement($element);
        tabid = $tabpage.attr('data-tabid');
        $fwtabs = $tabpage.closest('.fwtabs');
        $tab = $fwtabs.find('#' + tabid);
        return $tab;
    };
    //---------------------------------------------------------------------------------
    getTabsByTab($tab) {
        var $elementtab, $tabs;
        $tabs = $tab.closest('.tabs').find('.tab');
        return $tabs;
    };
    //---------------------------------------------------------------------------------
    getTabByCaption($element, tabcaption) {
        var $elementtab, $tabs, $tab;
        $elementtab = FwTabs.getTabByElement($element);
        $tabs = FwTabs.getTabsByTab($elementtab);
        $tab = $tabs.filter('[data-caption="' + tabcaption + '"]').first();
        return $tab;
    };
    //---------------------------------------------------------------------------------
    getTabPageByTab($tab) {
        var $tabpage, $fwtabs, tabpageid;
        $fwtabs = $tab.closest('.fwtabs');
        tabpageid = $tab.attr('data-tabpageid');
        $tabpage = $fwtabs.find('#' + tabpageid);
        return $tabpage;
    };
    //---------------------------------------------------------------------------------
    disableTab($tab) {
        $tab.attr('data-enabled', 'false');
    };
    //---------------------------------------------------------------------------------
    enableTab($tab) {
        $tab.removeAttr('data-enabled');
    };
    //---------------------------------------------------------------------------------
    showTab($tab) {
        $tab.removeAttr('data-tabdisplay');
        $tab.removeAttr('data-enabled');
    };
    //---------------------------------------------------------------------------------
    hideTab($tab) {
        $tab.attr('data-tabdisplay', 'hidden');
        $tab.attr('data-enabled', 'false');
    };
    //---------------------------------------------------------------------------------
    setTabColor($tab, color) {
        $tab.find('.border').css('background-color', color);
    };
    //---------------------------------------------------------------------------------

}

var FwTabs = new FwTabsClass();