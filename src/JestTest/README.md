# TestWorks

Testing library for RentalWorks and others, using Jest and Puppeteer

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

Node.js installed on the machine or partition you use to run RentalWorks

A seperate editor such as Visual Studio Code or Atom (I suppose you could also run another instance of Visual Studio)

### Installing

Clone this repo onto the machine or partition you use to run RentalWorks

From terminal or cmd, run 

```
npm i
```
Create a .env (named '.env') file in root directory with the values replaced with your actual login information or that of a test account

```
RW_EMAIL=youremail
RW_PASSWORD=yourpassword
```

## Running the tests

To run a test, you must have your Visual Studio development server running in the background.

A full list of scripts can be found in the package.json such as 
```
npm test
```
Run tests that match a string value such as tests containing 'vendor' in filename - Not case sensitive:
```
npm test -t vendor
```
## Built With

* [Jest](https://jestjs.io/en/) - Complete and ready to set-up JavaScript testing solution. 
* [Puppeteer](https://developers.google.com/web/tools/puppeteer/) - Provides a high-level API to control headless Chrome or Chromium
* [Winston](https://www.npmjs.com/package/winston) - A simple and universal logging library with support for multiple transports

## Authors

* **Mike Vermilion** - *Initial work* - [MikeVermilion](https://github.com/MikeVermilion)
* **Joshua Pace** - *Initial work* - [J.Pace](https://github.com/jcpace)