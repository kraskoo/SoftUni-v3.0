namespace CubicAssault
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Startup
    {
        private const string BlackMeteorType = "Black";
        private const string RedMeteorType = "Red";
        private const string GreenMeteorType = "Green";
        private const int UpperUpgradeableBound = 1000000;
        private static readonly Predicate<long> HasReachUpgradeableAmount = ua
            => ua >= UpperUpgradeableBound;

        public static void Main()
        {
            var regions = new Dictionary<string, Dictionary<string, long>>();
            string inputLine = Console.ReadLine();
            while (!inputLine.Equals("Count em all"))
            {
                string[] arguments = inputLine.Split(new[] { " -> " }, StringSplitOptions.None);
                string regionName = arguments[0];
                string meteorType = arguments[1];
                long meteorAmount = long.Parse(arguments[2]);
                if (!regions.ContainsKey(regionName))
                {
                    regions.Add(regionName, GetMeteorsTypes());
                }

                regions[regionName][meteorType] += meteorAmount;
                UpdateAfterIncrease(regions[regionName]);
                inputLine = Console.ReadLine();
            }

            var sortedRegions = regions
                .OrderByDescending(r => r.Value[BlackMeteorType])
                .ThenBy(r => r.Key.Length)
                .ThenBy(r => r.Key);

            foreach (var region in sortedRegions)
            {
                Console.WriteLine(region.Key);

                var sortedMeteors =
                    from pair in region.Value
                    orderby pair.Value descending, pair.Key ascending
                    select pair;
                foreach (var meteor in sortedMeteors)
                {
                    Console.WriteLine($"-> {meteor.Key} : {meteor.Value}");
                }
            }
        }

        private static void UpdateAfterIncrease(Dictionary<string, long> meteors)
        {
            if (HasReachUpgradeableAmount(meteors[GreenMeteorType]))
            {
                CombineMeteorsInDictionary(meteors, GreenMeteorType, RedMeteorType);
            }

            if (HasReachUpgradeableAmount(meteors[RedMeteorType]))
            {
                CombineMeteorsInDictionary(meteors, RedMeteorType, BlackMeteorType);
            }
        }

        private static void CombineMeteorsInDictionary(
            Dictionary<string, long> currentMeteors,
            string from,
            string to)
        {
            long fromValue = currentMeteors[from];
            long toValue = currentMeteors[to];
            CombineMeteors(ref fromValue, ref toValue);
            currentMeteors[from] = fromValue;
            currentMeteors[to] = toValue;
        }

        private static void CombineMeteors(ref long from, ref long to)
        {
            long upgradeAmount = from / UpperUpgradeableBound;
            from -= upgradeAmount * UpperUpgradeableBound;
            to += upgradeAmount;
        }

        private static Dictionary<string, long> GetMeteorsTypes()
        {
            return new Dictionary<string, long>
            {
                { BlackMeteorType, 0 },
                { RedMeteorType, 0 },
                { GreenMeteorType, 0 }
            };
        }
    }
}