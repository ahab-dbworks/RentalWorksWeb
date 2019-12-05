echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run a series of automated tests on RentalWorksWeb
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 12/05/2019
rem --------------------------------------------------------------------------
rem
rem
rem do not supply a trailing backslash.  do not surround path in double-quotes
set webinstallpath=C:\inetpub\wwwroot\rentalworkswebqa
rem
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
set testnumber=0
if not "%~1"=="" set testnumber=%~1

IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B
IF "%DwRegressionTestEmail%"=="" ECHO Warning: Environment Variable DwRegressionTestEmail is not defined.  Results will not be emailed.

IF "%webinstallpath%"=="" ECHO Web install path is not defined
IF "%webinstallpath%"=="" set /p=Hit ENTER to exit
IF "%webinstallpath%"=="" exit /B


set testrootpath=%DwRentalWorksWebPath%\src\JestTest
cd %testrootpath%
if not exist %testrootpath%\output\ (md output)
set testpath=%testrootpath%\output
cd %testpath%
if not exist %testrootpath%\%testnumber%\ (md %testnumber%)
set testpath=%testpath%\%testnumber%
cd %testrootpath%

call :getversion
call :runtest "LoginLogout" "Login Logout"
call :runtest "RwwShallowRegression" "Shallow Regression"
call :runtest "RwwMediumRegression" "Medium Regression"
call :runtest "RwwInventoryIntegrity" "Inventory Integrity"
call :runtest "RwwRunReports" "Reports"

set "file=%testrootpath%\jest.rentalworksweb%testnumber%.config.js"
if exist %file% (del %file%)
cd %testrootpath%
exit
rem exit /b

:runtest
set testname=%1
set friendlyname=%2
set testname=%testname:"=%
set friendlyname=%friendlyname:"=%
if not exist %testpath%\rwwlogo.png (copy rwwlogo.png %testpath%\rwwlogo.png)
if exist %testrootpath%\jest.rentalworksweb%testnumber%.config.js (del %testrootpath%\jest.rentalworksweb%testnumber%.config.js)
setlocal ENABLEDELAYEDEXPANSION
set newOutputPathLine="outputPath": "output/%testnumber%/test-report.html",
set newPageTitleLine="pageTitle": "%friendlyname% Test Report %version%",
set "file=%testrootpath%\jest.rentalworksweb.config.js"
set count=0
for /F "delims=" %%a in (%file%) do (
    set /A count+=1
    set "array[!count!]=%%a"
)
set "file=%testrootpath%\jest.rentalworksweb%testnumber%.config.js"
if exist %file% (del %file%)
setlocal ENABLEDELAYEDEXPANSION
for /L %%i in (1,1,%count%) do (
   set newline=!array[%%i]!
   echo !newline!|find "outputPath" >nul
   if not errorlevel 1 (set newline=!newOutputPathLine!)
   echo !newline!|find "pageTitle" >nul
   if not errorlevel 1 (set newline=!newPageTitleLine!)
   call echo !newline!>>%file%
   )
set htmlFileName="%testpath%\test-report.html"
set pdfFileName="%testpath%\RentalWorks %friendlyname% Test Report (%version%).pdf"
set emailSubject="RentalWorks %friendlyname% Results (%version%)"
if exist %htmlFileName% (del %htmlFileName%)
if exist %pdfFileName% (del %pdfFileName%)
call jest --config=jest.rentalworksweb%testnumber%.config.js "%testname%"
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%pdfFileName% %htmlFileName%
if exist %htmlFileName% (del %htmlFileName%)
IF "%DwRegressionTestEmail%"=="" start %pdfFileName%
IF not "%DwRegressionTestEmail%"=="" call powershell.exe -file %DwRentalWorksWebPath%\src\JestTest\emailtestresults.ps1 -subject %emailSubject% -attachment %pdfFileName%
exit /b


:getversion
set "file=%webinstallpath%\version.txt"
for /F "delims=" %%a in (%file%) do (
    set version=%%a
)
echo Testing Version %version%
exit /b

