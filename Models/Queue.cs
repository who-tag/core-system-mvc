using System;
using Core.Models;
using Core.Services;

namespace Core.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Concept Concept { get; set; }

        public Queue()
        {
            Id = 0;
            Name = "";
            Concept = new Concept();
        }

        public Queue(int idnt)
        {
            Id = idnt;
            Name = "";
            Concept = new Concept();
        }
    }

}
