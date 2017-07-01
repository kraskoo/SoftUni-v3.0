namespace ShoppinCenter
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ShoppingCenter : IShoppingCenter
    {
        private const string NoProductFound = "No products found";
        private const string AddedProduct = "Product added";
        private const string DeletedProduct = "{0} products deleted";
        private readonly MultiDictionary<string, Product> productByName =
            new MultiDictionary<string, Product>(true);
        private readonly MultiDictionary<string, Product> productByProducer =
            new MultiDictionary<string, Product>(true);
        private readonly MultiDictionary<string, Product> productByNameAndProducer =
            new MultiDictionary<string, Product>(true);
        private readonly OrderedMultiDictionary<decimal, Product> productByPrice =
            new OrderedMultiDictionary<decimal, Product>(true);

        public string AddProduct(string name, decimal price, string producer)
        {
            var newProduct = new Product(name, price, producer);
            this.productByName.Add(name, newProduct);
            this.productByProducer.Add(producer, newProduct);
            var nameAndProducer = this.GetNameAndProducer(name, producer);
            this.productByNameAndProducer.Add(nameAndProducer, newProduct);
            this.productByPrice.Add(price, newProduct);
            return AddedProduct;
        }

        public string DeleteProducts(string producer)
        {
            if (!this.productByProducer.ContainsKey(producer))
            {
                return NoProductFound;
            }

            var matchingProducts = this.productByProducer[producer].ToList();
            foreach (var product in matchingProducts)
            {
                this.productByName[product.Name].Remove(product);
                this.productByNameAndProducer[this.GetNameAndProducer(product.Name, product.Producer)].Remove(product);
                this.productByPrice[product.Price].Remove(product);
                this.productByProducer[product.Producer].Remove(product);
            }

            return string.Format(DeletedProduct, matchingProducts.Count);
        }

        public string DeleteProducts(string name, string producer)
        {
            var key = this.GetNameAndProducer(name, producer);
            if (!this.productByNameAndProducer.ContainsKey(key))
            {
                return NoProductFound;
            }

            var matchingProducts = this.productByNameAndProducer[key].ToList();
            foreach (var product in matchingProducts)
            {
                this.productByName[product.Name].Remove(product);
                this.productByProducer[product.Producer].Remove(product);
                this.productByNameAndProducer[this.GetNameAndProducer(product.Name, product.Producer)].Remove(product);
                this.productByPrice[product.Price].Remove(product);
            }

            return string.Format(DeletedProduct, matchingProducts.Count);
        }

        public string FindProductsByName(string name)
        {
            if (!this.productByName.ContainsKey(name))
            {
                return NoProductFound;
            }

            return string.Join(
                Environment.NewLine,
                this.productByName[name].OrderBy(p => p));
        }

        public string FindProductsByProducer(string producer)
        {
            if (!this.productByProducer.ContainsKey(producer))
            {
                return NoProductFound;
            }

            return string.Join(
                Environment.NewLine,
                this.productByProducer[producer].OrderBy(p => p));
        }

        public string FindProductsByPriceRange(decimal fromPrice, decimal toPrice)
        {
            var matches = this.productByPrice.Range(fromPrice, true, toPrice, true);
            if (!matches.Any())
            {
                return NoProductFound;
            }

            return string.Join(
                Environment.NewLine,
                matches.SelectMany(m => m.Value).OrderBy(p => p));
        }

        private string GetNameAndProducer(string name, string producer)
        {
            return $"{name}{producer}";
        }
    }
}