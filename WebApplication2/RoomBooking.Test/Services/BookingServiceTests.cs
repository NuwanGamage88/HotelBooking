using NSubstitute;
using RoomBooking.Model;
using RoomBooking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RoomBooking.Test.Services
{
    public class BookingServiceTests
    {
        private readonly IBookingUtil _bookingUtil;
        private List<Room> _roomList;
        private readonly IBookingService _bookingService;
        private readonly DateTime _startDateTime;
        private readonly DateTime _endDateTime;
        public BookingServiceTests()
        {
            _bookingUtil = Substitute.For<IBookingUtil>();
            _roomList = new List<Room>();
            _startDateTime = DateTime.Now;
            _endDateTime = DateTime.Now.AddDays(1);
            _roomList.Add(new Room { Number = "1A", FloorId = 1,Status = Status.Available });
            _roomList.Add(new Room { Number = "1B", FloorId = 1, Status = Status.Available });
           
            _roomList.Add(new Room { Number = "1D", FloorId = 1, Status = Status.Repair });
            _roomList.Add(new Room { Number = "1E", FloorId = 1, Status = Status.Vacant });

            _bookingUtil.GetRooms().Returns(_roomList);
            _bookingUtil.GetAvailableRooms(_startDateTime, _endDateTime).Returns(_roomList);
            _bookingService = new BookingService(_bookingUtil);
        }

        [Fact]
        public void Verify_Add_Room_Booking()
        {
            var result = _bookingService.AddRoomBooking(_startDateTime, _endDateTime);
            Assert.Equal(_roomList.First().Number,result);
        }

        [Fact]
        public void Verify_Get_Avalable_Rooms()
        {
            var result = _bookingService.GetAvailableRooms(_startDateTime, _endDateTime).ToList();
            Assert.Equal(_roomList[0].Number, result[0]);
            Assert.Equal(_roomList[1].Number, result[1]);
        }

        [Fact]
        public async Task Verify_Message_If_No_Any_Room_For_Clean()
        {
             Room outPut = null;
            _bookingUtil.UpdateRoomStatus("1A",Status.Vacant, Status.Available).Returns(outPut);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>  _bookingService.CleanRoom("1A"));
            Assert.Equal("Unable to find room no: 1A for clean", exception.Message);
           
        }

        [Fact]
        public async Task Verify_CheckOut_Room()
        {
            var roomReservation = new RoomReservation { RoomNumber = "1A" , Status = Status.Occupied };
            _bookingUtil.GetRoomReservations().Returns(new List<RoomReservation> { roomReservation });
            var result = await _bookingService.CheckoutRoom("1A");

            Assert.NotNull(result);
            Assert.Equal(Status.Vacant, result.Status);

        }
    }
}
