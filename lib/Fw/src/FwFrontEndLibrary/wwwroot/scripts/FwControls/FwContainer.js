//---------------------------------------------------------------------------------
var FwContainer = {};
//---------------------------------------------------------------------------------
FwContainer.init = function($control) {
    if (typeof $control.attr('data-version') === 'undefined') {
        $control.attr('data-version', '1');
    }
    if (typeof $control.attr('data-rendermode') === 'undefined') {
        $control.attr('data-rendermode', 'template');
    }
};
//---------------------------------------------------------------------------------
FwContainer.getHtmlTag = function(data_type) {
    var $tag, html, properties, i;
    properties = this.getDesignerProperties(data_type);
    template = [];
    template.push('<div ');
    for (i = 0; i < properties.length; i++) {
        template.push(properties[i].attribute + '="' + properties[i].defaultvalue + '"');
    }
    template.push('></div>');
    html = template.join('');
    return html;
};
//---------------------------------------------------------------------------------
FwContainer.getDesignerProperties = function(data_type) {
    var properties = [], propId, propClass, propDataControl, propDataType, propDataCaption, propDataEnabled, propDataOriginalValue, propDataImageUrl, propDataField, 
        propDataFieldCount, propUniqueId, propMode, propBodyHeight, propHasAudit;
    //id
    propId = {
        caption: 'ID'
      , datatype: 'string'
      , attribute: 'id'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    propId.defaultvalue = FwControl.generateControlId(data_type);
    //class
    propClass = {
        caption: 'CSS Class'
      , datatype: 'string'
      , attribute: 'class'
      , defaultvalue: ''
      , visible: false
      , enabled: false
    };
    propDataControl = {
        caption: 'Control'
      , datatype: 'string'
      , attribute: 'data-control'
      , defaultvalue: 'FwContainer'
      , visible: true
      , enabled: false
    };
    propDataType = {
        caption: 'Type'
      , datatype: 'string'
      , attribute: 'data-type'
      , defaultvalue: data_type
      , visible: true
      , enabled: false
    };
    //data-version
    propDataVersion = {
        caption: 'Version'
      , datatype: 'string'
      , attribute: 'data-version'
      , defaultvalue: '1'
      , visible: false
      , enabled: false
    };
    //data-caption
    propCaption = {
        caption: 'Caption'
      , datatype: 'string'
      , attribute: 'data-caption'
      , defaultvalue: 'Title'
      , visible: true
      , enabled: true
    };
    //data-enabled
    propEnabled = {
        caption: 'Enabled'
      , datatype: 'boolean'
      , attribute: 'data-enabled'
      , defaultvalue: true
      , visible: true
      , enabled: true
    };
    //data-originalvalue
    propOriginalValue = {
        caption: 'Original Value'
      , datatype: 'string'
      , attribute: 'data-originalvalue'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    //data-imgsrc
    propImageUrl = {
        caption: 'Image URL'
      , datatype: 'string' 
      , attribute: 'data-imgsrc'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    propDataField = {
        caption: 'Data Field'
      , datatype: 'string'
      , attribute: 'data-field'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    propMode = {
        caption: 'Mode'
      , datatype: 'string'
      , attribute: 'data-mode'
      , defaultvalue: ''
      , visible: false
      , enabled: false
    };
    propBodyHeight = {
        caption: 'Body Height'
      , datatype: 'string'
      , attribute: 'data-bodyheight'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    propHasAudit = {
        caption: 'Has Audit'
      , datatype: 'bool'
      , attribute: 'data-hasaudit'
      , defaultvalue: ''
      , visible: true
      , enabled: true
    };
    
    switch(data_type) {
        case 'fieldrow':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwform-fieldrow';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            break;
        case 'form':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwform';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            properties.push(propMode);
            properties.push(propHasAudit);
            break;
        case 'browse':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwbrowse';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            break;
        case 'page':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwpage';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            break;
        case 'section':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwform-section';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            properties.push(propBodyHeight);
            break;
        case 'groupbox':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwgroupbox';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            break;
        case 'panel':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwpanel';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            properties.push(propCaption);
            break;
        case 'status':
            properties.push(propId);
            propClass.defaultvalue = 'fwcontrol fwcontainer fwstatus';
            properties.push(propClass);
            properties.push(propDataControl);
            properties.push(propDataType);
            properties.push(propDataVersion);
            break;
    }

    return properties;
};
//---------------------------------------------------------------------------------
FwContainer.renderDesignerHtml = function($this) {
    var data_type, $children, data_rendermode, html, bodyHeight;
    data_type = $this.attr('data-type');
    data_rendermode = $this.attr('data-rendermode');
    $this.attr('data-rendermode', 'designer');
    $this.attr('title', $this.attr('id'));
    bodyHeight = (typeof $this.attr('data-bodyheight') === 'string') ? $this.attr('data-bodyheight') : '';
    switch(data_type)
    {
        case 'fieldrow':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'runtime':
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="children" designer-dropcontainer="true"></div>');
            html = html.join('');
            $this.html(html);
            $this.children('.children').append($children);
            break;
        case 'form':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-body').children().detach()
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="fwform-title"></div>');
            html.push('<div class="fwform-body" designer-dropcontainer="true">');
            html.push('</div>');
            $this.html(html.join(''));
            $this.children('.fwform-body').append($children);
            break;
        case 'browse':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="children" designer-dropcontainer="true"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
        case 'page':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="children" designer-dropcontainer="true"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
        case 'section':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-section-body').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="fwform-section-title">' + $this.attr('data-caption') + '</div>');
            html.push('<div class="fwform-section-body" designer-dropcontainer="true"');
            if (bodyHeight !== '') {
                html.push(' style="height:' + bodyHeight + '"');
            }
            html.push('></div>');
            $this.html(html.join(''));
            $this.children('.fwform-section-body').append($children);
            break;
         case 'groupbox':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.groupbox-body').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="groupbox-title">' + $this.attr('data-caption') + '</div>');
            html.push('<div class="groupbox-body" designer-dropcontainer="true"></div>');
            $this.html(html.join(''));
            $this.children('.groupbox-body').append($children);
            break;
        case 'panel':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="children" designer-dropcontainer="true"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
        case 'status':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push(FwControl.generateDesignerHandle(data_type, $this.attr('id')));
            html.push('<div class="children" designer-dropcontainer="true"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
    }
};
//---------------------------------------------------------------------------------
FwContainer.renderRuntimeHtml = function($this) {
    var data_type, $children, data_rendermode, html, bodyHeight;
    data_type = $this.attr('data-type');
    data_rendermode = $this.attr('data-rendermode');
    $this.attr('data-rendermode', 'runtime');
    bodyHeight = (typeof $this.attr('data-bodyheight') === 'string') ? $this.attr('data-bodyheight') : '';
    switch(data_type)
    {
        case 'fieldrow':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            $this.empty().append($children);
            break;
        case 'form':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-body').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push('<div class="fwform-menu"></div>');
            html.push('<div class="fwform-title"></div>');
            html.push('<div class="fwform-body">');
            html.push('</div>');
            $this.html(html.join(''));
            $this.children('.fwform-body').append($children);
            break;
        case 'browse':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push('<div class="children"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
        case 'page':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push('<div class="children"></div>');
            $this.html(html.join(''));
            $this.children('.children').append($children);
            break;
        case 'section':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-section-body').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push('<div class="fwform-section-title">' + $this.attr('data-caption') + '</div>');
            html.push('<div class="fwform-section-body"');
            if (bodyHeight !== '') {
                html.push(' style="height:' + bodyHeight + '"');
            }
            html.push('></div>');
            $this.html(html.join(''));
            $this.children('.fwform-section-body').append($children);
            break;
        case 'groupbox':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.groupbox-body').children().detach();
                    break;
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            html = [];
            html.push('<div class="groupbox-title">' + $this.attr('data-caption') + '</div>');
            html.push('<div class="groupbox-body"');
            if (bodyHeight !== '') {
                html.push(' style="height:' + bodyHeight + '"');
            }
            html.push('></div>');
            $this.html(html.join(''));
            $this.children('.groupbox-body').append($children);
            break;
        case 'panel':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'runtime':
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            $this.empty().append($children);
            break;
        case 'status':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'runtime':
                case 'template':
                    $children = $this.children().detach();
                    break;
            }
            $this.empty().append($children);
            break;
    }
};
//---------------------------------------------------------------------------------
FwContainer.renderTemplateHtml = function($this) {
    var data_type, $children, data_rendermode, html;
    data_type = $this.attr('data-type');
    data_rendermode = $this.attr('data-rendermode');
    $this.attr('data-rendermode', 'template');
    switch(data_type)
    {
        case 'fieldrow':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    $this.empty().append($children);
                case 'runtime':
                    $children = $this.children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'form':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-body').children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'browse':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'page':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'section':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.fwform-section-body').children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'groupbox':
            switch(data_rendermode) {
                case 'designer':
                case 'runtime':
                    $children = $this.children('.children').children().detach();
                    $this.empty().append($children);
                    break;
            }
            break;
        case 'panel':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'runtime':
                    $children = $this.children().detach();
                    $this.empty().append($children);
                    break;
                case 'template':
                    break;
            }
            break;
        case 'status':
            switch(data_rendermode) {
                case 'designer':
                    $children = $this.children('.children').children().detach();
                    break;
                case 'runtime':
                    $children = $this.children().detach();
                    $this.empty().append($children);
                    break;
                case 'template':
                    break;
            }
            break;
    }
};
//---------------------------------------------------------------------------------
FwContainer.disable = function(options, $these) {
    $these.each(function(index, element) {
        var $this;
        $this = jQuery(element);
        $this.attr('data-enabled', 'false');
    });
    return tables;
};
//---------------------------------------------------------------------------------
FwContainer.enable = function(options, $these) {
    var $this;
    $these.each(function(index, element) {
        var $this;
        $this = jQuery(element);
        $this.attr('data-enabled', 'true');
    });
    return tables;
};
//---------------------------------------------------------------------------------
