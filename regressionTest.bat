echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run a series of automated tests on RentalWorksWeb
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 08/23/2019
rem --------------------------------------------------------------------------
rem
rem
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------

rem -- #jhtodo: probably should add some configs up here if we want to set the URL or override the values in the .env file


rem -- #jhtodo: not sure if we want to call each test individually like this or make one gigantic "regression test" script and just run once

rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------

IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" exit /B



cd %DwRentalWorksWebPath%\src\JestTest

call npm run test-rentalworksweb -t NewContact
ren test-report.html test-report-NewContact.html

call npm run test-rentalworksweb -t RentalAvailability
ren test-report.html test-report-RentalAvailability.html