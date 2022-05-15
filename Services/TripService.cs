using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw_7_ko_xDejw.Models;
using cw_7_ko_xDejw.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cw_7_ko_xDejw.Services
{
    public class TripService : ITripService
    {

        private readonly _2019SBDContext _context;
        public TripService(_2019SBDContext context)
        {
            _context = context;
        }

        public async Task<int> DelClient(int id)
        {
            var client = new Models.Client
            {
                IdClient = id
            };
            var entry = _context.Entry(client);
            //_context.Remove(client);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return id;
        }

        public Task<List<Models.DTOs.TripDto>> GetTrips()
        {
            return _context.Trips
                .OrderByDescending(e => e.DateFrom)
                .Select(e => new TripDto
                {
                    Name = e.Name,
                    MaxPeople = e.MaxPeople,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Clients = e.ClientTrips.Select(e => new Models.DTOs.TripDto.Client
                    {
                        FirstName = e.IdClientNavigation.FirstName,
                        LastName = e.IdClientNavigation.LastName
                    }).ToList(),
                    Countries = e.CountryTrips.Select(e => new Models.DTOs.TripDto.Country
                    {
                        Name = e.IdCountryNavigation.Name,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task AddClient(Models.DTOs.ClientDto dto)
        {
            var client = new Models.Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel,
            };
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task AddTripClient(Models.DTOs.ClientDto dto)
        {
            var clientId = await getClientId(dto.Pesel);

            var tripId = await getTripId(dto.TripName);

            var clientTrip = new Models.ClientTrip
            {
                IdClient = clientId,
                IdTrip = tripId,
                RegisteredAt = DateTime.Now,
                PaymentDate = dto.PaymentDate,
                IdClientNavigation = await _context.Clients.Where(e => e.IdClient == clientId).FirstOrDefaultAsync(),
                IdTripNavigation = await _context.Trips.Where(e => e.IdTrip == tripId).FirstOrDefaultAsync(),
            };

            await _context.AddAsync(clientTrip);
            await _context.SaveChangesAsync();
        }


        // public async Task EditCountry(int id, string name)
        // {
        //     var country = await _context.Countries.Where(e => e.IdCountry == id).FirstOrDefaultAsync();
        //     if (country == null) return;
        //     country.Name = name;
        //     await _context.SaveChangesAsync();
        // }

        public async Task<int> getClientId(string pesel)
        {
            return await _context.Clients.Where(e => e.Pesel == pesel).Select(e => e.IdClient).FirstOrDefaultAsync();
        }

        public async Task<int> getTripId(string tripName)
        {
            return await _context.Trips.Where(e => e.Name == tripName).Select(e => e.IdTrip).FirstOrDefaultAsync();
        }

        public async Task<bool> clientExists(int clientId)
        {
             return await _context.Clients.AnyAsync(e => e.IdClient == clientId);
        }
        public async Task<bool> clientExists(int clientId, string firstName, string lastName)
        {
             return await _context.Clients.AnyAsync(e => e.IdClient == clientId && e.FirstName == firstName && e.LastName == lastName);
        }
        public async Task<bool> tripExists(int tripId, string tripName)
        {
            return await _context.Trips.AnyAsync(e => e.IdTrip == tripId && e.Name == tripName);
        }

        public async Task<bool> hasTrips(int idClient)
        {
            return await _context.ClientTrips.AnyAsync(e => e.IdClient == idClient);
        }
        public async Task<bool> isRegistered(int idClient, int idTrip)
        {
            return await _context.ClientTrips.AnyAsync(e => e.IdClient == idClient && e.IdTrip == idTrip);
        }
    }
}
