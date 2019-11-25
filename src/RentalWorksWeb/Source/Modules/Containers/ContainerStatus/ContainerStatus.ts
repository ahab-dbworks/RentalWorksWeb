routes.push({ pattern: /^module\/containerstatus$/, action: function (match: RegExpExecArray) { return ContainerStatusController.getModuleScreen(); } });

class ContainerStatus extends OrderStatusBase {
    Module:  string = 'ContainerStatus';
    apiurl:  string = 'api/v1/containerstatus'
    caption: string = Constants.Modules.Container.children.ContainerStatus.caption;
    nav:     string = Constants.Modules.Container.children.ContainerStatus.nav;
    id:      string = Constants.Modules.Container.children.ContainerStatus.id;
    Type:    string = 'ContainerItem';
    //----------------------------------------------------------------------------------------------
}
var ContainerStatusController = new ContainerStatus();