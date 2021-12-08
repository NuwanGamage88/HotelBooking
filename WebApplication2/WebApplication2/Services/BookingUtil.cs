using RoomBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RoomBooking.Services
{
    public class BookingUtil : IBookingUtil
    {
        public BookingUtil()
        {
            SetData();
        }
        public List<Floor> Floors { get; set; }
        public List<Room> RoomList { get; set; }
        public List<RoomReservation> RoomReservation { get; set; } = new List<RoomReservation>();

        private void SetData()
        {
            var f1 = new Floor { Id = 1, Name = "1" };
            var f2 = new Floor { Id = 2, Name = "2" };
            var f3 = new Floor { Id = 3, Name = "3" };
            var f4 = new Floor { Id = 4, Name = "4" };
            Floors = new List<Floor>();
            Floors.Add(f1);
            Floors.Add(f2);
            Floors.Add(f3);
            Floors.Add(f4);

            RoomList = new List<Room>();
            foreach (var f in Floors)
            {
                var r1 = new Room {  FloorId = f.Id, Number = $"{f.Id}A" };
                RoomList.Add(r1);
                var r2 = new Room {  FloorId = f.Id, Number = $"{f.Id}B" };
                RoomList.Add(r2);
                var r3 = new Room {  FloorId = f.Id, Number = $"{f.Id}C" };
                RoomList.Add(r3);
                var r4 = new Room {  FloorId = f.Id, Number = $"{f.Id}D" };
                RoomList.Add(r4);
                var r5 = new Room {  FloorId = f.Id, Number = $"{f.Id}E" };
                RoomList.Add(r5);
            }
        }

        public List<Room> GetAvailableRooms(DateTime sDate, DateTime eDate)
        {
            if (RoomReservation != null)
            {
                var bookingReservations =  RoomReservation.Where(x => sDate <= x.StartDate && x.StartDate <= eDate
                                            || sDate <= x.StartDate && x.EndDate <= eDate || x.StartDate <= sDate && eDate <= x.EndDate).Distinct();

                var bookRooms = new List<Room>();
                foreach (var reservation in bookingReservations)
                {
                    var reservedRoom = RoomList.SingleOrDefault(x => x.Number == reservation.RoomNumber);
                    RoomList.Remove(reservedRoom);
                }
                return RoomList.Where(x => x.Status == Status.Available).ToList();
            }
            return RoomList.Where(x => x.Status == Status.Available).ToList();
        }

        public Room? UpdateRoomStatus(string number, Status preStatus, Status newStatus)
        {
            var findItem = RoomList.FirstOrDefault(x => x.Number == number && x.Status == preStatus);
            if (findItem != null)
            {
                findItem.Status = newStatus;
            }
            return findItem;
        }

        public List<RoomReservation> GetRoomReservations()
        {
            return RoomReservation;
        }

        public List<Room> GetRooms()
        {
            return RoomList;
        }

        public  void AddReservation(Room room, DateTime startDateTime, DateTime endDateTime)
        {
            room.Status = Status.Occupied;
            var reservation = new RoomReservation
            {
                RoomNumber = room.Number,
                FloorId = room.FloorId,
                StartDate = startDateTime,
                EndDate = endDateTime,
                DateCreated = DateTime.Now,
                Status = Status.Occupied

            };
            RoomReservation.Add(reservation);
        }

    }
}
