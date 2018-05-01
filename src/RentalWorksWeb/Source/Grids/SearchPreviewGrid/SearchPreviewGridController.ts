﻿class SearchPreviewGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SearchPreviewGrid';
        this.apiurl = 'api/v1/inventorysearch/preview';
    }
}

var SearchPreviewGridController = new SearchPreviewGrid();
//----------------------------------------------------------------------------------------------