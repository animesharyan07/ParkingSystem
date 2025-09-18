 using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Services;
using ParkingSystem.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingServices parkingServices;

        public ParkingController(IParkingServices parkingServices)
        {
            this.parkingServices = parkingServices;
        }
        // GET: api/<ParkingController>
        [HttpGet]
        public ActionResult<List<Parking>> Get()
        {
            return parkingServices.Get();
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
