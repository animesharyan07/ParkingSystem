    using System.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ParkingSystem.Models;
    using ParkingSystem.Services;

    namespace ParkingSystem.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize]
        public class ParkingController : ControllerBase
        {
            private readonly IParkingServices parkingServices;
            private readonly ILogger<ParkingController> _logger;

            public ParkingController(IParkingServices parkingServices, ILogger<ParkingController> logger)
            {
                this.parkingServices = parkingServices;
                _logger = logger;
            }

            // GET: api/<ParkingController>
            [HttpGet]
            public ActionResult<List<Parking>> Get()
            {
                _logger.LogInformation("Fetching all parking records...");

                var parkings = parkingServices.Get();

                if (parkings == null || !parkings.Any())
                {
                    _logger.LogWarning("No parking records found.");
                    return NotFound("No parking records available.");
                }

                _logger.LogInformation("Successfully retrieved {count} parking records.", parkings.Count);
                return Ok(parkings);
            }

        
            // GET api/<ParkingController>/5
            [HttpGet("{id}")]
            [Authorize(Roles = "Admin,Moderator,ReadOnly")]
            public ActionResult<Parking> Get([FromRoute] string id)
            {
                _logger.LogInformation("Fetching parking record with ID: {id}", id);

                var parking = parkingServices.Get(id);
                if (parking == null)
                {
                    _logger.LogWarning("Parking record with ID {id} not found.", id);
                    return NotFound($"Parking with ID={id} not found");
                }

                _logger.LogInformation("Successfully retrieved parking record with ID: {id}", id);
                return Ok(parking);
            }

            // POST api/<ParkingController>
            [HttpPost]
            [Authorize(Roles ="Admin")]
            public ActionResult<Parking> Post([FromBody] Parking parking)
            {
                if (parking == null)
                {
                    _logger.LogError("POST request failed. Parking object is null.");
                    return BadRequest("Parking object cannot be null.");
                }

                parkingServices.Create(parking);
                _logger.LogInformation("Created new parking record with ID: {id}", parking.Id);

                return CreatedAtAction(nameof(Get), new { id = parking.Id }, parking);
            }

            // PUT api/<ParkingController>/5
            [HttpPut("{id}")]
            [Authorize(Roles ="Admin")]
            public ActionResult Put([FromRoute] string id, [FromBody] Parking parking)
            {
                if (parking == null)
                {
                    _logger.LogError("PUT request failed. Parking object is null for ID: {id}", id);
                    return BadRequest("Parking object cannot be null.");
                }

                var existingParking = parkingServices.Get(id);
                if (existingParking == null)
                {
                    _logger.LogWarning("Parking record with ID {id} not found for update.", id);
                    return NotFound($"Parking with ID={id} not found");
                }

                parkingServices.Update(id, parking);
                _logger.LogInformation("Updated parking record with ID: {id}", id);

                return NoContent();
            }

            // DELETE api/<ParkingController>/5
            [HttpDelete("{id}")]
            [Authorize(Roles ="Admin")]
            public ActionResult Delete([FromRoute] string id)
            {
                _logger.LogInformation("Attempting to delete parking record with ID: {id}", id);

                var parking = parkingServices.Get(id);
                if (parking == null)
                {
                    _logger.LogWarning("Parking record with ID {id} not found for deletion.", id);
                    return NotFound($"Parking with ID={id} not found");
                }

                parkingServices.Remove(parking.Id);
                _logger.LogInformation("Deleted parking record with ID: {id}", id);

                return Ok($"Parking with ID={id} deleted");
            }
        }
    }
