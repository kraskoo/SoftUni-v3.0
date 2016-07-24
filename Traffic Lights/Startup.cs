namespace P09TrafficLights
{
    using System;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            string[] inputData =
                Console.ReadLine().Split();
            int numberOfRotates = int.Parse(Console.ReadLine());
            TrafficLight trafficLight = new TrafficLight(inputData);
            for (int i = 0; i < numberOfRotates; i++)
            {
                trafficLight.Rotate();
                Console.WriteLine(trafficLight);
            }
        }
    }
}