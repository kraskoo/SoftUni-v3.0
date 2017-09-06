namespace BoatRacingSimulator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Database;
    using Enumerations;
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.Boats;
    using Models.Engines;
    using Utility;

    public class BoatSimulatorController : IBoatSimulatorController
    {
        public BoatSimulatorController(BoatSimulatorDatabase database, IRace currentRace)
        {
            this.Database = database;
            this.CurrentRace = currentRace;
        }

        public BoatSimulatorController() : this(new BoatSimulatorDatabase(), null)
        {
        }

        public IRace CurrentRace { get; private set; }

        public BoatSimulatorDatabase Database { get; }

        public string CreateBoatEngine(string model, int horsepower, int displacement, EngineType engineType)
        {
            IEngine engine = null;
            switch (engineType)
            {
                case EngineType.Jet:
                    engine = new JetEngine(model, horsepower, displacement);
                    break;
                case EngineType.Sterndrive:
                    engine = new SterndriveEngine(model, horsepower, displacement);
                    break;
            }

            this.Database.Engines.Add(engine);
            return $"Engine model {model} with {horsepower} HP and displacement {displacement} cm3 created successfully.";
        }

        public string CreateRowBoat(string model, int weight, int oars)
        {
            IBoat boat = new RowBoat(model, weight, oars);
            this.Database.Boats.Add(boat);
            return $"Row boat with model {model} registered successfully.";
        }

        public string CreateSailBoat(string model, int weight, int sailEfficiency)
        {
            IBoat boat = new SailBoat(model, weight, sailEfficiency);
            this.Database.Boats.Add(boat);
            return $"Sail boat with model {model} registered successfully.";
        }

        public string CreatePowerBoat(string model, int weight, string firstEngineModel, string secondEngineModel)
        {
            IEngine firstEngine = this.Database.Engines.GetItem(firstEngineModel);
            IEngine secondEngine = this.Database.Engines.GetItem(secondEngineModel);
            IBoat boat = new PowerBoat(model, weight, firstEngine, secondEngine);
            this.Database.Boats.Add(boat);
            return $"Power boat with model {model} registered successfully.";
        }

        public string CreateYacht(string model, int weight, string engineModel, int cargoWeight)
        {
            var engine = this.Database.Engines.GetItem(engineModel) as Engine;
            IBoat boat = new Yacht(model, weight, engine, cargoWeight);
            this.Database.Boats.Add(boat);
            return $"Yacht with model {model} registered successfully.";
        }

        public string OpenRace(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            IRace race = new Race(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
            this.ValidateRaceIsEmpty();
            this.CurrentRace = race;
            return $"A new race with distance {distance} meters, wind speed {windSpeed} m/s and ocean current speed {oceanCurrentSpeed} m/s has been set.";
        }

        public string SignUpBoat(string model)
        {
            var boat = this.Database.Boats.GetItem(model);
            this.ValidateRaceIsSet();
            if (!this.CurrentRace.AllowsMotorboats && boat.IsMotorBoat)
            {
                throw new ArgumentException(Constants.IncorrectBoatTypeMessage);
            }

            this.CurrentRace.AddParticipant(boat);
            return $"Boat with model {model} has signed up for the current Race.";
        }

        public string StartRace()
        {
            this.ValidateRaceIsSet();
            var participants = this.CurrentRace.GetParticipants();
            if (participants.Count < 3)
            {
                throw new InsufficientContestantsException(Constants.InsufficientContestantsMessage);
            }

            var raceResults = new List<KeyValuePair<double, IBoat>>();
            foreach (var participant in participants)
            {
                var speed = participant.CalculateRaceSpeed(this.CurrentRace);
                if (speed <= 0)
                {
                    raceResults.Add(new KeyValuePair<double, IBoat>(double.PositiveInfinity, participant));
                }
                else
                {
                    var time = this.CurrentRace.Distance / speed;
                    raceResults.Add(new KeyValuePair<double, IBoat>(time, participant));
                }
            }

            raceResults = raceResults.OrderBy(x => x.Key).ToList();

            var first = raceResults[0];
            var second = raceResults[1];
            var third = raceResults[2];

            var result = new StringBuilder();
            result.AppendLine($"First place: {first.Value.GetType().Name} Model: {first.Value.Model} Time: {(double.IsInfinity(first.Key) ? "Did not finish!" : first.Key.ToString("0.00") + " sec")}");
            result.AppendLine($"Second place: {second.Value.GetType().Name} Model: {second.Value.Model} Time: {(double.IsInfinity(second.Key) ? "Did not finish!" : second.Key.ToString("0.00") + " sec")}");
            result.Append($"Third place: {third.Value.GetType().Name} Model: {third.Value.Model} Time: {(double.IsInfinity(third.Key) ? "Did not finish!" : third.Key.ToString("0.00") + " sec")}");
            this.CurrentRace = null;
            return result.ToString();
        }

        public string GetStatistic()
        {
            var builder = new StringBuilder();
            var participants = this.CurrentRace.GetParticipants();
            var boatTypeNames = Enum.GetNames(typeof(BoatType));
            IDictionary<BoatType, int> countByBoatType = new Dictionary<BoatType, int>();
            foreach (var boatType in boatTypeNames)
            {
                var type = (BoatType)Enum.Parse(typeof(BoatType), boatType);
                countByBoatType[type] = this.GetCountByParticipantsBoatType(participants, type);
            }

            var allBoatCount = countByBoatType.Values.Sum();
            var orderedBoats = countByBoatType.OrderBy(b => b.Key.ToString());
            foreach (var boat in orderedBoats)
            {
                builder.AppendLine($"{boat.Key} -> {100 * ((double)boat.Value / allBoatCount):F2}%");
            }

            return builder.ToString().Trim();
        }

        private int GetCountByParticipantsBoatType(IEnumerable<IBoat> participants, BoatType boatType)
        {
            return participants.Count(p => p.BoatType == boatType);
        }

        private void ValidateRaceIsSet()
        {
            if (this.CurrentRace == null)
            {
                throw new NoSetRaceException(Constants.NoSetRaceMessage);
            }
        }

        private void ValidateRaceIsEmpty()
        {
            if (this.CurrentRace != null)
            {
                throw new RaceAlreadyExistsException(Constants.RaceAlreadyExistsMessage);
            }
        }
    }
}