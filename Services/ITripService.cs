using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cw_7_ko_xDejw.Models.DTOs;

namespace cw_7_ko_xDejw.Services
{
    public interface ITripService
    {
        public Task<List<Models.DTOs.TripDto>> GetTrips();
        public Task<int> DelClient(int id);

        public Task AddClient(Models.DTOs.ClientDto dto);
        public Task AddTripClient(Models.DTOs.ClientDto dto);
        public Task<int> getClientId(string pesel);
        public Task<bool> clientExists(int clientId);
        public Task<bool> clientExists(int clientId, string firstName, string lastName);
        public Task<bool> tripExists(int tripId, string tripName);
        public Task<bool> hasTrips(int idClient);
        public Task<bool> isRegistered(int idClient, int idTrip);
    }
}
