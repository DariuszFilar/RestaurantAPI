using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Route("restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize]
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