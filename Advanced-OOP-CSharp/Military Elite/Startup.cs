namespace P08MilitaryElite
{
    using Core;
    using Interfaces;

    public class Startup
    {
        public static void Main()
        {
            IRunnable engine = new Engine();
            engine.Run();
        }
    }
}