using System.Collections.Generic;

namespace BarsaTutorial.Web.Models.Models
{
    public class FileType
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public List<Education> Educations { get; set; } = new List<Education>();

    }
}
