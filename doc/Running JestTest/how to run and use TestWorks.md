# Using TestWorks

Last Updated 08/12/2019

## Up and running in Visual Studio
### Ensure the Node.js Development Environment is installed
- Click on 'Tools' in the navigation, choose 'Get Tools and Features'. This will bring up the VS installer.
- Under the 'Workloads' tab, if Node.js development is not checked, check the box and choose 'Modify' in the lower right.
- After the package downloads and installs, VS will be restarted.
- Find 'JestTest' in the solution explorer, right-click, and choose 'reload project'.

###Create a new environment file
- This can be done by right-clicking the 'JestTest' root directory in Visual Studio, choosing 'Add', then 'New Item'. Disregard file type options, name it '.env', and click 'Add'.
- Copy and paste the code below into the file, substituting 'youremail' and 'yourpassword' with your actual username and password used to sign into RWW (or a test account).
```
RW_EMAIL=youremail
RW_PASSWORD=yourpassword
```

## Running a test
### Terminal within the TestWorks Environment
- From Windows Explorer, navigate to the src folder within the RentalWorksWeb directory.
- Right-click 'JestTest' and choose 'Git bash here' if installed or Open PowerShell window at this directory.
- If it is the first time running tests on your local machine, start with 'npm install' to load required node packages
- Your Visual Studio environment must be running in order to execute tests.

### Running commands for TestWorks
- Testing commands are comprised a few components:
```
npm run test-rentalworksweb -t rwwdemo

```
- Firstly, 'npm run' always initializes the command string.
- The keyword 'test' hypened with the directory in which you wish to invoke tests (e.g. rentalworksapi OR rentalworksweb).
- Note, test directories are located within \JestTest\src\ts
- The '-t' flag and succeeding string can be utilized in order to target a particular test name or group of tests. It will target filename partial matches and is not case-sensitive.
- *Without the '-t' flag and search string, the program will run all files in the directory ending with the .test.ts file extension*.