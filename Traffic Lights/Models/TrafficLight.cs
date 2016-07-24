namespace P09TrafficLights.Models
{
    using System;
    using System.Linq;

    using Enums;

    public class TrafficLight
    {
        private static readonly Type EnumType =
            default(TrafficLightEnumeration).TypeOfEnum();
        private static readonly int SizeOfEnum = EnumType.SizeOfEnumeration();
        private Tuple<int, string>[] lightValue;

        public TrafficLight(params string[] args)
        {
            this.SetArguments(args);
        }

        public string[] Values => this.lightValue.Select(t => t.Item2).ToArray();

        public void Rotate()
        {
            for (int i = 0; i < this.lightValue.Length; i++)
            {
                var enumType =
                    EnumType
                        .GetStringValueAsEnumType(
                            this.lightValue[i].Item2);
                int indexOfEnum = enumType.GetIndexOfEnum();
                int newIndex = indexOfEnum + 1;
                if (EnumType.IsIndexOutOfBound(newIndex))
                {
                    newIndex = 0;
                }

                int tupleIndex = this.lightValue[i].Item1;
                string valueOfNewIndex = EnumType.GetValueOfIndex(newIndex);
                this.lightValue[i] = new Tuple<int, string>(tupleIndex, valueOfNewIndex);
            }
        }

        public override string ToString()
        {
            return $"{string.Join(" ", this.Values)}";
        }

        private void SetArguments(string[] args)
        {
            this.lightValue = new Tuple<int, string>[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                this.lightValue[i] =
                    new Tuple<int, string>(i, args[i]);
            }
        }
    }
}