namespace Models.Utilities
{
    using System;
    using System.Linq;
    using BindingModels;
    using Enums;
    using ViewModels;

    public static class TypeExtensions
    {
        public static User GetMappedToRegisterUserBindingModel(
            this RegistrationUserBindingModel rubm, UserRole role)
        {
            return Mapper.GetMapped(rubm, user => new User
            {
                Email = rubm.Email,
                Password = rubm.Password,
                FullName = rubm.FullName,
                UserRole = role
            });
        }

        public static Game GetGameMappedToAddGameBindingModel(
           this AddGameBindingModel agdm)
        {
            return Mapper.GetMapped(agdm, game => new Game
            {
                Title = agdm.Title,
                Description = agdm.Description,
                Price = agdm.Price,
                Size = agdm.Size,
                ImageThumbnail = agdm.ImageThumbnail,
                Trailer = agdm.Trailer,
                ReleaseDate = agdm.ReleaseDate
            });
        }

        public static Game GetGameMappedToEditGameBindingModel(
            this EditGameBindingModel egdm)
        {
            return Mapper.GetMapped(egdm, game => new Game
            {
                Id = egdm.Id,
                Title = egdm.Title,
                Description = egdm.Description,
                Price = egdm.Price,
                Size = egdm.Size,
                ImageThumbnail = egdm.ImageThumbnail,
                Trailer = egdm.Trailer
            });
        }

        public static Game GetAddGameViewModelToGame(this AddGameViewModel agvm)
        {
            return Mapper.GetMapped(agvm, game => new Game
            {
                Title = agvm.Title,
                Description = agvm.Description,
                ImageThumbnail = agvm.ImageThumbnail,
                Price = agvm.Price,
                Size = agvm.Size,
                Trailer = agvm.Trailer,
                ReleaseDate = agvm.ReleaseDate
            });
        }

        public static DeleteGameViewModel GetGameMappedToDeleteGameViewModel(this Game game)
        {
            return Mapper.GetMapped(game, edvm => new DeleteGameViewModel
            {
                Id = game.Id,
                Title = game.Title
            });
        }

        public static EditGameViewModel GetGameMappedToEditGameViewModel(this Game game)
        {
            return Mapper.GetMapped(game, edvm => new EditGameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Thumbnail = game.ImageThumbnail,
                Price = game.Price,
                Size = game.Size,
                Trailer = game.Trailer
            });
        }

        public static AdminGamesViewModel GetGameMappedToAdminGamesViewModel(this Game game)
        {
            return Mapper.GetMapped(game, agvm => new AdminGamesViewModel
            {
                Title = game.Title,
                Price = game.Price,
                Size = game.Size,
                GameId = game.Id
            });
        }

        public static HomeGameViewModel GetGameMappedToHomeGameViewModel(this Game game)
        {
            return Mapper.GetMapped(game, hgvm => new HomeGameViewModel
            {
                Id = game.Id,
                ImageThumbnail = game.ImageThumbnail,
                Title = game.Title,
                Price = game.Price,
                Size = game.Size,
                Description = string.Join("", game.Description.ToCharArray().Take(300))
            });
        }

        public static DetailsGameViewModel GetGameMappedToDetailGameViewModel(this Game game)
        {
            return Mapper.GetMapped(game, dgvm => new DetailsGameViewModel
            {
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate.GetValueOrDefault(),
                Size = game.Size,
                Trailer = game.Trailer
            });
        }

        public static Login GetLoginMappedToUser(this User user, string sessionId)
        {
            return Mapper.GetMapped(user, login => new Login
            {
                User = user,
                IsActive = true,
                SessionId = sessionId
            });
        }

        public static string GetFormatedDate(this DateTime date, bool hasSetTime = true)
        {
            if (!hasSetTime)
            {
                return $"{date:dd'/'MM'/'yyyy}";
            }

            return $"{date:M'/'d'/'yyyy h:mm:ss tt}";
        }
    }
}