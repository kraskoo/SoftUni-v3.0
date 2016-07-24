namespace P08PetClinic
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            ClinicRepo clinics = new ClinicRepo();
            int commandsRepeat = int.Parse(Console.ReadLine());
            for (int i = 0; i < commandsRepeat; i++)
            {
                string input = Console.ReadLine();
                string[] inputData =
                    input.Split(
                        new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string command = inputData[0];
                try
                {
                    switch (command)
                    {
                        case "Create":
                            CreateCommand(inputData, clinics);
                            break;
                        case "Add":
                            AddPetToClinicCommand(clinics, inputData);
                            break;
                        case "Release":
                            ReleaseCommand(clinics, inputData);
                            break;
                        case "HasEmptyRooms":
                            HasEmptyCommand(clinics, inputData);
                            break;
                        case "Print":
                            PrintCommand(inputData, clinics);
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }

        private static void CreateCommand(string[] inputData, ClinicRepo clinics)
        {
            string secondCreateArgument = inputData[1];
            if (secondCreateArgument.Equals("Clinic"))
            {
                clinics.AddClinic(inputData[2], int.Parse(inputData[3]));
            }
            else if (secondCreateArgument.Equals("Pet"))
            {
                clinics.AddPet(inputData[2], int.Parse(inputData[3]), inputData[4]);
            }
        }

        private static void AddPetToClinicCommand(ClinicRepo clinics, string[] inputData)
        {
            Console.WriteLine(clinics.AddPetToClinic(inputData[2], inputData[1]));
        }

        private static void ReleaseCommand(ClinicRepo clinics, string[] inputData)
        {
            Console.WriteLine(clinics.GetClinicByName(inputData[1]).ReleaseRoom());
        }

        private static void HasEmptyCommand(ClinicRepo clinics, string[] inputData)
        {
            Console.WriteLine(clinics.HasEmpty(inputData[1]));
        }

        private static void PrintCommand(string[] inputData, ClinicRepo clinics)
        {
            if (inputData.Length == 3)
            {
                Console.WriteLine(clinics.GetClinicByName(inputData[1]).Print(int.Parse(inputData[2])));
            }
            else
            {
                Console.WriteLine(clinics.GetClinicByName(inputData[1]).Print());
            }
        }
    }
}