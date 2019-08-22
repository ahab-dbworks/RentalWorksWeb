import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Contact extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'Contact';
        this.moduleBtnId = '#btnModule3F803517-618A-41C0-9F0B-2C96B8BDAFC4';
        this.moduleCaption = 'Contact';
    }

    async populateNewContact(): Promise<void> {
        await page.waitForSelector('.fwformfield[data-datafield="FirstName"]', {visible:true});
        await this.populateTextField("FirstName", `JEST - ${faker.name.firstName()}`);
        await this.populateTextField("LastName", faker.name.lastName());
        await this.populateTextField("Email", faker.internet.email());
    }

    async populateNewContactWithoutEmail(): Promise<void> {
        await page.waitForSelector('.fwformfield[data-datafield="FirstName"]', { visible: true });
        await this.populateTextField("FirstName", `JEST - ${faker.name.firstName()}`);
        await this.populateTextField("LastName", faker.name.lastName());
    }
}