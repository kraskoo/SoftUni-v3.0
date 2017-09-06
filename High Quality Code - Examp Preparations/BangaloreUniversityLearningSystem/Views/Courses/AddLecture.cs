namespace BangaloreUniversityLearningSystem.Views.Courses
{
    using System.Text;
    using BangaloreUniversityLearningSystem.Models;

    public class AddLecture : View
    {
        public AddLecture(Course course)
            : base(course)
        {
        }

        public override void BuildViewResult(StringBuilder viewResult)
        {
            var course = this.Model as Course;
            viewResult.AppendFormat("Lecture successfully added to course {0}.", course.Name).AppendLine();
        }
    }
}