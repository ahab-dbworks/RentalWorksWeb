var FwControl = (function () {
    function FwControl() {
    }
    FwControl.getHtmlTag = function ($this) {
        var functionName, html, data_control, data_type, designerObj;
        functionName = 'getHtmlTag';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        html = designerObj[functionName](data_type);
        return html;
    };
    ;
    FwControl.getDroppableRegions = function ($this) {
        var functionName, properties, data_control, data_type, designerObj;
        functionName = 'getDroppableRegions';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        properties = designerObj[functionName](data_type);
        return properties;
    };
    ;
    FwControl.getDesignerProperties = function ($this) {
        var functionName, properties, data_control, data_type, designerObj;
        functionName = 'getDesignerProperties';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        properties = designerObj[functionName](data_type);
        return properties;
    };
    ;
    FwControl.init = function ($these) {
        var functionName, data_control, $this, designerObj, designerObjName;
        functionName = 'init';
        $these
            .each(function (index, element) {
            $this = jQuery(element);
            data_control = $this.attr('data-control');
            designerObj = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
                throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        })
            .on('click', '.fwdesignerhandlemenu .delete', function () {
            jQuery(this).closest('.FwControl').remove();
        });
    };
    ;
    FwControl.renderDesignerHtml = function ($these) {
        var functionName, data_control, data_type, $this, designerObj;
        functionName = 'renderDesignerHtml';
        $these.each(function (index, element) {
            $this = jQuery(element);
            data_control = $this.attr('data-control');
            data_type = $this.attr('data-type');
            designerObj = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
                throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    ;
    FwControl.renderRuntimeHtml = function ($these) {
        var functionName;
        functionName = 'renderRuntimeHtml';
        $these.each(function (index, element) {
            var data_control, data_type, $this, designerObj;
            $this = jQuery(element);
            data_control = $this.attr('data-control');
            data_type = $this.attr('data-type');
            designerObj = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
                throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    ;
    FwControl.renderTemplateHtml = function ($these) {
        var functionName;
        functionName = 'renderTemplateHtml';
        $these.each(function (index, element) {
            var data_control, data_type, $this, designerObj;
            $this = jQuery(element);
            $this.removeClass('activeElement');
            data_control = $this.attr('data-control');
            data_type = $this.attr('data-type');
            designerObj = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
                throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    ;
    FwControl.generateControlId = function (prefix) {
        var isUniqueHtmlId, counterHtmlId, htmlId;
        isUniqueHtmlId = false;
        counterHtmlId = 1;
        while (!isUniqueHtmlId) {
            htmlId = prefix + counterHtmlId.toString();
            if (jQuery('#' + htmlId).length === 0) {
                isUniqueHtmlId = true;
            }
            else {
                counterHtmlId++;
            }
        }
        return htmlId;
    };
    ;
    FwControl.generateDesignerHandle = function (controltype, id) {
        var html;
        html = [];
        html.push('<div class="fwdesignerhandle">');
        html.push('<div class="fwdesignerhandlecaption" draggable="true">' + controltype + ': ' + id + '</div>');
        html.push('<div class="fwdesignerhandlemenu">');
        if (controltype == 'form' || controltype == 'page' || controltype == 'browse') {
        }
        else {
            html.push('<div class="delete">x</div>');
        }
        html.push('</div>');
        html.push('</div>');
        html = html.join('');
        return html;
    };
    ;
    FwControl.setIds = function ($these, formid) {
        $these.each(function (index, element) {
            var id, $this;
            $this = jQuery(element);
            id = $this.attr('id');
            if (typeof id !== 'undefined') {
                id = id + '-' + formid;
                $this.attr('id', id);
            }
        });
    };
    ;
    FwControl.renderRuntimeObject = function ($object) {
        var $fwcontrols = $object.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);
    };
    ;
    FwControl.renderRuntimeControls = function ($these) {
        FwControl.init($these);
        FwControl.renderRuntimeHtml($these);
    };
    ;
    FwControl.loadControls = function ($these) {
        var functionName;
        functionName = 'loadControl';
        $these.each(function (index, element) {
            var data_control, $this, controlObj;
            $this = jQuery(element);
            data_control = $this.attr('data-control');
            controlObj = window[data_control];
            if ((typeof controlObj !== 'undefined') && (typeof controlObj[functionName] === 'function')) {
                controlObj[functionName]($this);
            }
        });
    };
    ;
    return FwControl;
}());
//# sourceMappingURL=FwControl.js.map