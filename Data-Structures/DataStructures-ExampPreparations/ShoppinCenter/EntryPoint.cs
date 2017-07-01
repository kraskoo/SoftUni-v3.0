namespace ShoppinCenter
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            IShoppingCenter center = new ShoppingCenter();
            int lineNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < lineNumber; i++)
            {
                string commandLine = Console.ReadLine();
                int spaceIndex = commandLine.IndexOf(' ');
                string command = commandLine.Substring(0, spaceIndex);
                string[] commandArgs = commandLine
                    .Substring(spaceIndex + 1, commandLine.Length - 1 - spaceIndex)
                    .Split(';');
                switch (command)
                {
                    case "AddProduct":
                        Console.WriteLine(
                            center.AddProduct(
                                commandArgs[0],
                                decimal.Parse(commandArgs[1]),
                                commandArgs[2]));
                        break;
                    case "DeleteProducts":
                        Console.WriteLine(
                            commandArgs.Length == 1 ?
                                center.DeleteProducts(commandArgs[0]) :
                                center.DeleteProducts(commandArgs[0], commandArgs[1]));
                        break;
                    case "FindProductsByName":
                        Console.WriteLine(center.FindProductsByName(commandArgs[0]));
                        break;
                    case "FindProductsByProducer":
                        Console.WriteLine(center.FindProductsByProducer(commandArgs[0]));
                        break;
                    case "FindProductsByPriceRange":
                        Console.WriteLine(
                            center.FindProductsByPriceRange(
                                decimal.Parse(commandArgs[0]),
                                decimal.Parse(commandArgs[1])));
                        break;
                }
            }
        }
    }
}