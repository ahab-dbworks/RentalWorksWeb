echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run many concurrent automated tests on RentalWorksWeb
rem Indicate the number of concurrent tests as a paramter to this batch file execution
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
set numberoftests=1
if not "%~1"=="" set numberoftests=%~1

IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B
IF "%DwRegressionTestEmail%"=="" ECHO Warning: Environment Variable DwRegressionTestEmail is not defined.  Results will not be emailed.

for /L %%i in (1,1,%numberoftests%) do (
   echo %%i
   echo Running test number %%i
   start %DwRentalWorksWebPath%\src\JestTest\runRegressionTests %%i
   timeout 5
)
