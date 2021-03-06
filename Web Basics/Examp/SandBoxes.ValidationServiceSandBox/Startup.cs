﻿namespace SandBoxes.ValidationServiceSandBox
{
    using System;
    using Data;
    using Models.BindingModels;
    using Models.ValidationService;

    public class Startup
    {
        public static void Main()
        {
            var data = new SoftUniData(new SoftUniStoreContext());
            var gameValidationService = new GameValidationService(new ManageGameBindingModel
            {
                Title = "startCraft",
                Price = 0.34m,
                Size = 0.567m,
                Trailer = "a32dSqR",
                Description = "Abrakadabra",
                ImageThumbnail = "D:/img.jpeg"
            });
            foreach (var invalidProperty in gameValidationService.InvalidProperties)
            {
                Console.WriteLine($"{invalidProperty.Key} - {invalidProperty.Value}");
            }

            Console.WriteLine();
            var registration = new RegisterUserValidationService(
                new RegistrationUserBindingModel
                {
                    Email = "dfasg34",
                    Password = "dasfkjgh",
                    ConfirmPassword = "tr5yh",
                    FullName = string.Empty
                },
                false);
            foreach (var invalidProperty in registration.InvalidProperties)
            {
                Console.WriteLine($"{invalidProperty.Key} - {invalidProperty.Value}");
            }

            Console.WriteLine();
            var loginService = new LoginValidationService(
                new LoginUserBindingModel
                {
                    Email = "pesho",
                    Password = string.Empty
                },
                data.Users.GetAll());
            foreach (var invalidProperty in loginService.InvalidProperties)
            {
                Console.WriteLine($"{invalidProperty.Key} - {invalidProperty.Value}");
            }
        }
    }
}