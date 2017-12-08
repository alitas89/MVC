﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core.Aspects.Postsharp.ExceptionAspects;
using Core.Aspects.Postsharp.LogAspects;
using Core.Aspects.Postsharp.PerformanceAspects;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BusinessLayer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BusinessLayer")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
//Tüm managerlar loglanacaktır => Örneğin içinde Add geçenler için *Add* yazılabilir.
[assembly: LogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "BusinessLayer.Concrete.*")]
[assembly: LogAspect(typeof(FileLogger), AttributeTargetTypes = "BusinessLayer.Concrete.*")]
//Managerlarda herhangi bir hata meydana geldiği anda loglama yapılacaktır.
[assembly: ExceptionLogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "BusinessLayer.Concrete.*")]
[assembly: ExceptionLogAspect(typeof(FileLogger), AttributeTargetTypes = "BusinessLayer.Concrete.*")]
//Performans ölçme
[assembly: PerformanceCounterAspect(AttributeTargetTypes = "BusinessLayer.Concrete.*")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("99689944-0e2c-4cc7-bbe1-97f8f59c42fa")]

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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
