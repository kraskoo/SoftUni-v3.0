namespace Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Models.Utilities;
    using Interfaces;

    public class AdminService : Service
    {
        public AdminService(IDataProvidable data) : base(data)
        {
        }

        public void AddGame(AddGameBindingModel agvm)
        {
            var game = agvm.GetGameMappedToAddGameBindingModel();
            this.Data.Games.InsertOrUpdate(game);
            this.Data.SaveChanges();
        }

        public void EditGame(EditGameBindingModel egbm)
        {
            var game = egbm.GetGameMappedToEditGameBindingModel();
            this.Data.Games.InsertOrUpdate(game);
            this.Data.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var game = this.Data.Games.Find(id);
            foreach (var user in this.Data.Users.GetAll())
            {
                if (user.UserGames.Contains(game))
                {
                    user.UserGames.Remove(game);
                }
            }

            this.Data.Games.Delete(game);
            this.Data.SaveChanges();
        }

        public AddGameViewModel GetAddGame()
        {
            return new AddGameViewModel();
        }

        public DeleteGameViewModel GetDeleteGame(int id)
        {
            var game = this.Data.Games.Find(id);
            return game.GetGameMappedToDeleteGameViewModel();
        }

        public EditGameViewModel GetEditGame(int id)
        {
            var game = this.Data.Games.Find(id);
            return game.GetGameMappedToEditGameViewModel();
        }

        public IEnumerable<AdminGamesViewModel> GetGames()
        {
            var games = this.Data.Games.GetAll();
            var gameViewModels = games.Select(this.GetMappedHomeGameViewModel);
            return gameViewModels;
        }

        private AdminGamesViewModel GetMappedHomeGameViewModel(Game game)
        {
            return game.GetGameMappedToAdminGamesViewModel();
        }
    }
}