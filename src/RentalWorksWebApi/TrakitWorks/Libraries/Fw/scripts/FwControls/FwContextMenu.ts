class FwContextMenuClass {
    //----------------------------------------------------------------------------------------------
    render(title, position, $appendto?, event?) {
        if (typeof position !== 'string') {
            position = 'center';
        }
        if (typeof $appendto === 'undefined') {
            $appendto = jQuery('#application');
        }
        const html: Array<string> = [];
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
        //html.push('    <div class="fwcontextmenucancel">');
        //html.push('      <div class="fwcontextmenuitem">Cancel</div>');
        //html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        const $control = jQuery(html.join('\n'));
        const maxZIndex = FwFunc.getMaxZ('*');
        $control.css('z-index', maxZIndex);
        $appendto.append($control);

        const browseContextMenuCell = $appendto.closest('.browsecontextmenucell');
        browseContextMenuCell.css('z-index', '2'); //an effort to fix checkbox bleeding through context menu - z-index is removed when context menu is destroyed below in FwContextMenu.destroy(); - J.Pace
        //FwContextMenu.center($control);
        $control.on('click', function (event) {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        jQuery(document).one('scroll', event => {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        jQuery('.tablewrapper').one('scroll', event => {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        setTimeout(function () {
            jQuery('body').one('click', function (event) {
                try {
                    // event.stopPropagation();
                    FwContextMenu.destroy($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }, 0);

        return $control;
    }
    //----------------------------------------------------------------------------------------------
    //center($control) {
    //    const $confirmbox = $control.find('.fwcontextmenubox');
    //    $confirmbox
    //        .css({
    //            top:  Math.max(0, ((jQuery(window).height() - $confirmbox.outerHeight()) / 2) + jQuery(window).scrollTop()),
    //            left: Math.max(0, ((jQuery(window).width()  - $confirmbox.outerWidth())  / 2) + jQuery(window).scrollLeft())
    //        })
    //    ;
    //    $control.css({height: Math.max(window.innerHeight, document.documentElement.clientHeight, document.body.clientHeight)});
    //}
    //----------------------------------------------------------------------------------------------
    addMenuItem($control, text, onclick) {
        let $menuitem;
        if (text && typeof text === 'string') {
            const textClass = `${text.toLowerCase().replace(/\s/g, '')}option`;
            $menuitem = jQuery(`<div class="responsive fwcontextmenuitem ${textClass}">${text}</div>`);
        } else {
            $menuitem = jQuery(`<div class="responsive fwcontextmenuitem">${text}</div>`);
        }
        $control.find('.fwcontextmenuitems').append($menuitem);
        if (typeof onclick === 'function') {
            $menuitem.on('click', onclick);
        }
        //FwContextMenu.center($control);

        return $menuitem;
    }
    //----------------------------------------------------------------------------------------------
    destroy($control) {
        if (typeof $control.data('beforedestroy') === 'function') {
            typeof $control.data('beforedestroy')();
        }
        const browseContextMenuCell = $control.closest('.browsecontextmenucell');
        browseContextMenuCell.css('z-index', '');
        $control.remove();
    }
    //----------------------------------------------------------------------------------------------
}

var FwContextMenu = new FwContextMenuClass();