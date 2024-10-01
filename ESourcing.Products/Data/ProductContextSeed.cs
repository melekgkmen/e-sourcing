using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool exisProduct = productCollection.Find(p => true).Any();
            if (!exisProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name= "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship in years. It includes a borderless.",
                    Description = "description",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },

                new Product()
                {
                    Name= "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship in years. It includes a borderless.",
                    Description = "description",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                }

            };
        }
    }
}
