class Worksheet {
    renderSubWorksheet = function ($form) {
        var html = [];

        html.push('<div id="worksheetpopup" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');
        html.push('  <div id="searchTabs" class="fwcontrol fwtabs" data-rendermode="runtime" data-version="1" data-control="FwTabs">');
        html.push('    <div class="tabs"></div>');
        html.push('    <div class="tabpages"></div>');
        html.push('  </div>');
        html.push('  <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');

        var $popupHtml = html.join('');
        var $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
    }
}

var WorksheetController = new Worksheet();