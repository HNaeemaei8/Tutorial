using BarsaTutorial.Web.Models.Models;
using System.Collections.Generic;

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

    public class NewEducationRequest
    {
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public IEnumerable<NewLessonRequest> Lessons { get; set; } = new HashSet<NewLessonRequest>();
    }

    public class NewLessonRequest
    {
        public string Title { get; set; }
        public int LessonCode { get; set; }
        public string FileAddress { get; set; }
    }

    public class EditEducationRequest
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public IEnumerable<EditLessonRequest> Lessons { get; set; } = new HashSet<EditLessonRequest>();
    }

    public class EditLessonRequest : NewLessonRequest
    {
        public int ID { get; set; }
    }
}
