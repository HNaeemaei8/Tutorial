using BarsaTutorial.Web.Models.Models;
using Microsoft.AspNetCore.Http;
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

        public List<LessonViewModel> Lessons { get; set; } = new List<LessonViewModel>();
    }

    

    public class NewEducationRequest
    {
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public List<NewLessonRequest> Lessons { get; set; } = new List<NewLessonRequest>();
    }

    public class NewLessonRequest
    {
        public string Title { get; set; }
        public int LessonCode { get; set; }
        public IFormFile FileAddress { get; set; }
    }

    public class EditEducationRequest
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int FileTypeID { get; set; }
        public List<EditLessonRequest> Lessons { get; set; } = new List<EditLessonRequest>();
    }

    public class EditLessonRequest : NewLessonRequest
    {
        public int ID { get; set; }
        public string OldFileAddress { get; set; }

    }
}
