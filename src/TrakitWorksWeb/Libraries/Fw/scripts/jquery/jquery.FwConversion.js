function numberWithCommas(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}
function numberWithoutCommas(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(",", "");
    return parts.join(".");
}