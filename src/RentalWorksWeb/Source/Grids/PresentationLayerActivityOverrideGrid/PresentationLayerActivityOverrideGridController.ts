class PresentationLayerActivityOverrideGrid {
    Module: string = 'PresentationLayerActivityOverrideGrid';
    apiurl: string = 'api/v1/presentationlayeractivityoverride';

    addLegend($control: any) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }


    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="MasterId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    //beforeValidateActivity($browse, $grid, request) {
    //    var $form = $grid.closest('.fwform');
    //    request.uniqueIds = {
    //        PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
    //    }
    //}

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'PresentationLayerActivityId':
                request.uniqueIds = {
                    PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepresentationlayeractivity`);
                break;
            case 'MasterId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemaster`);
                break;
        }
    }
}

var PresentationLayerActivityOverrideGridController = new PresentationLayerActivityOverrideGrid();
//----------------------------------------------------------------------------------------------