var AppDocument = (function () {
    function AppDocument() {
    }
    AppDocument.prototype.uploadAppDocument = function (imgUri, appdocumentid, uniqueid1, uniqueid2, documenttypeid, successCallback, failCallback) {
        var fileEntry = this.getFileEntry(imgUri, function () {
            var fileURL = fileEntry.toURL();
            var options = new FileUploadOptions();
            options.fileKey = "file";
            options.fileName = fileURL.substr(fileURL.lastIndexOf('/') + 1);
            options.mimeType = "text/plain";
            options.params = {
                appdocumentid: appdocumentid,
                uniqueid1: uniqueid1,
                uniqueid2: uniqueid2,
                documenttypeid: documenttypeid
            };
            var uploadUrl = applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'appdocumenthandler.ashx';
            var ft = new FileTransfer();
            ft.upload(fileURL, encodeURI(uploadUrl), successCallback, failCallback, options);
        }, failCallback);
    };
    AppDocument.prototype.getFileEntry = function (imgUri, successCallback, failCallback) {
        var me = this;
        window.resolveLocalFileSystemURL(imgUri, function success(fileEntry) {
            // Do something with the FileEntry object, like write to it, upload it, etc.
            // writeFile(fileEntry, imgUri);
            console.log("got file: " + fileEntry.fullPath);
            // displayFileData(fileEntry.nativeURL, "Native URL");
        }, function () {
            // If don't get the FileEntry (which may happen when testing
            // on some emulators), copy to a new FileEntry.
            me.createNewFileEntry(imgUri, successCallback, failCallback);
        });
    };
    AppDocument.prototype.createNewFileEntry = function (imgUri, successCallback, failCallback) {
        window.resolveLocalFileSystemURL(cordova.file.cacheDirectory, function success(dirEntry) {
            var success = typeof successCallback === 'function' ? successCallback : function () { };
            var fail = typeof failCallback === 'function' ? failCallback : function () { };
            dirEntry.getFile("temp.jpeg", { create: true, exclusive: false }, function (fileEntry) {
                // Do something with it, like write to it, upload it, etc.
                // writeFile(fileEntry, imgUri);
                console.log("got file: " + fileEntry.fullPath);
                // displayFileData(fileEntry.fullPath, "File copied to");
            }, function () {
            });
        }, function () {
        });
    };
    return AppDocument;
}());
window.AppDocumentController = new AppDocument();
//# sourceMappingURL=AppDocument.js.map