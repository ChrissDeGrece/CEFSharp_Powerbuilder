# CEFSharp_Powerbuilder
CEFSharp visual control for powerbuilder

This is a modified project based on https://anvil-of-time.com/powerbuilder/powerbuilder-using-c-visual-objects-in-pb-classic-applications/ example of using C# visual objects in powerbuilder.
Thanks a lot for the example.

Project is tested with Visual Studio 2019 and PB 10.5 
You need to run VS2019 using admin rights in order for the build process to register the com object in the registry.

Add CEFSharpWinforms using Nuget before building and set target cpu to x86

Add IOPCsharpexample OLE control in powerbuilder and call : ole_1.object.navigate("www.google.com")

DONE!
