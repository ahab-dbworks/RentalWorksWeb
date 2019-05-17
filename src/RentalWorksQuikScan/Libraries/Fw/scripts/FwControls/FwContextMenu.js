var FwContextMenuClass = (function () {
    function FwContextMenuClass() {
    }
    FwContextMenuClass.prototype.render = function (title, position, $appendto, event) {
        var html = [], $control, viewPort, scrollTop, scrollLeft, topValue, leftValue, maxZIndex;
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
            html.push("<div style=\"top:" + topValue + "px;left:" + leftValue + "px;\" class=\"fwcontextmenu\" data-position=\"" + position + "\">");
        }
        else {
            html.push("<div class=\"fwcontextmenu\" data-position=\"" + position + "\">");
        }
        html.push("  <div class=\"fwcontextmenubox\">");
        if ((typeof title === 'string') && (title.length > 0)) {
            html.push('    <div class="fwcontextmenutitle">' + title + '</div>');
        }
        html.push('    <div class="fwcontextmenuitems">');
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        $control = jQuery(html.join('\n'));
        maxZIndex = FwFunc.getMaxZ('*');
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
        jQuery(document).one('scroll', function (event) {
            try {
                event.stopPropagation();
                FwContextMenu.destroy($control);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        jQuery('.tablewrapper').one('scroll', function (event) {
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
    };
    FwContextMenuClass.prototype.addMenuItem = function ($control, text, onclick) {
        var $menuitem;
        $menuitem = jQuery("<div class=\"responsive fwcontextmenuitem\">" + text + "</div>");
        $control.find('.fwcontextmenuitems').append($menuitem);
        if (typeof onclick === 'function') {
            $menuitem.on('click', onclick);
        }
        return $menuitem;
    };
    FwContextMenuClass.prototype.destroy = function ($control) {
        if (typeof $control.data('beforedestroy') === 'function') {
            typeof $control.data('beforedestroy')();
        }
        $control.remove();
    };
    return FwContextMenuClass;
}());
var FwContextMenu = new FwContextMenuClass();
//# sourceMappingURL=FwContextMenu.js.map