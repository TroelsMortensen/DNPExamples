using System.Collections.Generic;
using FourthCoffee.Models;

namespace FourthCoffee.Data {
public interface IProductsService {
    public IList<Product> Products { get; }
    
}
}