var RwPartialController: any = {};
//----------------------------------------------------------------------------------------------
RwPartialController.getScanBarcodeHtml = function(viewModel, caption) {
    var defaultViewModel, $view, html;

    defaultViewModel = {
        captionBarcodeICode: RwLanguages.translate(caption)
    };
    viewModel = jQuery.extend({}, defaultViewModel, viewModel);
    $view = jQuery('<div>');
    $view.html(Mustache.render(jQuery('#tmpl-scanBarcode').html(), viewModel));
    $view.find('#scanBarcodeView-options').hide();
    html = $view.html();

    return html;
};
//----------------------------------------------------------------------------------------------
