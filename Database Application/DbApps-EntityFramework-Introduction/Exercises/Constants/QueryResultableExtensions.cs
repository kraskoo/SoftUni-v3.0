namespace Exercises.Constants
{
    using System.Data.Entity;
    using CodeFirstFromDatabase;
    using Commands;
    using GringottsCodeFirstFromDatabase;
    using Interfaces;
    using Models.Queries;
    using Models.Writers;

    public static class QueryResultableExtensions
    {
        public static void FirstLetterExercise(this GringottsContext context)
        {
            context.Exercies(new FirstLetter(), "FirstLetter.txt");
        }

        public static void FindEmployeesByFirstNameStartingWithSAExercise(
            this SoftuniContext context)
        {
            context.Exercies(
                new FindEmployeesByFirstNameStartingWithSA(),
                "FindEmployeesByFirstNameStartingWithSA.txt");
        }

        public static void IncreaseSalariesExercise(this SoftuniContext context)
        {
            context.Exercies(new IncreaseSalaries(), "IncreaseSalaries.txt");
        }

        public static void FindLatest10ProjectsExercise(this SoftuniContext context)
        {
            context.Exercies(new FindLatest10Projects(), "FindLatest10Projects.txt");
        }

        public static void NativeSQLQueryExercisePartTwo(this SoftuniContext context)
        {
            context.Exercies(
                new NativeSQLQueryNativeQueryPart(), "NativeSQLQuery.txt", true);
        }

        public static void NativeSQLQueryExercisePartOne(this SoftuniContext context)
        {
            context.Exercies(new NativeSQLQueryCodeFirstPart(), "NativeSQLQuery.txt");
        }

        public static void DepartmentsWithMoreThan5EmployeesExercise(
               this SoftuniContext context)
        {
            context.Exercies(
                new DepartmentsWithMoreThan5Employees(),
                "DepartmentsWithMoreThan5Employees.txt");
        }

        public static void EmployeeWithId147SortedByProjectNamesExercise(
            this SoftuniContext context)
        {
            context.Exercies(
                new EmployeeWithId147SortedByProjectNames(),
                "EmployeeWithId147SortedByProjectNames.txt");
        }

        public static void AddressesByTownNameExercise(this SoftuniContext context)
        {
            context.Exercies(new AddressesByTownName(), "AddressesByTownName.txt");
        }

        public static void FindEmployeesInPeriodExercise(this SoftuniContext context)
        {
            context.Exercies(new FindEmployeesInPeriod(), "FindEmployeesInPeriod.txt");
        }

        public static void DeleteProjectByIdExercise(this SoftuniContext context)
        {
            context.Exercies(new DeleteProjectById(), "DeleteProjectById.txt");
        }

        public static void AddingNewAddressAndUpdatingEmployeeExercise(
               this SoftuniContext context)
        {
            context.Exercies(
                new AddingNewAddressAndUpdatingEmployee(),
                "AddingNewAddressAndUpdatingEmployee.txt");
        }

        public static void EmployeesFromSeattleExercise(this SoftuniContext context)
        {
            context.Exercies(
                new EmployeesFromSeattle(),
                "EmployeesFromSeattle.txt");
        }

        public static void EmployeesWithSalaryOver50000Exercise(this SoftuniContext context)
        {
            context.Exercies(
                new EmployeesWithSalaryOver50000(),
                "EmployeesWithSalaryOver50000.txt");
        }

        public static void EmployeesFullInformationExercise(this SoftuniContext context)
        {
            context.Exercies(new EmployeesFullInformation(), "EmployeeFullInformation.txt");
        }

        private static void Exercies<T>(
            this T context,
            IQueryResultable<T> queryResultable,
            string fileName,
            bool hasSetToAppend = false) where T : DbContext
        {
            var command = new ExerciseCommand<T>(context);
            command.Execute(queryResultable, new FileWriter(fileName, hasSetToAppend));
        }
    }
}