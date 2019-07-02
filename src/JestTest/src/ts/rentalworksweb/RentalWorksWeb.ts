// require('dotenv').config()
// import { TestUtils } from './shared/TestUtils';
// import { Quote } from './RentalWorks/Quote';
// import { Order } from './RentalWorks/Order';
// import { Customer } from './RentalWorks/Customer';
// import { Deal } from './RentalWorks/Deal';

// //import faker from 'faker';
// //import puppeteer from 'puppeteer';
// //import { resolve } from 'path';

// // IN ORDER TO RUN THIS TEST, add  '.test' before .ts in the file name


// try {

//     const BASE_URL = 'http://localhost/rentalworksweb';
//     if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
//     if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';

//     // const lead = {
//     //     name: faker.name.firstName(),
//     //     email: faker.internet.email(),
//     //     phone: faker.phone.phoneNumber(),
//     //     message: faker.random.words()
//     // };

//     // let page: puppeteer.Page;
//     // let browser: puppeteer.Browser;
//     // const width = 1920;
//     // const height = 1080;

//     // //jest.setTimeout(30000);
//     // process.on('uncaughtException', function (err) {
//     //     fail('Uncaught exception in async code: ' + err + '\n' + err.stack);
//     // });

//     // beforeAll(async () => {
//     //     browser = await puppeteer.launch({
//     //         headless: false,
//     //         //slowMo: 80,
//     //         args: [`--window-size=${width},${height}`]
//     //     });
//     //     page = await browser.newPage();
//     //     await page.setViewport({ width, height });
//     //     // await page.goto(BASE_URL);
//     //     // await page.waitForNavigation();
//     // });
//     // afterAll(() => {
//     //     //browser.close();
//     // });

//     describe('Authenticate', () => {
//         test('Login', async () => {
//             await page.goto(`${BASE_URL}/#/login`);
//             await page.waitForNavigation();
//             await page.click('.btnLogin');
//             await page.waitForSelector('#email', {visible:true});
//             await page.type('#email', <string>process.env.RW_EMAIL);
//             await page.click('#password');
//             await page.type('#password', <string>process.env.RW_PASSWORD);
//             await page.click('.btnLogin');
//             await TestUtils.sleepAsync(3000);
//             await page.waitForSelector('.appmenu', { visible:true, timeout:20000 });
//         }, 45000);
//     });

//     describe('Quote', () => {
//         let module: Quote;
//         beforeAll(() => {
//             module = new Quote('Quote', '#btnModule4D785844-BE8A-4C00-B1FA-2AA5B05183E5');
//         });
//         test('Open module', async() => {
//             await module.openModule();
//         }, 10000);
//         test('Open record', async () => {
//             await module.openRecord(1);
//         }, 45000);
//     });

//     describe('Order', () => {
//         let module: Order;
//         beforeAll(() => {
//             module = new Order('Order', '#btnModule64C46F51-5E00-48FA-94B6-FC4EF53FEA20');
//         });
//         test('Open module', async() => {
//             await module.openModule();
//         }, 10000);
//         test('Open record', async () => {
//             await module.openRecord(1);
//         }, 45000);
//     });

//     describe('Customer', () => {
//         let module: Customer;
//         beforeAll(() => {
//             module = new Customer('Customer', '#btnModule214C6242-AA91-4498-A4CC-E0F3DCCCE71E');
//         });
//         test('Open module', async() => {
//             await module.openModule();
//         }, 10000);
//         test('Open record', async () => {
//             await module.openRecord(1);
//         }, 45000);
//     });
//     describe('Deal', () => {
//         let module: Deal;
//         beforeAll(() => {
//             module = new Deal('Deal', '#btnModuleC67AD425-5273-4F80-A452-146B2008B41C');
//         });
//         test('Open module', async() => {
//             await module.openModule();
//         }, 10000);
//         test('Open record', async () => {
//             await module.openRecord(1);
//         }, 45000);
//     });

//     describe('Logoff', () => {
//         test('Logoff', async () => {
//             await page.goto(`${BASE_URL}/#/logoff`);
//             await page.waitForNavigation();
//             await page.waitForSelector('.btnLogin');
//         });
//     });

// } catch (ex) {
//     console.error(ex);
// }