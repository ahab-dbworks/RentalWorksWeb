class FwContextMenuClass {
    render(title, position, $appendto, event) {
        if (typeof position !== 'string') {
            position = 'center';
        }
        if (typeof $appendto === 'undefined') {
            $appendto = jQuery('#application');
        }
        const html = [];
        if (typeof event !== 'undefined' && event !== null) {
            const viewPort = document.querySelector('html');
            const scrollTop = viewPort.scrollTop;
            const scrollLeft = viewPort.scrollLeft;
            const topValue = event.pageY - scrollTop;
            const leftValue = event.pageX - scrollLeft - 105;
            html.push(`<div style="top:${topValue}px;left:${leftValue}px;" class="fwcontextmenu" data-position="${position}">`);
        }
        else {
            html.push(`<div class="fwcontextmenu" data-position="${position}">`);
        }
        html.push(`  <div class="fwcontextmenubox">`);
        if ((typeof title === 'string') && (title.length > 0)) {
            html.push(`    <div class="fwcontextmenutitle">${title}</div>`);
        }
        html.push('    <div class="fwcontextmenuitems">');
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        const $control = jQuery(html.join('\n'));
        const maxZIndex = FwFunc.getMaxZ('*');
        $control.css('z-index', maxZIndex);
        $appendto.append($control);
        $control.on('click', function (event) {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        jQuery(document).one('scroll', event => {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        jQuery('.tablewrapper').one('scroll', event => {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        setTimeout(function () {
            jQuery('body').one('click', function (event) {
                try {
                    FwContextMenu.destroy($control);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }, 0);
        return $control;
    }
    addMenuItem($control, text, onclick) {
        let $menuitem;
        if (text && typeof text === 'string') {
            const textClass = `${text.toLowerCase().replace(/\s/g, '')}option`;
            $menuitem = jQuery(`<div class="responsive fwcontextmenuitem ${textClass}">${text}</div>`);
        }
        else {
            $menuitem = jQuery(`<div class="responsive fwcontextmenuitem">${text}</div>`);
        }
        $control.find('.fwcontextmenuitems').append($menuitem);
        if (typeof onclick === 'function') {
            $menuitem.on('click', onclick);
        }
        return $menuitem;
    }
    destroy($control) {
        if (typeof $control.data('beforedestroy') === 'function') {
            typeof $control.data('beforedestroy')();
        }
        $control.remove();
    }
}
var FwContextMenu = new FwContextMenuClass();
//# sourceMappingURL=FwContextMenu.js.map