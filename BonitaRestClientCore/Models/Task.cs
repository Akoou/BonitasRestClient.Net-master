using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitasRestClient.Models
{
    public class Task
    {
        public long id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string displayDescription { get; set; }
        public string state { get; set; }
        public DateTime? reached_state_date { get; set; }
        public DateTime? last_update_date { get; set; }
        public DateTime? dueDate { get; set; }
        public string priority { get; set; }

        public long processId { get; set; }
        public long parentCaseId { get; set; }
        public long rootCaseId { get; set; }
        public object rootContainerId { get; set; }

        public long executedBy { get; set; }
        public long executedBySubstitute { get; set; }
        public long actorId { get; set; }
        public long? assigned_id { get; set; }
        public DateTime? assigned_date { get; set; }

        public Process getRootContainerId()
        {
            return JsonConvert.DeserializeObject<Process>(rootContainerId.ToString());
        }

    }

    public enum State
    {
        ready, completed, failed
    }
}
