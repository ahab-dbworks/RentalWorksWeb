class GeneratorMakeModelGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'GeneratorMakeModelGrid';
        this.apiurl = 'api/v1/generatormodel';
    }
}

(<any>window).GeneratorMakeModelGridController = new GeneratorMakeModelGrid();
//----------------------------------------------------------------------------------------------