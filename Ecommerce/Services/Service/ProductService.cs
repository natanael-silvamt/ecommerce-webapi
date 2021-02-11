using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models.Dto;
using Ecommerce.Models.EntityFrameWork;
using Ecommerce.Models.GenericRepository.Repository;
using Ecommerce.Services.Interface;

namespace Ecommerce.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryReadOnly _iRepository;

        public ProductService(IRepositoryReadOnly iRepository)
        {
            _iRepository = iRepository;
        }
        
        public async Task<IList<Product>> GetProductsAsync()
        {
            var products = await _iRepository.GetAllAsync<Product>();
            return products.ToList();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _iRepository.GetOneAsync<Product>(p => p.Id == productId);
            return product;
        }
    }
}