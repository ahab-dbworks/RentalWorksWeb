class AlertWebUsersGrid {
    Module: string = 'AlertWebUsers';
    apiurl: string = 'api/v1/alertwebusers';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="UserId"]').data('onchange', $tr => {
            const email = $tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue');
            $generatedtr.find('.field[data-browsedatafield="Email"]').text(email);
        });
    }
}

var AlertWebUsersGridController = new AlertWebUsersGrid();
//----------------------------------------------------------------------------------------------