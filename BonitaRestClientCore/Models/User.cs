using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitasRestClient.Models
{
    public class User
    {
        public DateTime? last_connection { get; set; }
        public long created_by_user_id { get; set; }
        public DateTime? creation_date { get; set; }
        public long id { get; set; }
        public string icon { get; set; }
        public bool enabled { get; set; }
        public string title { get; set; }
        public string job_title { get; set; }
        public string userName { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string password { get; set; }
        public DateTime? last_update_date { get; set; }
        public long manager_id { get; set; }
        public ProfessionalData professional_data { get; set; }
    }
}
