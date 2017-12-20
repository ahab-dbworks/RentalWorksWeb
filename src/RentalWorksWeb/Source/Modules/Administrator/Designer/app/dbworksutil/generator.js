var dbworksutil;
(function (dbworksutil) {
    var generator = (function () {
        function generator() {
        }
        generator.number_id = function () {
            var d = new Date(), day = d.getDay(), year = d.getFullYear(), hour = d.getHours(), seconds = d.getMilliseconds(), rand = 0;
            return rand = day + year + hour + seconds;
        };
        return generator;
    }());
    dbworksutil.generator = generator;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=generator.js.map