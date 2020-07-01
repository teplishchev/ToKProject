using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class CoursePersonalArea
    {
        public int CourseId { get; set; }

        public int PersonalAreaId { get; set; }

        public int CourseStructureId { get; set; }

        public CourseStructure CourseStructure { get; set; }
    }
}