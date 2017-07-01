namespace ShoppinCenter
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(string name, decimal price, string producer)
        {
            this.Name = name;
            this.Price = price;
            this.Producer = producer;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }

        public int CompareTo(Product other)
        {
            var nameCmp = string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);
            var producerCmp = string.Compare(this.Producer, other.Producer, StringComparison.CurrentCulture);
            var priceCmp = this.Price.CompareTo(other.Price);
            if (nameCmp == 0)
            {
                if (producerCmp == 0)
                {
                    return priceCmp;
                }

                return producerCmp;
            }

            return nameCmp;
        }

        public override string ToString()
        {
            return $"{{{this.Name};{this.Producer};{$"{this.Price:F2}"}}}";
        }
    }
}