using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class DiaEduVM
    {
        public DiaEduVM()
        {
            diabetesEducations = new List<DiabetesEducation>();
            titleList = new List<DiabetesEducation>();
        }
        public DiabetesEducation diabetesEducation { get; set; }
        public IEnumerable<DiabetesEducation> titleList { get; set; }
        public IEnumerable<DiabetesEducation> diabetesEducations { get; set; }
    }
}