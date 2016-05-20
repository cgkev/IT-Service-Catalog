using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_product_log.Models.Internal_Server_Names
{
    public class InternalVpnRequestNames
    {
        //server details
        public string SiteUrl = "http://qtcserver:80/Final";

        public string ListName = "VPN Request List";
        public string ApproversListName = "Approvers";

        //Title constants - fields with choice select
        //For some reason, I can't get the column retrival working with internal names (I will refactor if I have time) 
        public string vpnStatusType = "VPN User Status";
        public string deptName = "VPN User Dept";
        public string companyName = "Company Name";
        public string qtcOfficeLocation = "Office Location";
        public string qtcofficeSelect = "Office Address";
        public string machineOwner = "Machine Owner";
        public string radiusProfileSelect = "Radius Profile Select";

        //internal names for the Request List 
        public string internalVpnRecipientFirst = "VPN_x0020_Recipient_x0020_First";
        public string internalVpnRecipientLast = "VPN_x0020_Recipient_x0020_Last";
        public string internalWorkPhone = "Work_x0020_Phone";
        public string internalEmail = "VPN_x0020_Recipient_x0020_Email";
        public string internalUserCode = "VPN_x0020_User_x0020_Code";
        public string internalManager = "Manager";
        public string internalSystemsList = "Systems_x0020_List";
        public string internalJustification = "VPN_x0020_Justification";
        public string internalAccessStart = "VPN_x0020_Access_x0020_Start";
        public string internalAccessEnd = "VPN_x0020_Access_x0020_End";
        public string internalUserDept = "VPN_x0020_User_x0020_Dept";
        public string internalCompanyName = "field3";
        public string internalCompanyOther = "Company_x0020_Name";
        public string internalOfficeLocation = "Office_x0020_Location";
        public string internalOfficeAddress = "QTC_x0020_Office";
        public string internalMachineOwner = "Machine_x0020_Owner";
        public string internalUserStatus = "VPN_x0020_User_x0020_Status";
        public string internalCreatedBy = "Author";
        public string internalID = "Request_x0020_ID";
        public string internalCreated = "Created";
        public string internalRequestStatus = "VPN_x0020_Request_x0020_Status";
        public string internalAgency = "Agency";
        public string internalExtCode = "Telephone_x0020_Extension";
        public string internalVpnProfileSelect = "VPN_x0020_Profile_x0020_Select";
        public string internalRadiusSelect = "Radius_x0020_Profile_x0020_Selec";
        public string internalComments = "Reviewer_x0020_Comments";
        public string internalVpnRadiusSelectOther = "Radius_x0020_Profile_x0020_Other";

        //internal names for the Approvers List
        public string internalApproversUser = "User";
        public string internalApproversTitle = "Title";

        //the possible values of Approvers List, Title column 
        public string spNameForSecurity = "Security Manager";
        public string spNameForITManager = "IT Manager";

        //possible requests statuses 
        public string pendingSecurity = "Pending Security Manager Approval";
        public string pendingITManager = "Pending IT Manager Approval";
        public string pendingManager = "Pending Manager Approval";

        //built in field for all types
        public string internalWflowInstanceID = "WorkflowInstanceID";
    }
}