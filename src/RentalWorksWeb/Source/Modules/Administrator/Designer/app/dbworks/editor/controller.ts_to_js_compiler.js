var ts;
var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var ts_to_js_compiler = (function () {
                function ts_to_js_compiler() {
                }
                ts_to_js_compiler.compile = function (tscript) {
                    var result = ts.transpileModule(tscript, { compilerOptions: { module: ts.ModuleKind.CommonJS } });
                    return result;
                };
                return ts_to_js_compiler;
            }());
            controllers.ts_to_js_compiler = ts_to_js_compiler;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.ts_to_js_compiler.js.map