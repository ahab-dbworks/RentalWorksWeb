class CountryValidation {
    Module: string = 'CountryValidation';
    apiurl: string = 'api/v1/country';

    addValidationEvents($control) {
        $control.data('onchange', e => {
            const $form = $control.parents('.fwform');
            const phoneFields = $form.find('div[data-type="phone"][data-phonemask="true"]');
            phoneFields.find('input').inputmask('remove');
        })
    }
}

var CountryValidationController = new CountryValidation();