using System;

namespace OnlineCourseEnrollment
{
    public class Enrollment
    {
        private static int s_enrollmentID = 3001;
        public string EnrollmentID { get; set; }
        public string CourseID { get; set; }
        public string RegistrationID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(string courseID, string registrationID, DateTime enrollmentDate)
        {
            EnrollmentID = $"EMT{s_enrollmentID++}";
            CourseID = courseID;
            RegistrationID = registrationID;
            EnrollmentDate = enrollmentDate;
        }
    }
}