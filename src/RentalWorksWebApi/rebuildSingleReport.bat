echo off
set /p reportname="Type the name of the Report to watch: "
call npm run watch-reports-dev-byname %reportname%