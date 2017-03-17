namespace Application
{
    using SimpleHttpServer;
    using SimpleMVC;

    public class Startup
    {
        private const string StartupNamespace = nameof(Application);

        public static void Main()
        {
            var httpServer = new HttpServer(8081, RoutesTable.Routes);
            MvcEngine.Run(httpServer, StartupNamespace);
        }
    }
}