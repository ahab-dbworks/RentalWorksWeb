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
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B

cd %DwRentalWorksWebPath%\src\JestTest

if exist test-report.html (del test-report.html)
if exist RentalWorksWeb_TestReport_LoginLogout.pdf (del RentalWorksWeb_TestReport_LoginLogout.pdf)
call npm run test-rentalworksweb -t LoginLogout
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%DwRentalWorksWebPath%\build\RentalWorksWeb_TestReport_LoginLogout.pdf %DwRentalWorksWebPath%\src\JestTest\test-report.html
start RentalWorksWeb_TestReport_LoginLogout.pdf
if exist test-report.html (del test-report.html)

if exist test-report.html (del test-report.html)
if exist RentalWorksWeb_TestReport_ShallowRegression.pdf (del RentalWorksWeb_TestReport_ShallowRegression.pdf)
call npm run test-rentalworksweb -t RwwShallowRegression
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%DwRentalWorksWebPath%\build\RentalWorksWeb_TestReport_ShallowRegression.pdf %DwRentalWorksWebPath%\src\JestTest\test-report.html
start RentalWorksWeb_TestReport_ShallowRegression.pdf
if exist test-report.html (del test-report.html)

if exist test-report.html (del test-report.html)
if exist RentalWorksWeb_TestReport_MediumRegression.pdf (del RentalWorksWeb_TestReport_MediumRegression.pdf)
call npm run test-rentalworksweb -t RwwMediumRegression
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%DwRentalWorksWebPath%\build\RentalWorksWeb_TestReport_MediumRegression.pdf %DwRentalWorksWebPath%\src\JestTest\test-report.html
start RentalWorksWeb_TestReport_MediumRegression.pdf
if exist test-report.html (del test-report.html)
