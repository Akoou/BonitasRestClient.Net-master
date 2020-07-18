using BonitasRestClient;
using BonitasRestClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BpmClient bpmClient = new BpmClient("http://192.168.1.8:8080", "Abdelhak", "123456");

            var u = bpmClient.GetUsers("p=0&c=10&f=userName=Abdelhak").ToList();
            if (u.Count > 0)
            {
                var selectedUser = u[0];

                var p = bpmClient.GetProcesses($"c=10&p=0&f=user_id={selectedUser.id}").ToList();
                var c = bpmClient.GetCases($"c=10&p=0&f=user_id={selectedUser.id}").ToList();
                var t = bpmClient.GetTasks($"c=10&p=0&f=user_id={selectedUser.id}").ToList();
                var ac = bpmClient.GetArchivedCases($"c=10&p=0&f=user_id={selectedUser.id}").ToList();

                var ht = bpmClient.GetHumanTasks($"c=50&d=rootContainerId&f=state%3Dready&f=user_id%3D{selectedUser.id}&o=displayName+ASC&p=0").ToList();
                Process process = JsonConvert.DeserializeObject<Process>(ht[0].rootContainerId.ToString());
                var st = bpmClient.GetTask(40006);
                
                bpmClient.Assigned(40006, null);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
