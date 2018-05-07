var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var TiwAsset = (function (_super) {
    __extends(TiwAsset, _super);
    function TiwAsset() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.browseModel = {};
        return _this;
    }
    TiwAsset.prototype.getBrowseTemplate = function () {
    };
    return TiwAsset;
}(RwAsset));
window.AssetController = new TiwAsset();
//# sourceMappingURL=TiwAsset.js.map