using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseEnrollment
{
    public class Course
    {
        private static int s_courseID = 2001;
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public int CourseDuration { get; set; }
        public int SeatsAvailable { get; set; }

        public Course(string courseName, string trainerName, int courseDuration, int seatsAvailable)
        {
            CourseID = $"CS{s_courseID++}";
            CourseName = courseName;
            TrainerName = trainerName;
            CourseDuration = courseDuration;
            SeatsAvailable = seatsAvailable;
        }
    }
}