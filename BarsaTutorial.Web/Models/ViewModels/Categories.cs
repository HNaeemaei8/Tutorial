using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Models.ViewModels
{

    public class GetCategoryEdicationResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public List<GetEducationsResponse> Educations { get; set; } = new List<GetEducationsResponse>();
    }

    public class NewCategoryRequest
    {
        public string Title { get; set; }
    }

    public class EditCategoryRequest : NewCategoryRequest
    {
        public int ID { get; set; }
    }
}
