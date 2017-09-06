namespace BangaloreUniversityLearningSystem.Views
{
    using System.Text;
    using BangaloreUniversityLearningSystem.Interfaces;

    public abstract class View : IView
    {
        protected View(object model)
        {
            this.Model = model;
        }

        public object Model { get; }

        public string Display()
        {
            var viewResult = new StringBuilder();
            this.BuildViewResult(viewResult);
            return viewResult.ToString().Trim();
        }

        public abstract void BuildViewResult(StringBuilder viewResult);
    }
}