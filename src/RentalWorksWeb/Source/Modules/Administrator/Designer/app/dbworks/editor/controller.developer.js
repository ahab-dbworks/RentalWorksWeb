var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var developer = (function () {
                function developer(editor_controller) {
                    this._editor = editor_controller;
                    this.code_mirror = null;
                }
                developer.prototype.init = function () {
                    this.setup();
                    this.events();
                };
                developer.prototype.setup = function () {
                    this.configure_codemirror('cs');
                };
                developer.prototype.events = function () {
                    var _this = this;
                    jQuery(window).resize(function (e) {
                        _this.code_mirror.setSize('100%', jQuery('#main_content_body').height() + 'px');
                    });
                };
                developer.prototype.configure_codemirror = function (lang) {
                    var _this = this;
                    jQuery('.CodeMirror').remove();
                    var config = null;
                    switch (lang) {
                        case 'cs':
                            config = {
                                lineNumbers: true,
                                matchBrackets: true,
                                mode: "text/x-csharp",
                                extraKeys: { "Ctrl-Q": function (cm) { console.log(cm); cm.foldCode(cm.getCursor()); } },
                                foldGutter: true,
                                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                            };
                            break;
                        case 'ts':
                            config = {
                                mode: "text/typescript",
                                lineNumbers: true,
                                matchBrackets: true,
                                extraKeys: { "Ctrl-Q": function (cm) { console.log(cm); cm.foldCode(cm.getCursor()); } },
                                foldGutter: true,
                                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                            };
                            break;
                        case 'js':
                            config = {
                                mode: "javascript",
                                lineNumbers: true,
                                lineWrapping: true,
                                extraKeys: { "Ctrl-Q": function (cm) { console.log(cm); cm.foldCode(cm.getCursor()); } },
                                foldGutter: true,
                                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                            };
                            break;
                        case 'htm':
                            config = {
                                name: "htmlmixed",
                                scriptTypes: [{
                                        matches: '/\/x-handlebars-template|\/x-mustache/i',
                                        mode: null
                                    },
                                    {
                                        matches: '/(text|application)\/(x-)?vb(a|script)/i',
                                        mode: "vbscript"
                                    }],
                                lineNumbers: true,
                                lineWrapping: true,
                                extraKeys: { "Ctrl-Q": function (cm) { console.log(cm); cm.foldCode(cm.getCursor()); } },
                                foldGutter: true,
                                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                            };
                            break;
                    }
                    var textarea = document.getElementById('code_editor');
                    this.code_mirror = CodeMirror.fromTextArea(textarea, config);
                    this.code_mirror.on('keyup', function () {
                        var content = _this.code_mirror.getValue(), folderIndex = jQuery('#main_content_body').data('activefolderindex'), fileIndex = jQuery('#main_content_body').data('activefileindex'), file = _this._editor.get_file_from_folder(folderIndex, fileIndex);
                        file.fileContents = content;
                        file.hasChanged = true;
                    });
                    this.code_mirror.setSize('100%', jQuery('#main_content_body').height() + 'px');
                };
                developer.prototype.edit_file = function (file) {
                    jQuery('#code_editor').empty();
                    this.code_mirror.getDoc().setValue(file.fileContents);
                };
                developer.prototype.clear_editor = function () {
                    jQuery('#code_editor').empty();
                    this.code_mirror.getDoc().setValue("");
                };
                developer.prototype.get_editor_content = function () {
                    return this.code_mirror.getValue();
                };
                developer.prototype.get_file_html = function () {
                };
                return developer;
            }());
            controllers.developer = developer;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.developer.js.map