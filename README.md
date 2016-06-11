# IT Service Catalog

####How to run this package: 
1. Install Visual Studio 
2. Install SharePoint 2010
2. Extract GIT ZIP file
3. Click on the solution with in the folder, ( nameOfPackage.sln) 
4. Configure the SharePoint list and workflows
5. Configure Visual Studio Development Eviroment to run ASP.NET Code
 
For more detail on steps 4 and 5, please refer to our project report. 


####Software needed
* Microsoft Server 2008 R2 
* Visual Studio 
* SharePoint 2010

####Configure the SharePoint list and workflows
Specifications can be found in section 4 of Project Report.pdf

####Configure Visual Studio Development Eviroment to run ASP.NET Code

To run the ASP.NET web application, you will need to first spot check the internal names for the list fields. Most of them should not need to be changed, but some will. You must find a way to find the internal names of fields; there are multiple methods. The easiest is to use Visual Studio 2010 Professional (or higher) server explorer. 

After you are able to find the internal server names for fields, find the Models folder in the solution explorer in Visual Studio. Under the Models folder, navigate to Internal Server Names. 

The first file is named InternalTaskNames.cs. These are all the internal names related to the Task List on SharePoint. The variables are named with a standard convention: ‘internalTask’ followed by the field name on SharePoint. Using your method of finding internal server names, check that they are correct. 

The second file is named InternalVpnRequestNames.cs. These are all internal names related to the VPN Request List and the Approvers List on SharePoint. The code is commented and the variable names follow a consistent convention. Again, make sure the internal server names match up. Also, make sure the SiteUrl, ListName, and ApproversListName are variables are correct. SiteUrl should be the URL of the team site. 

After these model classes are correctly configured, the project will run. 

