var FwContextMenu = {};
//----------------------------------------------------------------------------------------------
FwContextMenu.render = function (title, position, $appendto, event) {
    let html = [], $control, viewPort, scrollTop, scrollLeft, topValue, leftValue, maxZIndex;
    if (typeof position !== 'string') {
        position = 'center';
    }
    if (typeof $appendto === 'undefined') {
        $appendto = jQuery('#application');
    }
    if (typeof event !== 'undefined' && event !== null) {
        viewPort = document.querySelector('html');
        scrollTop = viewPort.scrollTop;
        scrollLeft = viewPort.scrollLeft;
        topValue = event.pageY - scrollTop;
        leftValue = event.pageX - scrollLeft - 105;
        html.push(`<div style="top:${topValue}px;left:${leftValue}px;" class="fwcontextmenu" data-position="${position}">`);
    }
    else {
        html.push(`<div class="fwcontextmenu" data-position="${position}">`);
    }
    html.push(`  <div class="fwcontextmenubox">`);
    //if ((typeof title === 'string') && (title.length > 0)) {
    //    html.push('    <div class="fwcontextmenutitle">' + title + '</div>');
    //}
    html.push('    <div class="fwcontextmenuitems">');
    html.push('    </div>');
    //html.push('    <div class="fwcontextmenucancel">');
    //html.push('      <div class="fwcontextmenuitem">Cancel</div>');
    //html.push('    </div>');
    html.push('  </div>');
    html.push('</div>');
    $control = jQuery(html.join('\n'));
    maxZIndex = FwFunc.getMaxZ('*');
    $control.css('z-index', maxZIndex);
    $appendto.append($control);
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
};
//----------------------------------------------------------------------------------------------
//FwContextMenu.center = function($control) {
//    var $confirmbox;

//    $confirmbox = $control.find('.fwcontextmenubox');
//    $confirmbox
//        .css({
//            top:  Math.max(0, ((jQuery(window).height() - $confirmbox.outerHeight()) / 2) + jQuery(window).scrollTop()),
//            left: Math.max(0, ((jQuery(window).width()  - $confirmbox.outerWidth())  / 2) + jQuery(window).scrollLeft())
//        })
//    ;
//    $control.css({height: Math.max(window.innerHeight, document.documentElement.clientHeight, document.body.clientHeight)});
//};
//----------------------------------------------------------------------------------------------
FwContextMenu.addMenuItem = function ($control, text, onclick) {
    let $menuitem;

    $menuitem = jQuery(`<div class="responsive fwcontextmenuitem">${text}</div>`);
    $control.find('.fwcontextmenuitems').append($menuitem);
    if (typeof onclick === 'function') {
        $menuitem.on('click', onclick);
    }
    //FwContextMenu.center($control);

    return $menuitem;
};
//----------------------------------------------------------------------------------------------
FwContextMenu.destroy = function ($control) {
    if (typeof $control.data('beforedestroy') === 'function') {
        typeof $control.data('beforedestroy')();
    }
    $control.remove();
};
//----------------------------------------------------------------------------------------------