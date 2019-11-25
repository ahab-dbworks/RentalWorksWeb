routes.push({ pattern: /^module\/transferin$/, action: function (match: RegExpExecArray) { return TransferInController.getModuleScreen(); } });

class TransferIn extends CheckIn{
    Module: string = 'TransferIn';
    caption: string = Constants.Modules.Home.TransferIn.caption;
	nav: string = Constants.Modules.Home.TransferIn.nav;
	id: string = Constants.Modules.Home.TransferIn.id;
}
var TransferInController = new TransferIn();