namespace OnlineCourseEnrollment
{
    public enum Gender
    {
        Male,
        Female,
        Transgender
    }

    public class User
    {
        private static int s_registrationID = 1001;
        public string RegistrationID { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Qualification { get; set; }
        public string MobileNumber { get; set; }
        public string MailID { get; set; }

        public User(string userName, int age, Gender gender, string qualification, string mobileNumber, string mailID)
        {
            RegistrationID = $"SYNC{s_registrationID++}";
            UserName = userName;
            Age = age;
            Gender = gender;
            Qualification = qualification;
            MobileNumber = mobileNumber;
            MailID = mailID;
        }
    }
}