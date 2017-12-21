var index = /** @class */ (function () {
    function index() {
        this.main = new dbworks.editor.controllers.main();
    }
    index.prototype.start = function () {
        //this.default_start_page();
        this.main.init();
    };
    index.prototype.default_start_page = function () {
        var body = document.getElementById('master_designer_container');
        body.innerHTML = templates.editor.main();
    };
    return index;
}());
//# sourceMappingURL=index.js.map