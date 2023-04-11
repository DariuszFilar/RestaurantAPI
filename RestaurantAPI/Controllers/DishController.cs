using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/dish")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDishId = _dishService.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, int dishId)
        {
            DishDto dish = _dishService.Get(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> GetAll([FromRoute] int restaurantId)
        {
            var result = _dishService.GetAll(restaurantId);
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishService.RemoveAll(restaurantId);

            return NoContent();
        }
    }
}
