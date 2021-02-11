using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Dto;
using Ecommerce.Models.EntityFrameWork;
using Ecommerce.Models.GenericRepository.Repository;
using Ecommerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Service
{
    public class BasketService : IBasketService
    {
        private readonly IRepository _iRepository;

        public BasketService(IRepository iRepository)
        {
            _iRepository = iRepository;
        }
        
        public async Task<BasketItem> AddItemIntoBasketAsync(BasketItem basketItem)
        {
            IEnumerable<BasketItem> basketItems =
                await _iRepository.GetAsync<BasketItem>(b => b.UserId == basketItem.UserId);
            
            if (basketItems.Any())
            {
                basketItem.Id = basketItems.Last().Id + 1;
            }
            else
            {
                basketItem.Id = 1;
            }
            _iRepository.Create<BasketItem>(basketItem);
            await _iRepository.SaveAsync();
            return basketItem;
        }

        public async Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);

            if (basketItem == null)
                return null;

            basketItem.Quantity = quantity;
            _iRepository.Update<BasketItem>(basketItem);
            await _iRepository.SaveAsync();
            return await GetBasketItemsAsync(basketItem.UserId);
        }

        public async Task<IList<BasketItem>> ClearBasketAsync(int userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId);
            foreach (var basketItem in basketItems)
            {
                _iRepository.Delete<BasketItem>(basketItem);
            }

            await _iRepository.SaveAsync();
            return await GetBasketItemsAsync(userId);
        }

        public async Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id)
        {
            var basketItem = await _iRepository.GetByIdAsync<BasketItem>(id);
            if (basketItem == null) return null;
            
            _iRepository.Delete<BasketItem>(basketItem);
            await _iRepository.SaveAsync();
            return await GetBasketItemsAsync(basketItem.UserId);
        }

        public async Task<IList<BasketItem>> GetBasketItemsAsync(int userId)
        {
            var basketItems = await _iRepository.GetAsync<BasketItem>(b => b.UserId == userId);
            basketItems = PopulateProductIntoBasketItem(basketItems.ToList());
            return basketItems.ToList();
        }

        private List<BasketItem> PopulateProductIntoBasketItem(List<BasketItem> basketItems)
        {
            foreach (var basketItem in basketItems)
            {
                basketItem.Product = _iRepository.GetById<Product>(basketItem.ProductId);
            }

            return basketItems;
        }
    }
}