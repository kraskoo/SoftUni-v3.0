#define SoftUniContext
#define GrigottsContext
namespace Exercises
{
    using CodeFirstFromDatabase;
    using Constants;
    using GringottsCodeFirstFromDatabase;

    public class Startup
    {
        public static void Main()
        {
#if (GrigottsContext)
            using (var context = DbContextExtensions.GetContextType<GringottsContext>())
            {
                // context.FirstLetterExercise();
            }
#endif

#if (SoftUniContext)
            using (var context = DbContextExtensions.GetContextType<SoftuniContext>())
            {
                // context.FindEmployeesByFirstNameStartingWithSAExercise();
                // context.IncreaseSalariesExercise();
                // context.FindLatest10ProjectsExercise();
                context.NativeSQLQueryExercisePartOne();
                context.NativeSQLQueryExercisePartTwo();
                // context.DepartmentsWithMoreThan5EmployeesExercise();
                // context.EmployeeWithId147SortedByProjectNamesExercise();
                // context.AddressesByTownNameExercise();
                // context.FindEmployeesInPeriodExercise();
                // context.DeleteProjectByIdExercise();
                // context.AddingNewAddressAndUpdatingEmployeeExercise();
                // context.EmployeesFromSeattleExercise();
                // context.EmployeesWithSalaryOver50000Exercise();
                // context.EmployeesFullInformationExercise();
            }
#endif
        }
    }
}