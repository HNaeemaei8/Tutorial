namespace BarsaTutorial.Web.Models.ViewModels
{
    public class GetEducationsRequest
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public int? CategoryID { get; set; }
        public int? FileTypeID { get; set; }
    }

    public class GetEducationsResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public string CategoryTitle { get; set; }
        public string FileTypeTitle { get; set; }
        public int LessonCount { get; set; }
    }
}
