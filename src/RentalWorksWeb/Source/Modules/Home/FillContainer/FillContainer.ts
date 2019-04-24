routes.push({ pattern: /^module\/fillcontainer$/, action: function (match: RegExpExecArray) { return FillContainerController.getModuleScreen(); } });
class FillContainer extends StagingCheckoutBase{
    Module: string = 'FillContainer';
    caption: string = 'Fill Container';
    nav: string = 'module/fillcontainer';
    id: string = '0F1050FB-48DF-41D7-A969-37300B81B7B5';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'ContainerItem';
}
var FillContainerController = new FillContainer();