using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint.Client;
using SPClient = Microsoft.SharePoint.Client;

namespace IT_product_log.Models
{
    public class SpConnectionVPN
    {
        //server details
        string SiteUrl = "http://win-4okri914r3c:47690/Raymond";
        string ListName = "VPN Request";

        //Title constants - fields with choice select
        string vpnStatusType = "VPN Status Type";
        string deptName = "Dept Name";
        string companyName = "Company Name";
        string qtcOfficeLocation = "QTC Office Location";
        string qtcofficeSelect = "QTC Office Select";
        string machineOwner = "Machine Owner";

        //the rest of the title constants 
        string vpnRecipientFirst = "VPN Recipient First";
        string vpnRecipientLast = "VPN Recipient Last";
        string workPhone = "Work Phone";
        string email = "VPN Recipient Email";
        string userCode = "VPN User Code";
        string manager = "Manager";
        string systemsList = "Systems List";
        string vpnJustification = "VPN Justification";
        string startDate = "VPN Access Start";
        string endDate = "VPN Access End";
        string companyOther = "Company Other";

        //internal names 
        string internalVpnRecipientFirst = "VPN_x0020_Recipient_x0020_First";
        string internalVpnRecipientLast = "VPN_x0020_Recipient_x0020_Last";
        string internalWorkPhone = "Work_x0020_Phone";
        string internalEmail = "VPN_x0020_Recipient_x0020_Email";
        string internalUserCode = "VPN_x0020_User_x0020_Code";
        string internalManager = "Manager";
        string internalSystemsList = "Systems_x0020_List";
        string internalJustification = "VPN_x0020_Justification";
        string internalAccessStart = "VPN_x0020_Access_x0020_Start";
        string internalAccessEnd = "VPN_x0020_Access_x0020_End";
        string internalUserDept = "VPN_x0020_User_x0020_Department";
        string internalCompanyName = "NewColumn1";
        string internalCompanyOther = "Company_x0020_Other";
        string internalOfficeLocation = "QTC_x0020_Office_x0020_Location";
        string internalOfficeAddress = "QTC_x0020_Office_x0020_Select";
        string internalMachineOwner = "NewColumn10";
        string internalUserStatus = "VPN_x0020_User_x0020_Status";


        public SpConnectionVPN()
        {
           //left blank for now 
        }

        public string[] getFieldChoices(string field)
        {
            ClientContext clientContext = new ClientContext(SiteUrl);
            List spList = clientContext.Web.Lists.GetByTitle(ListName);
            clientContext.Load(spList);

            FieldChoice choiceField = clientContext.CastTo<FieldChoice>(spList.Fields.GetByTitle(field));
            clientContext.Load(choiceField);
            clientContext.ExecuteQuery();

            List<string> choices = new List<String>();
            choices.Add("");

            for (int i = 0; i < choiceField.Choices.Length; i++)
            {
                choices.Add(choiceField.Choices[i]);
            }

            return choices.ToArray();
        }

        public string[] getVpnStatusTypeChoices()
        {
            return this.getFieldChoices(vpnStatusType);
        }

        public string[] getDeptNameChoices()
        {
            return this.getFieldChoices(deptName);
        }

        public string[] getCompanyNameChoices()
        {
            return this.getFieldChoices(companyName);
        }

        public string[] getQtcOfficeLocationChoices()
        {
            return this.getFieldChoices(qtcOfficeLocation);
        }

        public string[] getQtcOfficeSelectChoices()
        {
            return this.getFieldChoices(qtcofficeSelect);
        }

        public string[] getMachineOwnerChoices()
        {
            return this.getFieldChoices(machineOwner);
        }

        public void addRequest(VpnRequest input)
        {
            ClientContext clientContext = new ClientContext(SiteUrl);
            List spList = clientContext.Web.Lists.GetByTitle(ListName);
            clientContext.Load(spList);

            var itemCreateInfo = new ListItemCreationInformation();
            var listItem = spList.AddItem(itemCreateInfo);

            listItem[internalVpnRecipientFirst] = input.VPN_recipientFirst;
            listItem[internalVpnRecipientLast] = input.VPN_recipientLast;
            listItem[internalWorkPhone] = input.Work_Phone;
            listItem[internalEmail] = input.VPN_recipientEmail;
            listItem[internalUserCode] = input.VPN_userCode;
            listItem[internalManager] = input.Manager;
            listItem[internalSystemsList] = input.Systems_List;
            listItem[internalJustification] = input.VPN_justification;
            listItem[internalAccessStart] = input.VPN_accessStart;
            listItem[internalAccessEnd] = input.VPN_accessEnd;
            listItem[internalUserDept] = input.VPN_userDept;
            listItem[internalCompanyName] = input.Company_Name;
            listItem[internalCompanyOther] = input.Company_Other;
            listItem[internalOfficeLocation] = input.Office_Location;
            listItem[internalOfficeAddress] = input.Office_Address;
            listItem[internalMachineOwner] = input.Machine_Owner;
            listItem[internalUserStatus] = input.VPN_userStatus;

            listItem.Update();
            clientContext.ExecuteQuery();
        }
    }
}