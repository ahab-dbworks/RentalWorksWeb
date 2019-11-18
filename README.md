# RentalWorks  [![Build Status](http://jenkins.dbworks.com:8080/buildStatus/icon?job=RentalWorksWeb-BuildDevelop)](http://jenkins.dbworks.com:8080/job/RentalWorksWeb-BuildDevelop/)

Complete Inventory Tracking & Asset Management Software

Learn more about: [RentalWorks](http://www.dbworks.com/products/rentalworks)

## License
RentalWorksWeb is copyright by Database Works, Inc.  All rights reserved.  You are not permitted to redistribute this source code or compiled binaries without permission from Database Works.

## Overview

The RentalWorksWeb repository consists of the following projects:

1. Fw.Json - The shared framework (The framework has it' own repository so changes need to be made there and then synced to this project with a script)
2. RentalWorksAPI - REST API for RentalWorks
3. RentalWorksQuikScan - iOS/Android web app
4. RentalWorksTest - Integration Tests
5. RentalWorksWeb - Responsive Web App

## Getting started

### Install Visual Studio 2017
- Install Visual Studio 2017 Enterprise.
- At minimum you will need to be able to compile for ASP.NET and for Windows Forms and Console apps.
- After installed, troubleshoot compatibility on the devenv.exe and tell it to always run with administrative rights to give permission for IIS Management.

### Install Git
- https://git-scm.com/)
- Setup your SSH keys for github https://help.github.com/articles/adding-a-new-ssh-key-to-your-github-account/

### Clone RentalWorksWeb Repository
- Open Windows Explorer and go to the parent directory where you want to create the "RentalWorksWeb" directory
- Right-Click and "Git Bash Here"
- git clone git@github.com:DatabaseWorks/RentalWorksWeb.git

### Visual Studio Configuration
1. Make the Solution configuration field wide enough to read
    - Go to Tools > Customize
    - Commands Tab
    - Toolbar: Standard
    - Select the item "Solution Configurations" and Modify Selection
    - Width needs to be wider so make it 180 or whatever you like
2. Set indenting for HTML files to 2 spaces

### Create Virtual Directories and set Application.config files to dev database
- run the powershell script set_devemode.ps1.  If it doesn't work you will need to run the second version of the script from an administrative powershell command prompt or manually do what the script does.

## Developing HTML reports
1. Go to the WebApi project and create a new folder in WebpackReports/src for your report.  This is going to hold the source code for our report.  The final report will be rendered to wwwroot/Reports.
2. Copy the web pages files: index.html, index.ts, and index.scss from another report and make any necessary edits.
3. In the root of the repository run "npm run build-dev-reports".  This will do a 1 time build of the reports into wwwroot/Reports.  There are other npm scripts that do folder watching/syncing or even the full webpack dev server with hotmodule reloading.
4. Now in your web browser, go to the url of the report in the wwwroot folder.  It should look something like: http://localhost:57949/Reports/OutContractReport/index.html.  You can also use the webpack dev server by running npm run start-reports and then the url will be like: http://localhost:9000/OutContractReport/.
5. Open another browser tab and login to RentalWorks Web: http://localhost:57949.  Copy the apitoken by running the following command in the JavaScript console: sessionStorage.getItem('apiToken')
6. Go back to the report web page and open the JavaScript console and run your report render command and pass in the apitoken and the parameters.  In the future, Chrome will remember anything you typed in the debug console, so hit the up arrow key if you've done this before and see if it rememebered it.  If not the command will looksomething like: report.renderReport('http://localhost:57949', 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJtaWtlLnVzZXIiLCJqdGkiOiIzZmNlNTc2MC0xNDNmLTQyM2QtYjAwNC1kNDgzZjZkMzZkZmEiLCJpYXQiOjE1Mjg0MjY0NTEsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtaWtlLnVzZXIiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy93ZWJ1c2Vyc2lkIjoiQTAwMEszUEgiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy91c2Vyc2lkIjoiQTAwMEszUEYiLCJodHRwOi8vd3d3LmRid29ya3MuY29tL2NsYWltcy9ncm91cHNpZCI6IjAwMDAwMDBCIiwiaHR0cDovL3d3dy5kYndvcmtzLmNvbS9jbGFpbXMvdXNlcnR5cGUiOiJVU0VSIiwibmJmIjoxNTI4NDI2NDUxLCJleHAiOjE3ODc2MjY0NTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTc5NDkiLCJhdWQiOiIqIn0.GJ1r6t3tYMZH8R-FOjKRR1l4A6YrQmcxEYImY-xA5n4', {contractid:'000012L8'})

## Validations

### Overriding a Validation's apiurl
If you want the validation to call a different apiurl than what is set in the validation's Browse.htm, you can either add a data-apiurl attribute on the validation tag in the module's Form.htm file or you can declare an anonymous method that returns the apiurl in the Module.ts file in the openForm method as shown below.  Having a method is useful if you need to customize the url at runtime. 
~~~~
FwFormField.getDataField($form, 'OfficeLocationId').data('getapiurl', () => 'api/v1/customer/validations/officelocations');
~~~~

## RentalWorks Color Codes
- Purple: `#6a3aaf;`

## 2019.1.1 stable branch