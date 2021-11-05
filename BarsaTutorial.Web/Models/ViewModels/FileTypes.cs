using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Models.ViewModels
{
    public class GetFileTypeEdicationResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public List<GetEducationsResponse> Educations { get; set; } = new List<GetEducationsResponse>();
    }

    public class NewFileTypeRequest
    {
        public string Title { get; set; }
    }

    public class EditFileTypeRequest : NewCategoryRequest
    {
        public int ID { get; set; }
    }

    public class FileTypeViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ActionName { get; set; }


    }
}
