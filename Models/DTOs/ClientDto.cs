using System;

namespace cw_7_ko_xDejw.Models.DTOs
{
    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        public string IdTrip { get; set; }
        public string TripName { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}