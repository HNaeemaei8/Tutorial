using System.Collections.Generic;

namespace BarsaTutorial.Web.Models.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public IEnumerable<Education> Educations { get; set; } = new HashSet<Education>();
    }
}
