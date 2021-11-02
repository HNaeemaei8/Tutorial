namespace BarsaTutorial.Web.Models.Models
{
    public class Lesson
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int LessonCode { get; set; }
        public string FileAddress { get; set; }
        public int EducationID { get; set; }
        public Education Education { get; set; }
    }
}
