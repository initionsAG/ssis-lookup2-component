using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Lookup2")]
#if     (SQL2008)
[assembly: AssemblyDescription("for Integration Services 2008")]
#elif   (SQL2012)
[assembly: AssemblyDescription("for Integration Services 2012")]
#elif   (SQL2014)
[assembly: AssemblyDescription("for Integration Services 2014")]
#else
[assembly: AssemblyDescription("for Integration Services 2008")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("innovative IT solutions AG")]
[assembly: AssemblyProduct("Lookup2")]
[assembly: AssemblyCopyright("Copyright © initions AG 2011-2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
// Set FileVersion and typelib GUID

#if     (Sql2008)
[assembly: Guid("11b65a5c-4f5f-4812-a22b-cd78518512e9")]

#elif   (SQL2012)
[assembly: Guid("30D708C8-910F-4B9D-84CD-3BB2E8B96192")]
#elif   (SQL2014)
[assembly: Guid("7D2BE9ED-D64D-4407-8758-3D210EEA39D1")]
#else
[assembly: Guid("11b65a5c-4f5f-4812-a22b-cd78518512e9")]
#endif

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.3.1.0")]
[assembly: AssemblyVersion("1.0.0.0")]

