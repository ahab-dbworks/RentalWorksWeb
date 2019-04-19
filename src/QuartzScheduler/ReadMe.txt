GateWorks Quartz Scheduler
-----------------------------------------------------------------------
This is a Windows service which is based on Quartz.NET for scheduling jobs to run at specific times.
-----------------------------------------------------------------------
Installation:

Copy the software to a directory on the server such as C:\RentalWorks\QuartzScheduler
-----------------------------------------------------------------------
Open QuartzScheduler.exe.config in a text editor:

Enable Verbose Logging: When intially configuring the system set verboseLogging to true.
  <appSettings>
    <add key="verboseLogging" value="true" />
  </appSettings>

Edit the SQL Connection Strings:
For more info see: https://www.connectionstrings.com/sql-server/

To login with an SQL server user account:
<connectionStrings>
    <add name="gateworks" connectionString="Server=sql01.yourdomain.com;Database=gateworks;User Id=dbworks;Password=dbworksaccountpassword;"/>
</connectionStrings>

You can also do active directory logins into the database using the service process account:
<connectionStrings>
    <add name="gateworks" connectionString="Server=sql01.yourdomain.com;Database=gateworks;Trusted_Connection=True;"/>
</connectionStrings>

You may need to give the SQL login write access to the database.
-----------------------------------------------------------------------
Configuring jobs:

Copy the file quartz_jobs.sample.xml to quartz_jobs.xml
Edit the file quartz_jobs.xml in a text editor.

Jobs Settings:
  For AmagIntegrationJob:
- verboseLogging {true/false} - This will cause it to log everything it does including all the queries

Trigger settings:
  For AmagIntegrationJob:
- cron-expression - This expression controls when the jobs fires.  See: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html
-----------------------------------------------------------------------
Debugging:

Double-click the QuartzScheduler.exe and view the output in the command window.
-----------------------------------------------------------------------
Install as a Windows Service
Right-Click on _InstallService.bat and Run As Administrator

In the script it will configure the service to auto start with Windows and also to automatically restart the service if it fails.

If you want to change the account the service runs under, you can add the following line to the _InstallService.bat file next to the other sc config lines
sc config "Service1" obj= mydomain\myuser password= mypassword
or you could also change it after it gets installed by manually editing it through the Windows Service Manager.
-----------------------------------------------------------------------
Uninstall as a Windows Service
Right-Click on _UninstallService.bat and Run As Administrator
-----------------------------------------------------------------------
Email Notifications on Errors

If you want to receive emails when an error occurs, edit app.config and configure the SmtpAppender.  The component that does this
is called log4net and you can read more about this below.
Examples: https://logging.apache.org/log4net/release/config-examples.html
Reference: https://logging.apache.org/log4net/release/sdk/html/T_log4net_Appender_SmtpAppender.htm

