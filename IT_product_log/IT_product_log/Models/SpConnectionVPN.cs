using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint.Client;
using SPClient = Microsoft.SharePoint.Client;
using System.Web.Mvc;

namespace IT_product_log.Models
{
    public class SpConnectionVPN
    {
        //server details
        string SiteUrl = "http://qtcserver:80/Zainab13";
        string ListName = "VPN-Request-list";
        string TaskListName = "Tasks";
        string ApproversListName = "Approvers";

        //Title constants - fields with choice select
        //For some reason, I can't get the column retrival working with internal names (I will refactor if I have time) 
        string vpnStatusType = "VPN User Status";
        string deptName = "VPN User Dept";
        string companyName = "Company Name";
        string qtcOfficeLocation = "Office Location";
        string qtcofficeSelect = "Office Address";
        string machineOwner = "Machine Owner";

        //internal names for the Request List 
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
        string internalUserDept = "VPN_x0020_User_x0020_Dept";
        string internalCompanyName = "field3";
        string internalCompanyOther = "Company_x0020_Name";
        string internalOfficeLocation = "Office_x0020_Location";
        string internalOfficeAddress = "QTC_x0020_Office";
        string internalMachineOwner = "Machine_x0020_Owner";
        string internalUserStatus = "VPN_x0020_User_x0020_Status";
        string internalCreatedBy = "Author";
        string internalID = "Request_x0020_ID";
        string internalCreated = "Created";
        string internalRequestStatus = "VPN_x0020_Request_x0020_Status";

        //internal names for the Tasks list
        string internalTasksAssignedTo = "AssingedTo";
        string internalTaskOutcome = "WorkflowOutcome";

        //internal names for the Approvers List
        string internalApproversUser = "User";
        string internalApproversTitle = "Title";

        //the possible values of Approvers List, Title column 
        string spNameForSecurity = "Compliance Officer";
        string spNameForITManager = "IT Manager";

        //possible requests statuses 
        string pendingSecurity = "Pending Security Manager Approval";
        string pendingITManager = "Pending IT Manager Approval";
        string pendingManager = "Pending Manager Approval";

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

            //pulling up the current user information
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            //pulling up the manager information 
            var manager = clientContext.Web.EnsureUser(input.Manager);
            clientContext.Load(manager);
            clientContext.ExecuteQuery();
            FieldUserValue userValue2 = new FieldUserValue();
            userValue2.LookupId = manager.Id;

            listItem[internalCreatedBy] = userValue;
            listItem[internalVpnRecipientFirst] = input.VPN_recipientFirst;
            listItem[internalVpnRecipientLast] = input.VPN_recipientLast;
            listItem[internalWorkPhone] = input.Work_Phone;
            listItem[internalEmail] = input.VPN_recipientEmail;
            listItem[internalUserCode] = input.VPN_userCode;
            listItem[internalManager] = userValue2;
            listItem[internalUserDept] = input.VPN_userDept;
            listItem[internalUserStatus] = input.VPN_userStatus;
            listItem[internalSystemsList] = input.Systems_List;
            listItem[internalJustification] = input.VPN_justification;
            listItem[internalAccessStart] = input.VPN_accessStart;
            listItem[internalAccessEnd] = input.VPN_accessEnd;
            listItem[internalCompanyName] = input.Company_Name;
            listItem[internalCompanyOther] = input.Company_Other;
            listItem[internalOfficeLocation] = input.Office_Location;
            listItem[internalOfficeAddress] = input.Office_Address;
            listItem[internalMachineOwner] = input.Machine_Owner;

            listItem.Update();
            clientContext.ExecuteQuery();
        }

