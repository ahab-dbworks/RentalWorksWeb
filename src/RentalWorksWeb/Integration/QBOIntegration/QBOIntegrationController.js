window.onload = function() {
    if ((typeof parent.RwChargeProcessingController == 'undefined') || (!parent.RwChargeProcessingController.verifyAuthToken())) {
       intuit.ipp.jQuery('#qboconnect').empty();
       intuit.ipp.jQuery('#qboconnect').append('<div>Please login to use this feature.</div>');
    } else {
        PageMethods.LoadScreen(JSON.parse(parent.sessionStorage.getItem('location')).locationid,
            function (result, userContext, methodName) {
                if (result.connected) {
                    intuit.ipp.jQuery('.dateconnected').html(result.dateconnected);
                    intuit.ipp.jQuery('.expiredays').html(result.expiresindays + ' Days');
                    if (parseInt(result.expiresindays) > 10)
                    {
                        intuit.ipp.jQuery('.connecttoqbo').addClass('disabled');
                    }
                } else {
                    intuit.ipp.jQuery('.dateconnected').html('Not Connected');
                    intuit.ipp.jQuery('.expire').hide();
                    intuit.ipp.jQuery('.disconnectqbo').addClass('disabled');
                    intuit.ipp.jQuery('.exporttoqbo').addClass('disabled');
                }
            },
            function (error, userContext, methodName) {
                if (error !== null) {
                    alert(error.get_message());
                }
            }
        );
    }
    intuit.ipp.jQuery('#qboconnect')
        .on('click', '.connecttoqbo', function() {
            if (!intuit.ipp.jQuery(this).hasClass('disabled')) {
                intuit.ipp.anywhere.controller.onConnectToIntuitClicked();
            }
        })
        .on('click', '.disconnectqbo', function() {
            if (!intuit.ipp.jQuery(this).hasClass('disabled')) {
                PageMethods.Disconnect(
                    function (result, userContext, methodName) {
                        location.reload();
                    },
                    function (error, userContext, methodName) {
                        if (error !== null) {
                            alert(error.get_message());
                        }
                    }
                );
            }
        })
        .on('click', '.exporttoqbo', function() {
            if (!intuit.ipp.jQuery(this).hasClass('disabled')) {
                var formdata = parent.RwChargeProcessingController.getFormData();
                if (formdata.run == true) {
                    var $confirm = parent.FwConfirmation.renderConfirmation('Export batch to Quickbooks', 'Running please wait...');
                    PageMethods.ExportToQBO(formdata,
                        function (result, userContext, methodName) {
                            if (result.status == "0") {
                                //$confirm.find('.message').html(result.message);
                                var html = [];
                                html.push('<table style="table-layout:fixed;border-collapse:collapse;text-align:left;font-size:13px;">');
                                html.push('<thead><tr><th style="width:90px;">Invoice No</th><th>Status</th></tr></thead>')
                                html.push('<tbody>');
                                for (var i = 0; i < result.invoices.length; i++) {
                                    html.push('<tr><th>' + result.invoices[i].invoiceno + '</th><th>' + result.invoices[i].message + '</th></tr>');
                                }
                                html.push('</tbody>');
                                html.push('</table>');
                                $confirm.find('.message').html(html.join(''));
                            } else {
                                $confirm.find('.message').html(result.message);
                            }
                            parent.FwConfirmation.addButton($confirm, 'Ok', true);
                        },
                        function (error, userContext, methodName) {
                            if (error !== null) {
                                alert(error.message);
                            }
                        }
                    );
                }
            }
        })
    ;
};