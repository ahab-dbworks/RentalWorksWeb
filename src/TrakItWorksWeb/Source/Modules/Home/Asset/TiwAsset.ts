class TiwAsset extends RwAsset {
    browseModel: any = {};

    getBrowseTemplate(): void {
        //let template = super.getBrowseTemplate();
        //return template;
    }

}

(<any>window).AssetController = new TiwAsset();