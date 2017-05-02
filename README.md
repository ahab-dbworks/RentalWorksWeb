# RentalWorks

Complete Inventory Tracking & Asset Management Software

Learn more about: [RentalWorks](http://www.dbworks.com/products/rentalworks)

## Overview

The RentalWorksWeb repository consists of the following projects:

1. Fw.Json
2. RentalWorksAPI
3. RentalWorksQuikScan
4. RentalWorksTest
5. RentalWorksWeb

## Getting started

### Install Visual Studio 2017
- Install Visual Studio 2017 Enterprise.
- At minimum you will need to be able to compile for ASP.NET and for Windows Forms and Console apps.
- After installed, troubleshoot compatibility on the devenv.exe and tell it to always run with administrative rights to give permission for IIS Management.

### Install Git
- https://git-scm.com/)

### Clone RentalWorksWeb Repository
- Open Windows Explorer and go to C:\project\RentalWorks or wherever you want to create the "RentalWorksWeb" diretory
- Right-Click and "Git Bash Here"
- git clone http://gitlab/DatabaseWorks/RentalWorksWeb.git

### Make the solution configuration field wider so you can read it
- Go to Tools > Customize
- Commands Tab
- Toolbar: Standard
- Select the item "Solution Configurations" and Modify Selection
- Width needs to be wider so make it 180 or whatever you like

### Create Virtual Directories and set Application.config files to dev database
- run the powershell script set_devemode.ps1

## License
RentalWorksWeb is copyright by Database Works, Inc.  All rights reserved.  You are not permitted to redistribute this source code or compiled binaries without permission from Database Works.
