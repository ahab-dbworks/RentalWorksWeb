# How to Release RentalWorksWeb

Last Updated 10/07/2019

## Prerequisites
- npm i -g md-to-pdf
- npm install github-release-notes -g

## Build a Release
- GitKraken: confirm that you are on local\develop branch
- Stash any unstaged changes
- Navigate to the solution root directory: %DwRentalWorksWebPath%
- run the releaseRentalworksWeb.bat file

## Deployment Server info:
### 4Wall:
#### Production
- web server: 4WE-LV-RWWEB
- database: 4WE-LV-SQL11..rentalworks
- Web path: C:\inetpub\wwwroot\rentalworksproductionweb
- API path: C:\inetpub\wwwroot\rentalworksproductionwebapi
#### Training
- web server: 4WE-LV-RWWEB
- database: 4WE-LV-SQL14..rentalworkstraining
- Web path: C:\inetpub\wwwroot\rentalworkstrainingweb
- API path: C:\inetpub\wwwroot\rentalworkstrainingwebapi

### Database Works:
#### Demo
- web server: WEBDEMO01
- database: SQLDDEMO03..rentalworks_demo2019
- Web path: c:\inetpub\web2019demo.rentalworks.net
- API path: c:\inetpub\webapi2019demo.rentalworks.net


## Update Web and API:
- copy the upgradeRentalworksWeb.bat file to the desktop on the target web server
- Edit the .bat file by providing the web path, api path, and application pool name at the top.
- run the upgradeRentalworksWeb.bat file
