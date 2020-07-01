using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class StudyViewModel
    {
        public Paragraph Paragraph { get; set; }

        public List<Test> Tests { get; set; }
    }
}