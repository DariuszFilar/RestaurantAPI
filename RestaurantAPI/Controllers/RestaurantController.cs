using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("restaurant")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {          
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {           
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}