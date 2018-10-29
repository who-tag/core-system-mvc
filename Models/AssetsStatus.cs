using System;

namespace Core.Models
{
    public class AssetsStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AssetsStatus()
        {
            Id = 0;
            Name = "";
            Description = "";
        }

        public AssetsStatus(int idnt) : this() {
            Id = idnt;
        }
    }
}
