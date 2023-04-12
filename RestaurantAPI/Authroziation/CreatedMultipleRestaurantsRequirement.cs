using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authroziation
{
    public class CreatedMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; }
        public CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
    }
}
