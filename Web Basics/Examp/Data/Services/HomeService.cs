namespace Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using Models.BindingModels;
    using Models.Utilities;
    using Models.ViewModels;

    public class HomeService : Service
    {
        public HomeService(IDataProvidable data) : base(data)
        {
        }

        public void BuyGame(BuyGameBindingModel bgbm)
        {
            var user = this.Data.Users.Find(bgbm.UserId);
            var gameToBuy = this.Data.Games.Find(bgbm.GameId);
            user.UserGames.Add(gameToBuy);
            this.Data.SaveChanges();
        }

        public IEnumerable<HomeGameViewModel> Games(User user, string filter)
        {
            if (string.IsNullOrEmpty(filter) || filter == "All")
            {
                var games = this.Data.Games.GetAll();
                var gameViewModels = games.Select(this.GetMappedToHomeGameViewModel);
                return gameViewModels;
            }
            else if (filter == "Owned")
            {
                var gameViewModels = user.UserGames.Select(this.GetMappedToHomeGameViewModel);
                return gameViewModels;
            }

            return null;
        }

        public DetailsGameViewModel GameDetails(int id)
        {
            var game = this.Data.Games.Find(id);
            return game?.GetGameMappedToDetailGameViewModel();
        }

        public HomeGameViewModel GetMappedToHomeGameViewModel(Game game)
        {
            return game.GetGameMappedToHomeGameViewModel();
        }
    }
}