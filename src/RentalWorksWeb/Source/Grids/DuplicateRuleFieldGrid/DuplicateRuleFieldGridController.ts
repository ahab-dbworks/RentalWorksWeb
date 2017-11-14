class DuplicateRuleFieldGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DuplicateRuleFieldGrid';
        this.apiurl = 'api/v1/duplicaterulefield';
    }
}

(<any>window).DuplicateRuleFieldGridController = new DuplicateRuleFieldGrid();
//----------------------------------------------------------------------------------------------