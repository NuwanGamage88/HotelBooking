using RoomBooking.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomBooking.Services
{
    public interface IBookingService
    {
        string AddRoomBooking(DateTime startDateTime, DateTime endDateTime);

        IEnumerable<string> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime);

        Task<Room> CheckoutRoom(string number);
        Task CleanRoom(string number);
        Task RepairRoom(string number);

        Task MakeRepairedRoomVacant(string number);
    }
}
