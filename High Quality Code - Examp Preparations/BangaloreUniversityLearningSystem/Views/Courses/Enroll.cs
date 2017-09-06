namespace BangaloreUniversityLearningSystem.Views.Courses
{
    using System.Text;
    using BangaloreUniversityLearningSystem.Models;

    public class Enroll : View
    {
        public Enroll(Course course)
            : base(course)
        {
        }

        public override void BuildViewResult(StringBuilder viewResult)
        {
            var course = this.Model as Course;
            viewResult.AppendFormat("Student successfully enrolled in course {0}.", course.Name).AppendLine();
        }
    }
}