//----------------------------------------------------------------------------------------------
var FwAppDocumentVersionGridController = {
    Module: 'AppDocumentVersion'
};
//----------------------------------------------------------------------------------------------
FwAppDocumentVersionGridController.onRowNewMode = function($control, $tr) {
    $tr.find('div[data-formdatafield="appdocument.received"] input.value').val(FwFunc.getDate());
    $tr.find('div[data-formdatafield="appdocument.reviewed"] input.value').val(FwFunc.getDate());
};
//----------------------------------------------------------------------------------------------
FwAppDocumentVersionGridController.afterRowEditMode = function($control, $tr) {
    if ($tr.find('div[data-formdatafield="appdocument.received"] input.value').val() === '') {
        $tr.find('div[data-formdatafield="appdocument.received"] input.value').val(FwFunc.getDate());
    }
    if ($tr.find('div[data-formdatafield="appdocument.reviewed"] input.value').val() === '') {
        $tr.find('div[data-formdatafield="appdocument.reviewed"] input.value').val(FwFunc.getDate());
    }
};
//----------------------------------------------------------------------------------------------