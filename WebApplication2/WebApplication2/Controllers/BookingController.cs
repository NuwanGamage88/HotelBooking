using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Model;
using RoomBooking.Services;


namespace RoomBooking.Controllers
{
    [Route("Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;

        }
        [HttpGet("AvailableRooms")]
        public async Task<IActionResult> Get(DateTime startDateTime, DateTime endDateTime)
        {
            var result = _bookingService.GetAvailableRooms(startDateTime, endDateTime);
            return Ok(result);
        }

        [HttpPost("BookRoom")]
        public async Task<IActionResult> BookRoom(Booking booking)
        {
            var result = _bookingService.AddRoomBooking(booking.StartDateTime, booking.EndDateTime);
            return Ok(result);
        }

        [HttpPut("CheckOut")]
        public async Task<IActionResult> CheckOut(string number)
        {
            _bookingService.CheckoutRoom(number);
            return Ok();
        }

        [HttpPut("CleanRoom")]
        public async Task<IActionResult> CleanRoom(string number)
        {
            await _bookingService.CleanRoom(number);
            return Ok();
        }

        [HttpPut("Repair")]
        public async Task<IActionResult> RepairRoom(string number)
        {
            _bookingService.RepairRoom(number);
            return Ok();
        }


    }
}
