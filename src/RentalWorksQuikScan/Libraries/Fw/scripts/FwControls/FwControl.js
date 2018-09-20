class FwControl {
    static getHtmlTag($this) {
        var functionName, html, data_control, data_type, designerObj;
        functionName = 'getHtmlTag';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        html = designerObj[functionName](data_type);
        return html;
    }
    ;
    static getDroppableRegions($this) {
        var functionName, properties, data_control, data_type, designerObj;
        functionName = 'getDroppableRegions';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        properties = designerObj[functionName](data_type);
        return properties;
    }
    ;
    static getDesignerProperties($this) {
        var functionName, properties, data_control, data_type, designerObj;
        functionName = 'getDesignerProperties';
        data_control = $this.attr('data-control');
        data_type = $this.attr('data-type');
        designerObj = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function'))
            throw 'Not implemented: ' + data_control + '.' + functionName;
        properties = designerObj[functionName](data_type);
        return properties;
    }
    ;
    static init($these) {
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
    }
    ;
    static renderDesignerHtml($these) {
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
    }
    ;
    static renderRuntimeHtml($these) {
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
    }
    ;
    static renderTemplateHtml($these) {
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
    }
    ;
    static generateControlId(prefix) {
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
    }
    ;
    static generateDesignerHandle(controltype, id) {
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
    }
    ;
    static setIds($these, formid) {
        $these.each(function (index, element) {
            var id, $this;
            $this = jQuery(element);
            id = $this.attr('id');
            if (typeof id !== 'undefined') {
                id = id + '-' + formid;
                $this.attr('id', id);
            }
        });
    }
    ;
    static renderRuntimeObject($object) {
        var $fwcontrols = $object.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);
    }
    ;
    static renderRuntimeControls($these) {
        FwControl.init($these);
        FwControl.renderRuntimeHtml($these);
    }
    ;
    static loadControls($these) {
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
    }
    ;
}
//# sourceMappingURL=FwControl.js.map