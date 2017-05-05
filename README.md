# RentalWorks  [![Build Status](http://jenkins.dbworks.com:8080/buildStatus/icon?job=RentalWorksWeb-Develop)](http://jenkins.dbworks.com:8080/job/RentalWorksWeb-Develop/)

Complete Inventory Tracking & Asset Management Software

Learn more about: [RentalWorks](http://www.dbworks.com/products/rentalworks)

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
- run the powershell script set_devemode.ps1.  If it doesn't work you will need to run the second version of the script from an administrative powershell command prompt or manually do what the script does.

## License
RentalWorksWeb is copyright by Database Works, Inc.  All rights reserved.  You are not permitted to redistribute this source code or compiled binaries without permission from Database Works.
