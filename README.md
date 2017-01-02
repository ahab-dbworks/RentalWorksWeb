# RentalWorks

Complete Inventory Tracking & Asset Management Software

RentalWorks takes your rental operation to the next level. This powerful 
software combines state-of-the-art rental inventory tracking with secure and 
accurate accounting and purchasing features.

Learn more about: [RentalWorks](http://www.dbworks.com/products/rentalworks)

## Overview

The RentalWorksWeb repository consists of the following projects:

1. Fw.Json
2. RentalWorksAPI
3. RentalWorksMiddleTier
4. RentalWorksMiddleTier.Tests
5. RentalWorksQuikScan
6. RentalWorksQuikScanLibrary
7. RentalWorksWeb
8. RentalWorksWebLibrary

## Contributing to RentalWorksWeb development

Please read [`CONTRIBUTING.md`](CONTRIBUTING.md) for more details.

## Getting started

### Install Visual Studio 2015
- Install Visual Studio 2015 Enterprise.
- You can do the default install, but you should use the C# developer configuration to get the best keybindings

### Install Git
- [Easiest thing is using git for windows](https://git-scm.com/)
- other option is using Linux subsystem for windows, although I has issues with it, so more R&D is needed

###Clone RentalWorksWeb Repository
- Open Windows Explorer and got to C:\project\RentalWorks
- Right-Click and "Git Bash Here"
- git clone http://gitlab/DatabaseWorks/RentalWorksWeb.git

### Make IIS work without running as administrator
- In Windows Explorer browse to C:\Windows\System32\inetsrv\config 
- When you try to open the folder you will get a popup that says: "You don't have access to this folder - Click continue to permanently get access to this folder"
- Click 'continue' for this folder, and with the Export folder underneath. I changed the shortcut back to "Run as me" (a member of the domain and local administrators ) and was able to open and deploy the solution.

### Create a RentalWorksWeb Module

### Create a RentalWorksQuikScan Module]

## Using git

- [Working locally](doc/usinggit/workinglocally.md)
- [Pushing/pulling from the gitlab server](doc/usinggit/pushingpulling.md)

## License

RentalWorksWeb is a closed source project owned by Database Works, Inc.  All rights reserved.