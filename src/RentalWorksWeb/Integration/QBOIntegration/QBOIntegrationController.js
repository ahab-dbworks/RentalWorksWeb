window.onload = function() {
    if ((typeof parent.RwIntegrationController == 'undefined') || (!parent.RwIntegrationController.verifyAuthToken())) {
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
    ;
};