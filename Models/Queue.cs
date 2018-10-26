using System;
using Core.Models;
using Core.Services;

namespace Core.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public Concept Concept { get; set; }

        public Queue()
        {
            Id = 0;
            Code = "";
            Name = "";
            Route = "";
            Concept = new Concept();
        }

        public Queue(int idnt)
        {
            Id = idnt;
            Code = "";
            Name = "";
            Route = "";
            Concept = new Concept();
        }
    }

}
