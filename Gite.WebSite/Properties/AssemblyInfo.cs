using Gite.WebSite;
using System.Reflection;
using System.Runtime.InteropServices;
using WebActivatorEx;

[assembly: AssemblyTitle("Gite.WebSite")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("Gite.WebSite")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("90811cb9-59fb-4a73-b505-7069f3c1e0cb")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Ninject
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]