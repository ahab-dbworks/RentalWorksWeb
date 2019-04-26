var FwLanguages = {};
FwLanguages.cultures = {};
if (!localStorage.getItem('currentCulture')) {
    localStorage.setItem('currentCulture', 'enUs');
}
FwLanguages.currentCulture = localStorage.getItem('currentCulture');

FwLanguages.translate = function(caption) {
    if (typeof FwLanguages.cultures[FwLanguages.currentCulture] === 'undefined') {
        FwLanguages.currentCulture = 'enUs';
        localStorage.setItem('currentCulture', 'enUs');
    }
    return FwLanguages.cultures[this.currentCulture][caption];
};

FwLanguages.captions = [
    'Active',
    'All',
    'Contact Event',
    'Contact Title',
    'Delete',
    'Edit',
    'E-mail Document',
    'E-mail PDF',
    'Event',
    'Inactive',
    'List',
    'Mail List',
    'New',
    'Save',
    'Show',
    'Title'
];

FwLanguages.cultures.neutral = {};
// needs closure wrapper since in javascript declaring a var in for loop goes into the outer scope, which is window in this case
(function() {
    for (var i = 0; i < FwLanguages.captions.length; i++) {
        FwLanguages.cultures.neutral[FwLanguages.captions[i]] = FwLanguages.captions[i];
    }
})();
FwLanguages.cultures.en = {

};

FwLanguages.cultures.enUs = {

};

FwLanguages.cultures.es = {

};

FwLanguages.cultures.en   = jQuery.extend({}, FwLanguages.cultures.neutral, FwLanguages.cultures.en);
FwLanguages.cultures.enUs = jQuery.extend({}, FwLanguages.cultures.en,      FwLanguages.cultures.enUs);
FwLanguages.cultures.es   = jQuery.extend({}, FwLanguages.cultures.neutral, FwLanguages.cultures.es);
