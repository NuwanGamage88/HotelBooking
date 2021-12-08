using RoomBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingUtil _bookingUtil;
        public BookingService(IBookingUtil bookingUtil)
        {
            _bookingUtil = bookingUtil;
        }

        public IEnumerable<string> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime)
        {
           return _bookingUtil.GetAvailableRooms(startDateTime, endDateTime).Select(x => x.Number).ToList();
        }
        public string AddRoomBooking(DateTime startDateTime, DateTime endDateTime)
        {
            var avalableRooms = _bookingUtil.GetAvailableRooms(startDateTime, endDateTime);
            var firstAvalableReservation = avalableRooms.First();
            _bookingUtil.AddReservation(firstAvalableReservation, startDateTime, endDateTime);
            return avalableRooms.First().Number;
        }

        public async Task<Room> CheckoutRoom(string number)
        {
            var reserVations = _bookingUtil.GetRoomReservations();
            var checkOutRoom = reserVations.FirstOrDefault(x => x.RoomNumber == number && x.Status == Status.Occupied);
            if (checkOutRoom == null)
                    throw new InvalidOperationException($"Unable to find room no: {number} for checkout");

            reserVations.Remove(checkOutRoom);
            var vacantRoom = _bookingUtil.GetRooms().FirstOrDefault(x => x.Number == checkOutRoom.RoomNumber);
            vacantRoom.Status = Status.Vacant;
            return vacantRoom;

        }

        public async Task CleanRoom(string number)
        {
          var result = _bookingUtil.UpdateRoomStatus(number, Status.Vacant, Status.Available);
            if (result == null)
                throw new InvalidOperationException($"Unable to find room no: {number} for clean");
        }

        public async Task RepairRoom(string number)
        {
           var result = _bookingUtil.UpdateRoomStatus(number, Status.Vacant, Status.Repair);
            if (result == null)
                throw new InvalidOperationException($"Unable to find room no: {number} for repair");
        }

        public async Task MakeRepairedRoomVacant(string number)
        {
            var result = _bookingUtil.UpdateRoomStatus(number, Status.Repair, Status.Vacant);
            if (result == null)
                throw new InvalidOperationException($"Unable to find room no: {number} for vacant");
        }

        
    }
}
