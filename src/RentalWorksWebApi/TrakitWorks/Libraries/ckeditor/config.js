/**
* @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
* For licensing, see LICENSE.md or http://ckeditor.com/license
*/
 
CKEDITOR.editorConfig = function( config ) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.toolbar_Full = [
        { name: 'document',    groups: [ 'mode', 'document', 'doctools' ], items: [ 'Source'/*, 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', 'Templates', 'document'*/ ] },
        { name: 'clipboard',   groups: [ 'clipboard', 'undo' ], items: [ 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Undo', 'Redo' ] },
        { name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ], items: [ 'Find', 'Replace', 'SelectAll', /*'Scayt'*/ ] },
        { name: 'insert', items: [ /*'CreatePlaceholder', 'Image',*//* 'Flash', */'Table', 'HorizontalRule',/* 'Smiley', */'SpecialChar', /*'PageBreak',*//* 'Iframe', *//*'InsertPre'*/ ] },
        //{ name: 'forms', items: [ /*'Form',*/ 'Checkbox', /*'Radio',*/ 'TextField', /*'Textarea',*/ /*'Select','Button', 'ImageButton', 'HiddenField'*/ ] },
        '/',
        { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ], items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'RemoveFormat' ] },
        { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ], items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'/*, 'BidiLtr', 'BidiRtl'*/ ] },
        { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
        '/',
        { name: 'styles', items: [ 'Styles', 'Format', 'Font', 'FontSize' ] },
        { name: 'colors', items: [ 'TextColor', 'BGColor' ] },
        { name: 'tools', items: [ 'UIColor', 'Maximize', 'ShowBlocks' ] },
        //{ name: 'about', items: [ 'About' ] }
    ];
 
    config.toolbar = "Full";
    config.enterMode = CKEDITOR.ENTER_BR;
    config.tabSpaces = 4;
    config.allowedContent = true;
    //config.allowedContent= 'span div strong i u table thead tbody tr td th p br hr ul ol li input [*]{*}{*};';
    //{
    //  //'input': {
    //  //    attributes: 'type,maxlength'
    //  //},
    //  '$': {
    //      elements: {'*': true},
    //      attributes: ['*'],
    //      styles: ['*'],
    //      classes: '*'
    //  }
    //};
    config.width = '100%';
    config.height = '600px';
};
CKEDITOR.config.allowedContent = true;