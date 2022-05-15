using cw_7_ko_xDejw.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace cw_7_ko_xDejw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _service;

        public TripsController(ITripService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await _service.GetTrips());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelClient(int id)
        {
            var clientExists = await _service.clientExists(id);

            if (!clientExists)
                return NotFound($"Client with id {id} does not exist in the database");

            var hasTrips = await _service.hasTrips(id);

            if (!hasTrips)
            {
                var clientId = await _service.DelClient(id);
                return Ok($"Deleted client with id {id}");

            }
            else
                return NotFound($"Client with id {id} has other references in tables");
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> PostClientTrip(int idTrip, Models.DTOs.ClientDto dto)
        {
            if (idTrip != Int32.Parse(dto.IdTrip))
            {
                return Conflict("Trip identificator is different than in the request");
            }

            var tripExists = await _service.tripExists(idTrip, dto.TripName);

            if (!tripExists)
                return NotFound("Trip with such name and id does not exist in the database");
                
            var clientId = await _service.getClientId(dto.Pesel);

            if (clientId == 0)
            {
                await _service.AddClient(dto);
                clientId = await _service.getClientId(dto.Pesel);
            }

            var clientExists = await _service.clientExists(clientId, dto.FirstName, dto.LastName);

            if(!clientExists)
                return Conflict("Identification of the person with such PESEL was not correct");        

            var isRegistered = await _service.isRegistered(clientId, Int32.Parse(dto.IdTrip));

            if (isRegistered)
                return Conflict("Client is already registered for this trip");

            await _service.AddTripClient(dto);
            return Ok("Added client to a trip");
        }
    }

}

