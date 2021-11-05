using System.Collections.Generic;

namespace BarsaTutorial.Web.Models.Models
{
    public class Education
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public Category Category { get; set; }
        public FileType FileType { get; set; }
    }
}
