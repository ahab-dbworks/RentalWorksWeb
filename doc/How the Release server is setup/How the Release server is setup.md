# How Release01 server is setup

- download and Install SQL Server 2019 Developer edition
- Set SQL Server Agent service to Automatic delayed startup
- download and Install SSMS
- change security settings to "SQL Server and Windows Authentication mode"
- create "dbworks" SA account
- restore database "rentalworks_qa2019" (copied from SQLDEMO03..rentalworks_demo2019)
- exec "refreshjobs" on the new database
- exec fw_enablecmdshell
- create the "rentalworksweb" server Login and database user, fw_grantall
- download and install 7-Zip
- Control Panel - add Windows Components - Install IIS
- Control Panel - add Windows Components - Install IIS Core Hosting
- Install dot-net-core (download from FTP\update\rentalworksweb\2018
- download the latest RentalWorksWeb release zip file to c:\temp
- Extract the zip file
- Copy the RentalWorksWeb and RentalWorksWebApi directories to c:\inetpub\wwwroot\ 

### Set the API configuration settings
- Navigate to c:\inetpub\wwwroot\rentalworkswebapi\ and rename appsettings.sample.json to appsettings.json
- Open appsettings.json, and make these edits:
 - ConnectionString: set to local database
 - ApplicationConfig.JwtIssuerOptions.Issuer, set to "*"
 - ApplicationConfig.JwtIssuerOptions.Authority, set to "*"

### Set the Web configuration settings
- Navigate to c:\inetpub\wwwroot\rentalworksweb\ and rename ApplicationConfig.sample.js to ApplicationConfig.js
- Open appsettings.json, and make these edits:
 - set the apiurl to 'http://localhost/rentalworkswebapi/'    (note, the backslash is required at the end)
 - Add line for applicationConfig.allCaps = true;

### Configure IIS:
- create new Application Pool for api "rentalworkswebapi", No Managed Code, Start immediately
- create new Application Pool for api "rentalworksweb", .NET CLR Version xxx, Start immediately
- Navigate to Sites - Default Web Site - RentalWorksWeb, right-click "Convert to Application", Select Application Pool as "rentalworksweb", Enable Preload = true
- Navigate to Sites - Default Web Site - RentalWorksWebApi, right-click "Convert to Application", Select Application Pool as "rentalworkswebapi", Enable Preload = true
- Click on the main server name in the tree, then double-click Feature Delegation, then click "Custom Site Delegation".
 - For the "Default Web Site", change all "Read Only" values to "Read/Write"

- Test RentalWorksWeb in Chrome 
- Create firewall rules to allow traffic on port 21 (inbound and outbound)
- Create firewall rules to allow application ftp.exe (inbound and outbound)

### The specs below need more work.  need to automate the maintenance of these test scripts on remote server
- Copy JestTest files to c:\regression\src\jesttest
- Modify "runRegressionTests.bat" - change webinstallpath value
- Create desktop shortcut for "runRegressionTests.bat"
- Install NPM 

### install Puppeteer and Jest
- Open command prompt and navigate to c:\regression\jesttest
- run:
 - npm i puppeteer
 - npm install --save-dev -g jest
 - npm i jest-html-reporter
 - npm i -g md-to-pdf

### set system environment variables	
- Open System Environment variables
 - DwRentalWorksWebPath (C:\Regression)
 - DwRegressionTestEmail (justin@dbworks.com)  -- not working currently

### Run Regression!