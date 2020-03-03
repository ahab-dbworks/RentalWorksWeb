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
if not exist %testnumber%\ (md %testnumber%)
set testpath=%testpath%\%testnumber%
cd %testrootpath%

call :getversion
if %testnumber%==0  (call :runtest "LoginLogout" "Login_Logout")
if %testnumber%==1  (call :runtest "LoginLogout" "Login_Logout")
if %testnumber%==0  (call :runtest "RwwShallowRegression" "Shallow_Regression")
if %testnumber%==2  (call :runtest "RwwShallowRegression" "Shallow_Regression")
if %testnumber%==0  (call :runtest "RwwMediumRegressionHome" "Medium_Regression_Home")
if %testnumber%==3  (call :runtest "RwwMediumRegressionHome" "Medium_Regression_Home")
if %testnumber%==0  (call :runtest "RwwMediumRegressionAdmin" "Medium_Regression_Administrator")
if %testnumber%==4  (call :runtest "RwwMediumRegressionAdmin" "Medium_Regression_Administrator")
if %testnumber%==0  (call :runtest "RwwMediumRegressionSettings01" "Medium_Regression_Settings_01")
if %testnumber%==5  (call :runtest "RwwMediumRegressionSettings01" "Medium_Regression_Settings_01")
if %testnumber%==0  (call :runtest "RwwMediumRegressionSettings02" "Medium_Regression_Settings_02")
if %testnumber%==6  (call :runtest "RwwMediumRegressionSettings02" "Medium_Regression_Settings_02")
if %testnumber%==0  (call :runtest "RwwMediumRegressionSettings03" "Medium_Regression_Settings_03")
if %testnumber%==7  (call :runtest "RwwMediumRegressionSettings03" "Medium_Regression_Settings_03")
if %testnumber%==0  (call :runtest "RwwMediumRegressionSettings04" "Medium_Regression_Settings_04")
if %testnumber%==8  (call :runtest "RwwMediumRegressionSettings04" "Medium_Regression_Settings_04")
if %testnumber%==0  (call :runtest "RwwMediumRegressionSettings05" "Medium_Regression_Settings_05")
if %testnumber%==9  (call :runtest "RwwMediumRegressionSettings05" "Medium_Regression_Settings_05")
if %testnumber%==0  (call :runtest "RwwInventoryIntegrity" "Inventory_Integrity")
if %testnumber%==10 (call :runtest "RwwInventoryIntegrity" "Inventory_Integrity")
if %testnumber%==0  (call :runtest "RwwTransfers" "Transferring_Inventory")
if %testnumber%==11 (call :runtest "RwwTransfers" "Transferring_Inventory")
if %testnumber%==0  (call :runtest "RwwRunReports" "Reports")
if %testnumber%==12 (call :runtest "RwwRunReports" "Reports")

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
set friendlyname=%friendlyname:_= %
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
set pdfFileNameOnly=RentalWorks %friendlyname% Test Report (%version%).pdf
set pdfFileName="%testpath%\%pdfFileNameOnly%"
set emailSubject="RentalWorks %friendlyname% Results (%version%)"
if exist %htmlFileName% (del %htmlFileName%)
if exist %pdfFileName% (del %pdfFileName%)
call jest --config=jest.rentalworksweb%testnumber%.config.js "%testname%"
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%pdfFileName% %htmlFileName%
copy %pdfFileName% %testrootpath%\output
if exist %htmlFileName% (del %htmlFileName%)
rem IF "%DwRegressionTestEmail%"=="" start "%pdfFileName%"
IF not "%DwRegressionTestEmail%"=="" call powershell.exe -file %DwRentalWorksWebPath%\src\JestTest\emailtestresults.ps1 -subject %emailSubject% -attachment %pdfFileName%
if exist "%testrootpath%\output\%pdfFileNameOnly%" rmdir %testrootpath%\output\%testnumber% /S /Q
exit /b


:getversion
set "file=%webinstallpath%\version.txt"
for /F "delims=" %%a in (%file%) do (
    set version=%%a
)
echo Testing Version %version%
exit /b

