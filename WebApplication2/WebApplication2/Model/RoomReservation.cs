using System;

namespace RoomBooking.Model
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int FloorId { get; set; }
        public string RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public Status Status { get; set; } = Status.Available;
    }
}
