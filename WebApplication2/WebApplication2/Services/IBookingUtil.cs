using RoomBooking.Model;
using System;
using System.Collections.Generic;

namespace RoomBooking.Services
{
    public interface IBookingUtil
    {
        public List<Floor> Floors { get; set; }
        public List<Room> RoomList { get; set; }
        public List<RoomReservation> RoomReservation { get; set; }
        List<Room> GetAvailableRooms(DateTime sDate, DateTime eDate);
        Room? UpdateRoomStatus(string number, Status preStatus, Status newStatus);

        void AddReservation(Room room, DateTime startDateTime, DateTime endDateTime);

        List<Room> GetRooms();
        List<RoomReservation> GetRoomReservations();
    }
}
