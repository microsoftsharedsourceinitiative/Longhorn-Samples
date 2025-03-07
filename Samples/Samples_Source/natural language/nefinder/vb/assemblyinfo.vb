'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------
Imports System
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security.Permissions
Imports System.Runtime.InteropServices
 
'
' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
'
<assembly: SecurityPermission (SecurityAction.RequestMinimum)> 
<assembly: ComVisible (False)> 
<assembly: CLSCompliant (True)>
<assembly: AssemblyTitle("NEFinder (Named Entity Finder)")> 
<assembly: AssemblyDescription("This assembly shows proper usage of the System.NaturalLanguage dll.")>
<assembly: AssemblyCompany("Microsoft Natural Language Group")>
<assembly: AssemblyProduct("SDK Sample")> 
<assembly: AssemblyCopyright("Microsoft Natural Language Group")> 
<assembly: AssemblyTrademark("Microsoft Natural Language Group")>  
<assembly: AssemblyCulture("")>

'
' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Revision and Build Numbers 
' by using the '*' as shown below:

<assembly: AssemblyVersion("1.0.0.0")> 

'
' In order to sign your assembly you must specify a key to use. Refer to the 
' Microsoft .NET Framework documentation for more information on assembly signing.
'
' Use the attributes below to control which key is used for signing. 
'
' Notes: 
'   (*) If no key is specified, the assembly is not signed.
'   (*) KeyName refers to a key that has been installed in the Crypto Service
'       Provider (CSP) on your machine. KeyFile refers to a file which contains
'       a key.
'   (*) If the KeyFile and the KeyName values are both specified, the 
'       following processing occurs:
'       (1) If the KeyName can be found in the CSP, that key is used.
'       (2) If the KeyName does not exist and the KeyFile does exist, the key 
'           in the KeyFile is installed into the CSP and used.
'   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
'       When specifying the KeyFile, the location of the KeyFile should be
'       relative to the project output directory which is
'       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
'       located in the project directory, you would specify the AssemblyKeyFile 
'       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
'   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
'       documentation for more information on this.
'
<assembly: AssemblyDelaySign(False)>
<assembly: AssemblyKeyFile ("")> 
<assembly: AssemblyKeyName("")>
