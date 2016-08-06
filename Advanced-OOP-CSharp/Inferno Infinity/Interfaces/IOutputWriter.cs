namespace P10InfernoInfinity.Interfaces
{
    public interface IOutputWriter
    {
        void WriteLine(string message);

        void WriteLine(object obj);
    }
}