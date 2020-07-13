using BonitasRestClient.Contracts;
using BonitasRestClient.Models;
using BonitasRestClient.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BonitasRestClient
{
    public class BpmClient : IBpmClient, IAuthorisation, IIdentityClient
    {
        private const string _ApiResource = "bonita/API/bpm";
        private const string _ApiIdentity = "bonita/API/identity";
        private string _BaseUrl;
        private RestClient _RestClient;
        private string _Username;
        private string _Password;
        private IList<RestResponseCookie> _Cookies;

        /// <summary>
        /// Create a new BPM client that connects to the default engine endpoint (http://localhost:8080/) and default user credentials
        /// </summary>
        public BpmClient()
        {
            _BaseUrl = "http://localhost:8080/";
            Init(_BaseUrl);
        }

        /// <summary>
        /// Create a new BPM client that connects to a specified URL, supply username and password if you don't want to use default user credentials
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public BpmClient(string baseUrl, string username = null, string password = null)
        {
            Init(baseUrl, username, password);
        }

        private void Init(string baseUrl, string username = null, string password = null)
        {
            _BaseUrl = baseUrl.ValidateBaseUrl();

            _RestClient = new RestClient(_BaseUrl);

            if (username != null || password != null)
            {
                _Username = username;
                _Password = password;
            }
            else
            {
                _Username = "walter.bates";
                _Password = "bpm";
            }
        }

        public void Login()
        {
            var request = new RestRequest("bonita/loginservice", Method.POST);
            request.AddQueryParameter("username", _Username);
            request.AddQueryParameter("password", _Password);
            request.AddQueryParameter("redirect", "false");

            var response = HandleResponse(_RestClient.Execute(request));

            _Cookies = response.Cookies;
        }

        public void Logout()
        {
            var request = new RestRequest("bonita/logoutservice", Method.GET);
            request.AddQueryParameter("redirect", "false");
            var response = HandleResponse<string>(_RestClient.Execute(request));
            _Cookies = null;
        }

        /// <summary>
        /// Get a list of active cases.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Case>> GetCasesAsync(String filter="p=0&c=10")
        {
            Login();

            var request = new RestRequest(_ApiResource + "/case?" + filter, Method.GET)
                .AddCookies(_Cookies)
                .Execute(_RestClient);

            IList<Case> caseModels = null;

            try
            {
                caseModels = HandleResponse<IList<Case>>(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return caseModels;
        }

        /// <summary>
        /// Get a specific instance of an open case.
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public async Task<Case> GetCaseAsync(string caseId)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/case/{id}", Method.GET)
                .AddCookies(_Cookies);
            request.AddParameter("id", caseId, ParameterType.UrlSegment);

            Case caseModel = null;

            try
            {
                caseModel = HandleResponse<Case>(_RestClient.Execute(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return caseModel;
        }

        /// <summary>
        /// Get a list of processes that exist on the Engine
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Process>> GetProcessesAsync(String filter = "p=0&c=10")
        {
            Login();
            

            var request = new RestRequest(_ApiResource + "/process?" + filter, Method.GET)
                .AddCookies(_Cookies)
                .Execute(_RestClient);

            IList<Process> processes = null;

            try
            {
                processes = HandleResponse<IList<Process>>(request);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return processes;
        }

        /// <summary>
        /// Get a specific process
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public async Task<Process> GetProcessAsync(string processId)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/process/{id}", Method.GET)
                .AddCookies(_Cookies);
            request.AddParameter("id", processId, ParameterType.UrlSegment);

            Process process = null;

            try
            {
                process = HandleResponse<Process>(_RestClient.Execute(request));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return process;
        }

        /// <summary>
        /// Get process constraint properties in order to instantiate a new process
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public async Task<ProcessConstraints> GetProcessConstraintsAsync(string processId)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/process/{id}/contract", Method.GET)
                .AddCookies(_Cookies);

            request.AddParameter("id", processId, ParameterType.UrlSegment);
            ProcessConstraints constraints = null;

            try
            {
                constraints = HandleResponse<ProcessConstraints>(_RestClient.Execute(request));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return constraints;
        }

        /// <summary>
        /// Instantiate a new process by providing constraint field with values. Use GetProcessConstraints method to get the fields.
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task ExecuteProcessAsync(string processId, JObject values)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/process/{id}/instantiation", Method.POST)
                .AddCookies(_Cookies);
            request.AddParameter("id", processId, ParameterType.UrlSegment);
            request.AddJsonBody(values.ToString());

            try
            {
                var response = HandleResponse<string>(_RestClient.Execute(request));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }
        }

        /// <summary>
        /// Update a process
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public async Task<Process> UpdateProcessAsync(string processId, ProcessUpdateFields fields)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/process/{id}", Method.PUT)
                .AddCookies(_Cookies);
            request.AddParameter("id", processId, ParameterType.UrlSegment);
            request.AddJsonBody(JsonConvert.SerializeObject(fields));

            Process process = null;

            try
            {
                process = HandleResponse<Process>(_RestClient.Execute(request));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return process;
        }

        public Case GetCase(string caseId)
        {
            return GetCaseAsync(caseId).Result;
        }

        public IList<Case> GetCases(String filter = "p=0&c=10")
        {
            return GetCasesAsync(filter).Result;
        }

        public Process GetProcess(string processId)
        {
            return GetProcessAsync(processId).Result;
        }

        public IList<Process> GetProcesses(String filter = "p=0&c=10")
        {
            return GetProcessesAsync(filter).Result;
        }

        public void ExecuteProcess(string processId, JObject payload)
        {
            ExecuteProcessAsync(processId, payload);
        }

        public ProcessConstraints GetProcessConstraints(string processId)
        {
            return GetProcessConstraintsAsync(processId).Result;
        }

        public Process UpdateProcess(string processId, ProcessUpdateFields fields)
        {
            return UpdateProcessAsync(processId, fields).Result;
        }

        private T HandleResponse<T>(IRestResponse response)
        {
            HandleResponseCode(response);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private IRestResponse HandleResponse(IRestResponse response)
        {
            HandleResponseCode(response);
            return response;
        }

        private static void HandleResponseCode(IRestResponse response)
        {
            if (response.StatusCode == 0)
                throw new Exception("The remote service is not available.");
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(string.Format("'{0}' responded with '{1}'.", response.Request.Resource, response.StatusCode), new Exception(response.Content));
        }

        public Task<Models.Task> GetTaskAsync(long TaskeId)
        {
            Login();

            var request = new RestRequest(_ApiResource + "/task/{id}", Method.GET)
                .AddCookies(_Cookies);
            request.AddParameter("id", TaskeId, ParameterType.UrlSegment);

            Models.Task task = null;

            try
            {
                task = HandleResponse<Models.Task>(_RestClient.Execute(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return Task.FromResult(task);
        }

        public Models.Task GetTask(long TaskeId)
        {
            return GetTaskAsync(TaskeId).Result;
        }

        public Task<IList<Models.Task>> GetTasksAsync(String filter = "p=0&c=10")
        {
            Login();

            var request = new RestRequest(_ApiResource + "/task?" + filter, Method.GET)
                .AddCookies(_Cookies)
                .Execute(_RestClient);

            IList<Models.Task> taskModels = null;

            try
            {
                taskModels = HandleResponse<IList<Models.Task>>(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return Task.FromResult( taskModels);
        }

        public IList<Models.Task> GetTasks(String filter = "p=0&c=10")
        {
            return GetTasksAsync(filter).Result;
        }

        public Task<User> GetUserAsync(long userId)
        {
            Login();

            var request = new RestRequest(_ApiIdentity + "/user/{id}", Method.GET)
                .AddCookies(_Cookies);
            request.AddParameter("id", userId, ParameterType.UrlSegment);

            Models.User user = null;

            try
            {
                user = HandleResponse<Models.User>(_RestClient.Execute(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return Task.FromResult(user);
        }

        public User GetUser(long userId)
        {
            return GetUserAsync(userId).Result;
        }

        public Task<IList<User>> GetUsersAsync(String filter = "p=0&c=10")
        {
            Login();

            var request = new RestRequest(_ApiIdentity + "/user?" + filter, Method.GET)
                .AddCookies(_Cookies)
                .Execute(_RestClient);

            IList<Models.User> userodels = null;

            try
            {
                userodels = HandleResponse<IList<Models.User>>(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Logout();
            }

            return Task.FromResult(userodels);
        }

        public IList<User> GetUsers(String filter = "p=0&c=10")
        {
            return GetUsersAsync(filter).Result;
        }
    }
}
