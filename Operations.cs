using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseEnrollment
{
    public class Operations
    {
        private static List<User> users = [];
        private static List<Course> courses = [];
        private static List<Enrollment> enrollments = [];
        private static User currentLoggedInUser;

        public static void MainMenu()
        {
            bool flag = true;

            do
            {
                Console.WriteLine("1. User Registration\n2. User Login\n3. Exit");
                string userChoice = Console.ReadLine().Trim();

                if (userChoice == "1")
                {
                    UserRegistration();
                }
                else if (userChoice == "2")
                {
                    UserLogin();
                }
                else if (userChoice == "3")
                {
                    flag = false;
                    Console.WriteLine("Thanks for visiting. Gooodbye!");
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again");
                    MainMenu();
                }
            } while (flag);
        }

        public static void UserRegistration()
        {
            Console.WriteLine("Enter your name");
            string userName = Console.ReadLine().Trim();
            Console.WriteLine("Enter your age");
            int age = int.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Enter your gender (Male/Female/Transgender)");
            Gender gender = Enum.Parse<Gender>(Console.ReadLine().Trim(), true);
            Console.WriteLine("Enter your qualification");
            string qualification = Console.ReadLine().Trim();
            Console.WriteLine("Enter your mobile number");
            string mobileNumber = Console.ReadLine().Trim();
            Console.WriteLine("Enter your mail ID");
            string mailID = Console.ReadLine().Trim();

            User user = new(userName, age, gender, qualification, mobileNumber, mailID);
            users.Add(user);
            Console.WriteLine($"User ID: {user.RegistrationID}");

            MainMenu();
        }

        public static void UserLogin()
        {
            Console.WriteLine("Enter User ID");
            string userID = Console.ReadLine().ToUpper().Trim();

            bool flag = true;
            foreach (User user in users)
            {
                if (user.RegistrationID == userID)
                {
                    flag = false;
                    currentLoggedInUser = user;
                    SubMenu();
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid User ID. Please enter a valid one");
                UserLogin();
            }
        }

        public static void SubMenu()
        {
            Console.WriteLine("a. Enroll Course\nb. View Enrollment History\nc. Next Enrollment\nd. Exit");
            string userChoice = Console.ReadLine().ToLower().Trim();

            if (userChoice == "a")
            {
                EnrollCourse();
            }
            else if (userChoice == "b")
            {
                ViewEnrollmentHistory();
            }
            else if (userChoice == "c")
            {
                NextEnrollment();
            }
            else if (userChoice == "d")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again");
                SubMenu();
            }
        }

        public static void EnrollCourse()
        {
            // loop through courses list to display available courses
            foreach (Course course in courses)
            {
                Console.WriteLine($"Course ID: {course.CourseID}\nCourse Name: {course.CourseName}\nTrainer Name: {course.TrainerName}\nCourse Duration: {course.CourseDuration}\nSeats Available: {course.SeatsAvailable}\n");
            }

            // ask user to pick one course
            Console.WriteLine("Enter course ID to enroll");
            string courseID = Console.ReadLine().ToUpper().Trim();

            // loop through courses list to validate course ID
            bool flag = true;
            foreach (Course course1 in courses)
            {
                if (course1.CourseID == courseID)
                {
                    flag = false;
                    // check seat availability
                    if (course1.SeatsAvailable > 0)
                    {
                        //check if user has already enrolled in any courses
                        int registrationCount = 0;
                        foreach (Enrollment enrollment in enrollments)
                        {
                            if (enrollment.RegistrationID == currentLoggedInUser.RegistrationID)
                            {
                                // keep track of the total enrolled coursess
                                registrationCount++;
                            }
                        }

                        //Console.WriteLine(registrationCount);

                        // continue checking eligibility for enrolling
                        if (registrationCount >= 2)
                        {
                            // find current users' enrolled courses courses
                            List<Enrollment> currentUserEnrollments = [];
                            foreach (Enrollment enrollment1 in enrollments)
                            {
                                if (enrollment1.RegistrationID == currentLoggedInUser.RegistrationID)
                                {
                                    currentUserEnrollments.Add(enrollment1);
                                }
                            }

                            // find course with least duration
                            Course leastDurationCourse = courses[0];
                            foreach (Enrollment enrollment2 in currentUserEnrollments)
                            {
                                for (int i = 0; i < courses.Count; i++)
                                {
                                    if (enrollment2.CourseID == courses[i].CourseID && courses[i].CourseDuration < leastDurationCourse.CourseDuration)
                                    {
                                        leastDurationCourse = courses[i];
                                    }
                                }
                            }
                            // Console.WriteLine(leastDurationCourse.CourseName);

                            // check enrollments and display next available date
                            foreach (Enrollment enrollment in enrollments)
                            {
                                if (leastDurationCourse.CourseID == enrollment.CourseID)
                                {
                                    DateTime endDate = enrollment.EnrollmentDate.AddMonths(leastDurationCourse.CourseDuration);
                                    Console.WriteLine($"You have already enrolled in two courses. You can enroll in the next course on {endDate.ToString("dd/MM/yyyy")}");
                                }
                            }
                        }
                        else
                        {
                            // enroll user
                            Enrollment enrollment = new(course1.CourseID, currentLoggedInUser.RegistrationID, DateTime.Now);
                            enrollments.Add(enrollment);
                            Console.WriteLine("Enrollment successful");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Seats are not available for the current course");
                        SubMenu();
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid Course ID. Please enter a valid one to proceed\n");
                EnrollCourse();
            }
            SubMenu();
        }

        public static void ViewEnrollmentHistory()
        {
            bool flag = true;
            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.RegistrationID == currentLoggedInUser.RegistrationID)
                {
                    flag = false;
                    Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentID}\nStudent ID: {enrollment.RegistrationID}\nCourse ID: {enrollment.CourseID}\nEnrollment Date: {enrollment.EnrollmentDate.ToString("dd/MM/yyyy")}\n");
                }
            }
            if (flag)
            {
                Console.WriteLine("No Enrollment History found");
            }
            SubMenu();
        }

        public static void NextEnrollment()
        {
            bool flag = true;
            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.RegistrationID == currentLoggedInUser.RegistrationID)
                {
                    flag = false;

                    bool flag1 = true;
                    foreach (Course course in courses)
                    {
                        DateTime endDate = enrollment.EnrollmentDate.AddMonths(course.CourseDuration);
                        if (enrollment.CourseID == course.CourseID)
                        {
                            flag1 = false;

                            Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentID}\nStudent ID: {enrollment.RegistrationID}\nCourse ID: {enrollment.CourseID}\nNext Enrollment Date: {endDate.ToString("dd/MM/yyyy")}\n");
                        }
                    }
                    if (flag1)
                    {

                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("No Enrollment History found");
            }
            SubMenu();
        }

        public static void DefaultData()
        {
            // create user objects
            User user1 = new("Star Lord", 30, Gender.Male, "ME", "9938388333", "star@gmail.com");
            User user2 = new("Gamora", 25, Gender.Female, "BE", "9944444455", "gamora@gmail.com");

            // add user objects to users list
            users.Add(user1);
            users.Add(user2);

            // create course objects
            Course course1 = new("C#", "Star Lord", 5, 0);
            Course course2 = new("HTML", "Gamora", 2, 5);
            Course course3 = new("CSS", "Tony Star", 2, 3);
            Course course4 = new("JS", "Peter Parker", 3, 1);
            Course course5 = new("TS", "Thanos", 1, 2);

            // add course objects to courses list
            courses.Add(course1);
            courses.Add(course2);
            courses.Add(course3);
            courses.Add(course4);
            courses.Add(course5);

            // create enrollment objects
            Enrollment enrollment1 = new(course1.CourseID, user1.RegistrationID, DateTime.Parse("28/01/2024"));
            Enrollment enrollment2 = new(course3.CourseID, user1.RegistrationID, DateTime.Parse("15/02/2024"));
            Enrollment enrollment3 = new(course4.CourseID, user2.RegistrationID, DateTime.Parse("18/02/2024"));
            Enrollment enrollment4 = new(course2.CourseID, user2.RegistrationID, DateTime.Parse("20/02/2024"));

            // add enrollment objects to enrollments list
            enrollments.Add(enrollment1);
            enrollments.Add(enrollment2);
            enrollments.Add(enrollment3);
            enrollments.Add(enrollment4);
        }
    }
}