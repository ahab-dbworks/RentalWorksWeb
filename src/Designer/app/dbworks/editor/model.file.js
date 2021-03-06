var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var models;
        (function (models) {
            var file = (function () {
                function file(file) {
                    if (file == null || undefined)
                        file = {};
                    this.fileName = file.fileName;
                    this.fileContents = file.fileContents;
                    this.path = file.path;
                    this.ext = file.ext;
                    this.isEditable = file.isEditable == null ? true : true;
                    this.hasChanged = file.hasChanged == null ? true : false;
                }
                file.prototype.update = function (prop, value) {
                    this.hasChanged = true;
                    return this;
                };
                return file;
            }());
            models.file = file;
        })(models = editor.models || (editor.models = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=model.file.js.map