namespace P08MilitaryElite.Interfaces
{
    public interface IOutputWriter
    {
        void WriteLine(string message);

        void WriteLine(object obj);
    }
}