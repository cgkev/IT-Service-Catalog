using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SharePoint.Client;
using System.Web.Mvc;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint;
using System.Text.RegularExpressions;
using names = IT_product_log.Models.Internal_Server_Names;

namespace IT_product_log.Models
{
    public class SpConnectionVPN
    {
        names.InternalVpnRequestNames requestNames = new names.InternalVpnRequestNames();
        names.InternalTaskNames taskNames = new names.InternalTaskNames();

        ClientContext clientContext; 

        public SpConnectionVPN()
        {
            this.clientContext = new ClientContext(requestNames.SiteUrl);
        }

        public string[] getFieldChoices(string field)
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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

        public string[] getVpnRadiusProfileSelect()
        {
            return this.getFieldChoices(requestNames.radiusProfileSelect);
        }

        public string[] getVpnStatusTypeChoices()
        {
            return this.getFieldChoices(requestNames.vpnStatusType);
        }

        public string[] getDeptNameChoices()
        {
            return this.getFieldChoices(requestNames.deptName);
        }

        public string[] getCompanyNameChoices()
        {
            return this.getFieldChoices(requestNames.companyName);
        }

        public string[] getQtcOfficeLocationChoices()
        {
            return this.getFieldChoices(requestNames.qtcOfficeLocation);
        }

        public string[] getQtcOfficeSelectChoices()
        {
            return this.getFieldChoices(requestNames.qtcofficeSelect);
        }

        public string[] getMachineOwnerChoices()
        {
            return this.getFieldChoices(requestNames.machineOwner);
        }

        public void addRequest(VpnRequest input)
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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

            listItem[requestNames.internalCreatedBy] = userValue;
            listItem[requestNames.internalVpnRecipientFirst] = input.VPN_recipientFirst;
            listItem[requestNames.internalVpnRecipientLast] = input.VPN_recipientLast;
            listItem[requestNames.internalWorkPhone] = input.Work_Phone;
            listItem[requestNames.internalEmail] = input.VPN_recipientEmail;
            listItem[requestNames.internalUserCode] = input.VPN_userCode;
            listItem[requestNames.internalManager] = userValue2;
            listItem[requestNames.internalUserDept] = input.VPN_userDept;
            listItem[requestNames.internalUserStatus] = input.VPN_userStatus;
            listItem[requestNames.internalSystemsList] = input.Systems_List;
            listItem[requestNames.internalJustification] = input.VPN_justification;
            listItem[requestNames.internalAccessStart] = input.VPN_accessStart;
            listItem[requestNames.internalAccessEnd] = input.VPN_accessEnd;
            listItem[requestNames.internalCompanyName] = input.Company_Name;
            listItem[requestNames.internalCompanyOther] = input.Company_Other;
            listItem[requestNames.internalOfficeLocation] = input.Office_Location;
            listItem[requestNames.internalOfficeAddress] = input.Office_Address;
            listItem[requestNames.internalMachineOwner] = input.Machine_Owner;
            listItem[requestNames.internalExtCode] = input.Ext_code;
            listItem[requestNames.internalAgency] = input.Agency;

            listItem.Update();
            clientContext.ExecuteQuery();
        }

        public List<VpnRequest> getAllMyRequests()
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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
            currentRequests = loadList(currentRequests, col);