        public List<VpnRequest> getMyRequests()
        {
            ClientContext clientContext = new ClientContext(SiteUrl);
            List spList = clientContext.Web.Lists.GetByTitle(ListName);
            clientContext.Load(spList);

            //pulling the current user's name 
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            //querying all requests created by the user 
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
                <View>
                    <Query>
                        <Where>
                            <Eq>
                                <FieldRef Name='Author' LookupId='True'/>
                                <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                            </Eq>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = spList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //modeling the query data into VpnRequest model 
            List<VpnRequest> currentRequests = new List<VpnRequest>();
            currentRequests = loadList(currentRequests,col);
            return currentRequests;
        }

        public List<VpnRequest> getPendingRequests()
        {
            //loading up all 3 lists 
            ClientContext clientContext = new ClientContext(SiteUrl);
            List vpnRequestList = clientContext.Web.Lists.GetByTitle(ListName);
            List taskList = clientContext.Web.Lists.GetByTitle(TaskListName);
            List approversList = clientContext.Web.Lists.GetByTitle(ApproversListName);
            clientContext.Load(vpnRequestList);
            clientContext.Load(taskList);
            clientContext.Load(approversList);

            //pulling the current user's name
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            List<VpnRequest> pendingRequests = new List<VpnRequest>();

            //Querying all requests where Manager = Current User AND where Request Status = Pending Manager Approval
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
                <View>
                    <Query>
                        <Where> 
                            <And>
                                <Eq>
                                    <FieldRef Name='" + internalManager + @"' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name='" + internalRequestStatus + @"' LookupId ='True'/>
                                    <Value Type = 'Text'>" + pendingManager + @"</Value>
                                </Eq>
                            </And>
                        </Where>
                    </Query>
                </View>";
            ListItemCollection col = vpnRequestList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //Modeling data into Vpnrequest model
            pendingRequests = loadList(pendingRequests, col);
            return pendingRequests;
        }


        private List<VpnRequest> loadList(List<VpnRequest> currentRequests,ListItemCollection col)
        {
            //this method takes a ListItemCollection and converts it into a list of VpnRequest (A model Kevin created)
            //if an ongoing list is passed in, requests are added
            //if there is no ongoing list, pass in an empty List<VpnRequest>

            ClientContext clientContext = new ClientContext(SiteUrl);
            List spList = clientContext.Web.Lists.GetByTitle(ListName);
            clientContext.Load(spList);

            foreach (ListItem item in col)
            {
                clientContext.Load(item);
                clientContext.ExecuteQuery();

                VpnRequest temp = new VpnRequest();
                temp.VPN_requestID = Int32.Parse((string)item[internalID]);
                temp.DateSubmitted = ((DateTime)item[internalCreated]).ToString("MM/dd/yyyy");
                temp.VPN_requestStatus = (string)item[internalRequestStatus];
                temp.VPN_accessEnd = (DateTime)item[internalAccessEnd];
                temp.VPN_accessStart = (DateTime)item[internalAccessStart];
                temp.VPN_recipientFirst = (string)item[internalVpnRecipientFirst];
                temp.VPN_recipientLast = (string)item[internalVpnRecipientLast];
                temp.Work_Phone = (string)item[internalWorkPhone];
                temp.VPN_recipientEmail = (string)item[internalEmail];
                temp.VPN_userCode = Convert.ToInt32(((double)item[internalUserCode]));
                temp.Manager = ((FieldUserValue)item[internalManager]).LookupValue;
                temp.Systems_List = (string)item[internalSystemsList];
                temp.VPN_justification = (string)item[internalJustification];
                temp.VPN_userDept = (string)item[internalUserDept];
                temp.Company_Name = (string)item[internalCompanyName];
                temp.Company_Other = (string)item[internalCompanyOther];
                temp.Office_Location = (string)item[internalOfficeLocation];
                temp.Office_Address = (string)item[internalOfficeAddress];
                temp.Machine_Owner = (string)item[internalMachineOwner];
                temp.VPN_userStatus = (string)item[internalUserStatus];
                temp.VPN_requestor = ((FieldUserValue)item[internalCreatedBy]).LookupValue;

                currentRequests.Add(temp);
            }

            return currentRequests;
        }
    }
}