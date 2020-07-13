using BonitasRestClient;
using BonitasRestClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BpmClient bpmClient = new BpmClient("http://192.168.1.8:8080", "Abdelhak", "1596");

            var p = bpmClient.GetProcesses("c=10&p=0&f=user_id=1").ToList();
            var c = bpmClient.GetCases("c=10&p=0&f=user_id=1").ToList();
            var t = bpmClient.GetTasks("c=10&p=0&f=user_id=1").ToList();
            var u = bpmClient.GetUsers("p=0&c=10&f=userName=Abdelhak").ToList();

            Console.WriteLine("Hello World!");
        }
    }
}
