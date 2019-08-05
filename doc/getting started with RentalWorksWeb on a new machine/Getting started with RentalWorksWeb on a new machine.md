# Getting Started developing RentalWorksWeb on a new machine
Updated 08/05/2019

## IIS
- Make Sure IIS is installed and configured with this option:
- Control Panel --> Turn Windows Features on or off 

   Internet Information Services --> World Wide Web Services --> Application Development Features --> ASP.NET 4.7

## GitKraken

## RentalWorksWeb repo
- Use GitKraken to clone the RentalWorksWeb Repo

## Visual Studio
- Install Visual Studio - use Key provided by Matt
- Navigate to %systemroot%\inetsrv\config and grant permissions to current user (one time)

   this allows Visual Studio to access IIS 
- Open the RentalWorksWeb solution.

   If prompted, downgrade the .NET Framework to whatever it suggests.
- Click on a Web project, and choose Properties.
- Click the down-arrow on .Net Target Framework and click "Install other Frameworks..."
- Select the correct framework from the Microsoft website (currently 4.7). Developer Pack
- Close Visual Studio and Install the latest .Net Framework (above)
- Reset any modified Project or Config files
- setup permissions for Visual Studio to Run as Administrator

   Right-click on the <VisualStudio>.exe file

   select Troubleshoot compatibility

   check the box for "this program requires additional permissions" (or similar)

   Go through the wizard
- Open the RentalWorksWeb solution
- Build Solution
- Create a new appsetting.json file in the root of the API project. Copy the sample and update the ConnectionString and QueryTimeout parameters
```
"ConnectionString": "Server=(local)\\sqlXXX;Database=rentalworksXXX;User Id=dbworks;Password=db2424;",
"QueryTimeout":  300
```
- Create a new Application.config file in the root of the Web project.  Copy the sample and update the Server and Database values in the XML
```
<Server>(local)\sqlXXX</Server> 
<Database>rentalworksXXX</Database>
```
- Create a new ApplicationConfig.js file in in the root of the Web project.  Copy the sample file
