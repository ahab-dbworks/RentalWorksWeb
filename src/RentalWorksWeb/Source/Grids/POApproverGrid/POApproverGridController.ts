class POApproverGrid {
    Module: string = 'POApproverGrid';
    apiurl: string = 'api/v1/poapprover';

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="HasLimit"] input').data('onchange', function ($tr) {
            var hasLimit = $tr.find('div[data-browsedatafield="HasLimit"]').prop('checked');
           alert(hasLimit)
            if (hasLimit == 'false') {
            FwFormField.disable($tr.find('.limit'));
                console.log("in false")
            }
        });
    }
}

var POApproverGridController = new POApproverGrid();
//----------------------------------------------------------------------------------------------