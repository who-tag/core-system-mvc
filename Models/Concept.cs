using System;
namespace Core.Models
{
    public class Concept
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Concept()
        {
            Id = 0;
            Name = "";
        }
    }
}
