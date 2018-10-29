using System;
namespace Core.Models
{
    public class AssetsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AssetsCategory()
        {
            Id = 0;
            Name = "";
            Description = "";
        }

        public AssetsCategory(int idnt) : this() {
            Id = idnt;
        }
    }
}
