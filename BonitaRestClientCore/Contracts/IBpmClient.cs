using BonitasRestClient.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitasRestClient.Contracts
{
    public interface IBpmClient
    {
        Task<Case> GetCaseAsync(string caseId);
        Case GetCase(string caseId);
        Task<IList<Case>> GetCasesAsync(String filter);
        IList<Case> GetCases(String filter);

        Task<Case> GetArchivedCaseAsync(string caseId);
        Case GetArchivedCase(string caseId);
        Task<IList<Case>> GetArchivedCasesAsync(String filter);
        IList<Case> GetArchivedCases(String filter);

        Task<Models.Task> GetTaskAsync(long TaskeId);
        Models.Task GetTask(long TaskeId);
        Task<IList<Models.Task>> GetTasksAsync(String filter);
        IList<Models.Task> GetTasks(String filter);

        Task<Models.Task> GetHumanTaskAsync(long TaskeId);
        Models.Task GetHumanTask(long TaskeId);
        Task<IList<Models.Task>> GetHumanTasksAsync(String filter);
        IList<Models.Task> GetHumanTasks(String filter);

        Task<Process> GetProcessAsync(string processId);
        Process GetProcess(string processId);
        Task<IList<Process>> GetProcessesAsync(String filter);
        IList<Process> GetProcesses(String filter);
        Task<ProcessConstraints> GetProcessConstraintsAsync(string processId);
        ProcessConstraints GetProcessConstraints(string processId);
        System.Threading.Tasks.Task ExecuteProcessAsync(string processId, JObject payload);
        void ExecuteProcess(string processId, JObject payload);
        Task<Process> UpdateProcessAsync(string processId, ProcessUpdateFields fields);
        Process UpdateProcess(string processId, ProcessUpdateFields fields);
    }
}
