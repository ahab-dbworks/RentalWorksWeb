class FwControl {
    //---------------------------------------------------------------------------------
    static getHtmlTag($this: JQuery) {
        var functionName, html, data_control, data_type, designerObj;
        functionName = 'getHtmlTag';
        data_control = $this.attr('data-control');
        data_type    = $this.attr('data-type');
        designerObj  = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
        html = designerObj[functionName](data_type);
        return html;
    };
    //---------------------------------------------------------------------------------
    static getDroppableRegions($this: JQuery) {
      var functionName, properties, data_control, data_type, designerObj;
      functionName = 'getDroppableRegions';
      data_control = $this.attr('data-control');
      data_type = $this.attr('data-type');
      designerObj = window[data_control];
      if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
      properties = designerObj[functionName](data_type);
      return properties;
    };
    //---------------------------------------------------------------------------------
    static getDesignerProperties($this: JQuery) {
        var functionName, properties, data_control, data_type, designerObj;
        functionName = 'getDesignerProperties';
        data_control = $this.attr('data-control');
        data_type    = $this.attr('data-type');
        designerObj  = window[data_control];
        if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
        properties   = designerObj[functionName](data_type);
        return properties;
    };
    //---------------------------------------------------------------------------------
    static init($these: JQuery) {
        var functionName, data_control, $this, designerObj, designerObjName;
        functionName = 'init';
        $these
            .each(function(index, element) {
                $this        = jQuery(element);
                data_control = $this.attr('data-control');
                designerObj     = window[data_control];
                if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
                designerObj[functionName]($this);
            })
            .on('click', '.fwdesignerhandlemenu .delete', function() {
                jQuery(this).closest('.FwControl').remove();
            })
        ;
    };
    //---------------------------------------------------------------------------------
    static renderDesignerHtml($these: JQuery) {
        var functionName, data_control, data_type, $this, designerObj;
        functionName = 'renderDesignerHtml';
        $these.each(function(index, element) {
            $this        = jQuery(element);
            data_control = $this.attr('data-control');
            data_type    = $this.attr('data-type');
            designerObj     = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    //---------------------------------------------------------------------------------
    static renderRuntimeHtml($these: JQuery) {
        var functionName;
        functionName = 'renderRuntimeHtml';
        $these.each(function(index, element) {
            var data_control, data_type, $this, designerObj;
            $this         = jQuery(element);
            data_control  = $this.attr('data-control');
            data_type     = $this.attr('data-type');
            designerObj   = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    //---------------------------------------------------------------------------------
    static renderTemplateHtml($these: JQuery) {
        var functionName;
        functionName = 'renderTemplateHtml';
        $these.each(function(index, element) {
            var data_control, data_type, $this, designerObj;
            $this = jQuery(element);
            $this.removeClass('activeElement');
            data_control = $this.attr('data-control');
            data_type    = $this.attr('data-type');
            designerObj     = window[data_control];
            if ((typeof designerObj === 'undefined') || (typeof designerObj[functionName] !== 'function')) throw 'Not implemented: ' + data_control + '.' + functionName;
            designerObj[functionName]($this);
        });
    };
    //---------------------------------------------------------------------------------
    static generateControlId(prefix: string) {
        var isUniqueHtmlId, counterHtmlId, htmlId;
        isUniqueHtmlId = false;
        counterHtmlId = 1;
        while(!isUniqueHtmlId) {
            htmlId = prefix + counterHtmlId.toString(); 
            if (jQuery('#' + htmlId).length === 0) {
                isUniqueHtmlId = true;
            } else {
                counterHtmlId++;
            }
        }
        return htmlId;
    };
    //---------------------------------------------------------------------------------
    static generateDesignerHandle(controltype: string, id: string) {
        var html;

        html = [];
        html.push('<div class="fwdesignerhandle">');
            html.push('<div class="fwdesignerhandlecaption" draggable="true">' + controltype + ': ' + id + '</div>');
            html.push('<div class="fwdesignerhandlemenu">');
                if (controltype == 'form' || controltype == 'page' || controltype == 'browse') {
                
                } else {
                    html.push('<div class="delete">x</div>');
                }
            html.push('</div>');
        html.push('</div>');
        html = html.join('');

        return html;
    };
    //---------------------------------------------------------------------------------
    static setIds($these: JQuery, formid: string) {
        $these.each(function(index, element) {
            var id, $this;
            $this = jQuery(element);
            id    = $this.attr('id');
            if (typeof id !== 'undefined') {
                id    = id + '-' + formid;
                $this.attr('id', id);
            }
        });
    };
    //---------------------------------------------------------------------------------
    static renderRuntimeObject($object: JQuery) {
        var $fwcontrols = $object.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);
    };
    //---------------------------------------------------------------------------------
    static renderRuntimeControls($these: JQuery) {
        FwControl.init($these);
        FwControl.renderRuntimeHtml($these);
    };
    //---------------------------------------------------------------------------------
    static loadControls($these: JQuery) {
        var functionName;
        functionName = 'loadControl';
        $these.each(function(index, element) {
            var data_control, $this, controlObj;
            $this         = jQuery(element);
            data_control  = $this.attr('data-control');
            controlObj   = window[data_control];
            if ((typeof controlObj !== 'undefined') && (typeof controlObj[functionName] === 'function')) {
                controlObj[functionName]($this);
            }
        });
    };
    //---------------------------------------------------------------------------------
}
