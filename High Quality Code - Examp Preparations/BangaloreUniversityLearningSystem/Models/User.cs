namespace BangaloreUniversityLearningSystem.Models
{
    using System;
    using System.Collections.Generic;
    using BangaloreUniversityLearningSystem.Utilities;

    public class User
    {
        private string username;
        private string password;

        public User(string username, string password, Role role)
        {
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.Courses = new List<Course>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    string message = string.Format("The username must be at least 5 symbols long.");
                    throw new ArgumentException(message);
                }
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 6)
                {
                    string message = string.Format("The password must be at least 6 symbols long.");
                    throw new ArgumentException(message);
                }

                string passwordHashed = HashUtilities.HashPassword(value);

                this.password = passwordHashed;
            }
        }

        public Role Role { get; private set; }

        public IList<Course> Courses { get; private set; }

        public void AddToCourse(Course course)
        {
            this.Courses.Add(course);
        }
    }
}