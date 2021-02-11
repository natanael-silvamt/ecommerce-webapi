using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Models.Dto;

namespace Ecommerce.Services.Interface
{
    public interface IBasketService
    {
        Task<BasketItem> AddItemIntoBasketAsync(BasketItem basketItem);
        Task<IList<BasketItem>> ChangeBasketItemQuantityAsync(int id, int quantity);
        Task<IList<BasketItem>> ClearBasketAsync(int userId);
        Task<IList<BasketItem>> DeleteBasketItemByIdAsync(int id);
        Task<IList<BasketItem>> GetBasketItemsAsync(int userId);
    }
}