cd /d %~dp0
QuartzScheduler install
sc config "RentalWorksQuartzServer"  start= auto
sc failure "RentalWorksQuartzServer"  actions= restart/180000/restart/180000/restart/180000 reset= 86400 
net start RentalWorksQuartzServer
pause