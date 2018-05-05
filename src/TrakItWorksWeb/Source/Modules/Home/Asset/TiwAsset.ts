class TiwAsset extends RwAsset {
    browseModel: any = {
        rentalWorksColumns: '',
        trakItWorksColumns: `
            <div class="column flexcolumn" data-width="0" data-visible="false">
                <div class="field" data-datafield="Inactive" data-browsedatatype="text"  data-visible="false"></div>
            </div>`
    };

    getBrowseTemplate(): string {
        let template = super.getBrowseTemplate();
        return template;
    }

}

(<any>window).AssetController = new TiwAsset();