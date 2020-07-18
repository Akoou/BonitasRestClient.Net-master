using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitasRestClient.Models
{
    public class Case
    {
        public string id { get; set; }
        public DateTime? end_date { get; set; }
        public long failedFlowNodes { get; set; }
        public object startedBySubstitute { get; set; }
        public DateTime? start { get; set; }
        public long activeFlowNodes { get; set; }
        public string state { get; set; }
        public long rootCaseId { get; set; }
        public Object started_by { get; set; }
        public object processDefinitionId { get; set; }
        public DateTime? last_update_date { get; set; }
        public string searchIndex1Label { get; set; }
        public string searchIndex2Label { get; set; }
        public string searchIndex3Label { get; set; }
        public string searchIndex4Label { get; set; }
        public string searchIndex5Label { get; set; }
        public string searchIndex1Value { get; set; }
        public string searchIndex2Value { get; set; }
        public string searchIndex3Value { get; set; }
        public string searchIndex4Value { get; set; }
        public string searchIndex5Value { get; set; }

        public Process getProcessDefinitionId()
        {
            return JsonConvert.DeserializeObject<Process>(processDefinitionId.ToString());
        }
        public User getStarted_by()
        {
            return JsonConvert.DeserializeObject<User>(started_by.ToString());
        }
        public User getStartedBySubstitute()
        {
            return JsonConvert.DeserializeObject<User>(startedBySubstitute.ToString());
        }
    }
}
