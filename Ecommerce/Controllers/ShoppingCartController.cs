using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Models.Dto;
using Ecommerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IBasketService _iBasketService;

        public ShoppingCartController(IBasketService iBasketService)
        {
            _iBasketService = iBasketService;
        }

        [HttpGet("GetBasketItems/{userId}")]
        public async Task<IActionResult> GetBasketItems(int userId)
        {
            IList<BasketItem> basketItems = await _iBasketService.GetBasketItemsAsync(userId);
            return Ok(basketItems);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BasketItem basketItem)
        {
            await _iBasketService.AddItemIntoBasketAsync(basketItem);
            return Created($"ShoppingCart", basketItem);
        }

        [HttpPut("ChangeItemQuantity/{basketItemId}/{quantity}")]
        public async Task<IActionResult> ChangeItemQuantity(int basketItemId, int quantity)
        {
            IList<BasketItem> basketItems = await _iBasketService.ChangeBasketItemQuantityAsync(basketItemId, quantity);

            if (basketItems == null)
                return NotFound("Item not found in the basket, please check the basketItemId");
            return Ok(basketItems);
        }

        [HttpDelete("ClearBasket/{userId}")]
        public async Task<IActionResult> ClearBasket(int userId)
        {
            IList<BasketItem> basketItems = await _iBasketService.ClearBasketAsync(userId);
            return Ok(basketItems);
        }

        [HttpDelete("DeleteItemFromBasket/{basketItemId}")]
        public async Task<IActionResult> DeleteItemFromBasket(int basketItemId)
        {
            IList<BasketItem> basketItems = await _iBasketService.DeleteBasketItemByIdAsync(basketItemId);
            if (basketItems == null)
                return NotFound("Item not found in the basket, please check the basketItemId");
            return Ok(basketItems);
        }
    }
}