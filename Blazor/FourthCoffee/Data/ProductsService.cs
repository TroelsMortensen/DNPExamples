using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FourthCoffee.Models;

namespace FourthCoffee.Data {
public class ProductsService : IProductsService {
    public ICollection<Product> Products { get; private set; }

    private string productsFile = "Products.json";

    public ProductsService() {
        if (!File.Exists(productsFile)) {
            Seed();
            string productsAsJson = JsonSerializer.Serialize(Products);
            File.WriteAllText(productsFile, productsAsJson);
        } else {
            string content = File.ReadAllText(productsFile);
            Products = JsonSerializer.Deserialize<List<Product>>(content);
        }
    }

    private void Seed() {
        Product[] ps = {
            new Product {
                Id = 1,
                Name = "Carrot Cake",
                Description = "A scrumptious mini-carrot cake encrusted with sliced almonds",
                Price = 8.99m,
                ImageName = "carrot_cake.jpg"
            },
            new Product {
                Id = 2,
                Name = "Lemon Tart",
                Description = "A delicious lemon tart with fresh meringue cooked to perfection",
                Price = 9.99m,
                ImageName = "lemon_tart.jpg"
            },
            new Product {
                Id = 3,
                Name = "Cupcakes",
                Description = "Delectable vanilla and chocolate cupcakes",
                Price = 5.99m,
                ImageName = "cupcakes.jpg"
            },
            new Product {
                Id = 4,
                Name = "Bread",
                Description = "Fresh baked French-style bread",
                Price = 1.49m,
                ImageName = "bread.jpg"
            },
            new Product {
                Id = 5,
                Name = "Pear Tart",
                Description = "A glazed pear tart topped with sliced almonds and a dash of cinnamon",
                Price = 5.99m,
                ImageName = "pear_tart.jpg"
            },
            new Product {
                Id = 6,
                Name = "Chocolate Cake",
                Description = "Rich chocolate frosting cover this chocolate lover's dream",
                Price = 8.99m,
                ImageName = "chocolate_cake.jpg"
            }
        };
        Products = ps.ToList();
    }
}
}