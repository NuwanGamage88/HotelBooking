
namespace RoomBooking.Model
{
    public class Room
    {
        public string Number { get; set; }
        public int FloorId { get; set; }
        public Status Status { get; set; } = Status.Available;
    }
}
