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

        public IEnumerable<GetEducationsResponse> Educations { get; set; } = new HashSet<GetEducationsResponse>();
    }

    public class NewFileTypeRequest
    {
        public string Title { get; set; }
    }

    public class EditFileTypeRequest : NewCategoryRequest
    {
        public int ID { get; set; }
    }
}
