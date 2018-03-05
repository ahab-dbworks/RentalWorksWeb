FwCore is reserved for .NET Core implementation specific files such as Controllers and Security Proviers.  
All C# code files need to go in FwStandard unless they will only compile in FwCore.  If you are working on something with multiple files,
then any filea that can compile in FwStandard need to be in FwStandard.  The reason for this is that FwStandard targets .NET Standard 
and having been through many variations of the .NET Framework over the years, we want to keep the bulk of our code base as portable
as possible.  FwCore has a dependency on the .NET Core runtime, while FwStandard can run on many .NET runtimes.