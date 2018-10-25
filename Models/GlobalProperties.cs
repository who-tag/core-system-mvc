using System;
namespace Core.Models
{
    public class GlobalProperties
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public GlobalProperties()
        {
            Id = 0;
            Item = "";
            Value = "";
            Description = "";
        }
    }
}
