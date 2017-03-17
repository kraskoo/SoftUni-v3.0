namespace SandBoxes.DataSandbox
{
    using System;
    using System.Linq;
    using Data;
    using Data.Interfaces;
    using Models.Utilities;

    public static class Startup
    {
        public static void Main()
        {
            IDataProvidable data = new SoftUniData(new SoftUniStoreContext());
            var firstUser = data.Users.Find(1);
            Console.WriteLine(firstUser.UserRole);
            var firstGameInfo = GetGameInfo(data);
            Console.WriteLine(firstGameInfo);
            var gamesCount = data.Games.GetAll().Count();
            Console.WriteLine($"Games count: {gamesCount}");
        }

        private static string GetGameInfo(IDataProvidable data)
        {
            var firstGame = data.Games.Find(1);
            var normalGameViewModel = firstGame.GetGameMappedToHomeGameViewModel();
            return normalGameViewModel.ToString();
        }
    }
}