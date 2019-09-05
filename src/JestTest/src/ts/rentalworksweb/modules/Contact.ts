import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Contact extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'Contact';
        this.moduleBtnId = '#btnModule3F803517-618A-41C0-9F0B-2C96B8BDAFC4';
        this.moduleCaption = 'Contact';
    }

    //async populateNewContact(email?:string): Promise<void> {
    //    await page.waitForSelector('.fwformfield[data-datafield="FirstName"]', {visible:true});
    //    await this.populateTextField("FirstName", `JEST - ${faker.name.firstName()}`);
    //    await this.populateTextField("LastName", faker.name.lastName());
    //    await this.populateTextField("Email", email != undefined ? email : faker.internet.email());
    //}

    //async populateNewContactWithoutEmail(): Promise<void> {
    //    await page.waitForSelector('.fwformfield[data-datafield="FirstName"]', { visible: true });
    //    await this.populateTextField("FirstName", `JEST - ${faker.name.firstName()}`);
    //    await this.populateTextField("LastName", faker.name.lastName());
    //}

    //async populateNewContactDuplicateEmail(): Promise<string> {
    //    await page.waitForSelector('.fwformfield[data-datafield="FirstName"]', { visible: true });
    //    await this.populateTextField("FirstName", `JEST - ${faker.name.firstName()}`);
    //    await this.populateTextField("LastName", faker.name.lastName());
    //    const date = new Date();
    //    const hours = date.getHours();
    //    const minutes = date.getMinutes();
    //    const seconds = date.getSeconds();
    //    const dateTime = `${date.toLocaleDateString().replace(/\//g, '')}${hours}${minutes}${seconds}`;

    //    const email = `email${dateTime}@email.com`;
    //    await this.populateTextField("Email", email);
    //    console.log(email, "popnewemail")
    //    return email;
    //}

}