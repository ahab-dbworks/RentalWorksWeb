var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var models;
        (function (models) {
            var folder = /** @class */ (function () {
                function folder(folder) {
                    if (folder == null || undefined)
                        folder = {};
                    this.folderName = folder.folderName;
                    this.path = folder.path == null || undefined ? null : folder.path;
                    this.parentPath = folder.parentPath == null || undefined ? null : folder.parentPath;
                    this.folders = folder.folders == null || undefined ? [] : folder.folders;
                    this.files = folder.files == null || undefined ? [] : folder.files;
                    this.isValidModuleFolder = folder.isValidModuleFolder == null || undefined ? false : folder.isValidModuleFolder;
                    this.potentialModuleFolder = folder.potentialModuleFolder == null || undefined ? false : folder.potentialModuleFolder;
                    this._id = dbworksutil.generator.number_id();
                }
                return folder;
            }());
            models.folder = folder;
        })(models = editor.models || (editor.models = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=model.folder.js.map