using CarsApi.Filters;
using CarsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CarsController : Controller
    {
        
        private static List<Car> cars = new();

        private readonly ILogger<CarsController> _logger;
        private readonly Counter _counter = new Counter();

        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;

            

        }
        [HttpPost]
        public ActionResult Add_V1(Car car)
        {
            _logger.LogCritical("Added successfully");
            car.Id = cars.Count + 1;
            car.Type = "Gas";
            cars.Add(car);

            return CreatedAtAction(actionName: nameof(GetById),
            routeValues: new { id = car.Id },
            new Response { Message = "Added successfully" });
        }

        [HttpPost]
        [Route("v2")]
        [ValidateCarTypeAttrbpute]
        public ActionResult Add_V2(Car car)
        {

            car.Id = cars.Count + 1;
            cars.Add(car);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            Car car = cars.FirstOrDefault(car => car.Id == id);
            if (car == null) { return NotFound(); }

            return Ok(car);
        }
        [HttpGet]

        public ActionResult GetAll()
        {

            return Ok(cars);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Edit(Car newCar, int id)
        {
            if (newCar.Id != id)
            {
                return BadRequest();
            }

            var oldCar = cars.FirstOrDefault(c => c.Id == id);

            if (oldCar is null)
            {
                return NotFound();
            }

            oldCar.Id = id;
            oldCar.Name = newCar.Name;
            oldCar.ProduactionDate = newCar.ProduactionDate;

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteById(int id)
        {
            Car car = cars.FirstOrDefault(c => c.Id == id);

            if (car is null)
            {
                return NotFound(); //Http Status Code 404
            }

            cars.Remove(car);
            return NoContent();
        }
        [HttpGet]
        [Route("GetCounterVlue")]
        public ActionResult GetCounterVlue()
        {
            return Ok(_counter.CountValue());
        }
    }
}
