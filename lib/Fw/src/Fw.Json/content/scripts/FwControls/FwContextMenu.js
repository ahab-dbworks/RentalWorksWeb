var FwContextMenu = {};
//----------------------------------------------------------------------------------------------
FwContextMenu.render = function(title) {
    var html=[], $contextmenu;

    html.push('<div class="fwcontextmenu">');
    html.push('  <div class="fwcontextmenubox">');
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
    $contextmenu = jQuery(html.join('\n'));
    var maxZIndex = FwFunc.getMaxZ('*');
    $contextmenu.css('z-index', maxZIndex);
    jQuery('#application').append($contextmenu);
    //FwContextMenu.center($contextmenu);
    $contextmenu.on('click', function() {
        FwContextMenu.destroy($contextmenu);
    });

    return $contextmenu;
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
FwContextMenu.addMenuItem = function($contextmenu, text, onclick) {
    var $menuitem;
    
    $menuitem = jQuery('<div class="responsive fwcontextmenuitem">' + text + '</div>');
    $contextmenu.find('.fwcontextmenuitems').append($menuitem);
    if (typeof onclick === 'function') {
        $menuitem.on('click', onclick);
    }
    //FwContextMenu.center($contextmenu);

    return $menuitem;
};
//----------------------------------------------------------------------------------------------
FwContextMenu.destroy = function($control) {
    $control.remove();
};
//----------------------------------------------------------------------------------------------