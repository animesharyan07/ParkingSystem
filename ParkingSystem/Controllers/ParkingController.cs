 using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ParkingController : ControllerBase
    {
        private readonly IParkingServices parkingServices;
        private readonly ILogger<ParkingController> _logger;

        public ParkingController(IParkingServices parkingServices, ILogger<ParkingController> logger)
        {
            this.parkingServices = parkingServices;
            _logger = logger;
        }
        [HttpGet]
        //public IEnumerable<Parking> Get()
        //{
        //    _logger.LogInformation("Get() method called in ParkingController");  // <-- Logging
        //    return parkingServices.Get();

        //}
        // GET: api/<ParkingController>
        [HttpGet]

        public ActionResult<List<Parking>> Get()
        {
            _logger.LogInformation("Get() method called in ParkingController");
            _logger.LogWarning("This is a warning message from Get()");
            _logger.LogError("This is an error message from Get()");
            _logger.LogDebug("This is a DEbug Mode");
            _logger.LogCritical("This is a Critical");

            var parkings = parkingServices.Get();
            return Ok(parkings);
        }

        // GET api/<ParkingController>/5
        [HttpGet("{id}")]
        public ActionResult<Parking> Get(string id)
        {

            var parking = parkingServices.Get(id);
            if (parking == null)
            {
                return NotFound($"Parking with ID={id} not found");
            }
            return parking;

        }

        // POST api/<ParkingController>
        [HttpPost]
        public ActionResult<Parking> Post([FromBody] Parking parking)
        {
            parkingServices.Create(parking);

            return CreatedAtAction(nameof(Get), new { id = parking.Id }, parking);
        }

        // PUT api/<ParkingController>/5
        [HttpPut("{id}")]
        public ActionResult<Parking> Put(string id, [FromBody] Parking parking)
        {
            var existingParking = parkingServices.Get(id);
            if (existingParking == null)
            {
                return NotFound($"Parking with ID={id} not found");
            }
            parkingServices.Update(id, parking);

            return NoContent();
        }

        // DELETE api/<ParkingController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var parking = parkingServices.Get(id);
            if (parking == null)
            {
                return NotFound($"Parking with ID={id} not found");
            }
            parkingServices.Remove(parking.Id);
            return Ok($"Parking with ID={id} deleted");

        }
    }
}
