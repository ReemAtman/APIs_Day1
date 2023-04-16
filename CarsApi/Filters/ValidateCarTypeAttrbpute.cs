using CarsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace CarsApi.Filters
{
    public class ValidateCarTypeAttrbpute : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
            Car car = context.ActionArguments["car"] as Car;
            
            var regex = new Regex("^(Electric|Gas|Diesel|Hybrid)$",
                RegexOptions.IgnoreCase,
                TimeSpan.FromSeconds(20));

            if (car is null || !regex.IsMatch(car.Type))
            {
                context.ModelState.AddModelError("Type", "The Type isn't exist");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
