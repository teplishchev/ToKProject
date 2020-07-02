using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class CoursesInAreaModel
    {
        public Dictionary<int, List<Triplet>> courses { get; set; }
    }

    public class Triplet
    {
        public int ParagraphId { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
    }
}