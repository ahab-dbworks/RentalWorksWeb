echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run a series of automated tests on RentalWorksWeb
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 10/15/2019
rem --------------------------------------------------------------------------
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

set testrootpath=%DwRentalWorksWebPath%\src\JestTest
cd %testrootpath%
if not exist %testrootpath%\output\ (md output)
set testpath=%testrootpath%\output
cd %testpath%
if not exist %testrootpath%\%testnumber%\ (md %testnumber%)
set testpath=%testpath%\%testnumber%
cd %testrootpath%

if not exist %testpath%\rwwlogo.png (copy rwwlogo.png %testpath%\rwwlogo.png)
if exist %testrootpath%\jest.rentalworksweb%testnumber%.config.js (del %testrootpath%\jest.rentalworksweb%testnumber%.config.js)
setlocal ENABLEDELAYEDEXPANSION
set newOutputPathLine="outputPath": "output/%testnumber%/test-report.html",
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
   echo !array[%%i]!|find "outputPath" >nul
   if errorlevel 1 (echo !array[%%i]!>>%file%) else (call echo !newOutputPathLine!>>%file%))

if exist %testpath%\test-report.html (del %testpath%\test-report.html)
if exist %testpath%\RentalWorksWeb_TestReport_LoginLogout.pdf (del %testpath%\RentalWorksWeb_TestReport_LoginLogout.pdf)
call jest --config=jest.rentalworksweb%testnumber%.config.js "LoginLogout"
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%testpath%\RentalWorksWeb_TestReport_LoginLogout.pdf %testpath%\test-report.html
if exist %testpath%\test-report.html (del %testpath%\test-report.html)
IF "%DwRegressionTestEmail%"=="" start %testpath%\RentalWorksWeb_TestReport_LoginLogout.pdf
IF not "%DwRegressionTestEmail%"=="" call powershell.exe -file %DwRentalWorksWebPath%\src\JestTest\emailtestresults.ps1 -subject "LoginLogout Regression Test Results" -attachment %testpath%\RentalWorksWeb_TestReport_LoginLogout.pdf

if exist %testpath%\test-report.html (del %testpath%\test-report.html)
if exist %testpath%\RentalWorksWeb_TestReport_ShallowRegression.pdf (del %testpath%\RentalWorksWeb_TestReport_ShallowRegression.pdf)
call jest --config=jest.rentalworksweb%testnumber%.config.js "RwwShallowRegression"
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%testpath%\RentalWorksWeb_TestReport_ShallowRegression.pdf %testpath%\test-report.html
if exist %testpath%\test-report.html (del %testpath%\test-report.html)
IF "%DwRegressionTestEmail%"=="" start %testpath%\RentalWorksWeb_TestReport_ShallowRegression.pdf
IF not "%DwRegressionTestEmail%"=="" call powershell.exe -file %DwRentalWorksWebPath%\src\JestTest\emailtestresults.ps1 -subject "Shallow Regression Test Results" -attachment %testpath%\RentalWorksWeb_TestReport_ShallowRegression.pdf

if exist %testpath%\test-report.html (del %testpath%\test-report.html)
if exist %testpath%\RentalWorksWeb_TestReport_MediumRegression.pdf (del %testpath%\RentalWorksWeb_TestReport_MediumRegression.pdf)
call jest --config=jest.rentalworksweb%testnumber%.config.js "RwwMediumRegression"
"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --headless --disable-gpu --print-to-pdf=%testpath%\RentalWorksWeb_TestReport_MediumRegression.pdf %testpath%\test-report.html
if exist %testpath%\test-report.html (del %testpath%\test-report.html)
IF "%DwRegressionTestEmail%"=="" start %testpath%\RentalWorksWeb_TestReport_MediumRegression.pdf
IF not "%DwRegressionTestEmail%"=="" call powershell.exe -file %DwRentalWorksWebPath%\src\JestTest\emailtestresults.ps1 -subject "Medium Regression Test Results" -attachment %testpath%\RentalWorksWeb_TestReport_MediumRegression.pdf

set "file=%testrootpath%\jest.rentalworksweb%testnumber%.config.js"
if exist %file% (del %file%)
cd %testrootpath%
exit
