var RwTest = {
    RwDwServices: {
        parameters: {}
      , reports: {}
    }
};
//----------------------------------------------------------------------------------------------
RwTest.RwDwServices.reports.customerRevenueByMonth = function() {
    var request = {
          fromDate:      '4/1/2013'
        , toDate:        '4/30/2013'
        , activityTypes: []
        , customers:     []
        , departments:   []
        , locations:     []
        , deals:         []
        , dealTypes:     []
        , categories:    []
        , iCodes:        []
    };
    RwDwServices.reports.customerRevenueByMonth(request, function(response) {
        jQuery("body")
            .remove('#fileDownload1')
            .append('<iframe id="fileDownload1" src="' + response.url + '" style="display: none;"></iframe>')
    });
};
//----------------------------------------------------------------------------------------------
