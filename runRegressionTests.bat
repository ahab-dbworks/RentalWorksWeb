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

if exist test-report.html (del test-report.html)
if exist test-report-RwwShallowRegression.html (del test-report-RwwShallowRegression.html)
call npm run test-rentalworksweb -t RwwShallowRegression
ren test-report.html test-report-RwwShallowRegression.html
start test-report-RwwShallowRegression.html

if exist test-report.html (del test-report.html)
if exist test-report-RwwFullRegression.html (del test-report-RwwFullRegression.html)
call npm run test-rentalworksweb -t RwwFullRegression
ren test-report.html test-report-RwwFullRegression.html
start test-report-RwwFullRegression.html
