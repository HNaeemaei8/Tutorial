using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarsaTutorial.Web.Models.ViewModels
{
    public class LessonViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int LessonCode { get; set; }
        public string FileAddress { get; set; }
    }
}
