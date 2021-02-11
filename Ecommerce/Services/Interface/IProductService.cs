using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Models.Dto;

namespace Ecommerce.Services.Interface
{
    public interface IProductService
    {
        Task<IList<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int productId);
    }
}