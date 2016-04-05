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

            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = spList.AddItem(itemCreateInfo);

            Console.WriteLine(input.VPN_recipientFirst);
            Console.WriteLine(input.VPN_recipientLast);

            listItem[spList.Fields.GetByTitle(vpnRecipientFirst).InternalName] = input.VPN_recipientFirst;
            listItem[spList.Fields.GetByTitle(vpnRecipientLast).InternalName] = input.VPN_recipientLast;
            listItem[spList.Fields.GetByTitle(workPhone).InternalName] = input.Work_Phone;
            listItem[spList.Fields.GetByTitle(email).InternalName] = input.VPN_recipientEmail;
            listItem[spList.Fields.GetByTitle(userCode).InternalName] = input.VPN_userCode;
            listItem[spList.Fields.GetByTitle(manager).InternalName] = input.Manager;
            listItem[spList.Fields.GetByTitle(systemsList).InternalName] = input.Systems_List;
            listItem[spList.Fields.GetByTitle(vpnJustification).InternalName] = input.VPN_justification;
            listItem[spList.Fields.GetByTitle(startDate).InternalName] = input.VPN_accessStart;
            listItem[spList.Fields.GetByTitle(endDate).InternalName] = input.VPN_accessEnd;
            listItem[spList.Fields.GetByTitle(deptName).InternalName] = input.VPN_userDept;
            listItem[spList.Fields.GetByTitle(companyName).InternalName] = input.Company_Name;
            listItem[spList.Fields.GetByTitle(companyOther).InternalName] = input.Company_Other;
            listItem[spList.Fields.GetByTitle(qtcOfficeLocation).InternalName] = input.Office_Location;
            listItem[spList.Fields.GetByTitle(qtcofficeSelect).InternalName] = input.Office_Address;
            listItem[spList.Fields.GetByTitle(machineOwner).InternalName] = input.Machine_Owner;

            listItem.Update();
            clientContext.ExecuteQuery();
        }
    }
}