            return currentRequests;
        }

        public List<VpnRequest> getRejectedMyRequests()
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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
                            <And>
                                <Eq>
                                    <FieldRef Name='Author' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <BeginsWith>
                                    <FieldRef Name = '" + requestNames.internalRequestStatus + @"' LookupId = 'True'/>
                                    <Value Type = 'Text'>Rejected by</Value>
                                </BeginsWith>
                            </And>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = spList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //modeling the query data into VpnRequest model 
            List<VpnRequest> currentRequests = new List<VpnRequest>();

            currentRequests = loadList(currentRequests, col);

            return currentRequests;
        }

        public List<VpnRequest> getApprovedMyrequests()
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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
                            <And>
                                <Eq>
                                    <FieldRef Name='Author' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name = '" + requestNames.internalRequestStatus + @"' LookupId = 'True'/>
                                    <Value Type = 'Text'>Approved</Value>
                                </Eq>
                            </And>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = spList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //modeling the query data into VpnRequest model 
            List<VpnRequest> currentRequests = new List<VpnRequest>();

            currentRequests = loadList(currentRequests, col);

            return currentRequests;
        }

        //gets a list of requests created by the current user currently in a status that is pending a manager approval
        public List<VpnRequest> getPendingMyRequests()
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
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
                            <And>
                                <Eq>
                                    <FieldRef Name='Author' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <BeginsWith>
                                    <FieldRef Name = '" + requestNames.internalRequestStatus + @"' LookupId = 'True'/>
                                    <Value Type = 'Text'>Pending</Value>
                                </BeingsWith>
                            </And>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = spList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //modeling the query data into VpnRequest model 
            List<VpnRequest> currentRequests = new List<VpnRequest>();

            currentRequests = loadList(currentRequests, col);

            return currentRequests;
        }

        //gets a list of requests currently pending review for the current user
        public List<VpnRequest> getPendingReviews()
        {
            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            List approversList = clientContext.Web.Lists.GetByTitle(requestNames.ApproversListName);
            clientContext.Load(vpnRequestList);
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
                                    <FieldRef Name='" + requestNames.internalManager + @"' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name='" + requestNames.internalRequestStatus + @"' LookupId ='True'/>
                                    <Value Type = 'Text'>" + requestNames.pendingManager + @"</Value>
                                </Eq>
                            </And>
                        </Where>
                    </Query>
                </View>";
            ListItemCollection col = vpnRequestList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();
            pendingRequests = loadList(pendingRequests, col);

            //Querying the approver list to see if the current user is either an IT Manager or Security Manager 
            bool isSecurity = false;
            bool isItManager = false;
            camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
            <View> 
                <Query>
                    <Where>
                        <Eq>
                            <FieldRef Name = 'Users' LookupId ='True'/>
                            <Value Type ='Lookup'>" + userValue.LookupId + @"</Value>
                        </Eq>
                    </Where>
                </Query>
            </View>";
            col = approversList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            foreach (ListItem current in col)
            {
                if (current[requestNames.internalApproversTitle].Equals(requestNames.spNameForITManager))
                {
                    isItManager = true;
                }

                if (current[requestNames.internalApproversTitle].Equals(requestNames.spNameForSecurity))
                {
                    isSecurity = true;
                }
            }
            if (isSecurity == true)
            {
                camlQuery = new CamlQuery();
                camlQuery.ViewXml = @"
               <View>
                    <Query>
                        <Where> 
                            <Eq>
                                <FieldRef Name='" + requestNames.internalRequestStatus + @"' LookupId ='True'/>
                                <Value Type = 'Text'>" + requestNames.pendingSecurity + @"</Value>
                            </Eq>
                        </Where>
                    </Query>
                </View>";
                col = vpnRequestList.GetItems(camlQuery);
                clientContext.Load(col);
                clientContext.ExecuteQuery();

                pendingRequests = loadList(pendingRequests, col);
            }

            if (isItManager == true)
            {
                camlQuery = new CamlQuery();
                camlQuery.ViewXml = @"
               <View>
                    <Query>
                        <Where> 
                            <Eq>
                                <FieldRef Name='" + requestNames.internalRequestStatus + @"' LookupId ='True'/>
                                <Value Type = 'Text'>" + requestNames.pendingITManager + @"</Value>
                            </Eq>
                        </Where>
                    </Query>
                </View>";
                col = vpnRequestList.GetItems(camlQuery);
                clientContext.Load(col);
                clientContext.ExecuteQuery();

                pendingRequests = loadList(pendingRequests, col);
            }

            return orderList(pendingRequests);
        }

        public List<VpnRequest> getApprovedReviews()
        {
            List<VpnRequest> returnList = new List<VpnRequest>();

            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            List taskList = clientContext.Web.Lists.GetByTitle(taskNames.TaskListName);
            clientContext.Load(vpnRequestList);
            clientContext.Load(taskList);

            //pulling the current user's name
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            //querying the tasklist for all tasks where current user = assigned to AND where outcome = approved
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
                <View>
                    <Query>
                        <Where> 
                            <And>
                                <Eq>
                                    <FieldRef Name='AssignedTo' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name='WorkflowOutcome' LookupId ='True'/>
                                    <Value Type = 'Text'>Approved</Value>
                                </Eq>
                            </And>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = taskList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //using the results from the query, a list containing the ID of the VPN Requests is created
            List<string> fetchIds = new List<string>();
            foreach (ListItem current in col)
            {
                string[] pieces = ((string)current[taskNames.internalTaskTitle]).Split('/');
                string vpnRequestID = pieces[2];
                fetchIds.Add(vpnRequestID);
            }

            //fetching all VPN Requests that have that have that ID
            returnList = loadList(returnList ,fetchIds);
            return returnList;
        }

        public List<VpnRequest> getRejectedReviews()
        {
            List<VpnRequest> returnList = new List<VpnRequest>();

            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            List taskList = clientContext.Web.Lists.GetByTitle(taskNames.TaskListName);
            clientContext.Load(vpnRequestList);
            clientContext.Load(taskList);

            //pulling the current user's name
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            //querying the tasklist for all tasks where current user = assigned to AND where outcome = approved
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
                <View>
                    <Query>
                        <Where> 
                            <And>
                                <Eq>
                                    <FieldRef Name='AssignedTo' LookupId='True'/>
                                    <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                                </Eq>
                                <Eq>
                                    <FieldRef Name='WorkflowOutcome' LookupId ='True'/>
                                    <Value Type = 'Text'>Rejected</Value>
                                </Eq>
                            </And>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = taskList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //using the results from the query, a list containing the ID of the VPN Requests is created
            List<string> fetchIds = new List<string>();
            foreach (ListItem current in col)
            {
                string[] pieces = ((string)current[taskNames.internalTaskTitle]).Split('/');
                string vpnRequestID = pieces[2];
                fetchIds.Add(vpnRequestID);
            }

            //fetching all VPN Requests that have that have that ID
            returnList = loadList(returnList, fetchIds);
            return returnList;
        }

        public List<VpnRequest> getAllReviews()
        {
            List<VpnRequest> returnList = new List<VpnRequest>();

            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            List taskList = clientContext.Web.Lists.GetByTitle(taskNames.TaskListName);
            clientContext.Load(vpnRequestList);
            clientContext.Load(taskList);

            //pulling the current user's name
            User user = clientContext.Web.EnsureUser(HttpContext.Current.User.Identity.Name);
            clientContext.Load(user);
            clientContext.ExecuteQuery();
            FieldUserValue userValue = new FieldUserValue();
            userValue.LookupId = user.Id;

            //querying the tasklist for all tasks where current user = assigned to AND where outcome = approved
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"
                <View>
                    <Query>
                        <Where> 
                            <Eq>
                                <FieldRef Name='AssignedTo' LookupId='True'/>
                                <Value Type='Lookup'>" + userValue.LookupId + @"</Value>
                            </Eq>
                        </Where>
                    </Query>
                </View>";

            ListItemCollection col = taskList.GetItems(camlQuery);
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            //using the results from the query, a list containing the ID of the VPN Requests is created
            List<string> fetchIds = new List<string>();
            foreach (ListItem current in col)
            {
                string[] pieces = ((string)current[taskNames.internalTaskTitle]).Split('/');
                string vpnRequestID = pieces[2];
                fetchIds.Add(vpnRequestID);
            }

            //fetching all VPN Requests that have that have that ID
            returnList = loadList(returnList, fetchIds);
            return returnList;
        }

        public void ReviewRequest(int id, string submit, string comments)
        {
            ListItem taskItem = null;
            List taskList = clientContext.Web.Lists.GetByTitle(taskNames.TaskListName);
            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            clientContext.Load(taskList);
            clientContext.Load(vpnRequestList);
            clientContext.ExecuteQuery();

            //get the current VPN Request 
            ListItem currentVpnRequestItem = vpnRequestList.GetItemById(id-1000);
            clientContext.Load(currentVpnRequestItem);
            clientContext.ExecuteQuery();

            string status = (string)currentVpnRequestItem[requestNames.internalRequestStatus];

            if (status.Equals("Pending Manager Approval") == true)
            {
                status = "Manager Approval";
            }
            else if (status.Equals("Pending Security Manager Approval") == true)
            {
                status = "Security Manager Approval";
            }
            else if (status.Equals("Pending IT Manager Approval") == true)
            {
                status = "IT Manager Approval";
            }
            string taskTitle = "VPN Request/" + status + "/" + id.ToString();

            //finding the task with the same title 
            ListItemCollection col = taskList.GetItems(new CamlQuery());
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            foreach(ListItem currentTask in col)
            {
                if (currentTask[taskNames.internalTaskTitle].Equals(taskTitle))
                {
                    taskItem = currentTask;
                }
            }

           //updating the task as needed
            if (submit.Equals("Approve"))
            {
                taskItem[taskNames.internalTaskOutcome] = "Approved";
                taskItem[taskNames.internalTaskStatus] = "Approved";

            }
            else //reject 
            {
                taskItem[taskNames.internalTaskOutcome] = "Rejected";
                taskItem[taskNames.internalTaskStatus] = "Rejected";
            }
            taskItem[taskNames.internalTaskPercentComplete] = 1.0;
            taskItem["Completed"] = true;
            taskItem["FormData"] = "Completed";

            //updating comments in both lists 
            taskItem[taskNames.internalTaskDesc] = comments;
            currentVpnRequestItem[requestNames.internalComments] = comments;

            currentVpnRequestItem.Update();
            taskItem.Update();
            clientContext.ExecuteQuery();
        }

        public void ReviewRequest(int id, string submit, string comments, string VPN_Radius, string VPN_Other, string VPN_accessStart, string VPN_accessEnd, string[] checkboxes)
        {
            ListItem taskItem = null;
            List taskList = clientContext.Web.Lists.GetByTitle(taskNames.TaskListName);
            List vpnRequestList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            clientContext.Load(taskList);
            clientContext.Load(vpnRequestList);
            clientContext.ExecuteQuery();

            //get the current VPN Request 
            ListItem currentVpnRequestItem = vpnRequestList.GetItemById(id - 1000);
            clientContext.Load(currentVpnRequestItem);
            clientContext.ExecuteQuery();

            string status = (string)currentVpnRequestItem[requestNames.internalRequestStatus];

            if (status.Equals("Pending Manager Approval") == true)
            {
                status = "Manager Approval";
            }
            else if (status.Equals("Pending Security Manager Approval") == true)
            {
                status = "Security Manager Approval";
            }
            else if (status.Equals("Pending IT Manager Approval") == true)
            {
                status = "IT Manager Approval";
            }
            string taskTitle = "VPN Request/" + status + "/" + id.ToString();

            //updating the start and end date of the request 

            currentVpnRequestItem[requestNames.internalAccessStart] = VPN_accessStart;
            currentVpnRequestItem[requestNames.internalAccessEnd] = VPN_accessEnd;

            //updating Radius Profile Select 

            currentVpnRequestItem[requestNames.internalRadiusSelect] = VPN_Radius;
            currentVpnRequestItem[requestNames.internalVpnRadiusSelectOther] = VPN_Other;

            //updating VPN Profile Select 

            if (checkboxes.Length == 2)
            {
                currentVpnRequestItem[requestNames.internalVpnProfileSelect] = "QTC/Transcriber";
            }
            else if (checkboxes.Length == 1)
            {
                currentVpnRequestItem[requestNames.internalVpnProfileSelect] = checkboxes[0];
            }

            currentVpnRequestItem.Update();

            //finding the task with the same title 
            ListItemCollection col = taskList.GetItems(new CamlQuery());
            clientContext.Load(col);
            clientContext.ExecuteQuery();

            foreach (ListItem currentTask in col)
            {
                if (currentTask[taskNames.internalTaskTitle].Equals(taskTitle))
                {
                    taskItem = currentTask;
                }
            }

            //updating the task as needed
            if (submit.Equals("Approve"))
            {
                taskItem[taskNames.internalTaskOutcome] = "Approved";
                taskItem[taskNames.internalTaskStatus] = "Approved";

            }
            else //reject 
            {
                taskItem[taskNames.internalTaskOutcome] = "Rejected";
                taskItem[taskNames.internalTaskStatus] = "Rejected";
            }
            taskItem[taskNames.internalTaskPercentComplete] = 1.0;
            taskItem["Completed"] = true;
            taskItem["FormData"] = "Completed";

            //updating comments in both lists 
            taskItem[taskNames.internalTaskDesc] = comments;
            currentVpnRequestItem[requestNames.internalComments] = comments;

            currentVpnRequestItem.Update();
            taskItem.Update();
            clientContext.ExecuteQuery();
        }

        private List<VpnRequest> loadList(List<VpnRequest> currentRequests, ListItemCollection col)
        {
            //this method takes a ListItemCollection and converts it into a list of VpnRequest (A model Kevin created)
            //if an ongoing list is passed in, requests are added
            //if there is no ongoing list, pass in an empty List<VpnRequest>
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            clientContext.Load(spList);
            foreach (ListItem item in col)
            {
                try
                {
                    clientContext.Load(item);
                    clientContext.ExecuteQuery();

                    VpnRequest temp = new VpnRequest();
                    temp.VPN_requestID = Int32.Parse((string)item[requestNames.internalID]);
                    temp.DateSubmitted = ((DateTime)item[requestNames.internalCreated]).ToString("MM/dd/yyyy");
                    temp.VPN_requestStatus = (string)item[requestNames.internalRequestStatus];
                    temp.VPN_accessEnd = ((DateTime)item[requestNames.internalAccessEnd]).ToString();
                    temp.VPN_accessStart = ((DateTime)item[requestNames.internalAccessStart]).ToString();
                    temp.VPN_recipientFirst = (string)item[requestNames.internalVpnRecipientFirst];
                    temp.VPN_recipientLast = (string)item[requestNames.internalVpnRecipientLast];
                    temp.Work_Phone = (string)item[requestNames.internalWorkPhone];
                    temp.VPN_recipientEmail = (string)item[requestNames.internalEmail];
                    temp.VPN_userCode = Convert.ToInt32(((double)item[requestNames.internalUserCode]));
                    temp.Manager = ((FieldUserValue)item[requestNames.internalManager]).LookupValue;
                    temp.Systems_List = (string)item[requestNames.internalSystemsList];
                    temp.VPN_justification = (string)item[requestNames.internalJustification];
                    temp.VPN_userDept = (string)item[requestNames.internalUserDept];
                    temp.Company_Name = (string)item[requestNames.internalCompanyName];
                    temp.Company_Other = (string)item[requestNames.internalCompanyOther];
                    temp.Office_Location = (string)item[requestNames.internalOfficeLocation];
                    temp.Office_Address = (string)item[requestNames.internalOfficeAddress];
                    temp.Machine_Owner = (string)item[requestNames.internalMachineOwner];
                    temp.VPN_userStatus = (string)item[requestNames.internalUserStatus];
                    temp.Agency = (string)item[requestNames.internalAgency];
                    temp.VPN_requestor = ((FieldUserValue)item[requestNames.internalCreatedBy]).LookupValue;
                    try
                    {
                        temp.Ext_code = Convert.ToInt32((double)item[requestNames.internalExtCode]);
                        temp.Reviewer_Comments = (string)item[requestNames.internalComments];
                        
                    }
                    catch (System.ArgumentNullException e)
                    {
                        //extension code can be null, so we'll skip over this
                    }
                    catch (System.NullReferenceException f)
                    {

                    }
                    currentRequests.Add(temp);
                }
                catch (System.ArgumentNullException e)
                {
                    //if something critical is null, we won't add it to the viewbag list
                    continue;
                }
            }
            return currentRequests;
        }

        //takes in a running list of request and a list of strings 
        //takes the list of strings, finds all VPN Requests with the same ID on the SP Server 
        //returns an updated list of requests 
        //currentRequests = currentRequests + col
        private List<VpnRequest> loadList (List<VpnRequest> currentRequests, List<string> col)
        {
            List spList = clientContext.Web.Lists.GetByTitle(requestNames.ListName);
            clientContext.Load(spList);

            //remove any duplicates from col 
            col = col.Distinct().ToList();

            //going to traverse the list of strings (which represent VPN Request IDs) to generate a list of ListItems 
            List<ListItem> spRequestList = new List<ListItem>();
            foreach(string current in col)
            {
                spRequestList.Add(spList.GetItemById(Int32.Parse(current) - 1000));
            }

            //converting the list of ListItems in to a list of VpnRequests
            foreach(ListItem item in spRequestList)
            {
                try
                {
                    clientContext.Load(item);
                    clientContext.ExecuteQuery();

                    VpnRequest temp = new VpnRequest();
                    temp.VPN_requestID = Int32.Parse((string)item[requestNames.internalID]);
                    temp.DateSubmitted = ((DateTime)item[requestNames.internalCreated]).ToString("MM/dd/yyyy");
                    temp.VPN_requestStatus = (string)item[requestNames.internalRequestStatus];
                    temp.VPN_accessEnd = ((DateTime)item[requestNames.internalAccessEnd]).ToString();
                    temp.VPN_accessStart = ((DateTime)item[requestNames.internalAccessStart]).ToString();
                    temp.VPN_recipientFirst = (string)item[requestNames.internalVpnRecipientFirst];
                    temp.VPN_recipientLast = (string)item[requestNames.internalVpnRecipientLast];
                    temp.Work_Phone = (string)item[requestNames.internalWorkPhone];
                    temp.VPN_recipientEmail = (string)item[requestNames.internalEmail];
                    temp.VPN_userCode = Convert.ToInt32(((double)item[requestNames.internalUserCode]));
                    temp.Manager = ((FieldUserValue)item[requestNames.internalManager]).LookupValue;
                    temp.Systems_List = (string)item[requestNames.internalSystemsList];
                    temp.VPN_justification = (string)item[requestNames.internalJustification];
                    temp.VPN_userDept = (string)item[requestNames.internalUserDept];
                    temp.Company_Name = (string)item[requestNames.internalCompanyName];
                    temp.Company_Other = (string)item[requestNames.internalCompanyOther];
                    temp.Office_Location = (string)item[requestNames.internalOfficeLocation];
                    temp.Office_Address = (string)item[requestNames.internalOfficeAddress];
                    temp.Machine_Owner = (string)item[requestNames.internalMachineOwner];
                    temp.VPN_userStatus = (string)item[requestNames.internalUserStatus];
                    temp.Agency = (string)item[requestNames.internalAgency];
                    temp.VPN_requestor = ((FieldUserValue)item[requestNames.internalCreatedBy]).LookupValue;
                    try
                    {
                        temp.Ext_code = Convert.ToInt32((double)item[requestNames.internalExtCode]);
                        temp.Reviewer_Comments = (string)item[requestNames.internalComments];
                    }
                    catch (System.ArgumentNullException e)
                    {
                        //extension code can be null, so we'll skip over this
                    }catch (System.NullReferenceException f)
                    {
                        //extension code can be null, so we'll skip over this 
                    }
                    currentRequests.Add(temp);
                }
                catch (System.ArgumentNullException e)
                {
                    //if something critical is null, we won't add it to the viewbag list
                    continue;
                }
            }
            return currentRequests;
        }

        private List<VpnRequest> orderList (List<VpnRequest> currentRequests)
        {
            //using bubble sort algorithm 
            int len = currentRequests.Count;
            while(true)
            {
                bool swapped = false;
                for (int i = 1 ; i < len ; i++)
                {
                    if(currentRequests[i-1].VPN_requestID > currentRequests[i].VPN_requestID)
                    {
                        //swap the two 
                        VpnRequest temp = currentRequests[i - 1];
                        currentRequests[i - 1] = currentRequests[i];
                        currentRequests[i] = temp;
                        swapped = true;
                     }
                }

                if (swapped == false)
                {
                    break;
                }
            }
            return currentRequests;
        }

    }
}
